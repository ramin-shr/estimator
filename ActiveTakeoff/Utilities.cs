using Microsoft.VisualBasic.Devices;
using QuoterPlan.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Clipboard = System.Windows.Forms.Clipboard;

namespace QuoterPlan
{
    public static class Utilities
    {
        private const int WM_SETREDRAW = 11;

        public const uint SW_SHOW = 5;

        public static string DefaultFontName;

        private static List<Variable> fonts;

        public static string AlternateFontName;

        private static int ScreenDpi;

        public static Hashtable FontResources;

        [CompilerGenerated]
        // CS$<>9__CachedAnonymousMethodDelegate4
        private static Predicate<char> CSu0024u003cu003e9__CachedAnonymousMethodDelegate4;

        public static string À_propos_de_Quoter_Plan
        {
            get
            {
                return string.Format(Resources.À_propos_de_Quoter_Plan, Utilities.ApplicationName);
            }
        }

        public static string À_propos_de_Quoter_Plan1
        {
            get
            {
                return string.Format(Resources.À_propos_de_Quoter_Plan1, Utilities.ApplicationName);
            }
        }

        public static string ApplicationBinaryName
        {
            get
            {
                return typeof(Program).Assembly.GetName().Name;
            }
        }

        public static string ApplicationHelpFile
        {
            get
            {
                return string.Concat(Utilities.ApplicationBinaryName, ".chm");
            }
        }

        public static string ApplicationName
        {
            get
            {
                return "Active Takeoff";
            }
        }

        public static string ApplicationWebsite
        {
            get
            {
                return "www.activetakeoff.com";
            }
        }

        public static string Ce_rapport_a_été_généré_grâce_à_Quoter_Plan
        {
            get
            {
                return string.Format(Resources.Ce_rapport_a_été_généré_grâce_à_Quoter_Plan, Utilities.ApplicationName, Utilities.ApplicationWebsite);
            }
        }

        public static string Certains_plans_peuvent_avoir_perdu_leur_intégrité
        {
            get
            {
                return string.Format(Resources.Certains_plans_peuvent_avoir_perdu_leur_intégrité, Utilities.ApplicationName, Utilities.CompanyName);
            }
        }

        public static string CompanyName
        {
            get
            {
                return "Quoter Software Inc.";
            }
        }

        public static string EnvoyerDonnées1
        {
            get
            {
                return string.Format(Resources.EnvoyerDonnées1, Utilities.ApplicationName);
            }
        }

        public static string Pdf_export_warning
        {
            get
            {
                return string.Format(Resources.Pdf_export_warning, Utilities.ApplicationName, Utilities.ApplicationWebsite);
            }
        }

        public static string Projets_Quoter_Plan
        {
            get
            {
                return string.Format(Resources.Projets_Quoter_Plan, Utilities.ApplicationName);
            }
        }

        public static string Vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif
        {
            get
            {
                return string.Format(Resources.Vous_devez_redémarrer_Quoter_Plan_pour_que_les_changements_soient_effectif, Utilities.ApplicationName);
            }
        }

        static Utilities()
        {
            Utilities.DefaultFontName = "Segoe UI";
            Utilities.fonts = new List<Variable>();
            Utilities.AlternateFontName = "MS Sans Serif";
            Utilities.ScreenDpi = 0;
            Utilities.FontResources = new Hashtable();
        }

        [CompilerGenerated]
        // <Clone>b__5
        private static T u003cCloneu003eb__5<T>(T item)
        where T : ICloneable
        {
            return (T)item.Clone();
        }

        [CompilerGenerated]
        // <StripNonAlphanumericalCharacter>b__3
        private static bool u003cStripNonAlphanumericalCharacteru003eb__3(char c)
        {
            if (char.IsLetterOrDigit(c))
            {
                return true;
            }
            return c == '\u005F';
        }

        public static void AddToStringArray(ref string[] stringArray, string newString)
        {
            Array.Resize<string>(ref stringArray, (int)stringArray.Length + 1);
            stringArray[(int)stringArray.Length - 1] = newString;
        }

        public static float AlternateFontSizeInPixels()
        {
            return 11f;
        }

        public static float AlternateFontSizeInPoints()
        {
            return Utilities.AlternateFontSizeInPixels() * 72f / (float)Utilities.GetScreenDpi();
        }

        public static void AttachedThreadInputAction(Action action)
        {
            uint windowThreadProcessId = Utilities.GetWindowThreadProcessId(Utilities.GetForegroundWindow(), IntPtr.Zero);
            uint currentThreadId = Utilities.GetCurrentThreadId();
            bool flag = false;
            try
            {
                flag = (windowThreadProcessId == currentThreadId ? true : Utilities.AttachThreadInput(windowThreadProcessId, currentThreadId, true));
                if (!flag)
                {
                    throw new ThreadStateException("AttachThreadInput failed.");
                }
                action();
            }
            finally
            {
                if (flag)
                {
                    Utilities.AttachThreadInput(windowThreadProcessId, currentThreadId, false);
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        public static extern bool BringWindowToTop(HandleRef hWnd);

        public static bool CheckAlphaNumericUnderscoreSpaces(ref string str)
        {
            if (str == null)
            {
                return false;
            }
            str = str.Trim();
            return Regex.Match(str, "[A-Za-z0-9-]+").Success;
        }

        public static List<T> Clone<T>(this List<T> listToClone)
        where T : ICloneable
        {
            return (
                from item in listToClone
                select (T)item.Clone()).ToList<T>();
        }

        public static string ColorToHex(Color c)
        {
            string str = c.R.ToString("X2");
            string str1 = c.G.ToString("X2");
            byte b = c.B;
            return string.Concat("#", str, str1, b.ToString("X2"));
        }

        public static double ComputeAvailableMemoryForImage()
        {
            double num = 40000000;
            double megabytes = Utilities.ConvertBytesToMegabytes((long)Utilities.GetTotalMemoryInBytes());
            for (int i = 1; i <= 32; i++)
            {
                num = 10000 * (4000 + (double)i * 250);
                if ((double)(0x200 * i) >= Math.Ceiling(megabytes))
                {
                    break;
                }
            }
            Console.WriteLine(string.Concat("Available megabytes --> ", megabytes));
            Console.WriteLine(string.Concat("ComputeAvailableMemoryForImage = ", num));
            return num;
        }

        private static double ConvertBytesToMegabytes(long bytes)
        {
            return (double)((float)bytes / 1024f / 1024f);
        }

        public static double ConvertCurrency(string value, double defaultValue)
        {
            double num;
            try
            {
                string str = value;
                string str1 = Utilities.NumberDecimalSeparator();
                str = str.Replace(",", str1).Replace(".", str1);
                num = double.Parse(str, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol | NumberStyles.Integer | NumberStyles.Number);
            }
            catch (Exception exception)
            {
                num = defaultValue;
            }
            return num;
        }

        private static double ConvertKilobytesToMegabytes(long kilobytes)
        {
            return (double)((float)kilobytes / 1024f);
        }

        public static bool ConvertToBoolean(object value, bool defaultValue)
        {
            bool flag;
            try
            {
                flag = Convert.ToBoolean(value);
            }
            catch (Exception exception)
            {
                flag = defaultValue;
            }
            return flag;
        }

        public static double ConvertToDouble(object value, int decimals = -1)
        {
            double num;
            try
            {
                string str = value.ToString();
                string str1 = Utilities.NumberDecimalSeparator();
                str = str.Replace(",", str1).Replace(".", str1);
                decimal num1 = decimal.Parse(str);
                if (decimals > -1)
                {
                    num1 = Math.Round(num1, decimals);
                }
                num = (double)((double)num1);
            }
            catch (Exception exception)
            {
                num = 0;
            }
            return num;
        }

        public static int ConvertToInt(object value)
        {
            int num;
            try
            {
                num = Convert.ToInt32(value);
            }
            catch (Exception exception)
            {
                num = 0;
            }
            return num;
        }

        public static string ConvertToString(object value, string defaultValue)
        {
            string str;
            try
            {
                str = value.ToString();
            }
            catch (Exception exception)
            {
                str = defaultValue;
            }
            return str;
        }

        public static void CopyToClipboard(string formatName, bool autoConvert, object data)
        {
            DataFormats.Format format = DataFormats.GetFormat(formatName);
            IDataObject dataObject = new DataObject();
            dataObject.SetData(format.Name, autoConvert, data);
            System.Windows.Forms.Clipboard.SetDataObject(dataObject, false);
        }

        public static bool CopyToClipboard(string text)
        {
            bool flag;
            try
            {
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(text);
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                flag = false;
            }
            return flag;
        }

        public static bool CopyToClipboard(string text, TextDataFormat textDataFormat)
        {
            bool flag;
            try
            {
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetText(text, textDataFormat);
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                flag = false;
            }
            return flag;
        }

        public static bool CreateDirectory(string directoryName)
        {
            bool flag;
            try
            {
                Directory.CreateDirectory(directoryName);
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static Font CreateFont(string fontFamily, float fontSize, FontStyle fontStyle)
        {
            string[] strArrays = new string[] { fontFamily, ";", fontSize.ToString(), ";", fontStyle.ToString() };
            string str = string.Concat(strArrays);
            if (Utilities.FontResources.ContainsKey(str))
            {
                return (Font)Utilities.FontResources[str];
            }
            Font font = new Font(fontFamily, fontSize, fontStyle);
            Utilities.FontResources.Add(str, font);
            return font;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr CreateIconIndirect(ref Utilities.IconInfo icon);

        public static float DefaultFontSizeInPixels()
        {
            return 12f;
        }

        public static float DefaultFontSizeInPoints()
        {
            return Utilities.DefaultFontSizeInPixels() * 72f / (float)Utilities.GetScreenDpi();
        }

        public static double DegreeToRadian(double angle)
        {
            return 3.14159265358979 * angle / 180;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        public static bool DirectoryDelete(string directoryName)
        {
            bool flag;
            try
            {
                Directory.Delete(directoryName);
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static bool DirectoryEmpty(string directoryName)
        {
            bool length;
            try
            {
                length = (int)Directory.GetFiles(directoryName).Length == 0;
            }
            catch
            {
                length = false;
            }
            return length;
        }

        public static bool DirectoryExists(string directoryName)
        {
            bool flag;
            try
            {
                flag = Directory.Exists(directoryName);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static DialogResult DisplayDeleteConfirmation(string title, string message)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayDeleteConfirmation, DialogResult.No, null, "", "");
        }

        public static DialogResult DisplayDeleteConfirmation(string title, string message, string customCaption)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayDeleteConfirmation, DialogResult.No, null, customCaption, "");
        }

        public static void DisplayError(string title, string message)
        {
            Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayError, DialogResult.OK, null, "", "");
        }

        public static void DisplayFileOpenError(string fileName, Exception exception)
        {
            string erreurDeLecture = Resources.Erreur_de_lecture;
            string empty = string.Empty;
            try
            {
                empty = string.Concat(Resources.Impossible_d_ouvrir_le_fichier, Environment.NewLine);
                empty = string.Concat(empty, fileName, Environment.NewLine, Environment.NewLine);
                empty = string.Concat(empty, exception.Message);
            }
            catch
            {
            }
            Utilities.DisplayMessageBoxEx(erreurDeLecture, empty, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
        }

        public static void DisplayFileSaveError(string fileName, Exception exception)
        {
            string erreurDÉcriture = Resources.Erreur_d_écriture;
            string empty = string.Empty;
            try
            {
                empty = string.Concat(Resources.Impossible_d_enregistrer_le_fichier, Environment.NewLine);
                empty = string.Concat(empty, fileName, Environment.NewLine, Environment.NewLine);
                empty = string.Concat(empty, exception.Message);
            }
            catch
            {
            }
            Utilities.DisplayMessageBoxEx(erreurDÉcriture, empty, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
        }

        public static void DisplayLogError(string title, string message)
        {
            Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
        }

        public static void DisplayMessage(string title, string message)
        {
            Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayMessage, DialogResult.OK, null, "", "");
        }

        private static DialogResult DisplayMessageBoxEx(string title, string message, MessageBoxEx.MessageTypeEnum messageType, DialogResult defautResult, Image customImage = null, string customCaption1 = "", string customCaption2 = "")
        {
            DialogResult dialogResult = defautResult;
            using (MessageBoxEx messageBoxEx = new MessageBoxEx(title, message, messageType, defautResult))
            {
                if (customImage != null)
                {
                    messageBoxEx.SetCustomImage(customImage);
                }
                if (customCaption1 != "")
                {
                    messageBoxEx.SetCustomCaption1(customCaption1);
                }
                if (customCaption2 != "")
                {
                    messageBoxEx.SetCustomCaption2(customCaption2);
                }
                messageBoxEx.ShowDialog();
                dialogResult = messageBoxEx.DialogResult;
            }
            return dialogResult;
        }

        public static void DisplayMessageCustom(string title, string message, Image customImage, string customCaption)
        {
            Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayMessage, DialogResult.OK, customImage, customCaption, "");
        }

        public static DialogResult DisplayQuestion(string title, string message)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayQuestion, DialogResult.Yes, null, "", "");
        }

        public static DialogResult DisplayQuestionCustom(string title, string message, string customCaption1, string customCaption2)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayQuestionCustom, DialogResult.Yes, null, customCaption1, customCaption2);
        }

        public static DialogResult DisplayQuestionCustom(string title, string message, Image customImage)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayQuestion, DialogResult.Yes, customImage, "", "");
        }

        public static void DisplaySystemError(Exception exception)
        {
            string uneErreurSystèmeEstSurvenue = Resources.Une_erreur_système_est_survenue;
            string empty = string.Empty;
            try
            {
                empty = string.Concat(exception.Message, Environment.NewLine, exception.StackTrace);
            }
            catch
            {
            }
            Utilities.DisplayMessageBoxEx(uneErreurSystèmeEstSurvenue, empty, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
        }

        public static void DisplayWarning(string title, string message)
        {
            Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarning, DialogResult.OK, null, "", "");
        }

        public static DialogResult DisplayWarningQuestion(string title, string message)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestion, DialogResult.No, null, "", "");
        }

        public static DialogResult DisplayWarningQuestionCustom(string title, string message, Image customImage, string customCaption)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionCustom, DialogResult.No, customImage, customCaption, "");
        }

        public static DialogResult DisplayWarningQuestionOkCancel(string title, string message)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionOkCancel, DialogResult.Cancel, null, "", "");
        }

        public static DialogResult DisplayWarningQuestionRetryCancel(string title, string message)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionOkCancel, DialogResult.Cancel, null, Resources.Réessayer, "");
        }

        public static DialogResult DisplayWarningQuestionYesNoCancel(string title, string message)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionYesNoCancel, DialogResult.Cancel, null, "", "");
        }

        public static DialogResult DisplayWarningQuestionYesNoCancelCustom(string title, string message, Image customImage, string customCaption)
        {
            return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionYesNoCancel, DialogResult.Cancel, customImage, customCaption, "");
        }

        public static void DisposeFontResources()
        {
            foreach (Font value in Utilities.FontResources.Values)
            {
                value.Dispose();
            }
            Utilities.FontResources.Clear();
            Utilities.FontResources = null;
        }

        public static void EnableInterface(Form form, bool enable)
        {
            Cursor.Current = (enable ? Cursors.Default : Cursors.WaitCursor);
            Utilities.EnableWindow(form.Handle, enable);
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        public static string EscapeString(string s)
        {
            return SecurityElement.Escape(s);
        }

        public static bool FileCopy(string sourceFileName, string destinationFileName)
        {
            bool flag;
            try
            {
                File.Copy(sourceFileName, destinationFileName, true);
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                flag = false;
            }
            return flag;
        }

        public static bool FileDelete(string fileName, bool displayError = true)
        {
            bool flag;
            try
            {
                File.Delete(fileName);
                Console.WriteLine(string.Concat("FileDelete ", fileName));
                flag = true;
            }
            catch (IOException oException1)
            {
                IOException oException = oException1;
                if (displayError)
                {
                    string str = string.Concat(Resources.Impossible_de_supprimer, "\n", Utilities.TruncatePath(fileName, 32));
                    Utilities.DisplayError(str, oException.Message);
                }
                flag = false;
            }
            return flag;
        }

        public static bool FileExists(string fileName)
        {
            bool flag;
            try
            {
                flag = File.Exists(fileName);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static bool FileMove(string sourceFileName, string destinationFileName)
        {
            bool flag;
            try
            {
                File.Move(sourceFileName, destinationFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                flag = false;
            }
            return flag;
        }

        public static void FilesCopy(string sourceDir, string targetDir, bool subDirectories, bool overwriteFiles = true)
        {
            Utilities.ValidateDirectory(targetDir);
            try
            {
                string[] files = Directory.GetFiles(sourceDir);
                for (int i = 0; i < (int)files.Length; i++)
                {
                    string str = files[i];
                    string str1 = Path.Combine(targetDir, Path.GetFileName(str));
                    if (overwriteFiles || !overwriteFiles && !Utilities.FileExists(str1))
                    {
                        Utilities.FileCopy(str, str1);
                    }
                }
                if (subDirectories)
                {
                    string[] directories = Directory.GetDirectories(sourceDir);
                    for (int j = 0; j < (int)directories.Length; j++)
                    {
                        string str2 = directories[j];
                        Utilities.FilesCopy(str2, Path.Combine(targetDir, Path.GetFileName(str2)), subDirectories, overwriteFiles);
                    }
                }
            }
            catch
            {
            }
        }

        [DllImport("User32.Dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static bool FindWindow(string className, ref IntPtr handle)
        {
            bool num;
            try
            {
                handle = Utilities.FindWindow(className, null);
                num = handle.ToInt32() > 0;
            }
            catch
            {
                num = false;
            }
            return num;
        }

        public static bool FindWindow(string className, string windowName, ref IntPtr handle)
        {
            bool num;
            try
            {
                handle = Utilities.FindWindow(className, windowName);
                num = handle.ToInt32() > 0;
            }
            catch
            {
                num = false;
            }
            return num;
        }

        public static float FontSizeInPoints(float fontSizeInPixels)
        {
            return fontSizeInPixels * 72f / (float)Utilities.GetScreenDpi();
        }

        public static void ForceWindowToForeground(IntPtr hwnd)
        {
            Utilities.AttachedThreadInputAction(() => {
                Utilities.BringWindowToTop(hwnd);
                Utilities.ShowWindow(hwnd, 5);
            });
        }

        public static string FormatDate(DateTime vDate)
        {
            string str = vDate.Year.ToString();
            string str1 = vDate.Month.ToString();
            string str2 = vDate.Day.ToString();
            string[] strArrays = new string[] { str, "/", str1, "/", str2 };
            return string.Concat(strArrays);
        }

        public static string FormatDateLong(DateTime vDate)
        {
            string str = vDate.Year.ToString();
            string str1 = vDate.Month.ToString();
            string str2 = vDate.Day.ToString();
            DateTime dateTime = new DateTime(vDate.Year, vDate.Month, vDate.Day, vDate.Hour, vDate.Minute, vDate.Second, vDate.Kind);
            TimeSpan timeOfDay = dateTime.TimeOfDay;
            string str3 = timeOfDay.ToString().Replace(':', '.');
            string[] strArrays = new string[] { str, "/", str1, "/", str2, "/", str3 };
            return string.Concat(strArrays);
        }

        public static string FormatToCurrency(string value)
        {
            string str = 0.ToString("C");
            try
            {
                double num = 0;
                if (double.TryParse(value, NumberStyles.Currency, (IFormatProvider)null, out num))
                {
                    str = num.ToString("C");
                }
            }
            catch
            {
            }
            return str;
        }

        public static string GetApplicationDataFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Utilities.ApplicationName);
        }

        public static string GetApplicationFolder()
        {
            string directoryName;
            try
            {
                directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            }
            catch
            {
                directoryName = "";
            }
            return directoryName;
        }

        public static AutoScaleMode GetAutoScaleMode()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                Console.WriteLine("Designtime");
                return AutoScaleMode.Font;
            }
            Console.WriteLine("Runtime");
            return AutoScaleMode.None;
        }

        public static string GetBackupDBFileName(string dateString)
        {
            return Path.Combine(Utilities.GetDBFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName)), string.Concat("DB (", dateString, ").dat.bak"));
        }

        public static string GetBackupSettingsFileName()
        {
            return Path.Combine(Utilities.GetDBFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName)), "Settings.bak");
        }

        public static Color GetBasicColor(Utilities.BasicColorEnum inColor)
        {
            return Utilities.GetBasicColor((int)inColor);
        }

        public static Color GetBasicColor(int inColor)
        {
            switch (inColor)
            {
                case 0:
                    {
                        return Color.FromArgb(0, 0, 0);
                    }
                case 1:
                    {
                        return Color.FromArgb(128, 0, 0);
                    }
                case 2:
                    {
                        return Color.FromArgb(0, 128, 0);
                    }
                case 3:
                    {
                        return Color.FromArgb(128, 128, 0);
                    }
                case 4:
                    {
                        return Color.FromArgb(0, 0, 128);
                    }
                case 5:
                    {
                        return Color.FromArgb(128, 0, 128);
                    }
                case 6:
                    {
                        return Color.FromArgb(0, 128, 128);
                    }
                case 7:
                    {
                        return Color.FromArgb(192, 192, 192);
                    }
                case 8:
                    {
                        return Color.FromArgb(128, 128, 128);
                    }
                case 9:
                    {
                        return Color.FromArgb(0xff, 0, 0);
                    }
                case 10:
                    {
                        return Color.FromArgb(0, 0xff, 0);
                    }
                case 11:
                    {
                        return Color.FromArgb(0xff, 0xff, 0);
                    }
                case 12:
                    {
                        return Color.FromArgb(0, 0, 0xff);
                    }
                case 13:
                    {
                        return Color.FromArgb(0xff, 0, 0xff);
                    }
                case 14:
                    {
                        return Color.FromArgb(0, 0xff, 0xff);
                    }
                case 15:
                    {
                        return Color.FromArgb(0xff, 0xff, 0xff);
                    }
            }
            return Color.FromArgb(0xff, 0xff, 0xff);
        }

        public static bool GetBoolAttribute(XmlTextReader reader, string name, bool defaultValue)
        {
            bool flag;
            try
            {
                flag = Convert.ToBoolean(reader.GetAttribute(name));
            }
            catch (Exception exception)
            {
                flag = defaultValue;
            }
            return flag;
        }

        public static string GetBuyUrl()
        {
            if (Utilities.GetCurrentValidUICultureShort() == "es")
            {
                return "http://www.activetakeoff.com/es/store/buy/";
            }
            return "http://www.activetakeoff.com/store/buy/";
        }

        [DllImport("User32.Dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int GetClassName(int hwnd, StringBuilder lpClassName, int nMaxCount);

        public static string GetCopyright()
        {
            object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(false);
            for (int i = 0; i < (int)customAttributes.Length; i++)
            {
                object obj = customAttributes[i];
                if (obj.GetType() == typeof(AssemblyCopyrightAttribute))
                {
                    return ((AssemblyCopyrightAttribute)obj).Copyright;
                }
            }
            return string.Empty;
        }

        public static string GetCountersFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes compteurs";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Counters";
                        return Path.Combine(Utilities.GetUserDataFolder(), str);
                    }
                    str = "Mis contadores";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
            }
            str = "My Counters";
            return Path.Combine(Utilities.GetUserDataFolder(), str);
        }

        public static string GetCurrencySymbol()
        {
            return RegionInfo.CurrentRegion.CurrencySymbol;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern uint GetCurrentThreadId();

        public static string GetCurrentValidUICulture()
        {
            string name = Thread.CurrentThread.CurrentUICulture.Name;
            string str = name;
            string str1 = str;
            if (str == null || !(str1 == "en-US") && !(str1 == "fr-FR") && !(str1 == "es"))
            {
                name = Utilities.GetDefaultInvariantUICulture();
            }
            return name;
        }

        public static string GetCurrentValidUICultureShort()
        {
            return Utilities.GetCurrentValidUICulture().Substring(0, 2);
        }

        public static string GetDateString(string sDateString, string languageShort)
        {
            string[] strArrays;
            string str;
            string str1 = "";
            string str2 = languageShort;
            string str3 = str2;
            if (str2 != null)
            {
                if (str3 == "fr")
                {
                    string[] strArrays1 = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };
                    strArrays = strArrays1;
                    goto Label0;
                }
                else
                {
                    if (str3 != "es")
                    {
                        goto Label3;
                    }
                    string[] strArrays2 = new string[] { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
                    strArrays = strArrays2;
                    goto Label0;
                }
            }
        Label3:
            string[] strArrays3 = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            strArrays = strArrays3;
        Label0:
            string[] strArrays4 = new string[] { "/" };
            string[] strArrays5 = sDateString.Split(strArrays4, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                string str4 = strArrays5.GetValue(0).ToString();
                string str5 = strArrays5.GetValue(1).ToString();
                string str6 = strArrays5.GetValue(2).ToString();
                try
                {
                    str1 = strArrays5.GetValue(3).ToString();
                }
                catch
                {
                }
                string str7 = languageShort;
                string str8 = str7;
                if (str7 != null)
                {
                    if (str8 == "fr")
                    {
                        object[] num = new object[] { Utilities.ConvertToInt(str6), " ", strArrays[Utilities.ConvertToInt(str5) - 1], " ", str4, null };
                        num[5] = (str1 == "" ? "" : string.Concat(" ", str1));
                        str = string.Concat(num);
                        return str;
                    }
                    else if (str8 == "es")
                    {
                        object[] objArray = new object[] { Utilities.ConvertToInt(str6), " de ", strArrays[Utilities.ConvertToInt(str5) - 1], " de ", str4, null };
                        objArray[5] = (str1 == "" ? "" : string.Concat(" ", str1));
                        str = string.Concat(objArray);
                        return str;
                    }
                }
                object[] objArray1 = new object[] { strArrays[Utilities.ConvertToInt(str5) - 1], " ", Utilities.ConvertToInt(str6), ", ", str4, null };
                objArray1[5] = (str1 == "" ? "" : string.Concat(" ", str1));
                str = string.Concat(objArray1);
            }
            catch
            {
                str = "";
            }
            return str;
        }

        public static string GetDBFolder(string userDataFolder = "")
        {
            string str;
            userDataFolder = (userDataFolder == "" ? Utilities.GetUserDataFolder() : userDataFolder);
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes données";
                    return Path.Combine(userDataFolder, str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Databases";
                        return Path.Combine(userDataFolder, str);
                    }
                    str = "Mis bases de datos";
                    return Path.Combine(userDataFolder, str);
                }
            }
            str = "My Databases";
            return Path.Combine(userDataFolder, str);
        }

        public static string GetDefaultDBName()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "DB.dat";
                    return str;
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "DB.dat";
                        return str;
                    }
                    str = "DB.dat";
                    return str;
                }
            }
            str = "DB.dat";
            return str;
        }

        public static Font GetDefaultFont()
        {
            if (Utilities.IsSegoeUIInstalled())
            {
                return Utilities.GetFontFromFactory(Utilities.DefaultFontName, Utilities.DefaultFontSizeInPoints(), FontStyle.Regular);
            }
            return Utilities.GetFontFromFactory(Utilities.AlternateFontName, Utilities.AlternateFontSizeInPoints(), FontStyle.Regular);
        }

        public static Font GetDefaultFont(FontStyle style)
        {
            if (Utilities.IsSegoeUIInstalled())
            {
                return Utilities.GetFontFromFactory(Utilities.DefaultFontName, Utilities.DefaultFontSizeInPoints(), style);
            }
            return Utilities.GetFontFromFactory(Utilities.AlternateFontName, Utilities.AlternateFontSizeInPoints(), style);
        }

        public static Font GetDefaultFont(float fontSizeInPixels, FontStyle style)
        {
            if (Utilities.IsSegoeUIInstalled())
            {
                return Utilities.GetFontFromFactory(Utilities.DefaultFontName, Utilities.FontSizeInPoints(fontSizeInPixels), style);
            }
            return Utilities.GetFontFromFactory(Utilities.AlternateFontName, Utilities.FontSizeInPoints(fontSizeInPixels), style);
        }

        public static string GetDefaultInvariantUICulture()
        {
            string str = "en-US";
            string lower = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToLower();
            string str1 = lower;
            if (lower != null)
            {
                if (str1 == "fr")
                {
                    str = "fr-FR";
                }
                else if (str1 == "es")
                {
                    str = "es";
                }
            }
            return str;
        }

        public static string GetDefaultLayersFileName()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Calques.txt";
                    return Path.Combine(Utilities.GetDefaultLayersFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "Layers.txt";
                        return Path.Combine(Utilities.GetDefaultLayersFolder(), str);
                    }
                    str = "Capas.txt";
                    return Path.Combine(Utilities.GetDefaultLayersFolder(), str);
                }
            }
            str = "Layers.txt";
            return Path.Combine(Utilities.GetDefaultLayersFolder(), str);
        }

        public static string GetDefaultLayersFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Par défaut";
                    return Path.Combine(Utilities.GetLayersFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "Default";
                        return Path.Combine(Utilities.GetLayersFolder(), str);
                    }
                    str = "Por defecto";
                    return Path.Combine(Utilities.GetLayersFolder(), str);
                }
            }
            str = "Default";
            return Path.Combine(Utilities.GetLayersFolder(), str);
        }

        public static string GetDefaultLayoutDefinition()
        {
            return "<value>&lt;dotnetbarlayout version=\"6\" zorder=\"2,3,4,5\"&gt;&lt;docksite size=\"0\" dockingside=\"Top\" originaldocksitesize=\"0\" /&gt;&lt;docksite size=\"0\" dockingside=\"Bottom\" originaldocksitesize=\"0\" /&gt;&lt;docksite size=\"333\" dockingside=\"Left\" originaldocksitesize=\"0\"&gt;&lt;dockcontainer orientation=\"1\" w=\"0\" h=\"0\"&gt;&lt;barcontainer w=\"330\" h=\"189\"&gt;&lt;bar name=\"containerBarNavigation\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"1\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemPreview\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;barcontainer w=\"330\" h=\"351\"&gt;&lt;bar name=\"containerBarProperties\" dockline=\"0\" layout=\"2\" dockoffset=\"193\" state=\"2\" dockside=\"1\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemProperties\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;barcontainer w=\"330\" h=\"111\"&gt;&lt;bar name=\"containerBarLayers\" dockline=\"0\" layout=\"2\" dockoffset=\"527\" state=\"2\" dockside=\"1\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemLayers\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;/dockcontainer&gt;&lt;/docksite&gt;&lt;docksite size=\"333\" dockingside=\"Right\" originaldocksitesize=\"0\"&gt;&lt;dockcontainer orientation=\"1\" w=\"0\" h=\"0\"&gt;&lt;barcontainer w=\"330\" h=\"438\"&gt;&lt;bar name=\"containerBarGroups\" dockline=\"0\" layout=\"2\" dockoffset=\"-1\" state=\"2\" dockside=\"2\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemGroups\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;barcontainer w=\"330\" h=\"216\"&gt;&lt;bar name=\"containerBarRecentPlans\" dockline=\"999\" layout=\"2\" dockoffset=\"427\" state=\"2\" dockside=\"2\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemRecentPlans\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;/dockcontainer&gt;&lt;/docksite&gt;&lt;bars&gt;&lt;bar name=\"containerBarEstimating\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"2\" visible=\"true\" autohide=\"true\" dockwidth=\"548\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemEstimating\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/bars&gt;&lt;/dotnetbarlayout&gt;</value>";
        }

        public static string GetDefaultPDFFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes plans PDF";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My PDF Plans";
                        return Path.Combine(Utilities.GetUserDataFolder(), str);
                    }
                    str = "Mis planos de PDF";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
            }
            str = "My PDF Plans";
            return Path.Combine(Utilities.GetUserDataFolder(), str);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public static string GetDirectoryName(string path)
        {
            string directoryName;
            try
            {
                directoryName = Path.GetDirectoryName(path);
            }
            catch
            {
                directoryName = "";
            }
            return directoryName;
        }

        public static double GetDoubleAttribute(XmlTextReader reader, string name, double defaultValue)
        {
            double num;
            try
            {
                string attribute = reader.GetAttribute(name);
                string str = Utilities.NumberDecimalSeparator();
                attribute = attribute.Replace(",", str).Replace(".", str);
                num = (double)((double)decimal.Parse(attribute));
            }
            catch (Exception exception)
            {
                num = defaultValue;
            }
            return num;
        }

        public static object GetField(string[] fields, int position)
        {
            object value;
            try
            {
                value = fields.GetValue(position);
            }
            catch
            {
                value = "";
            }
            return value;
        }

        public static string[] GetFields(string originalString, char separator)
        {
            char[] chrArray = new char[] { separator };
            return originalString.Split(chrArray, StringSplitOptions.RemoveEmptyEntries).ToArray<string>();
        }

        public static string[] GetFields(string originalString, char separator, StringSplitOptions stringSplitOptions)
        {
            char[] chrArray = new char[] { separator };
            return originalString.Split(chrArray, stringSplitOptions).ToArray<string>();
        }

        public static string[] GetFields(string originalString, char[] separators)
        {
            return originalString.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray<string>();
        }

        public static string GetFileName(string path, bool stripExtension = false)
        {
            string str;
            try
            {
                string fileName = Path.GetFileName(path);
                str = (stripExtension ? Path.GetFileNameWithoutExtension(fileName) : fileName);
            }
            catch
            {
                str = "";
            }
            return str;
        }

        public static Font GetFont(string family, float fontSizeInPixels, FontStyle style)
        {
            return Utilities.GetFontFromFactory(family, Utilities.FontSizeInPoints(fontSizeInPixels), style);
        }

        private static Font GetFontFromFactory(string family, float emSize, FontStyle style)
        {
            Variable variable = Utilities.fonts.Find((Variable x) => x.Name == string.Concat(new string[] { family, ";", emSize.ToString(), ";", style.ToString() }));
            if (variable == null)
            {
                string[] strArrays = new string[] { family, ";", emSize.ToString(), ";", style.ToString() };
                variable = new Variable(string.Concat(strArrays), new Font(family, emSize, style));
                Utilities.fonts.Add(variable);
                object[] objArray = new object[] { "Creating font [", family, "] in size [", emSize.ToString(), "] and style [", style, "]" };
                Console.WriteLine(string.Concat(objArray));
            }
            return (Font)variable.Value;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr GetForegroundWindow();

        public static string GetFoxProUrl()
        {
            return "https://www.microsoft.com/en-us/download/details.aspx?id=14839";
        }

        public static object GetFromClipboard(string formatName)
        {
            object data = null;
            IDataObject dataObject = System.Windows.Forms.Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(formatName))
            {
                data = dataObject.GetData(formatName);
            }
            return data;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref Utilities.IconInfo pIconInfo);

        public static bool GetImageDimension(string fileName, ref int width, ref int height, ref float dpiX, ref float dpiY, ref Exception exception)
        {
            bool flag = false;
            try
            {
                using (Stream stream = File.OpenRead(fileName))
                {
                    using (Image image = Image.FromStream(stream, false, false))
                    {
                        width = image.Width;
                        height = image.Height;
                        dpiX = image.HorizontalResolution;
                        dpiY = image.VerticalResolution;
                        Console.WriteLine(image.Width);
                        Console.WriteLine(image.Height);
                    }
                }
                flag = true;
            }
            catch (Exception exception1)
            {
                exception = exception1;
            }
            return flag;
        }

        public static ImageFormat GetImageFormat(string fileName, ref Exception exception)
        {
            ImageFormat png = ImageFormat.Png;
            exception = null;
            try
            {
                using (Stream stream = File.OpenRead(fileName))
                {
                    using (Image image = Image.FromStream(stream, false, false))
                    {
                        if (ImageFormat.Jpeg.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Jpeg;
                        }
                        else if (ImageFormat.Png.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Png;
                        }
                        else if (ImageFormat.Gif.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Gif;
                        }
                        else if (ImageFormat.Bmp.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Bmp;
                        }
                        else if (ImageFormat.Tiff.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Tiff;
                        }
                        else if (ImageFormat.Emf.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Emf;
                        }
                        else if (ImageFormat.Exif.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Exif;
                        }
                        else if (ImageFormat.Icon.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Icon;
                        }
                        else if (ImageFormat.MemoryBmp.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Bmp;
                        }
                        else if (ImageFormat.Wmf.Equals(image.RawFormat))
                        {
                            png = ImageFormat.Wmf;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                exception = exception1;
            }
            return png;
        }

        public static ImageFormat GetImageFormatFromExtension(string fileName)
        {
            ImageFormat png = ImageFormat.Png;
            if (Path.GetExtension(fileName).ToLower().Equals(".jpg"))
            {
                png = ImageFormat.Jpeg;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".jpeg"))
            {
                png = ImageFormat.Jpeg;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".png"))
            {
                png = ImageFormat.Png;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".gif"))
            {
                png = ImageFormat.Gif;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".bmp"))
            {
                png = ImageFormat.Bmp;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".tif"))
            {
                png = ImageFormat.Tiff;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".tiff"))
            {
                png = ImageFormat.Tiff;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".emf"))
            {
                png = ImageFormat.Emf;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".exif"))
            {
                png = ImageFormat.Exif;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".ico"))
            {
                png = ImageFormat.Icon;
            }
            else if (Path.GetExtension(fileName).ToLower().Equals(".wmf"))
            {
                png = ImageFormat.Wmf;
            }
            return png;
        }

        public static string GetImagesFolder()
        {
            if (Settings.Default.ImagesFolder == "")
            {
                return Utilities.GetPlansFolder();
            }
            if (!Utilities.DirectoryExists(Settings.Default.ImagesFolder))
            {
                return Utilities.GetPlansFolder();
            }
            return Settings.Default.ImagesFolder;
        }

        public static string GetInstallCountersFolder()
        {
            return Path.Combine(Utilities.GetInstallFolder(), "Counters");
        }

        public static string GetInstallExtensionsFolder()
        {
            return Path.Combine(Utilities.GetInstallFolder(), "Extensions");
        }

        public static string GetInstallFolder()
        {
            return Utilities.GetApplicationFolder();
        }

        public static string GetInstallHelpFolder()
        {
            return Path.Combine(Utilities.GetInstallFolder(), "Help");
        }

        public static string GetInstallLanguage()
        {
            string installLanguage = Settings.Default.InstallLanguage;
            string str = installLanguage;
            if (installLanguage == null || !(str == "fr") && !(str == "en") && !(str == "es"))
            {
                Settings.Default.InstallLanguage = "en";
                Settings.Default.Save();
            }
            return Settings.Default.InstallLanguage;
        }

        public static string GetInstallReportsFolder()
        {
            return Path.Combine(Utilities.GetInstallFolder(), "Reports");
        }

        public static string GetInstallTemplatesFolder()
        {
            return Path.Combine(Utilities.GetInstallFolder(), "Templates");
        }

        public static string GetInstallUICulture()
        {
            string str = "en-US";
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "fr-FR";
                }
                else if (str1 == "es")
                {
                    str = "es";
                }
            }
            return str;
        }

        public static int GetIntegerAttribute(XmlTextReader reader, string name, int defaultValue)
        {
            int num;
            try
            {
                num = Convert.ToInt32(reader.GetAttribute(name));
            }
            catch (Exception exception)
            {
                num = defaultValue;
            }
            return num;
        }

        public static object GetItemData(object item)
        {
            object data;
            try
            {
                data = ((Utilities.ItemData)item).Data;
            }
            catch
            {
                data = null;
            }
            return data;
        }

        public static string GetLayersFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes calques";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Layers";
                        return Path.Combine(Utilities.GetUserDataFolder(), str);
                    }
                    str = "Mis capas";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
            }
            str = "My Layers";
            return Path.Combine(Utilities.GetUserDataFolder(), str);
        }

        public static string GetLocalDBFolder()
        {
            return Utilities.GetDBFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName));
        }

        public static IntPtr GetNextWindow()
        {
            IntPtr window = Utilities.GetWindow(Process.GetCurrentProcess().MainWindowHandle, 2);
            while (true)
            {
                IntPtr parent = Utilities.GetParent(window);
                if (parent.Equals(IntPtr.Zero))
                {
                    break;
                }
                window = parent;
            }
            return window;
        }

        public static int GetOSArchitecture()
        {
            string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            if (!string.IsNullOrEmpty(environmentVariable) && string.Compare(environmentVariable, 0, "x86", 0, 3, true) != 0)
            {
                return 64;
            }
            return 32;
        }

        public static string GetOSInfo(bool versionOnly)
        {
            OperatingSystem oSVersion = Environment.OSVersion;
            Version version = oSVersion.Version;
            string str = "";
            if (oSVersion.Platform == PlatformID.Win32Windows)
            {
                int minor = version.Minor;
                if (minor == 0)
                {
                    str = "95";
                }
                else if (minor == 10)
                {
                    str = (version.Revision.ToString() == "2222A" ? "98SE" : "98");
                }
                else if (minor == 90)
                {
                    str = "Me";
                }
            }
            else if (oSVersion.Platform == PlatformID.Win32NT)
            {
                switch (version.Major)
                {
                    case 3:
                        {
                            str = "NT 3.51";
                            break;
                        }
                    case 4:
                        {
                            str = "NT 4.0";
                            break;
                        }
                    case 5:
                        {
                            str = (version.Minor == 0 ? "2000" : "XP");
                            break;
                        }
                    case 6:
                        {
                            if (version.Minor == 0)
                            {
                                str = "Vista";
                                break;
                            }
                            else if (version.Minor != 1)
                            {
                                str = "8";
                                break;
                            }
                            else
                            {
                                str = "7";
                                break;
                            }
                        }
                }
            }
            if (str != "" && !versionOnly)
            {
                str = string.Concat("Windows ", str);
                if (oSVersion.ServicePack != "")
                {
                    str = string.Concat(str, " ", oSVersion.ServicePack);
                }
                int oSArchitecture = Utilities.GetOSArchitecture();
                str = string.Concat(str, " ", oSArchitecture.ToString(), "-bit");
            }
            return str;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public static string GetParentDirectory(string folderName)
        {
            string fullName;
            try
            {
                fullName = (new DirectoryInfo(folderName)).Parent.FullName;
            }
            catch
            {
                fullName = folderName;
            }
            return fullName;
        }

        public static string GetPDFExportFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Fichiers exportés";
                    return Path.Combine(Utilities.GetDefaultPDFFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "Export Files";
                        return Path.Combine(Utilities.GetDefaultPDFFolder(), str);
                    }
                    str = "Archivos exportados";
                    return Path.Combine(Utilities.GetDefaultPDFFolder(), str);
                }
            }
            str = "Export Files";
            return Path.Combine(Utilities.GetDefaultPDFFolder(), str);
        }

        public static string GetPDFFolder()
        {
            if (Settings.Default.PDFFolder == "")
            {
                return Utilities.GetDefaultPDFFolder();
            }
            if (!Utilities.DirectoryExists(Settings.Default.PDFFolder))
            {
                return Utilities.GetDefaultPDFFolder();
            }
            return Settings.Default.PDFFolder;
        }

        public static string GetPlansFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes plans";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Plans";
                        return Path.Combine(Utilities.GetUserDataFolder(), str);
                    }
                    str = "Mis planos";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
            }
            str = "My Plans";
            return Path.Combine(Utilities.GetUserDataFolder(), str);
        }

        public static string GetProjectPlansFolder(string projectFolder)
        {
            if (projectFolder.ToLower() == Utilities.GetProjectsFolder().ToLower())
            {
                return Utilities.GetPlansFolder();
            }
            return string.Concat(projectFolder, "\\..\\", Utilities.GetShortPlansFolder());
        }

        public static string GetProjectsFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes projets";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Projects";
                        return Path.Combine(Utilities.GetUserDataFolder(), str);
                    }
                    str = "Mis proyectos";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
            }
            str = "My Projects";
            return Path.Combine(Utilities.GetUserDataFolder(), str);
        }

        public static string GetReportsFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes rapports";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Reports";
                        return Path.Combine(Utilities.GetUserDataFolder(), str);
                    }
                    str = "Mis informes";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
            }
            str = "My Reports";
            return Path.Combine(Utilities.GetUserDataFolder(), str);
        }

        public static int GetScreenDpi()
        {
            if (Utilities.ScreenDpi == 0)
            {
                Graphics graphic = Graphics.FromHwnd(IntPtr.Zero);
                Utilities.ScreenDpi = Utilities.GetDeviceCaps(graphic.GetHdc(), 88);
            }
            return Utilities.ScreenDpi;
        }

        public static string GetShortFileName(string long_name)
        {
            char[] chrArray = new char[0x400];
            long shortPathName = (long)Utilities.GetShortPathName(long_name, chrArray, (int)chrArray.Length);
            return (new string(chrArray)).Substring(0, (int)shortPathName);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        private static extern uint GetShortPathName(string lpszLongPath, char[] lpszShortPath, int cchBuffer);

        public static string GetShortPlansFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes plans";
                    return str;
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Plans";
                        return str;
                    }
                    str = "Mis planos";
                    return str;
                }
            }
            str = "My Plans";
            return str;
        }

        public static string GetStringAttribute(XmlTextReader reader, string name, string defaultValue)
        {
            string str;
            try
            {
                str = reader.GetAttribute(name).ToString();
            }
            catch (Exception exception)
            {
                str = defaultValue;
            }
            return str;
        }

        public static string GetTemplatesFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Mes modèles";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "My Templates";
                        return Path.Combine(Utilities.GetUserDataFolder(), str);
                    }
                    str = "Mis plantillas";
                    return Path.Combine(Utilities.GetUserDataFolder(), str);
                }
            }
            str = "My Templates";
            return Path.Combine(Utilities.GetUserDataFolder(), str);
        }

        public static string GetThumbnailsFolder()
        {
            string str;
            string installLanguage = Utilities.GetInstallLanguage();
            string str1 = installLanguage;
            if (installLanguage != null)
            {
                if (str1 == "fr")
                {
                    str = "Miniatures";
                    return Path.Combine(Utilities.GetApplicationDataFolder(), str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "Thumbnails";
                        return Path.Combine(Utilities.GetApplicationDataFolder(), str);
                    }
                    str = "Miniaturas";
                    return Path.Combine(Utilities.GetApplicationDataFolder(), str);
                }
            }
            str = "Thumbnails";
            return Path.Combine(Utilities.GetApplicationDataFolder(), str);
        }

        public static ulong GetTotalMemoryInBytes()
        {
            return (new ComputerInfo()).TotalPhysicalMemory;
        }

        public static string GetUniqueFileName(string extension)
        {
            return string.Format("{0}.{1}", Guid.NewGuid(), extension);
        }

        public static string GetUserDataFolder()
        {
            if (Settings.Default.DataFolder == "")
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName);
            }
            return Path.Combine(Settings.Default.DataFolder, Utilities.ApplicationName);
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        public static string[] GetWords(string originalString)
        {
            string[] strArrays = new string[] { " ", "\t", "\n" };
            return originalString.Split(strArrays, StringSplitOptions.RemoveEmptyEntries).ToArray<string>();
        }

        public static string GetYoutubeUrl()
        {
            string str;
            string currentValidUICultureShort = Utilities.GetCurrentValidUICultureShort();
            string str1 = currentValidUICultureShort;
            if (currentValidUICultureShort != null)
            {
                if (str1 == "fr")
                {
                    str = "fr/youtube";
                    return string.Concat("www.quoterplan.com/", str);
                }
                else
                {
                    if (str1 != "es")
                    {
                        str = "en/youtube";
                        return string.Concat("www.quoterplan.com/", str);
                    }
                    str = "es/youtube";
                    return string.Concat("www.quoterplan.com/", str);
                }
            }
            str = "en/youtube";
            return string.Concat("www.quoterplan.com/", str);
        }

        public static Color IdealTextColor(Color bg, int nThreshold = 105)
        {
            return (0xff - Convert.ToInt32((double)bg.R * 0.299 + (double)bg.G * 0.587 + (double)bg.B * 0.114) < nThreshold ? Color.Black : Color.White);
        }

        public static string InvalidCharacters()
        {
            return "\\ : * ? < > | ;";
        }

        public static bool IsDouble(string value)
        {
            double num = Utilities.ConvertToDouble(value, -1);
            return num.ToString() == value;
        }

        public static bool IsFontInstalled(string fontName)
        {
            bool flag;
            using (Font font = new Font(fontName, 8f))
            {
                flag = 0 == string.Compare(fontName, font.Name, StringComparison.InvariantCultureIgnoreCase);
            }
            return flag;
        }

        public static bool IsInteger(string value)
        {
            return Utilities.ConvertToInt(value).ToString() == value;
        }

        public static bool IsNumber(string value)
        {
            if (Utilities.IsInteger(value))
            {
                return true;
            }
            return Utilities.IsDouble(value);
        }

        public static bool IsSegoeUIInstalled()
        {
            if (SystemFonts.MessageBoxFont.Name == "Segoe UI")
            {
                return true;
            }
            return Utilities.IsFontInstalled("Segoe UI");
        }

        public static Cursor LoadCursor(string fileName, Cursor defaultCursor)
        {
            Cursor cursor;
            try
            {
                cursor = new Cursor(Utilities.LoadCursorFromFile(fileName));
            }
            catch
            {
                cursor = defaultCursor;
            }
            return cursor;
        }

        public static Cursor LoadCursor(Bitmap bmp, int xHotSpot, int yHotSpot, ref IntPtr cursorHandlePtr)
        {
            Cursor cursor;
            try
            {
                if (cursorHandlePtr != IntPtr.Zero)
                {
                    Utilities.DestroyIcon(cursorHandlePtr);
                }
                cursorHandlePtr = bmp.GetHicon();
                Utilities.IconInfo iconInfo = new Utilities.IconInfo();
                Utilities.GetIconInfo(cursorHandlePtr, ref iconInfo);
                iconInfo.xHotspot = xHotSpot;
                iconInfo.yHotspot = yHotSpot;
                iconInfo.fIcon = false;
                Utilities.DestroyIcon(cursorHandlePtr);
                cursorHandlePtr = Utilities.CreateIconIndirect(ref iconInfo);
                cursor = new Cursor(cursorHandlePtr);
            }
            catch
            {
                cursor = null;
            }
            return cursor;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern IntPtr LoadCursorFromFile(string lpFileName);

        public static IntPtr MainWindowHandle()
        {
            return Process.GetCurrentProcess().MainWindowHandle;
        }

        public static string MakeRelativePath(string root, string fileName)
        {
            Uri uri = (new Uri(root)).MakeRelativeUri(new Uri(fileName));
            string str = Uri.UnescapeDataString(uri.ToString()).Replace('/', Path.DirectorySeparatorChar);
            if (str.StartsWith("file:"))
            {
                str = str.Remove(0, "file:".Length);
            }
            if (str.IndexOf(":\\") != -1)
            {
                str = str.Remove(0, str.IndexOf(":\\") - 1);
            }
            return str;
        }

        public static FileInfo MakeUniqueFileName(string path)
        {
            FileInfo fileInfo;
            try
            {
                string directoryName = Path.GetDirectoryName(path);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                int num = 1;
                while (File.Exists(path))
                {
                    object[] objArray = new object[] { fileNameWithoutExtension, " (", num, ")", extension };
                    path = Path.Combine(directoryName, string.Concat(objArray));
                    num++;
                }
                fileInfo = new FileInfo(path);
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                fileInfo = null;
            }
            return fileInfo;
        }

        public static string MakeUniqueFolderName(string folder)
        {
            string str;
            try
            {
                string str1 = folder;
                int num = 1;
                while (Utilities.DirectoryExists(folder))
                {
                    object[] objArray = new object[] { str1, " (", num, ")" };
                    folder = string.Concat(objArray);
                    num++;
                }
                str = folder;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                str = null;
            }
            return str;
        }

        public static int MeasureTextHeight(string sourceString, Font font)
        {
            int height;
            try
            {
                Size size = TextRenderer.MeasureText(string.Copy(sourceString), font);
                height = size.Height;
            }
            catch
            {
                height = 0;
            }
            return height;
        }

        public static int MeasureTextWidth(string sourceString, Font font)
        {
            int width;
            try
            {
                Size size = TextRenderer.MeasureText(string.Copy(sourceString), font);
                width = size.Width;
            }
            catch
            {
                width = 0;
            }
            return width;
        }

        public static double NormalizeDecimal(double number, string separator)
        {
            string str = number.ToString();
            str = str.Replace(",", separator).Replace(".", separator);
            return Utilities.ConvertToDouble(str, -1);
        }

        public static string NumberDecimalSeparator()
        {
            return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        public static bool OpenDocument(string fileName)
        {
            bool flag;
            try
            {
                Process.Start(fileName);
                flag = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                string str = string.Concat(Resources.Impossible_d_ouvrir, "\n", fileName);
                Utilities.DisplayError(str, exception.Message);
                flag = false;
            }
            return flag;
        }

        public static string OpenFileDialog(string title, string initialFolder, string filter)
        {
            string empty = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                AutoUpgradeEnabled = false,
                Title = title,
                InitialDirectory = initialFolder,
                Filter = filter,
                Multiselect = false,
                SupportMultiDottedExtensions = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                empty = openFileDialog.FileName;
            }
            return empty;
        }

        public static string[] OpenMultiFilesDialog(string title, string initialFolder, string filter)
        {
            string empty = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                AutoUpgradeEnabled = false,
                Title = title,
                InitialDirectory = initialFolder,
                Filter = filter,
                Multiselect = true,
                SupportMultiDottedExtensions = true
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            return openFileDialog.FileNames;
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

        public static double RadianToDegree(double angle)
        {
            return angle * 57.2957795130823;
        }

        public static string ReadToString(string fileName)
        {
            string str;
            try
            {
                StreamReader streamReader = new StreamReader(fileName);
                string end = streamReader.ReadToEnd();
                streamReader.Close();
                str = end;
            }
            catch (Exception exception)
            {
                Utilities.DisplayFileOpenError(fileName, exception);
                str = "";
            }
            return str;
        }

        public static string ReadToString(string fileName, Encoding encoding)
        {
            string str;
            try
            {
                StreamReader streamReader = new StreamReader(fileName, encoding);
                string end = streamReader.ReadToEnd();
                streamReader.Close();
                str = end;
            }
            catch (Exception exception)
            {
                Utilities.DisplayFileOpenError(fileName, exception);
                str = "";
            }
            return str;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern uint RegisterWindowMessage(string lpString);

        public static string ReplaceWords(string originalString, string words, bool ignoreCase)
        {
            string str;
            try
            {
                List<string> strs = new List<string>();
                bool flag = false;
                string str1 = "";
                for (int i = 0; i < originalString.Length; i++)
                {
                    char chr = originalString[i];
                    bool flag1 = " \t\n".IndexOf(chr) == -1;
                    if (i == 0)
                    {
                        str1 = chr.ToString();
                        flag = flag1;
                    }
                    else if (i == originalString.Length - 1)
                    {
                        str1 = string.Concat(str1, chr.ToString());
                        strs.Add(str1);
                    }
                    else if (flag1 == flag)
                    {
                        str1 = string.Concat(str1, chr);
                    }
                    else
                    {
                        strs.Add(str1);
                        str1 = chr.ToString();
                        flag = flag1;
                    }
                }
                string[] strArrays = new string[] { ";", " ", "\t", "\n" };
                string[] strArrays1 = words.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < (int)strArrays1.Length; j++)
                {
                    string str2 = strArrays1[j];
                    string[] strArrays2 = new string[] { ",", " ", "\t", "\n" };
                    string[] strArrays3 = str2.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
                    string str3 = strArrays3.GetValue(0).ToString();
                    string str4 = strArrays3.GetValue(1).ToString();
                    for (int k = strs.Count - 1; k >= 0; k--)
                    {
                        string item = strs[k];
                        if ((ignoreCase ? str3.ToLower() : str3) == (ignoreCase ? item.ToString().ToLower() : item.ToString()))
                        {
                            strs[k] = str4;
                        }
                    }
                }
                string str5 = "";
                foreach (string str6 in strs)
                {
                    str5 = string.Concat(str5, str6.ToString());
                }
                str = str5;
            }
            catch (Exception exception)
            {
                str = originalString;
            }
            return str;
        }

        public static void ResumeDrawing(Control parent)
        {
            Utilities.SendMessage(parent.Handle, 11, true, 0);
            parent.Refresh();
        }

        public static string SaveFileDialog(string title, string initialFolder, string defaultFilename, string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                AutoUpgradeEnabled = false,
                Title = title,
                InitialDirectory = initialFolder,
                FileName = defaultFilename,
                Filter = filter
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return "";
            }
            return saveFileDialog.FileName;
        }

        public static bool SaveStringToFile(string fileName, string buffer)
        {
            bool flag;
            try
            {
                File.WriteAllText(fileName, buffer);
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplayFileSaveError(fileName, exception);
                flag = false;
            }
            return flag;
        }

        public static bool SaveStringToFile(string fileName, string buffer, Encoding encoding)
        {
            bool flag;
            try
            {
                File.WriteAllText(fileName, buffer, encoding);
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplayFileSaveError(fileName, exception);
                flag = false;
            }
            return flag;
        }

        public static void SelectComboText(ComboBox comboBox)
        {
            try
            {
                comboBox.Select(0, comboBox.Text.Length);
            }
            catch
            {
            }
        }

        public static string SelectFolder(string defaultFolder = "")
        {
            string str;
            try
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    if (defaultFolder != "")
                    {
                        folderBrowserDialog.SelectedPath = defaultFolder;
                    }
                    str = (folderBrowserDialog.ShowDialog() == DialogResult.OK ? folderBrowserDialog.SelectedPath : "");
                }
            }
            catch
            {
                str = "";
            }
            return str;
        }

        public static Utilities.ItemData SelectList(ComboBox.ObjectCollection items, object itemData, bool revertToDefault)
        {
            Utilities.ItemData itemDatum;
            Utilities.ItemData item;
            try
            {
                if (itemData != null)
                {
                    foreach (Utilities.ItemData item1 in items)
                    {
                        if (!item1.Data.Equals(itemData))
                        {
                            continue;
                        }
                        itemDatum = item1;
                        return itemDatum;
                    }
                }
                if (items.Count <= 0 || !revertToDefault)
                {
                    item = null;
                }
                else
                {
                    item = (Utilities.ItemData)items[0];
                }
                itemDatum = item;
            }
            catch
            {
                itemDatum = null;
            }
            return itemDatum;
        }

        public static void SelectText(TextBox textBox)
        {
            try
            {
                textBox.Select(0, textBox.TextLength);
            }
            catch
            {
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
            {
                return;
            }
            PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            property.SetValue(c, true, null);
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        public static IntPtr SetFocusAttached(IntPtr hWnd)
        {
            IntPtr intPtr = new IntPtr();
            Utilities.AttachedThreadInputAction(() => intPtr = Utilities.SetFocus(hWnd));
            return intPtr;
        }

        public static Image SetImageOpacity(Image image, float opacity)
        {
            Image image1;
            try
            {
                Bitmap bitmap = new Bitmap(image.Width, image.Height);
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    ColorMatrix colorMatrix = new ColorMatrix()
                    {
                        Matrix33 = opacity
                    };
                    ImageAttributes imageAttribute = new ImageAttributes();
                    imageAttribute.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    graphic.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttribute);
                }
                image1 = bitmap;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                image1 = null;
            }
            return image1;
        }

        public static void SetObjectFocus(Control control)
        {
            try
            {
                control.Focus();
            }
            catch
            {
            }
        }

        public static string ShortenString(string sourceString, Font font, int width, TextFormatFlags formatFlag)
        {
            string str;
            try
            {
                string str1 = string.Copy(sourceString);
                TextRenderer.MeasureText(str1, font, new Size(width, 0), TextFormatFlags.ModifyString | formatFlag);
                if (str1.IndexOf('\0') != -1)
                {
                    str1 = Utilities.Substring(str1, 0, str1.IndexOf('\0') - 1);
                }
                str = str1;
            }
            catch
            {
                str = sourceString;
            }
            return str;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        public static bool StartProcess(string fileName, ref IntPtr handle, int waitFor = 0x1388)
        {
            bool flag;
            try
            {
                handle = Process.Start(fileName).Handle;
                Thread.Sleep(waitFor);
                flag = true;
            }
            catch (Exception exception)
            {
                Utilities.DisplaySystemError(exception);
                flag = false;
            }
            return flag;
        }

        public static string StripIndexFromString(string buffer)
        {
            string[] fields = Utilities.GetFields(buffer, ' ');
            if (fields.GetUpperBound(0) > 0 && Utilities.IsInteger(fields.GetValue(fields.GetUpperBound(0)).ToString()))
            {
                buffer = "";
                for (int i = 0; i < fields.GetUpperBound(0); i++)
                {
                    buffer = string.Concat(buffer, fields.GetValue(i).ToString(), " ");
                }
            }
            return buffer.Trim();
        }

        public static string StripInvalidCharacters(string sourceString, string extraInvalidCharacters = "")
        {
            string empty = string.Empty;
            string str = Utilities.InvalidCharacters().Replace(" ", "");
            str = string.Concat(str, extraInvalidCharacters);
            for (int i = 0; i < sourceString.Length; i++)
            {
                char chr = sourceString[i];
                if (str.IndexOf(chr) == -1)
                {
                    empty = string.Concat(empty, chr);
                }
            }
            return empty;
        }

        public static string StripNonAlphanumericalCharacter(string sourceString)
        {
            return new string(Array.FindAll<char>(sourceString.ToCharArray(), (char c) => {
                if (char.IsLetterOrDigit(c))
                {
                    return true;
                }
                return c == '\u005F';
            }));
        }

        public static string Substring(string text, int startIndex, int length)
        {
            string str;
            try
            {
                text = text.Trim();
                text = text.Substring(startIndex, length);
                str = text;
            }
            catch
            {
                str = text;
            }
            return str;
        }

        public static void SuspendDrawing(Control parent)
        {
            Utilities.SendMessage(parent.Handle, 11, false, 0);
        }

        public static string TruncatePath(string path, int length)
        {
            string str;
            try
            {
                StringBuilder stringBuilder = new StringBuilder(length + 1);
                Utilities.PathCompactPathEx(stringBuilder, path, length, 0);
                str = stringBuilder.ToString();
            }
            catch
            {
                str = path;
            }
            return str;
        }

        public static bool ValidateDirectory(string directoryName)
        {
            if (Utilities.DirectoryExists(directoryName))
            {
                return true;
            }
            return Utilities.CreateDirectory(directoryName);
        }

        public static bool ValidateVariableName(string name, string extraInvalidCharacters = "")
        {
            if (name == "")
            {
                return false;
            }
            string str = Utilities.InvalidCharacters().Replace(" ", "");
            str = string.Concat(str, extraInvalidCharacters);
            string str1 = name;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str.IndexOf(str1[i]) != -1)
                {
                    return false;
                }
            }
            return true;
        }

        [CompilerGenerated]
        // <>c__DisplayClass1
        private sealed class u003cu003ec__DisplayClass1
        {
            public string family;

            public float emSize;

            public FontStyle style;

            public u003cu003ec__DisplayClass1()
            {
            }

            // <GetFontFromFactory>b__0
            public bool u003cGetFontFromFactoryu003eb__0(Variable x)
            {
                string name = x.Name;
                string[] str = new string[] { this.family, ";", this.emSize.ToString(), ";", this.style.ToString() };
                return name == string.Concat(str);
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClass7
        private sealed class u003cu003ec__DisplayClass7
        {
            public IntPtr hwnd;

            public u003cu003ec__DisplayClass7()
            {
            }

            // <ForceWindowToForeground>b__6
            public void u003cForceWindowToForegroundu003eb__6()
            {
                Utilities.BringWindowToTop(this.hwnd);
                Utilities.ShowWindow(this.hwnd, 5);
            }
        }

        [CompilerGenerated]
        // <>c__DisplayClassa
        private sealed class u003cu003ec__DisplayClassa
        {
            public IntPtr result;

            public IntPtr hWnd;

            public u003cu003ec__DisplayClassa()
            {
            }

            // <SetFocusAttached>b__9
            public void u003cSetFocusAttachedu003eb__9()
            {
                this.result = Utilities.SetFocus(this.hWnd);
            }
        }

        public enum BasicColorEnum
        {
            colorBlack,
            colorDarkRed,
            colorDarkGreen,
            colorDarkYellow,
            colorDarkBlue,
            colorDarkPurple,
            colorDarkCyan,
            colorPaleGray,
            colorMidGray,
            colorRed,
            colorGreen,
            colorYellow,
            colorBlue,
            colorMagenta,
            colorCyan,
            colorWhite,
            basicColorEnumCount
        }

        public enum DeviceCap
        {
            LOGPIXELSX = 88,
            LOGPIXELSY = 90
        }

        public class DisplayName
        {
            private object owner;

            private string caption = string.Empty;

            public string Caption
            {
                get
                {
                    return this.caption;
                }
                set
                {
                    this.caption = value;
                }
            }

            public object Owner
            {
                get
                {
                    return this.owner;
                }
            }

            public DisplayName(object owner, string caption)
            {
                this.owner = owner;
                this.caption = caption;
            }

            public override string ToString()
            {
                return this.caption;
            }
        }

        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST,
            GW_HWNDLAST,
            GW_HWNDNEXT,
            GW_HWNDPREV,
            GW_OWNER,
            GW_CHILD,
            GW_ENABLEDPOPUP
        }

        public struct IconInfo
        {
            public bool fIcon;

            public int xHotspot;

            public int yHotspot;

            public IntPtr hbmMask;

            public IntPtr hbmColor;
        }

        public class ItemData
        {
            private string name;

            private object data;

            public object Data
            {
                get
                {
                    return this.data;
                }
                set
                {
                    this.data = value;
                }
            }

            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }

            public ItemData(string name, object data)
            {
                this.name = name;
                this.data = data;
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        public class PerfomanceInfoData
        {
            public long CommitTotalPages;

            public long CommitLimitPages;

            public long CommitPeakPages;

            public long PhysicalTotalBytes;

            public long PhysicalAvailableBytes;

            public long SystemCacheBytes;

            public long KernelTotalBytes;

            public long KernelPagedBytes;

            public long KernelNonPagedBytes;

            public long PageSizeBytes;

            public int HandlesCount;

            public int ProcessCount;

            public int ThreadCount;

            public PerfomanceInfoData()
            {
            }
        }

        public static class PsApiWrapper
        {
            [DllImport("psapi.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
            private static extern bool GetPerformanceInfo(out Utilities.PsApiWrapper.PsApiPerformanceInformation PerformanceInformation, [In] int Size);

            public static Utilities.PerfomanceInfoData GetPerformanceInfo()
            {
                Utilities.PerfomanceInfoData perfomanceInfoDatum = new Utilities.PerfomanceInfoData();
                Utilities.PsApiWrapper.PsApiPerformanceInformation psApiPerformanceInformation = new Utilities.PsApiWrapper.PsApiPerformanceInformation();
                if (Utilities.PsApiWrapper.GetPerformanceInfo(out psApiPerformanceInformation, Marshal.SizeOf(psApiPerformanceInformation)))
                {
                    perfomanceInfoDatum.CommitTotalPages = psApiPerformanceInformation.CommitTotal.ToInt64();
                    perfomanceInfoDatum.CommitLimitPages = psApiPerformanceInformation.CommitLimit.ToInt64();
                    perfomanceInfoDatum.CommitPeakPages = psApiPerformanceInformation.CommitPeak.ToInt64();
                    long num = psApiPerformanceInformation.PageSize.ToInt64();
                    perfomanceInfoDatum.PhysicalTotalBytes = psApiPerformanceInformation.PhysicalTotal.ToInt64() * num;
                    perfomanceInfoDatum.PhysicalAvailableBytes = psApiPerformanceInformation.PhysicalAvailable.ToInt64() * num;
                    perfomanceInfoDatum.SystemCacheBytes = psApiPerformanceInformation.SystemCache.ToInt64() * num;
                    perfomanceInfoDatum.KernelTotalBytes = psApiPerformanceInformation.KernelTotal.ToInt64() * num;
                    perfomanceInfoDatum.KernelPagedBytes = psApiPerformanceInformation.KernelPaged.ToInt64() * num;
                    perfomanceInfoDatum.KernelNonPagedBytes = psApiPerformanceInformation.KernelNonPaged.ToInt64() * num;
                    perfomanceInfoDatum.PageSizeBytes = num;
                    perfomanceInfoDatum.HandlesCount = psApiPerformanceInformation.HandlesCount;
                    perfomanceInfoDatum.ProcessCount = psApiPerformanceInformation.ProcessCount;
                    perfomanceInfoDatum.ThreadCount = psApiPerformanceInformation.ThreadCount;
                }
                return perfomanceInfoDatum;
            }

            public struct PsApiPerformanceInformation
            {
                public int Size;

                public IntPtr CommitTotal;

                public IntPtr CommitLimit;

                public IntPtr CommitPeak;

                public IntPtr PhysicalTotal;

                public IntPtr PhysicalAvailable;

                public IntPtr SystemCache;

                public IntPtr KernelTotal;

                public IntPtr KernelPaged;

                public IntPtr KernelNonPaged;

                public IntPtr PageSize;

                public int HandlesCount;

                public int ProcessCount;

                public int ThreadCount;
            }
        }
    }
}