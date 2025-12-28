using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class ToolEditForm : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.groupBox.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.flowLayoutPanel1.Font = Utilities.GetDefaultFont(FontStyle.Regular);
		}

		private void AddToPanel(FlowLayoutPanel flowLayoutPanel, Control control)
		{
			control.Margin = new System.Windows.Forms.Padding((control.GetType().Name == "LabelEx") ? 0 : 3, 0, 0, (control.GetType().Name == "LabelEx") ? 4 : 9);
			control.Visible = true;
			flowLayoutPanel.Controls.Add(control);
		}

		private void InitializeGui()
		{
			if (this.editFromLibrary)
			{
				if (this.template == null)
				{
					this.Text = Resources.Créer_un_nouveau_modèle;
				}
				else if (this.duplicate)
				{
					this.Text = Resources.Dupliquer_le_modèle;
				}
				else
				{
					this.Text = Resources.Modifier_le_modèle;
				}
			}
			else
			{
				string objectType;
				if (this.drawObject != null && (objectType = this.drawObject.ObjectType) != null)
				{
					if (!(objectType == "Line"))
					{
						if (!(objectType == "Area"))
						{
							if (!(objectType == "Perimeter"))
							{
								if (objectType == "Counter")
								{
									this.Text = Resources.Créer_un_nouveau_compteur;
								}
							}
							else
							{
								this.Text = Resources.Créer_un_nouveau_périmètre;
							}
						}
						else
						{
							this.Text = Resources.Créer_une_nouvelle_surface;
						}
					}
					else
					{
						this.Text = Resources.Créer_une_nouvelle_distance;
					}
				}
				bool flag = this.template != null && !this.template.CreatedFromObject;
				this.Text += (flag ? (" " + Resources.à_partir_d_un_modèle) : "");
			}
			this.cbColor.AddStandardColors();
			this.cbColor.DropDownHeight = 240;
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(12, 12, 0, 0);
			this.AddToPanel(this.flowLayoutPanel1, this.lblToolName);
			this.AddToPanel(this.flowLayoutPanel1, this.txtToolName);
			this.AddToPanel(this.flowLayoutPanel1, this.lblToolColor);
			this.AddToPanel(this.flowLayoutPanel1, this.panelColor);
			this.txtToolName.Text = this.drawObject.Name;
			this.cbColor.SelectedValue = this.drawObject.Color;
			this.txtComment.Text = this.drawObject.Comment;
			string objectType2;
			if ((objectType2 = this.drawObject.ObjectType) != null)
			{
				if (!(objectType2 == "Area"))
				{
					if (!(objectType2 == "Line") && !(objectType2 == "Perimeter"))
					{
						if (objectType2 == "Counter")
						{
							this.cbShape.Items.Add(Resources.Cercle);
							this.cbShape.Items.Add(Resources.Carré);
							this.cbShape.Items.Add(Resources.Losange);
							this.cbShape.Items.Add(Resources.Triangle);
							this.cbShape.Items.Add(Resources.Triangle_inversé);
							this.cbShape.Items.Add(Resources.Trapèze);
							this.cbShape.Items.Add(Resources.Trapèze_inversé);
							this.AddToPanel(this.flowLayoutPanel1, this.lblShape);
							this.AddToPanel(this.flowLayoutPanel1, this.cbShape);
							try
							{
								this.cbShape.SelectedIndex = (int)((DrawCounter)this.drawObject).Shape;
							}
							catch
							{
								this.cbShape.SelectedIndex = 0;
							}
							this.AddToPanel(this.flowLayoutPanel1, this.lblDefaultSize);
							this.AddToPanel(this.flowLayoutPanel1, this.sliderDefaultSize);
							this.sliderDefaultSize.Value = ((DrawCounter)this.drawObject).DefaultSize;
							this.sliderDefaultSize.Text = this.sliderDefaultSize.Value.ToString();
							this.AddToPanel(this.flowLayoutPanel1, this.lblText);
							this.AddToPanel(this.flowLayoutPanel1, this.txtText);
							this.txtText.Text = this.drawObject.Text;
						}
					}
					else
					{
						this.AddToPanel(this.flowLayoutPanel1, this.lblPenWidth);
						this.AddToPanel(this.flowLayoutPanel1, this.sliderPenWidth);
						this.sliderPenWidth.Value = this.drawObject.PenWidth;
						this.sliderPenWidth.Text = this.sliderPenWidth.Value.ToString();
						this.AddToPanel(this.flowLayoutPanel1, this.lblSlope);
						this.AddToPanel(this.flowLayoutPanel1, this.panelSlope);
						this.txtSlope.Text = this.drawObject.SlopeFactor.GetSlopeLongString();
						this.drawObject.SlopeFactor.HipValley = SlopeFactor.HipValleyEnum.hipValleyDisabled;
					}
				}
				else
				{
					this.AddToPanel(this.flowLayoutPanel1, this.lblHatchStyle);
					this.AddToPanel(this.flowLayoutPanel1, this.cbHatchStyle);
					this.AddToPanel(this.flowLayoutPanel1, this.lblSlope);
					this.AddToPanel(this.flowLayoutPanel1, this.panelSlope);
					try
					{
						this.cbHatchStyle.SelectedValue = ((DrawPolyLine)this.drawObject).Pattern;
					}
					catch
					{
						this.cbHatchStyle.SelectedIndex = 0;
					}
					this.txtSlope.Text = this.drawObject.SlopeFactor.GetSlopeLongString();
					this.drawObject.SlopeFactor.HipValley = SlopeFactor.HipValleyEnum.hipValleyUnavailable;
				}
			}
			this.chkSaveAsTemplate.Visible = (this.template == null && !this.editFromLibrary);
			this.chkApplyChangesToTemplate.Visible = (this.template != null && !this.editFromLibrary);
			this.btDeleteTemplate.Visible = (this.template != null && !this.editFromLibrary);
			if (this.template != null)
			{
				if (!this.editFromLibrary)
				{
					this.btDeleteTemplate.Visible = !this.template.DeletionForbidden;
					if (this.template.SystemTemplate)
					{
						this.chkApplyChangesToTemplate.Checked = true;
						this.chkApplyChangesToTemplate.Visible = false;
					}
					if (this.template.CreatedFromObject)
					{
						this.chkSaveAsTemplate.Visible = true;
						this.chkApplyChangesToTemplate.Visible = false;
						this.btDeleteTemplate.Visible = false;
					}
				}
				if (this.template.DeletionForbidden)
				{
					this.systemTemplateName = this.drawObject.Name;
				}
			}
			this.InitializeEstimatingItemsGUI();
			if (this.mainForm.COInterface.IsReady)
			{
				this.InitializeCOfficeGui();
			}
			this.UpdatePreviewIcon();
			Utilities.SetObjectFocus(this.txtToolName);
		}

		public ToolEditForm(MainForm mainForm, DrawingArea drawArea, DrawObjectGroup group, DrawObject drawObject, Template template, ExtensionsSupport extensionsSupport, TemplatesSupport templatesSupport, TemplatesLibraryEditor templatesLibraryEditor, UnitScale unitScale, bool editFromLibrary, bool duplicate)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.mainForm = mainForm;
			this.drawArea = drawArea;
			this.group = group;
			this.drawObject = drawObject;
			this.template = template;
			this.extensionsSupport = extensionsSupport;
			this.extensionsManager.SelectGroup(group, extensionsSupport, unitScale);
			this.extensionsManager.HelpUtilities = mainForm.HelpUtilities;
			this.unitScale = unitScale;
			this.oldOpacity = base.Opacity;
			this.picPreview.Height += 10;
			this.templatesSupport = templatesSupport;
			this.templatesLibraryEditor = templatesLibraryEditor;
			this.editFromLibrary = editFromLibrary;
			this.duplicate = duplicate;
			this.coInterface = mainForm.COInterface;
			drawObject.Dirty = false;
			this.InitializeGui();
		}

		private void textBox_Enter(object sender, EventArgs e)
		{
			Utilities.SelectText((TextBox)sender);
		}

		private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void PopupControl(Control parentControl, Control popupControl)
		{
			base.Opacity = 100.0;
			popupControl.MinimumSize = popupControl.Size;
			ToolStripDropDown toolStripDropDown = new ToolStripDropDown();
			ToolStripControlHost toolStripControlHost = new ToolStripControlHost(popupControl);
			toolStripControlHost.Margin = System.Windows.Forms.Padding.Empty;
			toolStripDropDown.Padding = System.Windows.Forms.Padding.Empty;
			toolStripDropDown.Items.Add(toolStripControlHost);
			parentControl.Tag = toolStripDropDown;
			toolStripDropDown.Tag = popupControl;
			popupControl.Focus();
			Point point = parentControl.FindForm().PointToClient(parentControl.Parent.PointToScreen(parentControl.Location));
			toolStripDropDown.Opened += this.toolDrop_Opened;
			toolStripDropDown.Closed += this.toolDrop_Closed;
			toolStripDropDown.Show(this, new Point(point.X + parentControl.Width - popupControl.Width + 26, point.Y + parentControl.Height));
		}

		private void toolDrop_Opened(object sender, EventArgs e)
		{
			Control control = (Control)((ToolStripDropDown)sender).Tag;
			if (control == null)
			{
				return;
			}
			control.Focus();
		}

		private void toolDrop_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			base.Opacity = this.oldOpacity;
		}

		private void btMoreColors_Click(object sender, EventArgs e)
		{
			ColorPicker colorPicker = new ColorPicker();
			colorPicker.OnColorHover += this.colorPicker_OnColorHover;
			colorPicker.OnColorSelected += this.colorPicker_OnColorSelected;
			this.PopupControl(this.btMoreColors, colorPicker);
		}

		private void colorPicker_OnColorHover(Color color)
		{
			if (color == Color.Empty)
			{
				return;
			}
			this.drawObject.Color = color;
			this.drawObject.FillColor = color;
			this.UpdatePreviewIcon();
		}

		private void colorPicker_OnColorSelected(Color color)
		{
			ToolStripDropDown toolStripDropDown = (ToolStripDropDown)this.btMoreColors.Tag;
			if (toolStripDropDown == null)
			{
				return;
			}
			this.cbColor.SelectedValue = color;
			toolStripDropDown.Close(ToolStripDropDownCloseReason.CloseCalled);
		}

		private void UpdatePreviewIcon()
		{
			this.picPreview.Refresh();
		}

		private void txtToolName_TextChanged(object sender, EventArgs e)
		{
			this.drawObject.Name = this.txtToolName.Text;
			this.UpdatePreviewIcon();
		}

		private void txtText_TextChanged(object sender, EventArgs e)
		{
			this.drawObject.Text = this.txtText.Text;
			this.UpdatePreviewIcon();
		}

		private void cbColor_SelectedValueChanged(object sender, EventArgs e)
		{
			this.drawObject.Color = this.cbColor.SelectedValue;
			this.drawObject.FillColor = this.cbColor.SelectedValue;
			this.UpdatePreviewIcon();
		}

		private void sliderPenWidth_ValueChanged(object sender, EventArgs e)
		{
			this.sliderPenWidth.Text = this.sliderPenWidth.Value.ToString();
			this.drawObject.PenWidth = this.sliderPenWidth.Value;
			this.UpdatePreviewIcon();
		}

		private void picPreview_Paint(object sender, PaintEventArgs e)
		{
			this.mainForm.DrawObjectTypeImage(this.drawObject, e.Graphics);
		}

		private void btOk_Click(object sender, EventArgs e)
		{
			this.txtToolName.Text = this.txtToolName.Text.Trim();
			this.txtComment.Text = this.txtComment.Text.Trim();
			if (this.txtToolName.Text == "")
			{
				Utilities.SetObjectFocus(this.txtToolName);
				return;
			}
			string title = string.Empty;
			string message = string.Empty;
			string text = this.txtToolName.Text;
			if (this.editFromLibrary)
			{
				if (!this.drawArea.ValidateName(ref text, ref title, ref message, 50))
				{
					Utilities.DisplayError(title, message);
					Utilities.SetObjectFocus(this.txtToolName);
					return;
				}
			}
			else if (!this.drawArea.ValidateDrawObjectName(ref text, ref title, ref message))
			{
				Utilities.DisplayError(title, message);
				Utilities.SetObjectFocus(this.txtToolName);
				return;
			}
			this.txtToolName.Text = text;
			if ((this.chkSaveAsTemplate.Checked || (this.editFromLibrary && this.template == null) || (this.editFromLibrary && this.duplicate) || ((this.chkApplyChangesToTemplate.Checked || (this.editFromLibrary && this.template != null)) && text != this.template.DrawObject.Name)) && this.templatesSupport.ObjectExists(this.drawObject.ObjectType, text))
			{
				title = Resources.Nom_déjà_utilisé;
				message = Resources.Ce_nom_est_déjà_utilisé_par_un_autre_modèle;
				Utilities.DisplayError(title, message);
				Utilities.SetObjectFocus(this.txtToolName);
				return;
			}
			this.drawObject.Name = this.txtToolName.Text;
			this.drawObject.Comment = this.txtComment.Text;
			this.drawObject.Color = this.cbColor.SelectedValue;
			this.drawObject.FillColor = this.cbColor.SelectedValue;
			string objectType;
			if ((objectType = this.drawObject.ObjectType) != null)
			{
				if (!(objectType == "Area"))
				{
					if (!(objectType == "Line") && !(objectType == "Perimeter"))
					{
						if (objectType == "Counter")
						{
							((DrawCounter)this.drawObject).Shape = (DrawCounter.CounterShapeTypeEnum)this.cbShape.SelectedIndex;
							((DrawCounter)this.drawObject).DefaultSize = this.sliderDefaultSize.Value;
							this.drawObject.Text = this.txtText.Text;
						}
					}
					else
					{
						this.drawObject.PenWidth = this.sliderPenWidth.Value;
					}
				}
				else
				{
					((DrawPolyLine)this.drawObject).Pattern = this.cbHatchStyle.SelectedValue;
				}
			}
			if ((this.chkSaveAsTemplate.Checked || (this.editFromLibrary && this.template == null) || (this.editFromLibrary && this.duplicate)) && !this.SaveAsTemplate())
			{
				Utilities.SetObjectFocus(this.txtToolName);
				return;
			}
			if (this.chkApplyChangesToTemplate.Checked || (this.editFromLibrary && this.template != null && !this.duplicate))
			{
				this.templatesSupport.SetTemplateValues(this.template, this.drawObject, this.group.Presets, this.group);
				this.SaveTemplate(this.template, this.template.FullFileName);
			}
			this.drawObject.Dirty = true;
			base.Close();
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.tabControl.SelectedIndex)
			{
			case 0:
				Utilities.SetObjectFocus(this.txtToolName);
				return;
			case 1:
				Utilities.SetObjectFocus(this.extensionsManager);
				return;
			case 2:
				Utilities.SetObjectFocus(this.estimatingItemsControl);
				return;
			case 3:
				Utilities.SetObjectFocus(this.txtComment);
				return;
			default:
				return;
			}
		}

		private void ToolEditForm_Shown(object sender, EventArgs e)
		{
			Utilities.SetObjectFocus(this.txtToolName);
		}

		private bool SaveTemplate(Template template, string fileName)
		{
			if (this.systemTemplateName != "" && template.DrawObject != null)
			{
				template.DrawObject.Name = this.systemTemplateName;
			}
			return this.templatesSupport.SaveTemplate(template, fileName);
		}

		private bool SaveAsTemplate()
		{
			string fileName = Path.Combine(Utilities.GetTemplatesFolder(), Utilities.GetUniqueFileName("xml"));
			Template template = this.templatesSupport.CreateTemplate(this.drawObject, this.group.Presets, this.group);
			bool flag = this.SaveTemplate(template, fileName);
			if (flag)
			{
				this.group.TemplateID = template.ID;
				int selectedNodeID = this.templatesSupport.InsertTemplate(template);
				this.templatesLibraryEditor.RefreshSource(selectedNodeID);
			}
			return flag;
		}

		private void btDeleteTemplate_Click(object sender, EventArgs e)
		{
			string supprimer_ce_modèle = Resources.Supprimer_ce_modèle;
			string si_vous_supprimez_ce_modèle_il_ne_sera_plus_disponible_pour_vos_prochains_projets = Resources.Si_vous_supprimez_ce_modèle_il_ne_sera_plus_disponible_pour_vos_prochains_projets;
			if (Utilities.DisplayDeleteConfirmation(supprimer_ce_modèle, si_vous_supprimez_ce_modèle_il_ne_sera_plus_disponible_pour_vos_prochains_projets) == DialogResult.Yes)
			{
				Utilities.FileDelete(this.template.FullFileName, true);
				this.templatesSupport.RemoveTemplate(this.template);
				this.templatesLibraryEditor.RefreshSource(-1);
				base.Close();
			}
		}

		private void btSlope_Click(object sender, EventArgs e)
		{
			SlopeFactorControl slopeFactorControl = new SlopeFactorControl(this.drawObject.SlopeFactor);
			slopeFactorControl.OnSave += this.slopeFactorControl_OnSave;
			slopeFactorControl.OnCancel += this.slopeFactorControl_OnCancel;
			this.PopupControl(this.btSlope, slopeFactorControl);
		}

		private void CloseSlopPopUp()
		{
			ToolStripDropDown toolStripDropDown = (ToolStripDropDown)this.btSlope.Tag;
			if (toolStripDropDown == null)
			{
				return;
			}
			toolStripDropDown.Close(ToolStripDropDownCloseReason.CloseCalled);
		}

		private void slopeFactorControl_OnSave(SlopeFactor slopeFactor)
		{
			this.drawObject.SetSlopeFactor(slopeFactor);
			this.txtSlope.Text = this.drawObject.SlopeFactor.GetSlopeLongString();
			this.CloseSlopPopUp();
		}

		private void slopeFactorControl_OnCancel()
		{
			this.CloseSlopPopUp();
		}

		private void btControl_Leave(object sender, EventArgs e)
		{
			string name = ((Control)sender).Name;
			string a;
			if ((a = name) != null)
			{
				if (a == "cbColor" || a == "btMoreColors")
				{
					this.lblToolColor.Font = Utilities.GetDefaultFont();
					this.lblToolColor.ForeColor = Color.Black;
					return;
				}
				if (a == "cbHatchStyle")
				{
					this.lblHatchStyle.Font = Utilities.GetDefaultFont();
					this.lblHatchStyle.ForeColor = Color.Black;
					return;
				}
				if (a == "sliderPenWidth")
				{
					this.lblPenWidth.Font = Utilities.GetDefaultFont();
					this.lblPenWidth.ForeColor = Color.Black;
					return;
				}
				if (a == "sliderDefaultSize")
				{
					this.lblDefaultSize.Font = Utilities.GetDefaultFont();
					this.lblDefaultSize.ForeColor = Color.Black;
					return;
				}
				if (!(a == "btSlope"))
				{
					return;
				}
				this.lblSlope.Font = Utilities.GetDefaultFont();
				this.lblSlope.ForeColor = Color.Black;
			}
		}

		private void btControl_Enter(object sender, EventArgs e)
		{
			string name = ((Control)sender).Name;
			string a;
			if ((a = name) != null)
			{
				if (a == "cbColor" || a == "btMoreColors")
				{
					this.lblToolColor.Font = Utilities.GetDefaultFont(FontStyle.Bold);
					this.lblToolColor.ForeColor = Color.SteelBlue;
					return;
				}
				if (a == "cbHatchStyle")
				{
					this.lblHatchStyle.Font = Utilities.GetDefaultFont(FontStyle.Bold);
					this.lblHatchStyle.ForeColor = Color.SteelBlue;
					return;
				}
				if (a == "sliderPenWidth")
				{
					this.lblPenWidth.Font = Utilities.GetDefaultFont(FontStyle.Bold);
					this.lblPenWidth.ForeColor = Color.SteelBlue;
					return;
				}
				if (a == "sliderDefaultSize")
				{
					this.lblDefaultSize.Font = Utilities.GetDefaultFont(FontStyle.Bold);
					this.lblDefaultSize.ForeColor = Color.SteelBlue;
					return;
				}
				if (!(a == "btSlope"))
				{
					return;
				}
				this.lblSlope.Font = Utilities.GetDefaultFont(FontStyle.Bold);
				this.lblSlope.ForeColor = Color.SteelBlue;
			}
		}

		private void ToolEditForm_Activated(object sender, EventArgs e)
		{
			base.Opacity = this.oldOpacity;
		}

		private void extensionsManager_OnShowModalWindow(object sender, EventArgs e)
		{
			base.Opacity = 100.0;
		}

		private void sliderDefaultSize_ValueChanged(object sender, EventArgs e)
		{
			this.sliderDefaultSize.Text = this.sliderDefaultSize.Value.ToString();
			((DrawCounter)this.drawObject).DefaultSize = this.sliderDefaultSize.Value;
			this.UpdatePreviewIcon();
		}

		private void cbShape_SelectedIndexChanged(object sender, EventArgs e)
		{
			((DrawCounter)this.drawObject).Shape = (DrawCounter.CounterShapeTypeEnum)this.cbShape.SelectedIndex;
			this.UpdatePreviewIcon();
		}

		private void cbHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			((DrawPolyLine)this.drawObject).Pattern = this.cbHatchStyle.SelectedValue;
			this.UpdatePreviewIcon();
		}

		private void txtComment_Enter(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.SelectionStart = textBox.TextLength + 1;
		}

		private void GetDefaultFormula(CEstimatingItemsBrowserInterface browserInterface, CEstimatingItem product, string coUnit)
		{
			this.drawObject.GroupID = this.group.ID;
			this.drawObject.Group = this.group;
			GroupStats stats;
			if (!this.editFromLibrary)
			{
				stats = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, this.drawObject, this.drawArea.ActivePlan.UnitScale.ScaleSystemType, true, "");
			}
			else
			{
				stats = new GroupStats(this.drawObject.ObjectType);
			}
			browserInterface.GetDefaultFormula(this.drawObject, stats, product, coUnit);
			this.drawObject.GroupID = -1;
			this.drawObject.Group = null;
		}

		private void ProductAdd(CEstimatingItemsBrowserInterface browserInterface, CEstimatingsItemsControl itemsControl, CEstimatingItems products)
		{
			browserInterface.ShowBrowser(this);
			CEstimatingItem selectedProduct = browserInterface.Browser.SelectedProduct;
			if (selectedProduct == null)
			{
				return;
			}
			CEstimatingItem cestimatingItem = new CEstimatingItem(selectedProduct.ItemID, selectedProduct.Description, selectedProduct.Value, selectedProduct.Unit, selectedProduct.ItemType, selectedProduct.UnitMeasure, selectedProduct.CoverageValue, selectedProduct.CoverageUnit, selectedProduct.SectionID, selectedProduct.SubSectionID, selectedProduct.BidCode, "");
			cestimatingItem.Tag = cestimatingItem;
			this.GetDefaultFormula(browserInterface, cestimatingItem, selectedProduct.Unit);
			if (cestimatingItem.Formula == "")
			{
				this.drawObject.GroupID = this.group.ID;
				this.drawObject.Group = this.group;
				this.drawObject.DrawArea = this.drawArea;
				UnitScale.UnitSystem currentSystemType = this.unitScale.CurrentSystemType;
				Presets presets = this.drawObject.Group.Presets;
				GroupStats stats;
				if (!this.editFromLibrary)
				{
					stats = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, this.drawObject, currentSystemType, true, "");
				}
				else
				{
					stats = new GroupStats(this.drawObject.ObjectType);
				}
				FormulaResults formulaResults = new FormulaResults();
				formulaResults.Refresh(this.drawObject, stats, currentSystemType, this.unitScale, presets, this.extensionsSupport);
				using (FormulaBuilderForm formulaBuilderForm = new FormulaBuilderForm(cestimatingItem, formulaResults, this.drawObject, this.drawArea.Owner.ImageCollection))
				{
					formulaBuilderForm.ShowDialog(this.drawArea.Owner);
				}
				this.drawObject.GroupID = -1;
				this.drawObject.Group = null;
				this.drawObject.DrawArea = null;
			}
			if (cestimatingItem.Formula == "")
			{
				return;
			}
			products.Add(cestimatingItem);
			itemsControl.RefreshList();
			itemsControl.SelectItem(cestimatingItem);
			Console.WriteLine("product=" + cestimatingItem.Description);
		}

		private void ProductRemove(CEstimatingsItemsControl itemsControl, CEstimatingItems products)
		{
			CEstimatingItem selectedProduct = itemsControl.SelectedProduct;
			if (selectedProduct == null)
			{
				return;
			}
			string supprimer_ce_produit_ = Resources.Supprimer_ce_produit_;
			string ce_produit_sera_supprimé_de_ce_groupe = Resources.Ce_produit_sera_supprimé_de_ce_groupe;
			if (Utilities.DisplayDeleteConfirmation(supprimer_ce_produit_, ce_produit_sera_supprimé_de_ce_groupe) == DialogResult.No)
			{
				return;
			}
			products.Remove(selectedProduct);
			itemsControl.RefreshList();
		}

		private void ProductEdit(CEstimatingsItemsControl itemsControl)
		{
			CEstimatingItem selectedProduct = itemsControl.SelectedProduct;
			if (selectedProduct == null)
			{
				return;
			}
			this.drawObject.GroupID = this.group.ID;
			this.drawObject.Group = this.group;
			this.drawObject.DrawArea = this.drawArea;
			UnitScale.UnitSystem currentSystemType = this.unitScale.CurrentSystemType;
			Presets presets = this.drawObject.Group.Presets;
			GroupStats stats;
			if (!this.editFromLibrary)
			{
				stats = GroupUtilities.ComputeGroupStats(this.drawArea.ActivePlan, this.drawObject, currentSystemType, true, "");
			}
			else
			{
				stats = new GroupStats(this.drawObject.ObjectType);
			}
			FormulaResults formulaResults = new FormulaResults();
			formulaResults.Refresh(this.drawObject, stats, currentSystemType, this.unitScale, presets, this.extensionsSupport);
			using (FormulaBuilderForm formulaBuilderForm = new FormulaBuilderForm(selectedProduct, formulaResults, this.drawObject, this.drawArea.Owner.ImageCollection))
			{
				formulaBuilderForm.ShowDialog(this.drawArea.Owner);
			}
			this.drawObject.GroupID = -1;
			this.drawObject.Group = null;
			this.drawObject.DrawArea = null;
			itemsControl.RefreshList();
		}

		private void InitializeEstimatingItemsGUI()
		{
			TabPage tabPage = new TabPage(Resources.Items_d_estimation);
			tabPage.Name = "tabPage4";
			this.estimatingItemsControl = new CEstimatingsItemsControl(this.drawArea.Project);
			this.estimatingItemsControl.Name = "EstimatingItems";
			this.estimatingItemsControl.Dock = DockStyle.Fill;
			tabPage.Controls.Add(this.estimatingItemsControl);
			IntPtr handle = this.tabControl.Handle;
			this.tabControl.TabPages.Insert(2, tabPage);
			this.estimatingItemsControl.OnProductAdd += this.estimatingItemsControl_OnProductAdd;
			this.estimatingItemsControl.OnProductRemove += this.estimatingItemsControl_OnProductRemove;
			this.estimatingItemsControl.OnFormulaEdit += this.estimatingItemsControl_OnFormulaEdit;
			this.estimatingItemsControl.UpdateGUI(this.group.EstimatingItems);
		}

		private void estimatingItemsControl_OnProductAdd(object sender, EventArgs e)
		{
			this.ProductAdd(this.drawArea.Project.DBManagement.BrowserInterface, this.estimatingItemsControl, this.group.EstimatingItems);
		}

		private void estimatingItemsControl_OnProductRemove(object sender, EventArgs e)
		{
			this.ProductRemove(this.estimatingItemsControl, this.group.EstimatingItems);
		}

		private void estimatingItemsControl_OnFormulaEdit(object sender, EventArgs e)
		{
			this.ProductEdit(this.estimatingItemsControl);
		}

		private void InitializeCOfficeGui()
		{
			TabPage tabPage = new TabPage("Contractor's Office");
			tabPage.Name = "tabPage5";
			this.coControl = new CEstimatingsItemsControl(null);
			this.coControl.Name = "coControl";
			this.coControl.Dock = DockStyle.Fill;
			tabPage.Controls.Add(this.coControl);
			IntPtr handle = this.tabControl.Handle;
			this.tabControl.TabPages.Add(tabPage);
			this.coControl.OnProductAdd += this.coControl_OnProductAdd;
			this.coControl.OnProductRemove += this.coControl_OnProductRemove;
			this.coControl.OnFormulaEdit += this.coControl_OnFormulaEdit;
			this.coControl.UpdateGUI(this.group.COfficeProducts);
		}

		private void coControl_OnProductAdd(object sender, EventArgs e)
		{
			this.ProductAdd(this.coInterface.BrowserInterface, this.coControl, this.group.COfficeProducts);
		}

		private void coControl_OnProductRemove(object sender, EventArgs e)
		{
			this.ProductRemove(this.coControl, this.group.COfficeProducts);
		}

		private void coControl_OnFormulaEdit(object sender, EventArgs e)
		{
			this.ProductEdit(this.coControl);
		}

		private MainForm mainForm;

		private DrawingArea drawArea;

		private DrawObjectGroup group;

		private DrawObject drawObject;

		private Template template;

		private double oldOpacity;

		private string systemTemplateName = "";

		private ExtensionsSupport extensionsSupport;

		private TemplatesSupport templatesSupport;

		private TemplatesLibraryEditor templatesLibraryEditor;

		private UnitScale unitScale;

		private bool editFromLibrary;

		private bool duplicate;

		private CEstimatingsItemsControl coControl;

		private COfficeInterface coInterface;

		private CEstimatingsItemsControl estimatingItemsControl;
	}
}
