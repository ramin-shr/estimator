using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.VisualBasic.Devices;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public static class Utilities
	{
		public static bool IsFontInstalled(string fontName)
		{
			bool result;
			using (Font font = new Font(fontName, 8f))
			{
				result = (0 == string.Compare(fontName, font.Name, StringComparison.InvariantCultureIgnoreCase));
			}
			return result;
		}

		public static bool IsSegoeUIInstalled()
		{
			return SystemFonts.MessageBoxFont.Name == "Segoe UI" || Utilities.IsFontInstalled("Segoe UI");
		}

		public static float DefaultFontSizeInPixels()
		{
			return 12f;
		}

		public static float DefaultFontSizeInPoints()
		{
			return Utilities.DefaultFontSizeInPixels() * 72f / (float)Utilities.GetScreenDpi();
		}

		public static float FontSizeInPoints(float fontSizeInPixels)
		{
			return fontSizeInPixels * 72f / (float)Utilities.GetScreenDpi();
		}

		private static Font GetFontFromFactory(string family, float emSize, FontStyle style)
		{
			Variable variable = Utilities.fonts.Find(delegate(Variable x)
			{
				string name = x.Name;
				string[] array3 = new string[5];
				array3[0] = family;
				array3[1] = ";";
				array3[2] = emSize.ToString();
				array3[3] = ";";
				string[] array4 = array3;
				int num2 = 4;
				int style3 = (int)style;
				array4[num2] = style3.ToString();
				return name == string.Concat(array3);
			});
			if (variable == null)
			{
				string[] array = new string[5];
				array[0] = family;
				array[1] = ";";
				array[2] = emSize.ToString();
				array[3] = ";";
				string[] array2 = array;
				int num = 4;
				int style2 = (int)style;
				array2[num] = style2.ToString();
				variable = new Variable(string.Concat(array), new Font(family, emSize, style));
				Utilities.fonts.Add(variable);
				Console.WriteLine(string.Concat(new object[]
				{
					"Creating font [",
					family,
					"] in size [",
					emSize.ToString(),
					"] and style [",
					style,
					"]"
				}));
			}
			return (Font)variable.Value;
		}

		public static Font GetFont(string family, float fontSizeInPixels, FontStyle style)
		{
			return Utilities.GetFontFromFactory(family, Utilities.FontSizeInPoints(fontSizeInPixels), style);
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

		public static float AlternateFontSizeInPixels()
		{
			return 11f;
		}

		public static float AlternateFontSizeInPoints()
		{
			return Utilities.AlternateFontSizeInPixels() * 72f / (float)Utilities.GetScreenDpi();
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

		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

		public static int GetScreenDpi()
		{
			if (Utilities.ScreenDpi == 0)
			{
				Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
				IntPtr hdc = graphics.GetHdc();
				Utilities.ScreenDpi = Utilities.GetDeviceCaps(hdc, 88);
			}
			return Utilities.ScreenDpi;
		}

		[DllImport("user32.dll")]
		private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

		[DllImport("user32.dll")]
		private static extern IntPtr LoadCursorFromFile(string lpFileName);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetIconInfo(IntPtr hIcon, ref Utilities.IconInfo pIconInfo);

		[DllImport("user32.dll")]
		public static extern IntPtr CreateIconIndirect(ref Utilities.IconInfo icon);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool DestroyIcon(IntPtr hIcon);

		public static void EnableInterface(Form form, bool enable)
		{
			Cursor.Current = (enable ? Cursors.Default : Cursors.WaitCursor);
			Utilities.EnableWindow(form.Handle, enable);
		}

		public static Cursor LoadCursor(string fileName, Cursor defaultCursor)
		{
			Cursor result;
			try
			{
				result = new Cursor(Utilities.LoadCursorFromFile(fileName));
			}
			catch
			{
				result = defaultCursor;
			}
			return result;
		}

		public static Cursor LoadCursor(Bitmap bmp, int xHotSpot, int yHotSpot, ref IntPtr cursorHandlePtr)
		{
			Cursor result;
			try
			{
				if (cursorHandlePtr != IntPtr.Zero)
				{
					Utilities.DestroyIcon(cursorHandlePtr);
				}
				cursorHandlePtr = bmp.GetHicon();
				Utilities.IconInfo iconInfo = default(Utilities.IconInfo);
				Utilities.GetIconInfo(cursorHandlePtr, ref iconInfo);
				iconInfo.xHotspot = xHotSpot;
				iconInfo.yHotspot = yHotSpot;
				iconInfo.fIcon = false;
				Utilities.DestroyIcon(cursorHandlePtr);
				cursorHandlePtr = Utilities.CreateIconIndirect(ref iconInfo);
				result = new Cursor(cursorHandlePtr);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

		public static void SuspendDrawing(Control parent)
		{
			Utilities.SendMessage(parent.Handle, 11, false, 0);
		}

		public static void ResumeDrawing(Control parent)
		{
			Utilities.SendMessage(parent.Handle, 11, true, 0);
			parent.Refresh();
		}

		public static Font CreateFont(string fontFamily, float fontSize, FontStyle fontStyle)
		{
			string key = string.Concat(new string[]
			{
				fontFamily,
				";",
				fontSize.ToString(),
				";",
				fontStyle.ToString()
			});
			if (Utilities.FontResources.ContainsKey(key))
			{
				return (Font)Utilities.FontResources[key];
			}
			Font font = new Font(fontFamily, fontSize, fontStyle);
			Utilities.FontResources.Add(key, font);
			return font;
		}

		public static void DisposeFontResources()
		{
			foreach (object obj in Utilities.FontResources.Values)
			{
				Font font = (Font)obj;
				font.Dispose();
			}
			Utilities.FontResources.Clear();
			Utilities.FontResources = null;
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern uint GetShortPathName(string lpszLongPath, char[] lpszShortPath, int cchBuffer);

		public static string GetShortFileName(string long_name)
		{
			char[] array = new char[1024];
			long num = (long)((ulong)Utilities.GetShortPathName(long_name, array, array.Length));
			string text = new string(array);
			return text.Substring(0, (int)num);
		}

		public static string[] OpenMultiFilesDialog(string title, string initialFolder, string filter)
		{
			string empty = string.Empty;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.AutoUpgradeEnabled = false;
			openFileDialog.Title = title;
			openFileDialog.InitialDirectory = initialFolder;
			openFileDialog.Filter = filter;
			openFileDialog.Multiselect = true;
			openFileDialog.SupportMultiDottedExtensions = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				return openFileDialog.FileNames;
			}
			return null;
		}

		public static string OpenFileDialog(string title, string initialFolder, string filter)
		{
			string result = string.Empty;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.AutoUpgradeEnabled = false;
			openFileDialog.Title = title;
			openFileDialog.InitialDirectory = initialFolder;
			openFileDialog.Filter = filter;
			openFileDialog.Multiselect = false;
			openFileDialog.SupportMultiDottedExtensions = false;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				result = openFileDialog.FileName;
			}
			return result;
		}

		public static string SaveFileDialog(string title, string initialFolder, string defaultFilename, string filter)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.AutoUpgradeEnabled = false;
			saveFileDialog.Title = title;
			saveFileDialog.InitialDirectory = initialFolder;
			saveFileDialog.FileName = defaultFilename;
			saveFileDialog.Filter = filter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				return saveFileDialog.FileName;
			}
			return "";
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

		public static Utilities.ItemData SelectList(ComboBox.ObjectCollection items, object itemData, bool revertToDefault)
		{
			Utilities.ItemData result;
			try
			{
				if (itemData != null)
				{
					foreach (object obj in items)
					{
						Utilities.ItemData itemData2 = (Utilities.ItemData)obj;
						if (itemData2.Data.Equals(itemData))
						{
							return itemData2;
						}
					}
				}
				result = ((items.Count > 0 && revertToDefault) ? ((Utilities.ItemData)items[0]) : null);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static bool CheckAlphaNumericUnderscoreSpaces(ref string str)
		{
			if (str != null)
			{
				str = str.Trim();
				return Regex.Match(str, "[A-Za-z0-9-]+").Success;
			}
			return false;
		}

		public static object GetItemData(object item)
		{
			object result;
			try
			{
				result = ((Utilities.ItemData)item).Data;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Color IdealTextColor(Color bg, int nThreshold = 105)
		{
			int num = Convert.ToInt32((double)bg.R * 0.299 + (double)bg.G * 0.587 + (double)bg.B * 0.114);
			return (255 - num < nThreshold) ? Color.Black : Color.White;
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
				return Color.FromArgb(0, 0, 0);
			case 1:
				return Color.FromArgb(128, 0, 0);
			case 2:
				return Color.FromArgb(0, 128, 0);
			case 3:
				return Color.FromArgb(128, 128, 0);
			case 4:
				return Color.FromArgb(0, 0, 128);
			case 5:
				return Color.FromArgb(128, 0, 128);
			case 6:
				return Color.FromArgb(0, 128, 128);
			case 7:
				return Color.FromArgb(192, 192, 192);
			case 8:
				return Color.FromArgb(128, 128, 128);
			case 9:
				return Color.FromArgb(255, 0, 0);
			case 10:
				return Color.FromArgb(0, 255, 0);
			case 11:
				return Color.FromArgb(255, 255, 0);
			case 12:
				return Color.FromArgb(0, 0, 255);
			case 13:
				return Color.FromArgb(255, 0, 255);
			case 14:
				return Color.FromArgb(0, 255, 255);
			case 15:
				return Color.FromArgb(255, 255, 255);
			default:
				return Color.FromArgb(255, 255, 255);
			}
		}

		private static DialogResult DisplayMessageBoxEx(string title, string message, MessageBoxEx.MessageTypeEnum messageType, DialogResult defautResult, Image customImage = null, string customCaption1 = "", string customCaption2 = "")
		{
			DialogResult result = defautResult;
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
				result = messageBoxEx.DialogResult;
			}
			return result;
		}

		public static void DisplayMessage(string title, string message)
		{
			Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayMessage, DialogResult.OK, null, "", "");
		}

		public static void DisplayMessageCustom(string title, string message, Image customImage, string customCaption)
		{
			Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayMessage, DialogResult.OK, customImage, customCaption, "");
		}

		public static void DisplayWarning(string title, string message)
		{
			Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarning, DialogResult.OK, null, "", "");
		}

		public static void DisplayError(string title, string message)
		{
			Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayError, DialogResult.OK, null, "", "");
		}

		public static void DisplayLogError(string title, string message)
		{
			Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
		}

		public static void DisplayFileOpenError(string fileName, Exception exception)
		{
			string erreur_de_lecture = Resources.Erreur_de_lecture;
			string text = string.Empty;
			try
			{
				text = Resources.Impossible_d_ouvrir_le_fichier + Environment.NewLine;
				text = text + fileName + Environment.NewLine + Environment.NewLine;
				text += exception.Message;
			}
			catch
			{
			}
			Utilities.DisplayMessageBoxEx(erreur_de_lecture, text, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
		}

		public static void DisplayFileSaveError(string fileName, Exception exception)
		{
			string erreur_d_écriture = Resources.Erreur_d_écriture;
			string text = string.Empty;
			try
			{
				text = Resources.Impossible_d_enregistrer_le_fichier + Environment.NewLine;
				text = text + fileName + Environment.NewLine + Environment.NewLine;
				text += exception.Message;
			}
			catch
			{
			}
			Utilities.DisplayMessageBoxEx(erreur_d_écriture, text, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
		}

		public static void DisplaySystemError(Exception exception)
		{
			string une_erreur_système_est_survenue = Resources.Une_erreur_système_est_survenue;
			string message = string.Empty;
			try
			{
				message = exception.Message + Environment.NewLine + exception.StackTrace;
			}
			catch
			{
			}
			Utilities.DisplayMessageBoxEx(une_erreur_système_est_survenue, message, MessageBoxEx.MessageTypeEnum.DisplayLogError, DialogResult.OK, null, "", "");
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

		public static DialogResult DisplayWarningQuestion(string title, string message)
		{
			return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestion, DialogResult.No, null, "", "");
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

		public static DialogResult DisplayWarningQuestionCustom(string title, string message, Image customImage, string customCaption)
		{
			return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionCustom, DialogResult.No, customImage, customCaption, "");
		}

		public static DialogResult DisplayDeleteConfirmation(string title, string message)
		{
			return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayDeleteConfirmation, DialogResult.No, null, "", "");
		}

		public static DialogResult DisplayDeleteConfirmation(string title, string message, string customCaption)
		{
			return Utilities.DisplayMessageBoxEx(title, message, MessageBoxEx.MessageTypeEnum.DisplayDeleteConfirmation, DialogResult.No, null, customCaption, "");
		}

		public static double NormalizeDecimal(double number, string separator)
		{
			string text = number.ToString();
			text = text.Replace(",", separator).Replace(".", separator);
			return Utilities.ConvertToDouble(text, -1);
		}

		public static string NumberDecimalSeparator()
		{
			return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		}

		public static bool IsInteger(string value)
		{
			return Utilities.ConvertToInt(value).ToString() == value;
		}

		public static bool IsDouble(string value)
		{
			return Utilities.ConvertToDouble(value, -1).ToString() == value;
		}

		public static bool IsNumber(string value)
		{
			return Utilities.IsInteger(value) || Utilities.IsDouble(value);
		}

		public static int ConvertToInt(object value)
		{
			int result;
			try
			{
				result = Convert.ToInt32(value);
			}
			catch (Exception)
			{
				result = 0;
			}
			return result;
		}

		public static double ConvertToDouble(object value, int decimals = -1)
		{
			double result;
			try
			{
				string text = value.ToString();
				string newValue = Utilities.NumberDecimalSeparator();
				text = text.Replace(",", newValue).Replace(".", newValue);
				decimal num = decimal.Parse(text);
				if (decimals > -1)
				{
					num = Math.Round(num, decimals);
				}
				result = (double)num;
			}
			catch (Exception)
			{
				result = 0.0;
			}
			return result;
		}

		public static bool ConvertToBoolean(object value, bool defaultValue)
		{
			bool result;
			try
			{
				result = Convert.ToBoolean(value);
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}

		public static string ConvertToString(object value, string defaultValue)
		{
			string result;
			try
			{
				result = value.ToString();
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}

		public static double ConvertCurrency(string value, double defaultValue)
		{
			double result;
			try
			{
				string newValue = Utilities.NumberDecimalSeparator();
				string s = value.Replace(",", newValue).Replace(".", newValue);
				double num = double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowCurrencySymbol);
				result = num;
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}

		public static string Substring(string text, int startIndex, int length)
		{
			string result;
			try
			{
				text = text.Trim();
				text = text.Substring(startIndex, length);
				result = text;
			}
			catch
			{
				result = text;
			}
			return result;
		}

		public static string StripIndexFromString(string buffer)
		{
			string[] fields = Utilities.GetFields(buffer, ' ');
			if (fields.GetUpperBound(0) > 0)
			{
				string value = fields.GetValue(fields.GetUpperBound(0)).ToString();
				if (Utilities.IsInteger(value))
				{
					buffer = "";
					for (int i = 0; i < fields.GetUpperBound(0); i++)
					{
						buffer = buffer + fields.GetValue(i).ToString() + " ";
					}
				}
			}
			return buffer.Trim();
		}

		public static string InvalidCharacters()
		{
			return "\\ : * ? < > | ;";
		}

		public static string StripInvalidCharacters(string sourceString, string extraInvalidCharacters = "")
		{
			string text = string.Empty;
			string text2 = Utilities.InvalidCharacters().Replace(" ", "");
			text2 += extraInvalidCharacters;
			foreach (char c in sourceString)
			{
				if (text2.IndexOf(c) == -1)
				{
					text += c;
				}
			}
			return text;
		}

		public static string StripNonAlphanumericalCharacter(string sourceString)
		{
			char[] array = sourceString.ToCharArray();
			array = Array.FindAll<char>(array, (char c) => char.IsLetterOrDigit(c) || c == '_');
			return new string(array);
		}

		public static bool ValidateVariableName(string name, string extraInvalidCharacters = "")
		{
			if (name == "")
			{
				return false;
			}
			string text = Utilities.InvalidCharacters().Replace(" ", "");
			text += extraInvalidCharacters;
			foreach (char value in name)
			{
				if (text.IndexOf(value) != -1)
				{
					return false;
				}
			}
			return true;
		}

		public static string GetStringAttribute(XmlTextReader reader, string name, string defaultValue)
		{
			string result;
			try
			{
				result = reader.GetAttribute(name).ToString();
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}

		public static bool GetBoolAttribute(XmlTextReader reader, string name, bool defaultValue)
		{
			bool result;
			try
			{
				result = Convert.ToBoolean(reader.GetAttribute(name));
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}

		public static int GetIntegerAttribute(XmlTextReader reader, string name, int defaultValue)
		{
			int result;
			try
			{
				result = Convert.ToInt32(reader.GetAttribute(name));
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}

		public static double GetDoubleAttribute(XmlTextReader reader, string name, double defaultValue)
		{
			double result;
			try
			{
				string text = reader.GetAttribute(name);
				string newValue = Utilities.NumberDecimalSeparator();
				text = text.Replace(",", newValue).Replace(".", newValue);
				decimal value = decimal.Parse(text);
				result = (double)value;
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}

		public static object GetField(string[] fields, int position)
		{
			object result;
			try
			{
				result = fields.GetValue(position);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string[] GetWords(string originalString)
		{
			string[] source = originalString.Split(new string[]
			{
				" ",
				"\t",
				"\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			return source.ToArray<string>();
		}

		public static string[] GetFields(string originalString, char separator)
		{
			string[] source = originalString.Split(new char[]
			{
				separator
			}, StringSplitOptions.RemoveEmptyEntries);
			return source.ToArray<string>();
		}

		public static string[] GetFields(string originalString, char separator, StringSplitOptions stringSplitOptions)
		{
			string[] source = originalString.Split(new char[]
			{
				separator
			}, stringSplitOptions);
			return source.ToArray<string>();
		}

		public static string[] GetFields(string originalString, char[] separators)
		{
			string[] source = originalString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			return source.ToArray<string>();
		}

		public static string ReplaceWords(string originalString, string words, bool ignoreCase)
		{
			string result;
			try
			{
				List<string> list = new List<string>();
				bool flag = false;
				string text = "";
				for (int i = 0; i < originalString.Length; i++)
				{
					char c = originalString[i];
					bool flag2 = " \t\n".IndexOf(c) == -1;
					if (i == 0)
					{
						text = c.ToString();
						flag = flag2;
					}
					else if (i == originalString.Length - 1)
					{
						text += c.ToString();
						list.Add(text);
					}
					else if (flag2 != flag)
					{
						list.Add(text);
						text = c.ToString();
						flag = flag2;
					}
					else
					{
						text += c;
					}
				}
				string[] array = words.Split(new string[]
				{
					";",
					" ",
					"\t",
					"\n"
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text2 in array)
				{
					string[] array3 = text2.Split(new string[]
					{
						",",
						" ",
						"\t",
						"\n"
					}, StringSplitOptions.RemoveEmptyEntries);
					string text3 = array3.GetValue(0).ToString();
					string value = array3.GetValue(1).ToString();
					for (int k = list.Count - 1; k >= 0; k--)
					{
						string text4 = list[k];
						if ((ignoreCase ? text3.ToLower() : text3) == (ignoreCase ? text4.ToString().ToLower() : text4.ToString()))
						{
							list[k] = value;
						}
					}
				}
				string text5 = "";
				foreach (string text6 in list)
				{
					text5 += text6.ToString();
				}
				result = text5;
			}
			catch (Exception)
			{
				result = originalString;
			}
			return result;
		}

		public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
		{
			return (from item in listToClone
			select (T)((object)item.Clone())).ToList<T>();
		}

		public static void SetDoubleBuffered(Control c)
		{
			if (SystemInformation.TerminalServerSession)
			{
				return;
			}
			PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			property.SetValue(c, true, null);
		}

		[DllImport("shlwapi.dll")]
		private static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

		public static string TruncatePath(string path, int length)
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder(length + 1);
				Utilities.PathCompactPathEx(stringBuilder, path, length, 0);
				result = stringBuilder.ToString();
			}
			catch
			{
				result = path;
			}
			return result;
		}

		public static string ShortenString(string sourceString, Font font, int width, TextFormatFlags formatFlag)
		{
			string result;
			try
			{
				string text = string.Copy(sourceString);
				TextRenderer.MeasureText(text, font, new Size(width, 0), TextFormatFlags.ModifyString | formatFlag);
				if (text.IndexOf('\0') != -1)
				{
					text = Utilities.Substring(text, 0, text.IndexOf('\0') - 1);
				}
				result = text;
			}
			catch
			{
				result = sourceString;
			}
			return result;
		}

		public static int MeasureTextWidth(string sourceString, Font font)
		{
			int result;
			try
			{
				string text = string.Copy(sourceString);
				result = TextRenderer.MeasureText(text, font).Width;
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		public static int MeasureTextHeight(string sourceString, Font font)
		{
			int result;
			try
			{
				string text = string.Copy(sourceString);
				result = TextRenderer.MeasureText(text, font).Height;
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		public static string GetUniqueFileName(string extension)
		{
			return string.Format("{0}.{1}", Guid.NewGuid(), extension);
		}

		public static bool FileDelete(string fileName, bool displayError = true)
		{
			bool result;
			try
			{
				File.Delete(fileName);
				Console.WriteLine("FileDelete " + fileName);
				result = true;
			}
			catch (IOException ex)
			{
				if (displayError)
				{
					string title = Resources.Impossible_de_supprimer + "\n" + Utilities.TruncatePath(fileName, 32);
					string message = ex.Message;
					Utilities.DisplayError(title, message);
				}
				result = false;
			}
			return result;
		}

		public static string FormatDate(DateTime vDate)
		{
			string text = vDate.Year.ToString();
			string text2 = vDate.Month.ToString();
			string text3 = vDate.Day.ToString();
			return string.Concat(new string[]
			{
				text,
				"/",
				text2,
				"/",
				text3
			});
		}

		public static string FormatDateLong(DateTime vDate)
		{
			string text = vDate.Year.ToString();
			string text2 = vDate.Month.ToString();
			string text3 = vDate.Day.ToString();
			string text4 = new DateTime(vDate.Year, vDate.Month, vDate.Day, vDate.Hour, vDate.Minute, vDate.Second, vDate.Kind).TimeOfDay.ToString().Replace(':', '.');
			return string.Concat(new string[]
			{
				text,
				"/",
				text2,
				"/",
				text3,
				"/",
				text4
			});
		}

		public static string GetDateString(string sDateString, string languageShort)
		{
			string text = "";
			string[] array;
			if (languageShort != null)
			{
				if (languageShort == "fr")
				{
					array = new string[]
					{
						"Janvier",
						"Février",
						"Mars",
						"Avril",
						"Mai",
						"Juin",
						"Juillet",
						"Août",
						"Septembre",
						"Octobre",
						"Novembre",
						"Décembre"
					};
					goto IL_1AC;
				}
				if (languageShort == "es")
				{
					array = new string[]
					{
						"enero",
						"febrero",
						"marzo",
						"abril",
						"mayo",
						"junio",
						"julio",
						"agosto",
						"septiembre",
						"octubre",
						"noviembre",
						"diciembre"
					};
					goto IL_1AC;
				}
			}
			array = new string[]
			{
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December"
			};
			IL_1AC:
			string[] array2 = sDateString.Split(new string[]
			{
				"/"
			}, StringSplitOptions.RemoveEmptyEntries);
			string result;
			try
			{
				string text2 = array2.GetValue(0).ToString();
				string value = array2.GetValue(1).ToString();
				string value2 = array2.GetValue(2).ToString();
				try
				{
					text = array2.GetValue(3).ToString();
				}
				catch
				{
				}
				if (languageShort != null)
				{
					if (languageShort == "fr")
					{
						return string.Concat(new object[]
						{
							Utilities.ConvertToInt(value2),
							" ",
							array[Utilities.ConvertToInt(value) - 1],
							" ",
							text2,
							(text == "") ? "" : (" " + text)
						});
					}
					if (languageShort == "es")
					{
						return string.Concat(new object[]
						{
							Utilities.ConvertToInt(value2),
							" de ",
							array[Utilities.ConvertToInt(value) - 1],
							" de ",
							text2,
							(text == "") ? "" : (" " + text)
						});
					}
				}
				result = string.Concat(new object[]
				{
					array[Utilities.ConvertToInt(value) - 1],
					" ",
					Utilities.ConvertToInt(value2),
					", ",
					text2,
					(text == "") ? "" : (" " + text)
				});
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static bool FileExists(string fileName)
		{
			bool result;
			try
			{
				result = File.Exists(fileName);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool FileCopy(string sourceFileName, string destinationFileName)
		{
			bool result;
			try
			{
				File.Copy(sourceFileName, destinationFileName, true);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = false;
			}
			return result;
		}

		public static void FilesCopy(string sourceDir, string targetDir, bool subDirectories, bool overwriteFiles = true)
		{
			Utilities.ValidateDirectory(targetDir);
			try
			{
				foreach (string text in Directory.GetFiles(sourceDir))
				{
					string text2 = Path.Combine(targetDir, Path.GetFileName(text));
					if (overwriteFiles || (!overwriteFiles && !Utilities.FileExists(text2)))
					{
						Utilities.FileCopy(text, text2);
					}
				}
				if (subDirectories)
				{
					foreach (string text3 in Directory.GetDirectories(sourceDir))
					{
						Utilities.FilesCopy(text3, Path.Combine(targetDir, Path.GetFileName(text3)), subDirectories, overwriteFiles);
					}
				}
			}
			catch
			{
			}
		}

		public static bool FileMove(string sourceFileName, string destinationFileName)
		{
			bool result;
			try
			{
				File.Move(sourceFileName, destinationFileName);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = false;
			}
			return result;
		}

		public static bool DirectoryExists(string directoryName)
		{
			bool result;
			try
			{
				result = Directory.Exists(directoryName);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool CreateDirectory(string directoryName)
		{
			bool result;
			try
			{
				Directory.CreateDirectory(directoryName);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool ValidateDirectory(string directoryName)
		{
			return Utilities.DirectoryExists(directoryName) || Utilities.CreateDirectory(directoryName);
		}

		public static bool DirectoryDelete(string directoryName)
		{
			bool result;
			try
			{
				Directory.Delete(directoryName);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool DirectoryEmpty(string directoryName)
		{
			bool result;
			try
			{
				result = (Directory.GetFiles(directoryName).Length == 0);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static string GetFileName(string path, bool stripExtension = false)
		{
			string result;
			try
			{
				string fileName = Path.GetFileName(path);
				result = (stripExtension ? Path.GetFileNameWithoutExtension(fileName) : fileName);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string GetDirectoryName(string path)
		{
			string result;
			try
			{
				result = Path.GetDirectoryName(path);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string SelectFolder(string defaultFolder = "")
		{
			string result;
			try
			{
				using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
				{
					if (defaultFolder != "")
					{
						folderBrowserDialog.SelectedPath = defaultFolder;
					}
					result = ((folderBrowserDialog.ShowDialog() == DialogResult.OK) ? folderBrowserDialog.SelectedPath : "");
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static FileInfo MakeUniqueFileName(string path)
		{
			FileInfo result;
			try
			{
				string directoryName = Path.GetDirectoryName(path);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
				string extension = Path.GetExtension(path);
				int num = 1;
				while (File.Exists(path))
				{
					path = Path.Combine(directoryName, string.Concat(new object[]
					{
						fileNameWithoutExtension,
						" (",
						num,
						")",
						extension
					}));
					num++;
				}
				result = new FileInfo(path);
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = null;
			}
			return result;
		}

		public static string MakeUniqueFolderName(string folder)
		{
			string result;
			try
			{
				string text = folder;
				int num = 1;
				while (Utilities.DirectoryExists(folder))
				{
					folder = string.Concat(new object[]
					{
						text,
						" (",
						num,
						")"
					});
					num++;
				}
				result = folder;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = null;
			}
			return result;
		}

		public static bool OpenDocument(string fileName)
		{
			bool result;
			try
			{
				Process.Start(fileName);
				result = true;
			}
			catch (Exception ex)
			{
				string title = Resources.Impossible_d_ouvrir + "\n" + fileName;
				string message = ex.Message;
				Utilities.DisplayError(title, message);
				result = false;
			}
			return result;
		}

		public static string ReadToString(string fileName)
		{
			string result;
			try
			{
				StreamReader streamReader = new StreamReader(fileName);
				string text = streamReader.ReadToEnd();
				streamReader.Close();
				result = text;
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileOpenError(fileName, exception);
				result = "";
			}
			return result;
		}

		public static string ReadToString(string fileName, Encoding encoding)
		{
			string result;
			try
			{
				StreamReader streamReader = new StreamReader(fileName, encoding);
				string text = streamReader.ReadToEnd();
				streamReader.Close();
				result = text;
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileOpenError(fileName, exception);
				result = "";
			}
			return result;
		}

		public static bool SaveStringToFile(string fileName, string buffer)
		{
			bool result;
			try
			{
				File.WriteAllText(fileName, buffer);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileSaveError(fileName, exception);
				result = false;
			}
			return result;
		}

		public static bool SaveStringToFile(string fileName, string buffer, Encoding encoding)
		{
			bool result;
			try
			{
				File.WriteAllText(fileName, buffer, encoding);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplayFileSaveError(fileName, exception);
				result = false;
			}
			return result;
		}

		public static string ColorToHex(Color c)
		{
			return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
		}

		public static double DegreeToRadian(double angle)
		{
			return 3.141592653589793 * angle / 180.0;
		}

		public static double RadianToDegree(double angle)
		{
			return angle * 57.29577951308232;
		}

		public static Image SetImageOpacity(Image image, float opacity)
		{
			Image result;
			try
			{
				Bitmap bitmap = new Bitmap(image.Width, image.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					ColorMatrix colorMatrix = new ColorMatrix();
					colorMatrix.Matrix33 = opacity;
					ImageAttributes imageAttributes = new ImageAttributes();
					imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
					graphics.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
				}
				result = bitmap;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				result = null;
			}
			return result;
		}

		public static string GetCopyright()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			object[] customAttributes = executingAssembly.GetCustomAttributes(false);
			foreach (object obj in customAttributes)
			{
				if (obj.GetType() == typeof(AssemblyCopyrightAttribute))
				{
					AssemblyCopyrightAttribute assemblyCopyrightAttribute = (AssemblyCopyrightAttribute)obj;
					return assemblyCopyrightAttribute.Copyright;
				}
			}
			return string.Empty;
		}

		public static string GetApplicationFolder()
		{
			string result;
			try
			{
				result = Path.GetDirectoryName(Application.ExecutablePath);
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string GetInstallFolder()
		{
			return Utilities.GetApplicationFolder();
		}

		public static string GetUserDataFolder()
		{
			if (Settings.Default.DataFolder == "")
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName);
			}
			return Path.Combine(Settings.Default.DataFolder, Utilities.ApplicationName);
		}

		public static string GetApplicationDataFolder()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Utilities.ApplicationName);
		}

		public static string GetDBFolder(string userDataFolder = "")
		{
			userDataFolder = ((userDataFolder == "") ? Utilities.GetUserDataFolder() : userDataFolder);
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes données";
					goto IL_52;
				}
				if (installLanguage == "es")
				{
					path = "Mis bases de datos";
					goto IL_52;
				}
			}
			path = "My Databases";
			IL_52:
			return Path.Combine(userDataFolder, path);
		}

		public static string GetLocalDBFolder()
		{
			return Utilities.GetDBFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName));
		}

		public static string GetBackupSettingsFileName()
		{
			return Path.Combine(Utilities.GetDBFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName)), "Settings.bak");
		}

		public static string GetBackupDBFileName(string dateString)
		{
			return Path.Combine(Utilities.GetDBFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Utilities.ApplicationName)), "DB (" + dateString + ").dat.bak");
		}

		public static string GetDefaultDBName()
		{
			string installLanguage;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					return "DB.dat";
				}
				if (installLanguage == "es")
				{
					return "DB.dat";
				}
			}
			return "DB.dat";
		}

		public static string GetProjectsFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes projets";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Mis proyectos";
					goto IL_3B;
				}
			}
			path = "My Projects";
			IL_3B:
			return Path.Combine(Utilities.GetUserDataFolder(), path);
		}

		public static string GetPlansFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes plans";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Mis planos";
					goto IL_3B;
				}
			}
			path = "My Plans";
			IL_3B:
			return Path.Combine(Utilities.GetUserDataFolder(), path);
		}

		public static string GetLayersFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes calques";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Mis capas";
					goto IL_3B;
				}
			}
			path = "My Layers";
			IL_3B:
			return Path.Combine(Utilities.GetUserDataFolder(), path);
		}

		public static string GetDefaultLayersFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Par défaut";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Por defecto";
					goto IL_3B;
				}
			}
			path = "Default";
			IL_3B:
			return Path.Combine(Utilities.GetLayersFolder(), path);
		}

		public static string GetDefaultLayersFileName()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Calques.txt";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Capas.txt";
					goto IL_3B;
				}
			}
			path = "Layers.txt";
			IL_3B:
			return Path.Combine(Utilities.GetDefaultLayersFolder(), path);
		}

		public static string GetShortPlansFolder()
		{
			string installLanguage;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					return "Mes plans";
				}
				if (installLanguage == "es")
				{
					return "Mis planos";
				}
			}
			return "My Plans";
		}

		public static string GetProjectPlansFolder(string projectFolder)
		{
			if (projectFolder.ToLower() == Utilities.GetProjectsFolder().ToLower())
			{
				return Utilities.GetPlansFolder();
			}
			return projectFolder + "\\..\\" + Utilities.GetShortPlansFolder();
		}

		public static string GetThumbnailsFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Miniatures";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Miniaturas";
					goto IL_3B;
				}
			}
			path = "Thumbnails";
			IL_3B:
			return Path.Combine(Utilities.GetApplicationDataFolder(), path);
		}

		public static string GetDefaultPDFFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes plans PDF";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Mis planos de PDF";
					goto IL_3B;
				}
			}
			path = "My PDF Plans";
			IL_3B:
			return Path.Combine(Utilities.GetUserDataFolder(), path);
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

		public static string GetPDFExportFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Fichiers exportés";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Archivos exportados";
					goto IL_3B;
				}
			}
			path = "Export Files";
			IL_3B:
			return Path.Combine(Utilities.GetDefaultPDFFolder(), path);
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

		public static string GetTemplatesFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes modèles";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Mis plantillas";
					goto IL_3B;
				}
			}
			path = "My Templates";
			IL_3B:
			return Path.Combine(Utilities.GetUserDataFolder(), path);
		}

		public static string GetReportsFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes rapports";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Mis informes";
					goto IL_3B;
				}
			}
			path = "My Reports";
			IL_3B:
			return Path.Combine(Utilities.GetUserDataFolder(), path);
		}

		public static string GetCountersFolder()
		{
			string installLanguage;
			string path;
			if ((installLanguage = Utilities.GetInstallLanguage()) != null)
			{
				if (installLanguage == "fr")
				{
					path = "Mes compteurs";
					goto IL_3B;
				}
				if (installLanguage == "es")
				{
					path = "Mis contadores";
					goto IL_3B;
				}
			}
			path = "My Counters";
			IL_3B:
			return Path.Combine(Utilities.GetUserDataFolder(), path);
		}

		public static string GetInstallExtensionsFolder()
		{
			return Path.Combine(Utilities.GetInstallFolder(), "Extensions");
		}

		public static string GetInstallReportsFolder()
		{
			return Path.Combine(Utilities.GetInstallFolder(), "Reports");
		}

		public static string GetInstallTemplatesFolder()
		{
			return Path.Combine(Utilities.GetInstallFolder(), "Templates");
		}

		public static string GetInstallCountersFolder()
		{
			return Path.Combine(Utilities.GetInstallFolder(), "Counters");
		}

		public static string GetInstallHelpFolder()
		{
			return Path.Combine(Utilities.GetInstallFolder(), "Help");
		}

		public static string GetBuyUrl()
		{
			if (Utilities.GetCurrentValidUICultureShort() == "es")
			{
				return "http://www.activetakeoff.com/es/store/buy/";
			}
			return "http://www.activetakeoff.com/store/buy/";
		}

		public static string GetYoutubeUrl()
		{
			string currentValidUICultureShort;
			string str;
			if ((currentValidUICultureShort = Utilities.GetCurrentValidUICultureShort()) != null)
			{
				if (currentValidUICultureShort == "fr")
				{
					str = "fr/youtube";
					goto IL_3B;
				}
				if (currentValidUICultureShort == "es")
				{
					str = "es/youtube";
					goto IL_3B;
				}
			}
			str = "en/youtube";
			IL_3B:
			return "www.quoterplan.com/" + str;
		}

		public static string GetFoxProUrl()
		{
			return "https://www.microsoft.com/en-us/download/details.aspx?id=14839";
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
			OperatingSystem osversion = Environment.OSVersion;
			Version version = osversion.Version;
			string text = "";
			if (osversion.Platform == PlatformID.Win32Windows)
			{
				int minor = version.Minor;
				if (minor != 0)
				{
					if (minor != 10)
					{
						if (minor == 90)
						{
							text = "Me";
						}
					}
					else
					{
						text = ((version.Revision.ToString() == "2222A") ? "98SE" : "98");
					}
				}
				else
				{
					text = "95";
				}
			}
			else if (osversion.Platform == PlatformID.Win32NT)
			{
				switch (version.Major)
				{
				case 3:
					text = "NT 3.51";
					break;
				case 4:
					text = "NT 4.0";
					break;
				case 5:
					text = ((version.Minor == 0) ? "2000" : "XP");
					break;
				case 6:
					if (version.Minor == 0)
					{
						text = "Vista";
					}
					else if (version.Minor == 1)
					{
						text = "7";
					}
					else
					{
						text = "8";
					}
					break;
				}
			}
			if (text != "" && !versionOnly)
			{
				text = "Windows " + text;
				if (osversion.ServicePack != "")
				{
					text = text + " " + osversion.ServicePack;
				}
				text = text + " " + Utilities.GetOSArchitecture().ToString() + "-bit";
			}
			return text;
		}

		public static bool GetImageDimension(string fileName, ref int width, ref int height, ref float dpiX, ref float dpiY, ref Exception exception)
		{
			bool result = false;
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
				result = true;
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			return result;
		}

		public static ImageFormat GetImageFormat(string fileName, ref Exception exception)
		{
			ImageFormat result = ImageFormat.Png;
			exception = null;
			try
			{
				using (Stream stream = File.OpenRead(fileName))
				{
					using (Image image = Image.FromStream(stream, false, false))
					{
						if (ImageFormat.Jpeg.Equals(image.RawFormat))
						{
							result = ImageFormat.Jpeg;
						}
						else if (ImageFormat.Png.Equals(image.RawFormat))
						{
							result = ImageFormat.Png;
						}
						else if (ImageFormat.Gif.Equals(image.RawFormat))
						{
							result = ImageFormat.Gif;
						}
						else if (ImageFormat.Bmp.Equals(image.RawFormat))
						{
							result = ImageFormat.Bmp;
						}
						else if (ImageFormat.Tiff.Equals(image.RawFormat))
						{
							result = ImageFormat.Tiff;
						}
						else if (ImageFormat.Emf.Equals(image.RawFormat))
						{
							result = ImageFormat.Emf;
						}
						else if (ImageFormat.Exif.Equals(image.RawFormat))
						{
							result = ImageFormat.Exif;
						}
						else if (ImageFormat.Icon.Equals(image.RawFormat))
						{
							result = ImageFormat.Icon;
						}
						else if (ImageFormat.MemoryBmp.Equals(image.RawFormat))
						{
							result = ImageFormat.Bmp;
						}
						else if (ImageFormat.Wmf.Equals(image.RawFormat))
						{
							result = ImageFormat.Wmf;
						}
					}
				}
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			return result;
		}

		public static ImageFormat GetImageFormatFromExtension(string fileName)
		{
			ImageFormat result = ImageFormat.Png;
			if (Path.GetExtension(fileName).ToLower().Equals(".jpg"))
			{
				result = ImageFormat.Jpeg;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".jpeg"))
			{
				result = ImageFormat.Jpeg;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".png"))
			{
				result = ImageFormat.Png;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".gif"))
			{
				result = ImageFormat.Gif;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".bmp"))
			{
				result = ImageFormat.Bmp;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".tif"))
			{
				result = ImageFormat.Tiff;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".tiff"))
			{
				result = ImageFormat.Tiff;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".emf"))
			{
				result = ImageFormat.Emf;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".exif"))
			{
				result = ImageFormat.Exif;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".ico"))
			{
				result = ImageFormat.Icon;
			}
			else if (Path.GetExtension(fileName).ToLower().Equals(".wmf"))
			{
				result = ImageFormat.Wmf;
			}
			return result;
		}

		public static string GetDefaultInvariantUICulture()
		{
			string result = "en-US";
			string twoLetterISOLanguageName = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
			string a;
			if ((a = twoLetterISOLanguageName.ToLower()) != null)
			{
				if (!(a == "fr"))
				{
					if (a == "es")
					{
						result = "es";
					}
				}
				else
				{
					result = "fr-FR";
				}
			}
			return result;
		}

		public static string GetCurrentValidUICulture()
		{
			string text = Thread.CurrentThread.CurrentUICulture.Name;
			string a;
			if ((a = text) == null || (!(a == "en-US") && !(a == "fr-FR") && !(a == "es")))
			{
				text = Utilities.GetDefaultInvariantUICulture();
			}
			return text;
		}

		public static string GetCurrentValidUICultureShort()
		{
			string currentValidUICulture = Utilities.GetCurrentValidUICulture();
			return currentValidUICulture.Substring(0, 2);
		}

		public static string GetInstallLanguage()
		{
			string installLanguage;
			if ((installLanguage = Settings.Default.InstallLanguage) == null || (!(installLanguage == "fr") && !(installLanguage == "en") && !(installLanguage == "es")))
			{
				Settings.Default.InstallLanguage = "en";
				Settings.Default.Save();
			}
			return Settings.Default.InstallLanguage;
		}

		public static string GetInstallUICulture()
		{
			string result = "en-US";
			string installLanguage = Utilities.GetInstallLanguage();
			string a;
			if ((a = installLanguage) != null)
			{
				if (!(a == "fr"))
				{
					if (a == "es")
					{
						result = "es";
					}
				}
				else
				{
					result = "fr-FR";
				}
			}
			return result;
		}

		public static string EscapeString(string s)
		{
			return SecurityElement.Escape(s);
		}

		public static string MakeRelativePath(string root, string fileName)
		{
			Uri uri = new Uri(root);
			Uri uri2 = new Uri(fileName);
			Uri uri3 = uri.MakeRelativeUri(uri2);
			string text = Uri.UnescapeDataString(uri3.ToString()).Replace('/', Path.DirectorySeparatorChar);
			if (text.StartsWith("file:"))
			{
				text = text.Remove(0, "file:".Length);
			}
			if (text.IndexOf(":\\") != -1)
			{
				text = text.Remove(0, text.IndexOf(":\\") - 1);
			}
			return text;
		}

		public static string GetParentDirectory(string folderName)
		{
			string result;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
				result = directoryInfo.Parent.FullName;
			}
			catch
			{
				result = folderName;
			}
			return result;
		}

		public static string FormatToCurrency(string value)
		{
			string result = 0.ToString("C");
			try
			{
				double num = 0.0;
				if (double.TryParse(value, NumberStyles.Currency, null, out num))
				{
					result = num.ToString("C");
				}
			}
			catch
			{
			}
			return result;
		}

		public static string GetCurrencySymbol()
		{
			return RegionInfo.CurrentRegion.CurrencySymbol;
		}

		public static string GetDefaultLayoutDefinition()
		{
			return "<value>&lt;dotnetbarlayout version=\"6\" zorder=\"2,3,4,5\"&gt;&lt;docksite size=\"0\" dockingside=\"Top\" originaldocksitesize=\"0\" /&gt;&lt;docksite size=\"0\" dockingside=\"Bottom\" originaldocksitesize=\"0\" /&gt;&lt;docksite size=\"333\" dockingside=\"Left\" originaldocksitesize=\"0\"&gt;&lt;dockcontainer orientation=\"1\" w=\"0\" h=\"0\"&gt;&lt;barcontainer w=\"330\" h=\"189\"&gt;&lt;bar name=\"containerBarNavigation\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"1\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemPreview\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;barcontainer w=\"330\" h=\"351\"&gt;&lt;bar name=\"containerBarProperties\" dockline=\"0\" layout=\"2\" dockoffset=\"193\" state=\"2\" dockside=\"1\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemProperties\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;barcontainer w=\"330\" h=\"111\"&gt;&lt;bar name=\"containerBarLayers\" dockline=\"0\" layout=\"2\" dockoffset=\"527\" state=\"2\" dockside=\"1\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemLayers\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;/dockcontainer&gt;&lt;/docksite&gt;&lt;docksite size=\"333\" dockingside=\"Right\" originaldocksitesize=\"0\"&gt;&lt;dockcontainer orientation=\"1\" w=\"0\" h=\"0\"&gt;&lt;barcontainer w=\"330\" h=\"438\"&gt;&lt;bar name=\"containerBarGroups\" dockline=\"0\" layout=\"2\" dockoffset=\"-1\" state=\"2\" dockside=\"2\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemGroups\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;barcontainer w=\"330\" h=\"216\"&gt;&lt;bar name=\"containerBarRecentPlans\" dockline=\"999\" layout=\"2\" dockoffset=\"427\" state=\"2\" dockside=\"2\" visible=\"true\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemRecentPlans\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/barcontainer&gt;&lt;/dockcontainer&gt;&lt;/docksite&gt;&lt;bars&gt;&lt;bar name=\"containerBarEstimating\" dockline=\"0\" layout=\"2\" dockoffset=\"0\" state=\"2\" dockside=\"2\" visible=\"true\" autohide=\"true\" dockwidth=\"548\"&gt;&lt;items&gt;&lt;item name=\"dockContainerItemEstimating\" origBar=\"\" origPos=\"-1\" pos=\"0\" /&gt;&lt;/items&gt;&lt;/bar&gt;&lt;/bars&gt;&lt;/dotnetbarlayout&gt;</value>";
		}

		public static double ComputeAvailableMemoryForImage()
		{
			double num = 40000000.0;
			double num2 = Utilities.ConvertBytesToMegabytes((long)Utilities.GetTotalMemoryInBytes());
			for (int i = 1; i <= 32; i++)
			{
				num = 10000.0 * (4000.0 + (double)i * 250.0);
				if ((double)(512 * i) >= Math.Ceiling(num2))
				{
					break;
				}
			}
			Console.WriteLine("Available megabytes --> " + num2);
			Console.WriteLine("ComputeAvailableMemoryForImage = " + num);
			return num;
		}

		private static double ConvertBytesToMegabytes(long bytes)
		{
			return (double)((float)bytes / 1024f / 1024f);
		}

		private static double ConvertKilobytesToMegabytes(long kilobytes)
		{
			return (double)((float)kilobytes / 1024f);
		}

		public static ulong GetTotalMemoryInBytes()
		{
			return new ComputerInfo().TotalPhysicalMemory;
		}

		public static void AddToStringArray(ref string[] stringArray, string newString)
		{
			Array.Resize<string>(ref stringArray, stringArray.Length + 1);
			stringArray[stringArray.Length - 1] = newString;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetParent(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(HandleRef hWnd);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

		public static IntPtr GetNextWindow()
		{
			IntPtr intPtr = Utilities.GetWindow(Process.GetCurrentProcess().MainWindowHandle, 2U);
			for (;;)
			{
				IntPtr parent = Utilities.GetParent(intPtr);
				if (parent.Equals(IntPtr.Zero))
				{
					break;
				}
				intPtr = parent;
			}
			return intPtr;
		}

		public static void AttachedThreadInputAction(Action action)
		{
			uint windowThreadProcessId = Utilities.GetWindowThreadProcessId(Utilities.GetForegroundWindow(), IntPtr.Zero);
			uint currentThreadId = Utilities.GetCurrentThreadId();
			bool flag = false;
			try
			{
				flag = (windowThreadProcessId == currentThreadId || Utilities.AttachThreadInput(windowThreadProcessId, currentThreadId, true));
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

		public static void ForceWindowToForeground(IntPtr hwnd)
		{
			Utilities.AttachedThreadInputAction(delegate
			{
				Utilities.BringWindowToTop(hwnd);
				Utilities.ShowWindow(hwnd, 5U);
			});
		}

		public static IntPtr SetFocusAttached(IntPtr hWnd)
		{
			IntPtr result = 0;
			Utilities.AttachedThreadInputAction(delegate
			{
				result = Utilities.SetFocus(hWnd);
			});
			return result;
		}

		[DllImport("User32.Dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("User32.Dll")]
		public static extern int GetClassName(int hwnd, StringBuilder lpClassName, int nMaxCount);

		public static bool FindWindow(string className, ref IntPtr handle)
		{
			bool result;
			try
			{
				handle = Utilities.FindWindow(className, null);
				result = (handle.ToInt32() > 0);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool FindWindow(string className, string windowName, ref IntPtr handle)
		{
			bool result;
			try
			{
				handle = Utilities.FindWindow(className, windowName);
				result = (handle.ToInt32() > 0);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool StartProcess(string fileName, ref IntPtr handle, int waitFor = 5000)
		{
			bool result;
			try
			{
				Process process = Process.Start(fileName);
				handle = process.Handle;
				Thread.Sleep(waitFor);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = false;
			}
			return result;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern uint RegisterWindowMessage(string lpString);

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

		public static IntPtr MainWindowHandle()
		{
			return Process.GetCurrentProcess().MainWindowHandle;
		}

		public static void CopyToClipboard(string formatName, bool autoConvert, object data)
		{
			DataFormats.Format format = DataFormats.GetFormat(formatName);
			IDataObject dataObject = new DataObject();
			dataObject.SetData(format.Name, autoConvert, data);
			Clipboard.SetDataObject(dataObject, false);
		}

		public static object GetFromClipboard(string formatName)
		{
			object result = null;
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject.GetDataPresent(formatName))
			{
				result = dataObject.GetData(formatName);
			}
			return result;
		}

		public static bool CopyToClipboard(string text)
		{
			bool result;
			try
			{
				Clipboard.Clear();
				Clipboard.SetText(text);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = false;
			}
			return result;
		}

		public static bool CopyToClipboard(string text, TextDataFormat textDataFormat)
		{
			bool result;
			try
			{
				Clipboard.Clear();
				Clipboard.SetText(text, textDataFormat);
				result = true;
			}
			catch (Exception exception)
			{
				Utilities.DisplaySystemError(exception);
				result = false;
			}
			return result;
		}

		public static string ApplicationName
		{
			get
			{
				return "Active Takeoff";
			}
		}

		public static string CompanyName
		{
			get
			{
				return "Quoter Software Inc.";
			}
		}

		public static string ApplicationBinaryName
		{
			get
			{
				return typeof(Program).Assembly.GetName().Name;
			}
		}

		public static string ApplicationWebsite
		{
			get
			{
				return "www.activetakeoff.com";
			}
		}

		public static string ApplicationHelpFile
		{
			get
			{
				return Utilities.ApplicationBinaryName + ".chm";
			}
		}

		public static string À_propos_de_Quoter_Plan
		{
			get
			{
				return string.Format(Resources.À_propos_de_Quoter_Plan, Utilities.ApplicationName);
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

		public static string À_propos_de_Quoter_Plan1
		{
			get
			{
				return string.Format(Resources.À_propos_de_Quoter_Plan1, Utilities.ApplicationName);
			}
		}

		[CompilerGenerated]
		private static bool <StripNonAlphanumericalCharacter>b__3(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_';
		}

		[CompilerGenerated]
		private static T <Clone>b__5<T>(T item) where T : ICloneable
		{
			return (T)((object)item.Clone());
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Utilities()
		{
		}

		private const int WM_SETREDRAW = 11;

		public const uint SW_SHOW = 5U;

		public static string DefaultFontName = "Segoe UI";

		private static List<Variable> fonts = new List<Variable>();

		public static string AlternateFontName = "MS Sans Serif";

		private static int ScreenDpi = 0;

		public static Hashtable FontResources = new Hashtable();

		[CompilerGenerated]
		private static Predicate<char> CS$<>9__CachedAnonymousMethodDelegate4;

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
			public ItemData(string name, object data)
			{
				this.name = name;
				this.data = data;
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

			public override string ToString()
			{
				return this.name;
			}

			private string name;

			private object data;
		}

		public class DisplayName
		{
			public DisplayName(object owner, string caption)
			{
				this.owner = owner;
				this.caption = caption;
			}

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

			public override string ToString()
			{
				return this.caption;
			}

			private object owner;

			private string caption = string.Empty;
		}

		public class PerfomanceInfoData
		{
			public PerfomanceInfoData()
			{
			}

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
		}

		public static class PsApiWrapper
		{
			[DllImport("psapi.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool GetPerformanceInfo(out Utilities.PsApiWrapper.PsApiPerformanceInformation PerformanceInformation, [In] int Size);

			public static Utilities.PerfomanceInfoData GetPerformanceInfo()
			{
				Utilities.PerfomanceInfoData perfomanceInfoData = new Utilities.PerfomanceInfoData();
				Utilities.PsApiWrapper.PsApiPerformanceInformation psApiPerformanceInformation = default(Utilities.PsApiWrapper.PsApiPerformanceInformation);
				if (Utilities.PsApiWrapper.GetPerformanceInfo(out psApiPerformanceInformation, Marshal.SizeOf(psApiPerformanceInformation)))
				{
					perfomanceInfoData.CommitTotalPages = psApiPerformanceInformation.CommitTotal.ToInt64();
					perfomanceInfoData.CommitLimitPages = psApiPerformanceInformation.CommitLimit.ToInt64();
					perfomanceInfoData.CommitPeakPages = psApiPerformanceInformation.CommitPeak.ToInt64();
					long num = psApiPerformanceInformation.PageSize.ToInt64();
					perfomanceInfoData.PhysicalTotalBytes = psApiPerformanceInformation.PhysicalTotal.ToInt64() * num;
					perfomanceInfoData.PhysicalAvailableBytes = psApiPerformanceInformation.PhysicalAvailable.ToInt64() * num;
					perfomanceInfoData.SystemCacheBytes = psApiPerformanceInformation.SystemCache.ToInt64() * num;
					perfomanceInfoData.KernelTotalBytes = psApiPerformanceInformation.KernelTotal.ToInt64() * num;
					perfomanceInfoData.KernelPagedBytes = psApiPerformanceInformation.KernelPaged.ToInt64() * num;
					perfomanceInfoData.KernelNonPagedBytes = psApiPerformanceInformation.KernelNonPaged.ToInt64() * num;
					perfomanceInfoData.PageSizeBytes = num;
					perfomanceInfoData.HandlesCount = psApiPerformanceInformation.HandlesCount;
					perfomanceInfoData.ProcessCount = psApiPerformanceInformation.ProcessCount;
					perfomanceInfoData.ThreadCount = psApiPerformanceInformation.ThreadCount;
				}
				return perfomanceInfoData;
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

		[CompilerGenerated]
		private sealed class <>c__DisplayClass1
		{
			public <>c__DisplayClass1()
			{
			}

			public bool <GetFontFromFactory>b__0(Variable x)
			{
				string name = x.Name;
				string[] array = new string[5];
				array[0] = this.family;
				array[1] = ";";
				array[2] = this.emSize.ToString();
				array[3] = ";";
				string[] array2 = array;
				int num = 4;
				int num2 = (int)this.style;
				array2[num] = num2.ToString();
				return name == string.Concat(array);
			}

			public string family;

			public float emSize;

			public FontStyle style;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass7
		{
			public <>c__DisplayClass7()
			{
			}

			public void <ForceWindowToForeground>b__6()
			{
				Utilities.BringWindowToTop(this.hwnd);
				Utilities.ShowWindow(this.hwnd, 5U);
			}

			public IntPtr hwnd;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClassa
		{
			public <>c__DisplayClassa()
			{
			}

			public void <SetFocusAttached>b__9()
			{
				this.result = Utilities.SetFocus(this.hWnd);
			}

			public IntPtr result;

			public IntPtr hWnd;
		}
	}
}
