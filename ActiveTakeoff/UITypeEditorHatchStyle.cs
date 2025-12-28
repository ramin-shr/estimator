using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace QuoterPlan
{
	internal class UITypeEditorHatchStyle : UITypeEditor
	{
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override void PaintValue(PaintValueEventArgs e)
		{
			HatchStylePickerCombo.HatchStylePickerEnum hatchStylePickerEnum = (HatchStylePickerCombo.HatchStylePickerEnum)e.Value;
			Rectangle rect = new Rectangle(e.Bounds.Location, e.Bounds.Size);
			if (hatchStylePickerEnum != HatchStylePickerCombo.HatchStylePickerEnum.Solid)
			{
				using (HatchBrush hatchBrush = new HatchBrush((HatchStyle)hatchStylePickerEnum, Color.Black, Color.White))
				{
					e.Graphics.FillRectangle(hatchBrush, rect);
					e.Graphics.DrawRectangle(SystemPens.WindowText, rect);
					return;
				}
			}
			using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(120, Color.Black)))
			{
				e.Graphics.FillRectangle(solidBrush, rect);
				e.Graphics.DrawRectangle(SystemPens.WindowText, rect);
			}
		}

		public UITypeEditorHatchStyle()
		{
		}
	}
}
