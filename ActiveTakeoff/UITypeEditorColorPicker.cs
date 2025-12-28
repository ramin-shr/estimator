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

		public UITypeEditorColorPicker()
		{
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
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
			this.colorPicker.OnColorHover += this.colorPicker_ColorHover;
			this.colorPicker.OnColorSelected += this.colorPicker_ColorSelected;
			this.frmSvr.DropDownControl(this.colorPicker);
			if (this.selectedColor == Color.Empty)
			{
				this.ColorHover(this.originalColor);
			}
			return this.selectedColor;
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

		private OnColorHoverHandler OnColorHover;

		private IWindowsFormsEditorService frmSvr;

		private Color originalColor;

		private Color selectedColor;

		private ColorPicker colorPicker;
	}
}
