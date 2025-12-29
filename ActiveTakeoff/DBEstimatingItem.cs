using QuoterPlan.Properties;
using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class DBEstimatingItem
    {
        private static string[] itemTypeString;

        private static string[] unitMeasureString;

        public string purchaseUnit;

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

        public bool Dirty
        {
            get;
            set;
        }

        public int ID
        {
            get;
            private set;
        }

        public bool IsSystemItem
        {
            get;
            set;
        }

        public DBEstimatingItem.EstimatingItemType ItemType
        {
            get;
            private set;
        }

        public double PriceEach
        {
            get;
            set;
        }

        public string PurchaseUnit
        {
            get
            {
                string str = "";
                if (this.purchaseUnit.Length > 0)
                {
                    if (this.purchaseUnit[0] != '~')
                    {
                        str = this.purchaseUnit;
                    }
                    else
                    {
                        str = this.purchaseUnit.Substring(1, this.purchaseUnit.Length - 1);
                        if (UnitScale.CastUnitSystem(Settings.Default.DefaultSystemType) == UnitScale.UnitSystem.metric)
                        {
                            if (str == Resources.pi)
                            {
                                str = "m";
                            }
                            else if (str == Resources.pi_2)
                            {
                                str = "m²";
                            }
                            else if (str == Resources.pi_3)
                            {
                                str = "m³";
                            }
                        }
                    }
                }
                return str;
            }
            set
            {
                this.purchaseUnit = value;
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

        public DBEstimatingItem.UnitMeasureType UnitMeasure
        {
            get;
            set;
        }

        public string UnitMeasureCaption
        {
            get
            {
                return DBEstimatingItem.unitMeasureString[(int)this.UnitMeasure];
            }
        }

        static DBEstimatingItem()
        {
            string[] matériel = new string[] { Resources.Matériel, Resources.Main_d_oeuvre, Resources.Sous_contracteur, Resources.Équipement };
            DBEstimatingItem.itemTypeString = matériel;
            string[] pi2 = new string[] { Resources.aucun, Resources.pi, Resources.pi_2, Resources.v_3, "m", "m²", "m³" };
            DBEstimatingItem.unitMeasureString = pi2;
        }

        public DBEstimatingItem(int id, DBEstimatingItem.EstimatingItemType itemType, string description, string purchaseUnit, DBEstimatingItem.UnitMeasureType unitMeasure, double coverageValue, double coverageUnit, double priceEach, int sectionID, int subSectionID, string bidCode, bool isSystemItem)
        {
            this.ID = id;
            this.ItemType = (itemType < DBEstimatingItem.EstimatingItemType.MaterialItem || itemType > DBEstimatingItem.EstimatingItemType.count ? DBEstimatingItem.EstimatingItemType.MaterialItem : itemType);
            this.Description = description;
            this.PurchaseUnit = purchaseUnit;
            this.UnitMeasure = (unitMeasure < DBEstimatingItem.UnitMeasureType.none || unitMeasure >= DBEstimatingItem.UnitMeasureType.count ? DBEstimatingItem.UnitMeasureType.none : unitMeasure);
            this.CoverageValue = coverageValue;
            this.CoverageUnit = (coverageUnit <= 0 ? 1 : coverageUnit);
            this.PriceEach = priceEach;
            this.SectionID = sectionID;
            this.SubSectionID = subSectionID;
            this.BidCode = bidCode;
            this.IsSystemItem = isSystemItem;
            this.Dirty = false;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("ItemType = ", this.ItemType));
            Console.WriteLine(string.Concat("Description = ", this.Description));
            Console.WriteLine(string.Concat("PriceEach = ", this.PriceEach));
            Console.WriteLine(string.Concat("SectionID = ", this.SectionID));
            Console.WriteLine(string.Concat("SubSectionID = ", this.SubSectionID));
            Console.WriteLine(string.Concat("BidCode = ", this.BidCode));
            Console.WriteLine(string.Concat("IsSystemItem = ", this.IsSystemItem));
            Console.WriteLine(string.Concat("IsDirty = ", this.Dirty));
        }

        public DBEstimatingItem Duplicate(int id)
        {
            return new DBEstimatingItem(id, this.ItemType, this.Description, this.PurchaseUnit, this.UnitMeasure, this.CoverageValue, this.CoverageUnit, this.PriceEach, this.SectionID, this.SubSectionID, this.BidCode, this.IsSystemItem);
        }

        public static string GetItemTypeCaption(DBEstimatingItem.EstimatingItemType itemType)
        {
            if (itemType < DBEstimatingItem.EstimatingItemType.MaterialItem || itemType >= DBEstimatingItem.EstimatingItemType.count)
            {
                return "";
            }
            return DBEstimatingItem.itemTypeString[(int)itemType];
        }

        public static string GetUnitMeasureCaption(DBEstimatingItem.UnitMeasureType unitMeasure)
        {
            if (unitMeasure < DBEstimatingItem.UnitMeasureType.none || unitMeasure >= DBEstimatingItem.UnitMeasureType.count)
            {
                return "";
            }
            return DBEstimatingItem.unitMeasureString[(int)unitMeasure];
        }

        public bool MatchResultType(ExtensionResult.ExtensionResultTypeEnum resultType)
        {
            switch (resultType)
            {
                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeLength:
                    {
                        if (this.PurchaseUnit == "m")
                        {
                            return true;
                        }
                        return this.PurchaseUnit == Resources.pi;
                    }
                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeArea:
                    {
                        if (this.PurchaseUnit == "m²")
                        {
                            return true;
                        }
                        return this.PurchaseUnit == Resources.pi_2;
                    }
                case ExtensionResult.ExtensionResultTypeEnum.ResultTypeVolume:
                    {
                        if (this.PurchaseUnit == "m³")
                        {
                            return true;
                        }
                        return this.PurchaseUnit == Resources.pi_3;
                    }
            }
            if (!(this.PurchaseUnit != "m") || !(this.PurchaseUnit != Resources.pi) || !(this.PurchaseUnit != "m²") || !(this.PurchaseUnit != Resources.pi_2) || !(this.PurchaseUnit != "m³"))
            {
                return false;
            }
            return this.PurchaseUnit != Resources.pi_3;
        }

        public enum EstimatingItemType
        {
            MaterialItem,
            LaborItem,
            SubcontractItem,
            EquipmentItem,
            count
        }

        public enum UnitMeasureType
        {
            none,
            lin_ft,
            sq_ft,
            cu_yd,
            m,
            sq_m,
            cu_m,
            count
        }
    }
}