using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class DrawObjectGroup
    {
        public string BasicInfo
        {
            get;
            set;
        }

        public CEstimatingItems COfficeProducts
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public bool Deleted
        {
            get;
            set;
        }

        public CEstimatingItems EstimatingItems
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ObjectType
        {
            get;
            set;
        }

        public Presets Presets
        {
            get;
            private set;
        }

        public Preset SelectedPreset
        {
            get;
            set;
        }

        public Preset SelectedRenderingPreset
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public string TemplateID
        {
            get;
            set;
        }

        public DrawObjectGroup(int id, string template)
        {
            this.ID = id;
            this.Name = "";
            this.ObjectType = "";
            this.TemplateID = template;
            this.BasicInfo = "";
            this.Deleted = false;
            this.Presets = new Presets();
            this.SelectedPreset = null;
            this.SelectedRenderingPreset = null;
            this.EstimatingItems = new CEstimatingItems();
            this.COfficeProducts = new CEstimatingItems();
        }

        public DrawObjectGroup(int id, string name, string objectType, string template)
        {
            this.ID = id;
            this.Name = name;
            this.ObjectType = objectType;
            this.TemplateID = template;
            this.BasicInfo = "";
            this.Deleted = false;
            this.Presets = new Presets();
            this.SelectedPreset = null;
            this.SelectedRenderingPreset = null;
            this.EstimatingItems = new CEstimatingItems();
            this.COfficeProducts = new CEstimatingItems();
        }

        public DrawObjectGroup(int id, string name, string objectType, Color color, string basicInfo, object tag)
        {
            this.ID = id;
            this.Name = name;
            this.ObjectType = objectType;
            this.Color = color;
            this.TemplateID = "";
            this.BasicInfo = basicInfo;
            this.Tag = tag;
            this.Deleted = false;
            this.Presets = new Presets();
            this.SelectedPreset = null;
            this.SelectedRenderingPreset = null;
            this.EstimatingItems = new CEstimatingItems();
            this.COfficeProducts = new CEstimatingItems();
        }

        public void Clear()
        {
            this.ID = -1;
            this.Name = "";
            this.ObjectType = "";
            this.TemplateID = "";
            this.BasicInfo = "";
            this.Tag = null;
            this.Presets.Clear();
            this.SelectedPreset = null;
            this.SelectedRenderingPreset = null;
            this.EstimatingItems = new CEstimatingItems();
            this.COfficeProducts = new CEstimatingItems();
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("ObjectType = ", this.ObjectType));
            Console.WriteLine(string.Concat("Color = ", this.Color));
            Console.WriteLine(string.Concat("Template = ", this.TemplateID));
            Console.WriteLine(string.Concat("BasicInfo = ", this.BasicInfo));
            Console.WriteLine(string.Concat("Deleted = ", this.Deleted));
            this.Presets.Dump();
        }
    }
}