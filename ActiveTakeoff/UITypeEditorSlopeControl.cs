using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Threading;
using System.Windows.Forms.Design;

namespace QuoterPlan
{
    public class UITypeEditorSlopeControl : UITypeEditor
    {
        private IWindowsFormsEditorService frmSvr;

        private SlopeFactor slopeFactor;

        private SlopeFactorControl slopeFactorControl;

        public UITypeEditorSlopeControl()
        {
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
            this.slopeFactorControl.OnSave += new OnSlopeFactorSaveHandler(this.slopeFactorControl_OnSave);
            this.slopeFactorControl.OnCancel += new OnSlopeFactorCancelHandler(this.slopeFactorControl_OnCancel);
            this.frmSvr.DropDownControl(this.slopeFactorControl);
            return this.slopeFactor;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        private void slopeFactorControl_OnCancel()
        {
            if (this.frmSvr == null)
            {
                return;
            }
            this.frmSvr.CloseDropDown();
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

        public event OnSlopeFactorSaveHandler OnSave;
    }
}