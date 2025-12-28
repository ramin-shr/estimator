using System;
using System.Collections;

namespace QuoterPlan
{
	public class TemplatesSupport
	{
		public TreeViewNodes TreeItems
		{
			get
			{
				return this.treeItems;
			}
		}

		public Templates Templates
		{
			get
			{
				return this.templates;
			}
		}

		public TemplatesSupport()
		{
			this.treeItems = new TreeViewNodes();
			this.templates = new Templates();
		}

		public void Clear()
		{
			this.treeItems.Clear();
			this.templates.Clear();
		}

		private int ValidateTreeNodeParent(Template template)
		{
			int num = -1;
			foreach (TreeViewNode treeViewNode in this.TreeItems.Collection)
			{
				if (treeViewNode.Tag.GetType().Name == "String")
				{
					string a = (string)treeViewNode.Tag;
					if (a == template.DrawObject.ObjectTypeDisplayName)
					{
						num = treeViewNode.ID;
					}
					if (num > -1)
					{
						break;
					}
				}
			}
			if (num == -1)
			{
				num = this.TreeItems.Add(0, template.DrawObject.ObjectTypeDisplayName);
			}
			return num;
		}

		public int InsertTreeNode(Template template)
		{
			int result = -1;
			int num = this.ValidateTreeNodeParent(template);
			if (num > -1)
			{
				result = this.TreeItems.Add(num, template);
			}
			return result;
		}

		public int InsertTemplate(Template template)
		{
			this.templates.Add(template);
			return this.InsertTreeNode(template);
		}

		public void RemoveTemplate(Template template)
		{
			this.treeItems.Delete(template);
			for (int i = this.templates.Count - 1; i >= 0; i--)
			{
				if (this.templates[i].Equals(template))
				{
					this.templates.RemoveAt(i);
				}
			}
		}

		public Template CreateTemplate(DrawObject drawObject, Presets presets, DrawObjectGroup group)
		{
			Template template = new Template();
			this.SetTemplateValues(template, drawObject, presets, group);
			return template;
		}

		public void LoadTemplate(string fileName, ExtensionsSupport extensionsSupport)
		{
			Template template = new Template();
			if (template.Open(fileName, extensionsSupport))
			{
				this.InsertTemplate(template);
				return;
			}
			if (Utilities.FileExists(fileName))
			{
				Utilities.FileDelete(fileName, true);
			}
		}

		public bool SaveTemplate(Template template, string fileName)
		{
			return template.Save(fileName);
		}

		public void SetTemplateValues(Template template, DrawObject drawObject, Presets presets, DrawObjectGroup group)
		{
			string fullFileName = template.FullFileName;
			template.Clear();
			template.FullFileName = fullFileName;
			template.DrawObject = drawObject.Clone();
			foreach (object obj in presets.Collection)
			{
				Preset preset = (Preset)obj;
				Preset preset2 = preset.Clone(false);
				template.Presets.Add(preset2);
			}
			if (group != null)
			{
				foreach (CEstimatingItem cestimatingItem in group.EstimatingItems.Collection)
				{
					CEstimatingItem cestimatingItem2 = new CEstimatingItem(cestimatingItem.ItemID, cestimatingItem.Description, cestimatingItem.Value, cestimatingItem.Unit, cestimatingItem.ItemType, cestimatingItem.UnitMeasure, cestimatingItem.CoverageValue, cestimatingItem.CoverageUnit, cestimatingItem.SectionID, cestimatingItem.SubSectionID, cestimatingItem.BidCode, cestimatingItem.Formula);
					cestimatingItem2.Tag = cestimatingItem2;
					template.EstimatingItems.Add(cestimatingItem);
				}
			}
			if (group != null)
			{
				foreach (CEstimatingItem cestimatingItem3 in group.COfficeProducts.Collection)
				{
					CEstimatingItem cestimatingItem4 = new CEstimatingItem(cestimatingItem3.ItemID, cestimatingItem3.Description, cestimatingItem3.Value, cestimatingItem3.Unit, cestimatingItem3.ItemType, cestimatingItem3.UnitMeasure, cestimatingItem3.CoverageValue, cestimatingItem3.CoverageUnit, cestimatingItem3.SectionID, cestimatingItem3.SubSectionID, cestimatingItem3.BidCode, cestimatingItem3.Formula);
					cestimatingItem4.Tag = cestimatingItem4;
					template.COfficeProducts.Add(cestimatingItem3);
				}
			}
		}

		public string GetFreeTemplateObjectName(string objectType, string prefix)
		{
			string text = "";
			return this.GetFreeTemplateObjectName(objectType, prefix, ref text);
		}

		public string GetFreeTemplateObjectName(string objectType, string prefix, ref string suffix)
		{
			int num = 0;
			string arg = Utilities.StripIndexFromString(prefix) + " ";
			do
			{
				num++;
			}
			while (this.ObjectExists(objectType, arg + num));
			suffix = num.ToString();
			return arg + num;
		}

		public void GetTemplatesNotInUse(Project project, string objectType, ref ArrayList templatesArray)
		{
			foreach (object obj in this.templates.Collection)
			{
				Template template = (Template)obj;
				DrawObject drawObject = template.DrawObject;
				if (drawObject != null && drawObject.ObjectType == objectType && !project.Groups.TemplateInUse(template.ID))
				{
					templatesArray.Add(template);
				}
			}
		}

		public bool ObjectExists(string objectType, string name)
		{
			foreach (object obj in this.templates.Collection)
			{
				Template template = (Template)obj;
				DrawObject drawObject = template.DrawObject;
				if (drawObject != null && drawObject.ObjectType == objectType && drawObject.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		public void Dump()
		{
			this.templates.Dump();
		}

		private readonly TreeViewNodes treeItems;

		private readonly Templates templates;
	}
}
