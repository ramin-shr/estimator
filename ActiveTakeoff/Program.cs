using DevComponents.DotNetBar;
using QuoterPlan.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    internal static class Program
    {
        private static Guid guid;

        public static SingleInstance singleInstance;

        private static MainForm mainForm;

        [CompilerGenerated]
        // CS$<>9__CachedAnonymousMethodDelegate1
        private static Action<string[]> CSu0024u003cu003e9__CachedAnonymousMethodDelegate1;

        static Program()
        {
            Program.guid = new Guid("{3074FD6B-AECE-4884-AB4A-06F81DF1D036}");
            Program.singleInstance = new SingleInstance(Program.guid);
        }

        [CompilerGenerated]
        // <singleInstance_ArgumentsReceived>b__0
        private static void u003csingleInstance_ArgumentsReceivedu003eb__0(string[] arguments)
        {
            Program.mainForm.WindowState = (Program.mainForm.WindowState == FormWindowState.Minimized ? FormWindowState.Normal : Program.mainForm.WindowState);
            Program.mainForm.OpenFileFromArgs(arguments, 1);
            Program.mainForm.Focus();
        }

        private static Program.CheckForUpdatesEnum CheckForUpdates()
        {
            Program.CheckForUpdatesEnum checkForUpdatesEnum;
            try
            {
                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo(Path.Combine(Utilities.GetInstallFolder(), "wyUpdate.exe"), "-quickcheck -justcheck -noerr")
                };
                using (Process process1 = process)
                {
                    process1.Start();
                    process1.WaitForExit();
                    checkForUpdatesEnum = (process1.ExitCode == 2 ? Program.CheckForUpdatesEnum.Available : Program.CheckForUpdatesEnum.NotAvailable);
                }
            }
            catch
            {
                checkForUpdatesEnum = Program.CheckForUpdatesEnum.Error;
            }
            return checkForUpdatesEnum;
        }

        [STAThread]
        private static void Main()
        {
            using (Program.singleInstance)
            {
                bool flag = (Program.singleInstance.IsFirstInstance ? true : Settings.Default.AllowMultipleInstances);
                bool allowMultipleInstances = Settings.Default.AllowMultipleInstances;
                if (flag)
                {
                    if (!allowMultipleInstances)
                    {
                        Program.singleInstance.ArgumentsReceived += new EventHandler<ArgumentsReceivedEventArgs>(Program.singleInstance_ArgumentsReceived);
                        Program.singleInstance.ListenForArgumentsFromSuccessiveInstances();
                    }
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    if (!Program.WillDoUpdates() && Program.ValidateSettings())
                    {
                        MainForm mainForm = new MainForm();
                        Program.mainForm = mainForm;
                        Application.Run(mainForm);
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
            Action<string[]> windowState = (string[] arguments) => {
                Program.mainForm.WindowState = (Program.mainForm.WindowState == FormWindowState.Minimized ? FormWindowState.Normal : Program.mainForm.WindowState);
                Program.mainForm.OpenFileFromArgs(arguments, 1);
                Program.mainForm.Focus();
            };
            MainForm mainForm = Program.mainForm;
            object[] args = new object[] { e.Args };
            mainForm.Invoke(windowState, args);
        }

        private static bool StartUpdater()
        {
            IntPtr zero = IntPtr.Zero;
            return Utilities.StartProcess(Path.Combine(Utilities.GetInstallFolder(), "wyUpdate.exe"), ref zero, 0x1388);
        }

        private static bool ValidateSettings()
        {
            bool flag;
            try
            {
                Settings.Default.Reload();
                UnitScale.DefaultUnitPrecision();
                flag = true;
            }
            catch (ConfigurationErrorsException configurationErrorsException)
            {
                string filename = ((ConfigurationErrorsException)configurationErrorsException.InnerException).Filename;
                string uneErreurSystèmeEstSurvenue = Resources.Une_erreur_système_est_survenue;
                string[] applicationName = new string[] { Utilities.ApplicationName, " ", Resources.SettingsCorrupt1, " ", Utilities.ApplicationName, " ", Resources.SettingsCorrupt2 };
                Utilities.DisplayError(uneErreurSystèmeEstSurvenue, string.Concat(applicationName));
                Utilities.FileDelete(filename, true);
                Application.Restart();
                Environment.Exit(0);
                flag = false;
            }
            return flag;
        }

        private static bool WillDoUpdates()
        {
            if (Program.CheckForUpdates() != Program.CheckForUpdatesEnum.Available)
            {
                return false;
            }
            string str = "";
            string str1 = "";
            string installLanguage = Utilities.GetInstallLanguage();
            string str2 = installLanguage;
            if (installLanguage != null)
            {
                if (str2 == "fr")
                {
                    str = "Nouvelle version disponible";
                    str1 = "Lancer la mise à jour automatique maintenant ?";
                    if (Utilities.DisplayQuestionCustom(str, str1, Resources.update_available_48x48) == DialogResult.No)
                    {
                        return false;
                    }
                    return Program.StartUpdater();
                }
                else
                {
                    if (str2 != "es")
                    {
                        str = "New version available";
                        str1 = "Run the automatic updater now?";
                        if (Utilities.DisplayQuestionCustom(str, str1, Resources.update_available_48x48) == DialogResult.No)
                        {
                            return false;
                        }
                        return Program.StartUpdater();
                    }
                    str = "Nueva versión disponible";
                    str1 = "¿Ejecutar la actualización automática ahora?";
                    if (Utilities.DisplayQuestionCustom(str, str1, Resources.update_available_48x48) == DialogResult.No)
                    {
                        return false;
                    }
                    return Program.StartUpdater();
                }
            }
            str = "New version available";
            str1 = "Run the automatic updater now?";
            if (Utilities.DisplayQuestionCustom(str, str1, Resources.update_available_48x48) == DialogResult.No)
            {
                return false;
            }
            return Program.StartUpdater();
        }

        private enum CheckForUpdatesEnum
        {
            Available,
            NotAvailable,
            Error
        }
    }
}