using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar.ColorPickers;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class ColorPicker : UserControl
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ColorPicker));
			this.lblAdvancedColors = new LabelEx();
			this.lblStandardColors = new LabelEx();
			this.colorCombControl = new ColorCombControl();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.lblAdvancedColors, "lblAdvancedColors");
			this.lblAdvancedColors.BackColor = Color.Transparent;
			this.lblAdvancedColors.ForeColor = Color.Black;
			this.lblAdvancedColors.Name = "lblAdvancedColors";
			this.lblAdvancedColors.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.lblStandardColors, "lblStandardColors");
			this.lblStandardColors.BackColor = Color.Transparent;
			this.lblStandardColors.ForeColor = Color.Black;
			this.lblStandardColors.Name = "lblStandardColors";
			this.lblStandardColors.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			componentResourceManager.ApplyResources(this.colorCombControl, "colorCombControl");
			this.colorCombControl.Name = "colorCombControl";
			this.colorCombControl.SelectedColorChanged += this.colorCombControl_SelectedColorChanged;
			this.colorCombControl.MouseOverColorChanged += this.colorCombControl_MouseOverColorChanged;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.WhiteSmoke;
			base.Controls.Add(this.lblAdvancedColors);
			base.Controls.Add(this.lblStandardColors);
			base.Controls.Add(this.colorCombControl);
			base.Name = "ColorPicker";
			base.Load += this.ColorPicker_Load;
			base.MouseClick += this.ColorPicker_MouseClick;
			base.MouseDoubleClick += this.ColorPicker_MouseDoubleClick;
			base.MouseMove += this.ColorPicker_MouseMove;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public event OnColorHoverHandler OnColorHover
		{
			add
			{
				OnColorHoverHandler onColorHoverHandler = this.OnColorHover;
				OnColorHoverHandler onColorHoverHandler2;
				do
				{
					onColorHoverHandler2 = onColorHoverHandler;
					OnColorHoverHandler value2 = (OnColorHoverHandler)Delegate.Combine(onColorHoverHandler2, value);
					onColorHoverHandler = Interlocked.CompareExchange<OnColorHoverHandler>(ref this.OnColorHover, value2, onColorHoverHandler2);
				}
				while (onColorHoverHandler != onColorHoverHandler2);
			}
			remove
			{
				OnColorHoverHandler onColorHoverHandler = this.OnColorHover;
				OnColorHoverHandler onColorHoverHandler2;
				do
				{
					onColorHoverHandler2 = onColorHoverHandler;
					OnColorHoverHandler value2 = (OnColorHoverHandler)Delegate.Remove(onColorHoverHandler2, value);
					onColorHoverHandler = Interlocked.CompareExchange<OnColorHoverHandler>(ref this.OnColorHover, value2, onColorHoverHandler2);
				}
				while (onColorHoverHandler != onColorHoverHandler2);
			}
		}

		public event OnColorSelectedHandler OnColorSelected
		{
			add
			{
				OnColorSelectedHandler onColorSelectedHandler = this.OnColorSelected;
				OnColorSelectedHandler onColorSelectedHandler2;
				do
				{
					onColorSelectedHandler2 = onColorSelectedHandler;
					OnColorSelectedHandler value2 = (OnColorSelectedHandler)Delegate.Combine(onColorSelectedHandler2, value);
					onColorSelectedHandler = Interlocked.CompareExchange<OnColorSelectedHandler>(ref this.OnColorSelected, value2, onColorSelectedHandler2);
				}
				while (onColorSelectedHandler != onColorSelectedHandler2);
			}
			remove
			{
				OnColorSelectedHandler onColorSelectedHandler = this.OnColorSelected;
				OnColorSelectedHandler onColorSelectedHandler2;
				do
				{
					onColorSelectedHandler2 = onColorSelectedHandler;
					OnColorSelectedHandler value2 = (OnColorSelectedHandler)Delegate.Remove(onColorSelectedHandler2, value);
					onColorSelectedHandler = Interlocked.CompareExchange<OnColorSelectedHandler>(ref this.OnColorSelected, value2, onColorSelectedHandler2);
				}
				while (onColorSelectedHandler != onColorSelectedHandler2);
			}
		}

		public Color SelectedColor
		{
			get
			{
				return this.selectedColor;
			}
			set
			{
				this.selectedColor = value;
			}
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.Font = Utilities.GetDefaultFont(11f, FontStyle.Regular);
		}

		public ColorPicker()
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.Paint += this.ColorPicker_Paint;
		}

		public static bool IsStandardColor(Color color)
		{
			for (int i = 0; i < 27; i++)
			{
				Color standardColor = ColorPicker.GetStandardColor((ColorPicker.StandardColorEnum)i);
				if (color.R == standardColor.R && color.G == standardColor.G && color.B == standardColor.B)
				{
					return true;
				}
			}
			return false;
		}

		public static bool ColorNameExists(string colorName)
		{
			return (bool)ColorPicker.ConvertValue(colorName, ColorPicker.ConvertToEnum.TestIfNameExists);
		}

		public static string ColorToString(Color color)
		{
			return (string)ColorPicker.ConvertValue(color, ColorPicker.ConvertToEnum.FromColorToString);
		}

		public static Color StringToColor(string colorString)
		{
			return (Color)ColorPicker.ConvertValue(colorString, ColorPicker.ConvertToEnum.FromStringToColor);
		}

		public static Color GetStandardColor(ColorPicker.StandardColorEnum colorEnum)
		{
			return (Color)ColorPicker.ConvertValue((int)colorEnum, ColorPicker.ConvertToEnum.FromIndexToColor);
		}

		private static object ConvertValue(object value, ColorPicker.ConvertToEnum typeOfConversion)
		{
			Array array = new Color[]
			{
				Color.Red,
				Color.Lime,
				Color.Blue,
				Color.Yellow,
				Color.Magenta,
				Color.Turquoise,
				Color.Orange,
				Color.Peru,
				Color.Silver,
				Color.DarkRed,
				Color.Green,
				Color.DarkBlue,
				Color.Gold,
				Color.Purple,
				Color.Teal,
				Color.OrangeRed,
				Color.SaddleBrown,
				Color.SlateGray,
				Color.HotPink,
				Color.Chartreuse,
				Color.SteelBlue,
				Color.Khaki,
				Color.Violet,
				Color.PaleTurquoise,
				Color.LightSalmon,
				Color.Tan,
				Color.Gainsboro
			};
			Array array2 = new string[]
			{
				Resources.Rouge,
				Resources.Vert,
				Resources.Bleu,
				Resources.Jaune,
				Resources.Magenta,
				Resources.Turquoise,
				Resources.Orange,
				Resources.Brun,
				Resources.Argent,
				Resources.Bourgogne,
				Resources.Vert_foncé,
				Resources.Bleu_foncé,
				Resources.Or,
				Resources.Mauve,
				Resources.Sarcelle,
				Resources.Orange_brûlée,
				Resources.Brun_foncé,
				Resources.Gris,
				Resources.Rose,
				Resources.Vert_pâle,
				Resources.Bleu_pâle,
				Resources.Kaki,
				Resources.Violet,
				Resources.Turquoise_pâle,
				Resources.Saumon,
				Resources.Marron,
				Resources.Gris_pâle
			};
			int num;
			switch (typeOfConversion)
			{
			case ColorPicker.ConvertToEnum.FromColorToString:
			{
				Color color = (Color)value;
				num = Array.IndexOf(array, color);
				if (num != -1)
				{
					return array2.GetValue(num);
				}
				return string.Concat(new object[]
				{
					color.R,
					", ",
					color.G,
					", ",
					color.B
				});
			}
			case ColorPicker.ConvertToEnum.FromStringToColor:
			{
				string value2 = (string)value;
				num = Array.IndexOf(array2, value2);
				if (num != -1)
				{
					return array.GetValue(num);
				}
				return Color.Empty;
			}
			case ColorPicker.ConvertToEnum.FromIndexToColor:
				try
				{
					return array.GetValue((int)value);
				}
				catch
				{
					return Color.Empty;
				}
				break;
			case ColorPicker.ConvertToEnum.TestIfNameExists:
				break;
			default:
				return Color.Empty;
			}
			string value3 = (string)value;
			num = Array.IndexOf(array2, value3);
			return num != -1;
		}

		private int HitTestStandardColors(Point location)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					Rectangle rectangle = this.ColorRectangle(i, j);
					if (rectangle.Contains(location))
					{
						int num = j + 9 * i;
						if (num != this.selectedColorIndex)
						{
							this.RefreshSelectedRectangle();
						}
						this.selectedColorRectangle = new Rectangle(new Point(rectangle.Location.X - 2, rectangle.Location.Y - 2), new Size(rectangle.Width + 2, rectangle.Height + 2));
						return num;
					}
				}
			}
			return -1;
		}

		private Rectangle ColorRectangle(ColorPicker.StandardColorEnum standardColorEnum)
		{
			int num = (int)Math.Floor((double)standardColorEnum / 9.0);
			int column = standardColorEnum - (ColorPicker.StandardColorEnum)(num * 9);
			return this.ColorRectangle(num, column);
		}

		private Rectangle ColorRectangle(int row, int column)
		{
			int num = 21;
			int num2 = 12;
			int num3 = 2;
			Point point = new Point(13, 26);
			Point location = new Point(point.X + (num + num3) * column, point.Y + (num2 + num3) * row);
			return new Rectangle(location, new Size(num, num2));
		}

		private void DrawStandardColors(Graphics g)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					Color standardColor = ColorPicker.GetStandardColor((ColorPicker.StandardColorEnum)(j + 9 * i));
					Rectangle rect = this.ColorRectangle(i, j);
					g.FillRectangle(new SolidBrush(standardColor), rect);
					g.DrawRectangle(new Pen(Color.DarkSlateGray, 1f), new Rectangle(rect.Location, new Size(rect.Width - 1, rect.Height - 1)));
				}
			}
		}

		private void DrawSelectedColor(Graphics g)
		{
			if (this.selectedColorIndex < 0 || this.selectedColorIndex > 26)
			{
				return;
			}
			Rectangle rectangle = this.ColorRectangle((ColorPicker.StandardColorEnum)this.selectedColorIndex);
			Pen pen = new Pen(Color.FromArgb(200, Color.FromKnownColor(KnownColor.Highlight)), 2f);
			g.DrawRectangle(pen, new Rectangle(new Point(rectangle.Location.X, rectangle.Location.Y), new Size(rectangle.Width, rectangle.Height)));
			pen.Dispose();
		}

		private void ColorPicker_Paint(object sender, PaintEventArgs e)
		{
			this.DrawStandardColors(e.Graphics);
			this.DrawSelectedColor(e.Graphics);
		}

		private void ColorHover(Color color)
		{
			this.selectedColor = color;
			if (this.OnColorHover != null)
			{
				this.OnColorHover(color);
			}
		}

		private void ColorChanged(Color color)
		{
			this.selectedColor = color;
			if (this.OnColorSelected != null)
			{
				this.OnColorSelected(color);
			}
		}

		private void colorCombControl_SelectedColorChanged(object sender, EventArgs e)
		{
			this.ColorChanged(this.colorCombControl.SelectedColor);
		}

		private void HitTestForSelection(MouseEventArgs e)
		{
			int num = this.HitTestStandardColors(e.Location);
			if (num == -1)
			{
				return;
			}
			this.selectedColorIndex = num;
			this.ColorChanged(ColorPicker.GetStandardColor((ColorPicker.StandardColorEnum)this.selectedColorIndex));
		}

		private void RefreshSelectedRectangle()
		{
			if (this.selectedColorRectangle == Rectangle.Empty)
			{
				return;
			}
			this.selectedColorRectangle.Inflate(1, 1);
			base.Invalidate(this.selectedColorRectangle);
		}

		private void picColor_MouseEnter()
		{
			if (this.selectedColorIndex < 0 || this.selectedColorIndex > 26)
			{
				return;
			}
			this.ColorHover(ColorPicker.GetStandardColor((ColorPicker.StandardColorEnum)this.selectedColorIndex));
			this.RefreshSelectedRectangle();
		}

		private void picColor_MouseLeave()
		{
			this.ColorHover(Color.Empty);
			this.RefreshSelectedRectangle();
		}

		private void colorCombControl_MouseOverColorChanged(object sender, EventArgs e)
		{
			this.ColorHover(this.colorCombControl.MouseOverColor);
		}

		private void ColorPicker_MouseMove(object sender, MouseEventArgs e)
		{
			int num = this.HitTestStandardColors(e.Location);
			if (num != this.selectedColorIndex)
			{
				this.selectedColorIndex = num;
				if (this.selectedColorIndex == -1)
				{
					this.picColor_MouseLeave();
					return;
				}
				this.picColor_MouseEnter();
			}
		}

		private void ColorPicker_MouseClick(object sender, MouseEventArgs e)
		{
			this.HitTestForSelection(e);
		}

		private void ColorPicker_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.HitTestForSelection(e);
		}

		private void ColorPicker_Load(object sender, EventArgs e)
		{
		}

		private IContainer components;

		private LabelEx lblAdvancedColors;

		private LabelEx lblStandardColors;

		private ColorCombControl colorCombControl;

		private OnColorHoverHandler OnColorHover;

		private OnColorSelectedHandler OnColorSelected;

		private int selectedColorIndex = -1;

		private Color selectedColor;

		private Rectangle selectedColorRectangle;

		public enum ConvertToEnum
		{
			FromColorToString,
			FromStringToColor,
			FromIndexToColor,
			TestIfNameExists
		}

		public enum StandardColorEnum
		{
			ColorRed,
			ColorLime,
			ColorBlue,
			ColorYellow,
			ColorMagenta,
			ColorTurquoise,
			ColorOrange,
			ColorPeru,
			ColorSilver,
			ColorDarkRed,
			ColorGreen,
			ColorDarkBlue,
			ColorGold,
			ColorPurple,
			ColorTeal,
			ColorOrangeRed,
			ColorSaddleBrown,
			ColorSlateGray,
			ColorHotPink,
			ColorChartreuse,
			ColorSteelBlue,
			ColorKhaki,
			ColorViolet,
			ColorPaleTurquoise,
			ColorLightSalmon,
			ColorTan,
			ColorGainsboro,
			StandardColorEnumCount
		}
	}
}
