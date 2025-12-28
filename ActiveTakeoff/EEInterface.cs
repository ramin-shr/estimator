using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class EEInterface
	{
		public uint MessageEEItemSelectRequest
		{
			[CompilerGenerated]
			get
			{
				return this.<MessageEEItemSelectRequest>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MessageEEItemSelectRequest>k__BackingField = value;
			}
		}

		public uint MessageEEItemSelectCancelled
		{
			[CompilerGenerated]
			get
			{
				return this.<MessageEEItemSelectCancelled>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MessageEEItemSelectCancelled>k__BackingField = value;
			}
		}

		public uint MessageEEItemSelectCompleted
		{
			[CompilerGenerated]
			get
			{
				return this.<MessageEEItemSelectCompleted>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MessageEEItemSelectCompleted>k__BackingField = value;
			}
		}

		public uint MessagePENewExportFile
		{
			[CompilerGenerated]
			get
			{
				return this.<MessagePENewExportFile>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MessagePENewExportFile>k__BackingField = value;
			}
		}

		public void Initialize(DrawingArea drawArea)
		{
			this.MessageEEItemSelectRequest = Utilities.RegisterWindowMessage("Expert Estimator Item Select Request");
			this.MessageEEItemSelectCancelled = Utilities.RegisterWindowMessage("Expert Estimator Item Select Cancelled");
			this.MessageEEItemSelectCompleted = Utilities.RegisterWindowMessage("Expert Estimator Item Select Completed");
			this.MessagePENewExportFile = Utilities.RegisterWindowMessage("Plan Expert New Export File");
			this.drawArea = drawArea;
		}

		public string InstalledPath()
		{
			string result;
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\AGCI\\EEWIN", false);
				result = Utilities.ConvertToString(registryKey.GetValue("EXEPath", ""), "");
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		public bool IsInstalled()
		{
			string text = this.InstalledPath();
			return !(text == string.Empty) && Utilities.FileExists(Path.Combine(text, "EEWin.exe"));
		}

		public bool IsRunning(ref IntPtr handle)
		{
			return Utilities.FindWindow("TApplication", "EEWin", ref handle);
		}

		public bool Start(ref IntPtr handle)
		{
			string text = this.InstalledPath();
			return !(text == string.Empty) && Utilities.StartProcess(Path.Combine(text, "EEWin.exe"), ref handle, 5000);
		}

		public bool EnsureRunning(bool setFocus)
		{
			IntPtr zero = IntPtr.Zero;
			bool result;
			try
			{
				bool flag = this.IsRunning(ref zero);
				if (!flag)
				{
					if (this.IsInstalled())
					{
						flag = this.Start(ref zero);
					}
					else
					{
						Utilities.DisplayError(Resources.Impossible_d_effectuer_l_opération_désirée, Resources.Expert_Estimateur_ne_semble_pas_installé_sur_cet_ordinateur);
					}
				}
				if (flag && setFocus)
				{
					Utilities.ForceWindowToForeground(zero);
				}
				result = flag;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public void SendItemSelectRequest(int windowHandle)
		{
			IntPtr zero = IntPtr.Zero;
			bool flag = this.IsRunning(ref zero);
			if (flag)
			{
				Utilities.SendMessage(zero, (int)this.MessageEEItemSelectRequest, windowHandle, 0);
			}
		}

		public void SendNewExportFileRequest(string exportFileName, string projectFileName)
		{
			IntPtr zero = IntPtr.Zero;
			if (!this.IsRunning(ref zero))
			{
				return;
			}
			string text = string.Concat(new object[]
			{
				"XML_FILE",
				'\t',
				"QPL_FILE",
				Environment.NewLine,
				exportFileName,
				'\t',
				projectFileName
			});
			if (!Utilities.CopyToClipboard(text))
			{
				return;
			}
			Utilities.SendMessage(zero, (int)this.MessagePENewExportFile, 0, 0);
		}

		private string GetFreeObjectName(string objectName)
		{
			int num = 0;
			if (this.drawArea.FindObjectByName(objectName, true) == null)
			{
				return objectName;
			}
			do
			{
				num++;
			}
			while (this.drawArea.FindObjectByName(objectName + " " + num, true) != null);
			return objectName + " " + num;
		}

		private string ValidateName(string name)
		{
			string result;
			try
			{
				name = Utilities.StripInvalidCharacters(name, "").Trim();
				if (name.Length > 45)
				{
					name = name.Substring(1, 45);
				}
				result = this.GetFreeObjectName(name);
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = "";
			}
			return result;
		}

		public EEExchangeData QueryItemSelectData()
		{
			try
			{
				EEExchangeData eeexchangeData = new EEExchangeData();
				IDataObject dataObject = Clipboard.GetDataObject();
				if (dataObject.GetDataPresent("Expert Estimator Item Select Data"))
				{
					object data = dataObject.GetData("Expert Estimator Item Select Data");
					Stream stream = (Stream)data;
					stream.Position = 0L;
					using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
					{
						streamReader.ReadLine();
						string text = streamReader.ReadLine();
						string[] array = text.Split(new char[]
						{
							'\t'
						}, StringSplitOptions.None);
						eeexchangeData.ItemType = Utilities.ConvertToString(array.GetValue(0), "");
						eeexchangeData.ItemID = Utilities.ConvertToString(array.GetValue(1), "");
						eeexchangeData.Name = Utilities.ConvertToString(array.GetValue(2), "");
						eeexchangeData.Key = Utilities.ConvertToString(array.GetValue(3), "");
						eeexchangeData.PersonalKey = Utilities.ConvertToString(array.GetValue(4), "");
						eeexchangeData.Description = Utilities.ConvertToString(array.GetValue(5), "");
						eeexchangeData.Name = this.ValidateName(eeexchangeData.Name);
					}
					return eeexchangeData;
				}
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
			}
			return null;
		}

		public EEInterface()
		{
		}

		private const string EEBinaryName = "EEWin.exe";

		private const string EEItemSelectDataFormatName = "Expert Estimator Item Select Data";

		private const string EEPlanExpertNewExportData = "Plan Expert New Export Data";

		private DrawingArea drawArea;

		[CompilerGenerated]
		private uint <MessageEEItemSelectRequest>k__BackingField;

		[CompilerGenerated]
		private uint <MessageEEItemSelectCancelled>k__BackingField;

		[CompilerGenerated]
		private uint <MessageEEItemSelectCompleted>k__BackingField;

		[CompilerGenerated]
		private uint <MessagePENewExportFile>k__BackingField;
	}
}
