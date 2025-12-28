using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar;

namespace QuoterPlan
{
	public class UITypeEditorChoiceElements : UITypeEditor
	{
		public UITypeEditorChoiceElements()
		{
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		public ExtensionChoice Choice
		{
			get
			{
				return this.choice;
			}
			set
			{
				this.choice = value;
			}
		}

		public AdvPropertyGrid PropertyGrid
		{
			get
			{
				return this.propertyGrid;
			}
			set
			{
				this.propertyGrid = value;
			}
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.frmSvr = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (this.frmSvr == null)
			{
				return null;
			}
			ListBox listBox = new ListBox();
			listBox.BorderStyle = BorderStyle.None;
			listBox.SelectionMode = SelectionMode.One;
			listBox.SelectedValueChanged += this.OnListBoxSelectedValueChanged;
			int width = 200;
			if (this.propertyGrid != null)
			{
				width = (int)((float)this.propertyGrid.Width * ((float)this.propertyGrid.PropertyTree.Columns[1].Width.Relative / 100f)) - 14;
			}
			listBox.Width = width;
			foreach (object obj in this.choice.Elements.Collection)
			{
				ExtensionChoiceElement extensionChoiceElement = (ExtensionChoiceElement)obj;
				listBox.Items.Add(new Utilities.ItemData(extensionChoiceElement.Caption, extensionChoiceElement));
			}
			ExtensionChoiceElement extensionChoiceElement2 = (ExtensionChoiceElement)value;
			int num = listBox.FindString(extensionChoiceElement2.Caption);
			if (num != -1)
			{
				listBox.SelectedIndex = num;
			}
			this.frmSvr.DropDownControl(listBox);
			if (listBox.SelectedItem == null)
			{
				return value;
			}
			return Utilities.GetItemData(listBox.SelectedItem);
		}

		private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
		{
			this.frmSvr.CloseDropDown();
		}

		private IWindowsFormsEditorService frmSvr;

		private AdvPropertyGrid propertyGrid;

		private ExtensionChoice choice;
	}
}
