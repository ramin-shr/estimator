using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			using (Program.singleInstance)
			{
				bool flag = Program.singleInstance.IsFirstInstance || Settings.Default.AllowMultipleInstances;
				bool allowMultipleInstances = Settings.Default.AllowMultipleInstances;
				if (flag)
				{
					if (!allowMultipleInstances)
					{
						Program.singleInstance.ArgumentsReceived += Program.singleInstance_ArgumentsReceived;
						Program.singleInstance.ListenForArgumentsFromSuccessiveInstances();
					}
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					if (!Program.WillDoUpdates() && Program.ValidateSettings())
					{
						Application.Run(Program.mainForm = new MainForm());
					}
				}
				else if (!allowMultipleInstances)
				{
					Program.singleInstance.PassArgumentsToFirstInstance(Environment.GetCommandLineArgs());
				}
			}
		}

		private static void singleInstance_ArgumentsReceived(object sender, ArgumentsReceivedEventArgs e)
		{
			if (Program.mainForm == null)
			{
				return;
			}
			Action<string[]> method = delegate(string[] arguments)
			{
				Program.mainForm.WindowState = ((Program.mainForm.WindowState == FormWindowState.Minimized) ? FormWindowState.Normal : Program.mainForm.WindowState);
				Program.mainForm.OpenFileFromArgs(arguments, 1);
				Program.mainForm.Focus();
			};
			Program.mainForm.Invoke(method, new object[]
			{
				e.Args
			});
		}

		private static bool ValidateSettings()
		{
			bool result;
			try
			{
				Settings.Default.Reload();
				UnitScale.DefaultUnitPrecision();
				result = true;
			}
			catch (ConfigurationErrorsException ex)
			{
				string filename = ((ConfigurationErrorsException)ex.InnerException).Filename;
				Utilities.DisplayError(Resources.Une_erreur_système_est_survenue, string.Concat(new string[]
				{
					Utilities.ApplicationName,
					" ",
					Resources.SettingsCorrupt1,
					" ",
					Utilities.ApplicationName,
					" ",
					Resources.SettingsCorrupt2
				}));
				Utilities.FileDelete(filename, true);
				Application.Restart();
				Environment.Exit(0);
				result = false;
			}
			return result;
		}

		private static Program.CheckForUpdatesEnum CheckForUpdates()
		{
			Program.CheckForUpdatesEnum result;
			try
			{
				using (Process process = new Process
				{
					StartInfo = new ProcessStartInfo(Path.Combine(Utilities.GetInstallFolder(), "wyUpdate.exe"), "-quickcheck -justcheck -noerr")
				})
				{
					process.Start();
					process.WaitForExit();
					result = ((process.ExitCode == 2) ? Program.CheckForUpdatesEnum.Available : Program.CheckForUpdatesEnum.NotAvailable);
				}
			}
			catch
			{
				result = Program.CheckForUpdatesEnum.Error;
			}
			return result;
		}

		private static bool StartUpdater()
		{
			IntPtr zero = IntPtr.Zero;
			return Utilities.StartProcess(Path.Combine(Utilities.GetInstallFolder(), "wyUpdate.exe"), ref zero, 5000);
		}

		private static bool WillDoUpdates()
		{
			if (Program.CheckForUpdates() != Program.CheckForUpdatesEnum.Available)
			{
				return false;
			}
			string installLanguage;
			string title;
			string message;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					title = "Nouvelle version disponible";
					message = "Lancer la mise à jour automatique maintenant ?";
					goto IL_62;
				}
				if (installLanguage == "es")
				{
					title = "Nueva versión disponible";
					message = "¿Ejecutar la actualización automática ahora?";
					goto IL_62;
				}
			}
			title = "New version available";
			message = "Run the automatic updater now?";
			IL_62:
			return Utilities.DisplayQuestionCustom(title, message, Resources.update_available_48x48) != DialogResult.No && Program.StartUpdater();
		}

		[CompilerGenerated]
		private static void <singleInstance_ArgumentsReceived>b__0(string[] arguments)
		{
			Program.mainForm.WindowState = ((Program.mainForm.WindowState == FormWindowState.Minimized) ? FormWindowState.Normal : Program.mainForm.WindowState);
			Program.mainForm.OpenFileFromArgs(arguments, 1);
			Program.mainForm.Focus();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Program()
		{
		}

		private static Guid guid = new Guid("{3074FD6B-AECE-4884-AB4A-06F81DF1D036}");

		public static SingleInstance singleInstance = new SingleInstance(Program.guid);

		private static MainForm mainForm;

		[CompilerGenerated]
		private static Action<string[]> CS$<>9__CachedAnonymousMethodDelegate1;

		private enum CheckForUpdatesEnum
		{
			Available,
			NotAvailable,
			Error
		}
	}
}
