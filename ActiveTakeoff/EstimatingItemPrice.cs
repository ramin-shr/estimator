using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class EstimatingItemPrice
    {
        public double CostEach
        {
            get;
            set;
        }

        public string ExtensionID
        {
            get;
            private set;
        }

        public int GroupID
        {
            get;
            private set;
        }

        public string Key
        {
            get
            {
                string[] str = new string[] { this.GroupID.ToString(), ";", this.ExtensionID, ";", this.ResultID, ";" };
                return string.Concat(str);
            }
        }

        public double MarkupEach
        {
            get;
            set;
        }

        public string ResultID
        {
            get;
            private set;
        }

        public UnitScale.UnitSystem SystemType
        {
            get;
            set;
        }

        public EstimatingItemPrice(string key, double costEach, double markupEach, UnitScale.UnitSystem systemType)
        {
            try
            {
                string[] fields = Utilities.GetFields(key, ';', StringSplitOptions.None);
                this.GroupID = Utilities.ConvertToInt(fields[0]);
                this.ExtensionID = fields[1].ToString();
                this.ResultID = fields[2].ToString();
                this.CostEach = costEach;
                this.MarkupEach = markupEach;
                this.SystemType = systemType;
            }
            catch
            {
            }
        }

        public EstimatingItemPrice(EstimatingItem estimatingItem, UnitScale.UnitSystem systemType)
        {
            this.GroupID = estimatingItem.GroupID;
            this.ExtensionID = estimatingItem.ExtensionID;
            this.ResultID = estimatingItem.ResultID;
            this.CostEach = estimatingItem.CostEach;
            this.MarkupEach = estimatingItem.MarkupEach;
            this.SystemType = systemType;
        }

        public EstimatingItemPrice(int groupID, string extensionID, string resultID, double costEach, double markupEach, UnitScale.UnitSystem systemType)
        {
            this.GroupID = groupID;
            this.ExtensionID = extensionID;
            this.ResultID = resultID;
            this.CostEach = costEach;
            this.MarkupEach = markupEach;
            this.SystemType = systemType;
        }

        public static string GenerateKey(EstimatingItem estimatingItem)
        {
            string[] str = new string[] { estimatingItem.GroupID.ToString(), ";", estimatingItem.ExtensionID, ";", estimatingItem.ResultID, ";" };
            return string.Concat(str);
        }
    }
}