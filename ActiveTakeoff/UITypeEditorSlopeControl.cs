using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Threading;
using System.Windows.Forms.Design;

namespace QuoterPlan
{
	public class UITypeEditorSlopeControl : UITypeEditor
	{
		public event OnSlopeFactorSaveHandler OnSave
		{
			add
			{
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler = this.OnSave;
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler2;
				do
				{
					onSlopeFactorSaveHandler2 = onSlopeFactorSaveHandler;
					OnSlopeFactorSaveHandler value2 = (OnSlopeFactorSaveHandler)Delegate.Combine(onSlopeFactorSaveHandler2, value);
					onSlopeFactorSaveHandler = Interlocked.CompareExchange<OnSlopeFactorSaveHandler>(ref this.OnSave, value2, onSlopeFactorSaveHandler2);
				}
				while (onSlopeFactorSaveHandler != onSlopeFactorSaveHandler2);
			}
			remove
			{
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler = this.OnSave;
				OnSlopeFactorSaveHandler onSlopeFactorSaveHandler2;
				do
				{
					onSlopeFactorSaveHandler2 = onSlopeFactorSaveHandler;
					OnSlopeFactorSaveHandler value2 = (OnSlopeFactorSaveHandler)Delegate.Remove(onSlopeFactorSaveHandler2, value);
					onSlopeFactorSaveHandler = Interlocked.CompareExchange<OnSlopeFactorSaveHandler>(ref this.OnSave, value2, onSlopeFactorSaveHandler2);
				}
				while (onSlopeFactorSaveHandler != onSlopeFactorSaveHandler2);
			}
		}

		public UITypeEditorSlopeControl()
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
			this.slopeFactor = (SlopeFactor)value;
			this.slopeFactorControl = new SlopeFactorControl(this.slopeFactor);
			this.slopeFactorControl.OnSave += this.slopeFactorControl_OnSave;
			this.slopeFactorControl.OnCancel += this.slopeFactorControl_OnCancel;
			this.frmSvr.DropDownControl(this.slopeFactorControl);
			return this.slopeFactor;
		}

		private void slopeFactorControl_OnSave(SlopeFactor slopeFactor)
		{
			if (this.frmSvr == null)
			{
				return;
			}
			this.slopeFactor.SetValues(slopeFactor);
			if (this.OnSave != null)
			{
				this.OnSave(slopeFactor);
			}
			this.frmSvr.CloseDropDown();
		}

		private void slopeFactorControl_OnCancel()
		{
			if (this.frmSvr == null)
			{
				return;
			}
			this.frmSvr.CloseDropDown();
		}

		private OnSlopeFactorSaveHandler OnSave;

		private IWindowsFormsEditorService frmSvr;

		private SlopeFactor slopeFactor;

		private SlopeFactorControl slopeFactorControl;
	}
}
