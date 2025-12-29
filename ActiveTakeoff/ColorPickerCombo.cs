using DevComponents.DotNetBar.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class ColorPickerCombo : ComboBoxEx
    {
        private IContainer components;

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
                if (this.SelectedIndex < 0)
                {
                    return string.Empty;
                }
                return this.SelectedItem.Text;
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
                if (this.SelectedIndex < 0)
                {
                    return Color.White;
                }
                return this.SelectedItem.Color;
            }
            set
            {
                bool flag = false;
                int num = 0;
                while (num < base.Items.Count)
                {
                    if (((ColorPickerCombo.ColorInfo)base.Items[num]).Color != value)
                    {
                        num++;
                    }
                    else
                    {
                        flag = true;
                        this.SelectedIndex = num;
                        break;
                    }
                }
                if (!flag)
                {
                    ComboBox.ObjectCollection items = base.Items;
                    object[] r = new object[] { value.R, ", ", value.G, ", ", value.B };
                    this.SelectedIndex = items.Add(new ColorPickerCombo.ColorInfo(string.Concat(r), value));
                }
            }
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
            base.DrawItem += new DrawItemEventHandler(this.OnDrawItem);
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

        private void InitializeFonts()
        {
            this.Font = Utilities.GetDefaultFont();
        }

        private void LoadResources()
        {
        }

        protected void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ColorPickerCombo.ColorInfo item = (ColorPickerCombo.ColorInfo)base.Items[e.Index];
                e.DrawBackground();
                Rectangle rectangle = new Rectangle()
                {
                    X = e.Bounds.X + 2,
                    Y = e.Bounds.Y + 2,
                    Width = 18,
                    Height = e.Bounds.Height - 5
                };
                e.Graphics.FillRectangle(new SolidBrush(item.Color), rectangle);
                e.Graphics.DrawRectangle(SystemPens.WindowText, rectangle);
                Brush brush = ((e.State & DrawItemState.Selected) != DrawItemState.None ? SystemBrushes.HighlightText : SystemBrushes.WindowText);
                Graphics graphics = e.Graphics;
                string text = item.Text;
                Font font = this.Font;
                Rectangle bounds = e.Bounds;
                float x = (float)(bounds.X + rectangle.X + rectangle.Width + 2);
                int y = e.Bounds.Y;
                Rectangle bounds1 = e.Bounds;
                graphics.DrawString(text, font, brush, x, (float)(y + (bounds1.Height - this.Font.Height) / 2 - 1));
            }
        }

        public class ColorInfo
        {
            public Color Color
            {
                get;
                set;
            }

            public string Text
            {
                get;
                set;
            }

            public ColorInfo(string text, Color color)
            {
                this.Text = text;
                this.Color = color;
            }
        }
    }
}