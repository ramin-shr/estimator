using QuoterPlan.Properties;
using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class CEstimatingItem
    {
        public string BidCode
        {
            get;
            set;
        }

        public double CoverageRate
        {
            get
            {
                return this.CoverageValue / (this.CoverageUnit <= 0 ? 1 : this.CoverageUnit);
            }
        }

        public double CoverageUnit
        {
            get;
            set;
        }

        public double CoverageValue
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Formula
        {
            get;
            set;
        }

        public string InternalKey
        {
            get;
            set;
        }

        public string ItemID
        {
            get;
            set;
        }

        public DBEstimatingItem.EstimatingItemType ItemType
        {
            get;
            set;
        }

        public string ItemTypeCaption
        {
            get
            {
                return DBEstimatingItem.GetItemTypeCaption(this.ItemType);
            }
        }

        public int SectionID
        {
            get;
            set;
        }

        public int SubSectionID
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public DBEstimatingItem.UnitMeasureType UnitMeasure
        {
            get;
            set;
        }

        public string UnitMeasureCaption
        {
            get
            {
                return DBEstimatingItem.GetUnitMeasureCaption(this.UnitMeasure);
            }
        }

        public double Value
        {
            get;
            set;
        }

        public CEstimatingItem()
        {
            this.Clear();
        }

        public CEstimatingItem(string itemID, string description, double value, string unit, DBEstimatingItem.EstimatingItemType itemType, DBEstimatingItem.UnitMeasureType unitMeasure, double coverageValue, double coverageUnit, int sectionID, int subSectionID, string bidCode, string formula)
        {
            this.ItemID = itemID;
            this.Description = description;
            this.Value = value;
            this.Unit = unit;
            this.ItemType = itemType;
            this.UnitMeasure = unitMeasure;
            this.CoverageValue = coverageValue;
            this.CoverageUnit = coverageUnit;
            this.SectionID = sectionID;
            this.SubSectionID = subSectionID;
            this.BidCode = bidCode;
            this.Formula = formula;
            this.InternalKey = Guid.NewGuid().ToString();
        }

        public void Clear()
        {
            this.ItemID = "";
            this.Description = "";
            this.Value = 0;
            this.Unit = "";
            this.UnitMeasure = DBEstimatingItem.UnitMeasureType.none;
            this.CoverageValue = 0;
            this.CoverageUnit = 1;
            this.SectionID = 0;
            this.SubSectionID = 0;
            this.BidCode = "";
            this.Formula = "";
            this.Tag = null;
            this.InternalKey = Guid.NewGuid().ToString();
        }

        public CEstimatingItem Clone(bool preserveInternalKey = false)
        {
            CEstimatingItem cEstimatingItem = new CEstimatingItem(
                this.ItemID,
                this.Description,
                this.Value,
                this.Unit,
                this.ItemType,
                this.UnitMeasure,
                this.CoverageValue,
                this.CoverageUnit,
                this.SectionID,
                this.SubSectionID,
                this.BidCode,
                this.Formula
            );

            cEstimatingItem.Tag = cEstimatingItem;

            if (preserveInternalKey)
            {
                cEstimatingItem.InternalKey = this.InternalKey;
            }
            return cEstimatingItem;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("SectionID = ", this.ItemID));
            Console.WriteLine(string.Concat("Name = ", this.Description));
            Console.WriteLine(string.Concat("Value = ", this.Value));
            Console.WriteLine(string.Concat("Unit = ", this.Unit));
            Console.WriteLine(string.Concat("UnitMeasure = ", this.UnitMeasure));
            Console.WriteLine(string.Concat("CoverageValue = ", this.CoverageValue));
            Console.WriteLine(string.Concat("CoverageUnit = ", this.CoverageUnit));
            Console.WriteLine(string.Concat("SectionID = ", this.SectionID));
            Console.WriteLine(string.Concat("SubSectionID = ", this.SubSectionID));
            Console.WriteLine(string.Concat("BidCode = ", this.BidCode));
            Console.WriteLine(string.Concat("Formula = ", this.Formula));
            Console.WriteLine(string.Concat("InternalKey = ", this.InternalKey));
        }

        public UnitScale.UnitSystem ResultSystemType(UnitScale.UnitSystem defaultSystemType)
        {
            if (this.UnitMeasure == DBEstimatingItem.UnitMeasureType.m || this.UnitMeasure == DBEstimatingItem.UnitMeasureType.sq_m || this.UnitMeasure == DBEstimatingItem.UnitMeasureType.cu_m || this.Unit.ToUpper() == "M" || this.Unit.ToUpper() == "M²" || this.Unit.ToUpper() == "M³")
            {
                return UnitScale.UnitSystem.metric;
            }
            if (this.UnitMeasure != DBEstimatingItem.UnitMeasureType.lin_ft && this.UnitMeasure != DBEstimatingItem.UnitMeasureType.sq_ft && this.UnitMeasure != DBEstimatingItem.UnitMeasureType.cu_yd && !(this.Unit.ToUpper() == Resources.pi.ToUpper()) && !(this.Unit.ToUpper() == Resources.pi_2.ToUpper()) && !(this.Unit.ToUpper() == Resources.pi_3.ToUpper()) && !(this.Unit.ToUpper() == Resources.v_3.ToUpper()))
            {
                return defaultSystemType;
            }
            return UnitScale.UnitSystem.imperial;
        }
    }
}