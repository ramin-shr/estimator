using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace QuoterPlan
{
    public class UITypeEditorColorPicker : UITypeEditor
    {
        private IWindowsFormsEditorService frmSvr;

        private Color originalColor;

        private Color selectedColor;

        private ColorPicker colorPicker;

        public UITypeEditorColorPicker()
        {
        }

        private void ColorHover(Color color)
        {
            if (this.OnColorHover != null)
            {
                this.OnColorHover(color);
            }
        }

        private void colorPicker_ColorHover(Color color)
        {
            if (color == Color.Empty)
            {
                return;
            }
            this.ColorHover(color);
            Application.DoEvents();
        }

        private void colorPicker_ColorSelected(Color color)
        {
            if (this.frmSvr == null)
            {
                return;
            }
            this.selectedColor = color;
            this.frmSvr.CloseDropDown();
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            this.frmSvr = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (this.frmSvr == null)
            {
                return null;
            }
            this.originalColor = (Color)value;
            this.selectedColor = Color.Empty;
            this.colorPicker = new ColorPicker();
            this.colorPicker.OnColorHover += new OnColorHoverHandler(this.colorPicker_ColorHover);
            this.colorPicker.OnColorSelected += new OnColorSelectedHandler(this.colorPicker_ColorSelected);
            this.frmSvr.DropDownControl(this.colorPicker);
            if (this.selectedColor == Color.Empty)
            {
                this.ColorHover(this.originalColor);
            }
            return this.selectedColor;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public event OnColorHoverHandler OnColorHover;
    }
}