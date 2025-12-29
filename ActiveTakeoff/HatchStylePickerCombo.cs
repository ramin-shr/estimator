using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class HatchStylePickerCombo : DevComponents.DotNetBar.Controls.ComboBoxEx
	{
		public static string PatternToString(HatchStylePickerCombo.HatchStylePickerEnum pattern)
		{
			switch (pattern)
			{
			case HatchStylePickerCombo.HatchStylePickerEnum.Solid:
				return Resources.Solide;
			case HatchStylePickerCombo.HatchStylePickerEnum.Horizontal:
				return Resources.Horizontal;
			case HatchStylePickerCombo.HatchStylePickerEnum.Vertical:
				return Resources.Vertical;
			case HatchStylePickerCombo.HatchStylePickerEnum.ForwardDiagonal:
				return Resources.ForwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.BackwardDiagonal:
				return Resources.BackwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DiagonalCross:
				return Resources.DiagonalCross;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent05:
				return Resources.Percent05;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent10:
				return Resources.Percent10;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent20:
				return Resources.Percent20;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent25:
				return Resources.Percent25;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent30:
				return Resources.Percent30;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent40:
				return Resources.Percent40;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent50:
				return Resources.Percent50;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent60:
				return Resources.Percent60;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent70:
				return Resources.Percent70;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent75:
				return Resources.Percent75;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent80:
				return Resources.Percent80;
			case HatchStylePickerCombo.HatchStylePickerEnum.Percent90:
				return Resources.Percent90;
			case HatchStylePickerCombo.HatchStylePickerEnum.LightDownwardDiagonal:
				return Resources.LightDownwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.LightUpwardDiagonal:
				return Resources.LightUpwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DarkDownwardDiagonal:
				return Resources.DarkDownwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DarkUpwardDiagonal:
				return Resources.DarkUpwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.WideDownwardDiagonal:
				return Resources.WideDownwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.WideUpwardDiagonal:
				return Resources.WideUpwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.LightVertical:
				return Resources.LightVertical;
			case HatchStylePickerCombo.HatchStylePickerEnum.LightHorizontal:
				return Resources.LightHorizontal;
			case HatchStylePickerCombo.HatchStylePickerEnum.NarrowVertical:
				return Resources.NarrowVertical;
			case HatchStylePickerCombo.HatchStylePickerEnum.NarrowHorizontal:
				return Resources.NarrowHorizontal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DarkVertical:
				return Resources.DarkVertical;
			case HatchStylePickerCombo.HatchStylePickerEnum.DarkHorizontal:
				return Resources.DarkHorizontal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DashedDownwardDiagonal:
				return Resources.DashedDownwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DashedUpwardDiagonal:
				return Resources.DashedUpwardDiagonal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DashedHorizontal:
				return Resources.DashedHorizontal;
			case HatchStylePickerCombo.HatchStylePickerEnum.DashedVertical:
				return Resources.DashedVertical;
			case HatchStylePickerCombo.HatchStylePickerEnum.SmallConfetti:
				return Resources.SmallConfetti;
			case HatchStylePickerCombo.HatchStylePickerEnum.LargeConfetti:
				return Resources.LargeConfetti;
			case HatchStylePickerCombo.HatchStylePickerEnum.ZigZag:
				return Resources.ZigZag;
			case HatchStylePickerCombo.HatchStylePickerEnum.Wave:
				return Resources.Wave;
			case HatchStylePickerCombo.HatchStylePickerEnum.DiagonalBrick:
				return Resources.DiagonalBrick;
			case HatchStylePickerCombo.HatchStylePickerEnum.HorizontalBrick:
				return Resources.HorizontalBrick;
			case HatchStylePickerCombo.HatchStylePickerEnum.Weave:
				return Resources.Weave;
			case HatchStylePickerCombo.HatchStylePickerEnum.Plaid:
				return Resources.Plaid;
			case HatchStylePickerCombo.HatchStylePickerEnum.Divot:
				return Resources.Divot;
			case HatchStylePickerCombo.HatchStylePickerEnum.DottedGrid:
				return Resources.DottedGrid;
			case HatchStylePickerCombo.HatchStylePickerEnum.DottedDiamond:
				return Resources.DottedDiamond;
			case HatchStylePickerCombo.HatchStylePickerEnum.Shingle:
				return Resources.Shingle;
			case HatchStylePickerCombo.HatchStylePickerEnum.Trellis:
				return Resources.Trellis;
			case HatchStylePickerCombo.HatchStylePickerEnum.Sphere:
				return Resources.Sphere;
			case HatchStylePickerCombo.HatchStylePickerEnum.SmallGrid:
				return Resources.SmallGrid;
			case HatchStylePickerCombo.HatchStylePickerEnum.SmallCheckerBoard:
				return Resources.SmallCheckerBoard;
			case HatchStylePickerCombo.HatchStylePickerEnum.LargeCheckerBoard:
				return Resources.LargeCheckerBoard;
			case HatchStylePickerCombo.HatchStylePickerEnum.OutlinedDiamond:
				return Resources.OutlinedDiamond;
			case HatchStylePickerCombo.HatchStylePickerEnum.SolidDiamond:
				return Resources.SolidDiamond;
			}
			return string.Empty;
		}

		public static HatchStylePickerCombo.HatchStylePickerEnum StringToPattern(string pattern)
		{
			if (pattern == Resources.Solide)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Solid;
			}
			if (pattern == Resources.Horizontal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Horizontal;
			}
			if (pattern == Resources.Vertical)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Vertical;
			}
			if (pattern == Resources.ForwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.ForwardDiagonal;
			}
			if (pattern == Resources.BackwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.BackwardDiagonal;
			}
			if (pattern == Resources.DiagonalCross)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DiagonalCross;
			}
			if (pattern == Resources.Percent05)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent05;
			}
			if (pattern == Resources.Percent10)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent10;
			}
			if (pattern == Resources.Percent20)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent20;
			}
			if (pattern == Resources.Percent25)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent25;
			}
			if (pattern == Resources.Percent30)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent30;
			}
			if (pattern == Resources.Percent40)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent40;
			}
			if (pattern == Resources.Percent50)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent50;
			}
			if (pattern == Resources.Percent60)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent60;
			}
			if (pattern == Resources.Percent70)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent70;
			}
			if (pattern == Resources.Percent75)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent75;
			}
			if (pattern == Resources.Percent80)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent80;
			}
			if (pattern == Resources.Percent90)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Percent90;
			}
			if (pattern == Resources.LightDownwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.LightDownwardDiagonal;
			}
			if (pattern == Resources.LightUpwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.LightUpwardDiagonal;
			}
			if (pattern == Resources.DarkDownwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DarkDownwardDiagonal;
			}
			if (pattern == Resources.DarkUpwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DarkUpwardDiagonal;
			}
			if (pattern == Resources.WideDownwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.WideDownwardDiagonal;
			}
			if (pattern == Resources.WideUpwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.WideUpwardDiagonal;
			}
			if (pattern == Resources.LightVertical)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.LightVertical;
			}
			if (pattern == Resources.LightHorizontal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.LightHorizontal;
			}
			if (pattern == Resources.NarrowVertical)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.NarrowVertical;
			}
			if (pattern == Resources.NarrowHorizontal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.NarrowHorizontal;
			}
			if (pattern == Resources.DarkVertical)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DarkVertical;
			}
			if (pattern == Resources.DarkHorizontal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DarkHorizontal;
			}
			if (pattern == Resources.DashedDownwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DashedDownwardDiagonal;
			}
			if (pattern == Resources.DashedUpwardDiagonal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DashedUpwardDiagonal;
			}
			if (pattern == Resources.DashedHorizontal)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DashedHorizontal;
			}
			if (pattern == Resources.DashedVertical)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DashedVertical;
			}
			if (pattern == Resources.SmallConfetti)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.SmallConfetti;
			}
			if (pattern == Resources.LargeConfetti)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.LargeConfetti;
			}
			if (pattern == Resources.ZigZag)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.ZigZag;
			}
			if (pattern == Resources.Wave)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Wave;
			}
			if (pattern == Resources.DiagonalBrick)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DiagonalBrick;
			}
			if (pattern == Resources.HorizontalBrick)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.HorizontalBrick;
			}
			if (pattern == Resources.Weave)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Weave;
			}
			if (pattern == Resources.Plaid)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Plaid;
			}
			if (pattern == Resources.Divot)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Divot;
			}
			if (pattern == Resources.DottedGrid)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DottedGrid;
			}
			if (pattern == Resources.DottedDiamond)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.DottedDiamond;
			}
			if (pattern == Resources.Shingle)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Shingle;
			}
			if (pattern == Resources.Trellis)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Trellis;
			}
			if (pattern == Resources.Sphere)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.Sphere;
			}
			if (pattern == Resources.SmallGrid)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.SmallGrid;
			}
			if (pattern == Resources.SmallCheckerBoard)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.SmallCheckerBoard;
			}
			if (pattern == Resources.LargeCheckerBoard)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.LargeCheckerBoard;
			}
			if (pattern == Resources.OutlinedDiamond)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.OutlinedDiamond;
			}
			if (pattern == Resources.SolidDiamond)
			{
				return HatchStylePickerCombo.HatchStylePickerEnum.SolidDiamond;
			}
			return HatchStylePickerCombo.HatchStylePickerEnum.Solid;
		}

		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.Font = Utilities.GetDefaultFont();
		}

		public HatchStylePickerCombo()
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.AddStandardHashStyles();
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			base.DrawMode = DrawMode.OwnerDrawFixed;
			base.DropDownHeight = 240;
			base.DisableInternalDrawing = true;
			base.DrawItem += this.OnDrawItem;
		}

		private void AddStandardHashStyles()
		{
			base.Items.Clear();
			base.Items.Add(new HatchStylePickerCombo.HatchStyleInfo(Resources.Solide, HatchStylePickerCombo.HatchStylePickerEnum.Solid));
			foreach (string text in Enum.GetNames(typeof(HatchStylePickerCombo.HatchStylePickerEnum)))
			{
				if (text != "Solid")
				{
					HatchStylePickerCombo.HatchStylePickerEnum hatchStyle = (HatchStylePickerCombo.HatchStylePickerEnum)Enum.Parse(typeof(HatchStylePickerCombo.HatchStylePickerEnum), text);
					base.Items.Add(new HatchStylePickerCombo.HatchStyleInfo(text, hatchStyle));
				}
			}
			this.SelectedValue = HatchStylePickerCombo.HatchStylePickerEnum.Solid;
		}

		protected void OnDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index >= 0)
			{
				HatchStylePickerCombo.HatchStyleInfo hatchStyleInfo = (HatchStylePickerCombo.HatchStyleInfo)base.Items[e.Index];
				e.DrawBackground();
				Rectangle rect = default(Rectangle);
				rect.X = e.Bounds.X + 2;
				rect.Y = e.Bounds.Y + 2;
				rect.Height = e.Bounds.Height - 5;
				if (hatchStyleInfo.HatchStyle == HatchStylePickerCombo.HatchStylePickerEnum.Solid)
				{
					rect.Width = 0;
				}
				else
				{
					rect.Width = e.Bounds.Width - 5;
					using (HatchBrush hatchBrush = new HatchBrush((HatchStyle)hatchStyleInfo.HatchStyle, Color.Black, Color.White))
					{
						e.Graphics.FillRectangle(hatchBrush, rect);
						e.Graphics.DrawRectangle(SystemPens.WindowText, rect);
					}
				}
				Brush brush = ((e.State & DrawItemState.Selected) != DrawItemState.None) ? SystemBrushes.HighlightText : SystemBrushes.WindowText;
				if (hatchStyleInfo.HatchStyle == HatchStylePickerCombo.HatchStylePickerEnum.Solid)
				{
					e.Graphics.DrawString(hatchStyleInfo.Text, this.Font, brush, 2f, (float)(e.Bounds.Y + (e.Bounds.Height - this.Font.Height) / 2));
				}
			}
		}

		public new HatchStylePickerCombo.HatchStyleInfo SelectedItem
		{
			get
			{
				return (HatchStylePickerCombo.HatchStyleInfo)base.SelectedItem;
			}
			set
			{
				base.SelectedItem = value;
			}
		}

		public new string SelectedText
		{
			get
			{
				if (this.SelectedIndex >= 0)
				{
					return this.SelectedItem.Text;
				}
				return string.Empty;
			}
			set
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					if (((HatchStylePickerCombo.HatchStyleInfo)base.Items[i]).Text == value)
					{
						this.SelectedIndex = i;
						return;
					}
				}
			}
		}

		public new HatchStylePickerCombo.HatchStylePickerEnum SelectedValue
		{
			get
			{
				if (this.SelectedIndex >= 0)
				{
					return this.SelectedItem.HatchStyle;
				}
				return HatchStylePickerCombo.HatchStylePickerEnum.Solid;
			}
			set
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					if (((HatchStylePickerCombo.HatchStyleInfo)base.Items[i]).HatchStyle == value)
					{
						this.SelectedIndex = i;
						return;
					}
				}
			}
		}

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
			this.components = new Container();
		}

		private IContainer components;

		public enum HatchStylePickerEnum
		{
			Solid = -1,
			Horizontal,
			Vertical,
			ForwardDiagonal,
			BackwardDiagonal,
			DiagonalCross = 5,
			Percent05,
			Percent10,
			Percent20,
			Percent25,
			Percent30,
			Percent40,
			Percent50,
			Percent60,
			Percent70,
			Percent75,
			Percent80,
			Percent90,
			LightDownwardDiagonal,
			LightUpwardDiagonal,
			DarkDownwardDiagonal,
			DarkUpwardDiagonal,
			WideDownwardDiagonal,
			WideUpwardDiagonal,
			LightVertical,
			LightHorizontal,
			NarrowVertical,
			NarrowHorizontal,
			DarkVertical,
			DarkHorizontal,
			DashedDownwardDiagonal,
			DashedUpwardDiagonal,
			DashedHorizontal,
			DashedVertical,
			SmallConfetti,
			LargeConfetti,
			ZigZag,
			Wave,
			DiagonalBrick,
			HorizontalBrick,
			Weave,
			Plaid,
			Divot,
			DottedGrid,
			DottedDiamond,
			Shingle,
			Trellis,
			Sphere,
			SmallGrid,
			SmallCheckerBoard,
			LargeCheckerBoard,
			OutlinedDiamond,
			SolidDiamond
		}

        public class HatchStyleInfo
        {
            public HatchStylePickerCombo.HatchStylePickerEnum HatchStyle
            {
                get;
                set;
            }

            public string Text
            {
                get;
                set;
            }

            public HatchStyleInfo(string text, HatchStylePickerCombo.HatchStylePickerEnum hatchStyle)
            {
                this.Text = text;
                this.HatchStyle = hatchStyle;
            }
        }
    }
}
