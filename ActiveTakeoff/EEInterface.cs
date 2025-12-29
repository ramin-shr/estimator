using Microsoft.Win32;
using QuoterPlan.Properties;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class EEInterface
    {
        private const string EEBinaryName = "EEWin.exe";

        private const string EEItemSelectDataFormatName = "Expert Estimator Item Select Data";

        private const string EEPlanExpertNewExportData = "Plan Expert New Export Data";

        private DrawingArea drawArea;

        public uint MessageEEItemSelectCancelled
        {
            get;
            private set;
        }

        public uint MessageEEItemSelectCompleted
        {
            get;
            private set;
        }

        public uint MessageEEItemSelectRequest
        {
            get;
            private set;
        }

        public uint MessagePENewExportFile
        {
            get;
            private set;
        }

        public EEInterface()
        {
        }

        public bool EnsureRunning(bool setFocus)
        {
            bool flag;
            IntPtr zero = IntPtr.Zero;
            try
            {
                bool flag1 = this.IsRunning(ref zero);
                if (!flag1)
                {
                    if (!this.IsInstalled())
                    {
                        Utilities.DisplayError(Resources.Impossible_d_effectuer_l_opération_désirée, Resources.Expert_Estimateur_ne_semble_pas_installé_sur_cet_ordinateur);
                    }
                    else
                    {
                        flag1 = this.Start(ref zero);
                    }
                }
                if (flag1 && setFocus)
                {
                    Utilities.ForceWindowToForeground(zero);
                }
                flag = flag1;
            }
            catch
            {
                flag = false;
            }
            return flag;
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
            while (this.drawArea.FindObjectByName(string.Concat(objectName, " ", num), true) != null);
            return string.Concat(objectName, " ", num);
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
            string str;
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\AGCI\\EEWIN", false);
                str = Utilities.ConvertToString(registryKey.GetValue("EXEPath", ""), "");
            }
            catch
            {
                str = string.Empty;
            }
            return str;
        }

        public bool IsInstalled()
        {
            string str = this.InstalledPath();
            if (str == string.Empty)
            {
                return false;
            }
            return Utilities.FileExists(Path.Combine(str, "EEWin.exe"));
        }

        public bool IsRunning(ref IntPtr handle)
        {
            return Utilities.FindWindow("TApplication", "EEWin", ref handle);
        }

        public EEExchangeData QueryItemSelectData()
        {
            try
            {
                EEExchangeData eEExchangeDatum = new EEExchangeData();
                IDataObject dataObject = System.Windows.Forms.Clipboard.GetDataObject();
                if (dataObject.GetDataPresent("Expert Estimator Item Select Data"))
                {
                    Stream data = (Stream)dataObject.GetData("Expert Estimator Item Select Data");
                    data.Position = (long)0;
                    using (StreamReader streamReader = new StreamReader(data, Encoding.UTF8))
                    {
                        streamReader.ReadLine();
                        string str = streamReader.ReadLine();
                        char[] chrArray = new char[] { '\t' };
                        string[] strArrays = str.Split(chrArray, StringSplitOptions.None);
                        eEExchangeDatum.ItemType = Utilities.ConvertToString(strArrays.GetValue(0), "");
                        eEExchangeDatum.ItemID = Utilities.ConvertToString(strArrays.GetValue(1), "");
                        eEExchangeDatum.Name = Utilities.ConvertToString(strArrays.GetValue(2), "");
                        eEExchangeDatum.Key = Utilities.ConvertToString(strArrays.GetValue(3), "");
                        eEExchangeDatum.PersonalKey = Utilities.ConvertToString(strArrays.GetValue(4), "");
                        eEExchangeDatum.Description = Utilities.ConvertToString(strArrays.GetValue(5), "");
                        eEExchangeDatum.Name = this.ValidateName(eEExchangeDatum.Name);
                    }
                    return eEExchangeDatum;
                }
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
            }
            return null;
        }

        public void SendItemSelectRequest(int windowHandle)
        {
            IntPtr zero = IntPtr.Zero;
            if (this.IsRunning(ref zero))
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
            object[] newLine = new object[] { "XML_FILE", '\t', "QPL_FILE", Environment.NewLine, exportFileName, '\t', projectFileName };
            if (!Utilities.CopyToClipboard(string.Concat(newLine)))
            {
                return;
            }
            Utilities.SendMessage(zero, (int)this.MessagePENewExportFile, 0, 0);
        }

        public bool Start(ref IntPtr handle)
        {
            string str = this.InstalledPath();
            if (str == string.Empty)
            {
                return false;
            }
            return Utilities.StartProcess(Path.Combine(str, "EEWin.exe"), ref handle, 0x1388);
        }

        private string ValidateName(string name)
        {
            string freeObjectName;
            try
            {
                name = Utilities.StripInvalidCharacters(name, "").Trim();
                if (name.Length > 45)
                {
                    name = name.Substring(1, 45);
                }
                freeObjectName = this.GetFreeObjectName(name);
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                freeObjectName = "";
            }
            return freeObjectName;
        }
    }
}