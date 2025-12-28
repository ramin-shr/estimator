using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace QuoterPlan
{
	public class ColorPickerCombo : ComboBoxEx
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.Font = Utilities.GetDefaultFont();
		}

		public ColorPickerCombo()
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			base.DrawMode = DrawMode.OwnerDrawFixed;
			base.DropDownHeight = 200;
			base.DisableInternalDrawing = true;
			base.DrawItem += this.OnDrawItem;
		}

		public void AddStandardColors()
		{
			base.Items.Clear();
			for (int i = 0; i < 26; i++)
			{
				Color standardColor = ColorPicker.GetStandardColor((ColorPicker.StandardColorEnum)i);
				base.Items.Add(new ColorPickerCombo.ColorInfo(ColorPicker.ColorToString(standardColor), standardColor));
			}
		}

		protected void OnDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index >= 0)
			{
				ColorPickerCombo.ColorInfo colorInfo = (ColorPickerCombo.ColorInfo)base.Items[e.Index];
				e.DrawBackground();
				Rectangle rect = default(Rectangle);
				rect.X = e.Bounds.X + 2;
				rect.Y = e.Bounds.Y + 2;
				rect.Width = 18;
				rect.Height = e.Bounds.Height - 5;
				e.Graphics.FillRectangle(new SolidBrush(colorInfo.Color), rect);
				e.Graphics.DrawRectangle(SystemPens.WindowText, rect);
				Brush brush = ((e.State & DrawItemState.Selected) != DrawItemState.None) ? SystemBrushes.HighlightText : SystemBrushes.WindowText;
				e.Graphics.DrawString(colorInfo.Text, this.Font, brush, (float)(e.Bounds.X + rect.X + rect.Width + 2), (float)(e.Bounds.Y + (e.Bounds.Height - this.Font.Height) / 2 - 1));
			}
		}

		public new ColorPickerCombo.ColorInfo SelectedItem
		{
			get
			{
				return (ColorPickerCombo.ColorInfo)base.SelectedItem;
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
					if (((ColorPickerCombo.ColorInfo)base.Items[i]).Text == value)
					{
						this.SelectedIndex = i;
						return;
					}
				}
			}
		}

		public new Color SelectedValue
		{
			get
			{
				if (this.SelectedIndex >= 0)
				{
					return this.SelectedItem.Color;
				}
				return Color.White;
			}
			set
			{
				bool flag = false;
				for (int i = 0; i < base.Items.Count; i++)
				{
					if (((ColorPickerCombo.ColorInfo)base.Items[i]).Color == value)
					{
						flag = true;
						this.SelectedIndex = i;
						break;
					}
				}
				if (!flag)
				{
					this.SelectedIndex = base.Items.Add(new ColorPickerCombo.ColorInfo(string.Concat(new object[]
					{
						value.R,
						", ",
						value.G,
						", ",
						value.B
					}), value));
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
			base.SuspendLayout();
			this.BackColor = Color.White;
			base.Name = "ColorPicker";
			base.Size = new Size(216, 123);
			base.ResumeLayout(false);
		}

		private IContainer components;

		public class ColorInfo
		{
			public string Text
			{
				[CompilerGenerated]
				get
				{
					return this.<Text>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Text>k__BackingField = value;
				}
			}

			public Color Color
			{
				[CompilerGenerated]
				get
				{
					return this.<Color>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Color>k__BackingField = value;
				}
			}

			public ColorInfo(string text, Color color)
			{
				this.Text = text;
				this.Color = color;
			}

			[CompilerGenerated]
			private string <Text>k__BackingField;

			[CompilerGenerated]
			private Color <Color>k__BackingField;
		}
	}
}
