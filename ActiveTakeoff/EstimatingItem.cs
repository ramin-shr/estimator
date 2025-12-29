using DevExpress.XtraEditors.DXErrorProvider;
using QuoterPlan.Properties;
using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class EstimatingItem : IDXDataErrorInfo
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

        public int ID
        {
            get;
            private set;
        }

        public string InternalUnit
        {
            get;
            set;
        }

        public int ItemID
        {
            get;
            set;
        }

        public double MarkupEach
        {
            get;
            set;
        }

        public string ObjectType
        {
            get;
            set;
        }

        public int ParentID
        {
            get;
            private set;
        }

        public double PriceEach
        {
            get
            {
                return Math.Round(this.CostEach * (1 + this.MarkupEach / 100), 2);
            }
        }

        public double PriceTotal
        {
            get
            {
                return Math.Round(this.ResultValue * this.PriceEach, 2);
            }
        }

        public string ResultID
        {
            get;
            private set;
        }

        public string ResultName
        {
            get;
            set;
        }

        public string ResultUnit
        {
            get;
            set;
        }

        public double ResultValue
        {
            get;
            set;
        }

        public EstimatingItem(int id, int parentID, int groupID, string extensionID, string resultID, string objectType, string resultName, double resultValue, string resultUnit, double costEach, double markupEach, int itemID = -1)
        {
            this.ID = id;
            this.ParentID = parentID;
            this.GroupID = groupID;
            this.ExtensionID = extensionID;
            this.ResultID = resultID;
            this.ObjectType = objectType;
            this.ResultName = resultName;
            if (resultUnit != Utilities.GetCurrencySymbol())
            {
                this.ResultValue = resultValue;
                this.ResultUnit = resultUnit;
                this.CostEach = costEach;
            }
            else
            {
                this.ResultValue = 1;
                this.ResultUnit = Resources.chaque;
                this.CostEach = resultValue;
            }
            this.MarkupEach = markupEach;
            this.InternalUnit = resultUnit;
            this.ItemID = itemID;
        }

        public void Clear()
        {
            this.ID = 0;
            this.ParentID = 0;
            this.GroupID = -1;
            this.ExtensionID = "";
            this.ResultID = "";
            this.ObjectType = "";
            this.ResultName = "";
            this.ResultValue = 0;
            this.ResultUnit = "";
        }

        void DevExpress.XtraEditors.DXErrorProvider.IDXDataErrorInfo.GetError(ErrorInfo info)
        {
            if (this.IsStringEmpty(this.ObjectType))
            {
                this.SetErrorInfo(info, "Vous devez fournir une valeur.", ErrorType.Critical);
            }
        }

        void DevExpress.XtraEditors.DXErrorProvider.IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
        {
            string str = propertyName;
            string str1 = str;
            if (str != null)
            {
                if (str1 != "Name")
                {
                    return;
                }
                if (this.IsStringEmpty(this.ObjectType))
                {
                    this.SetErrorInfo(info, "Vous devez fournir une valeur", ErrorType.Information);
                }
            }
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ID = ", this.ID));
            Console.WriteLine(string.Concat("ParentID = ", this.ParentID));
            Console.WriteLine(string.Concat("GroupID = ", this.GroupID));
            Console.WriteLine(string.Concat("ExtensionID = ", this.ExtensionID));
            Console.WriteLine(string.Concat("ResultID = ", this.ResultID));
            Console.WriteLine(string.Concat("ObjectType = ", this.ObjectType));
            Console.WriteLine(string.Concat("ResultName = ", this.ResultName));
            Console.WriteLine(string.Concat("ResultValue = ", this.ResultValue));
            Console.WriteLine(string.Concat("ResultUnit = ", this.ResultUnit));
            Console.WriteLine(string.Concat("CostEach = ", this.CostEach));
            Console.WriteLine(string.Concat("MarkupEach = ", this.MarkupEach));
            Console.WriteLine(string.Concat("PriceEach = ", this.PriceEach));
            Console.WriteLine(string.Concat("PriceTotal = ", this.PriceTotal));
        }

        private bool IsStringEmpty(string str)
        {
            if (str == null)
            {
                return false;
            }
            return str.Trim().Length == 0;
        }

        private void SetErrorInfo(ErrorInfo info, string errorText, ErrorType errorType)
        {
            info.ErrorText = errorText;
            info.ErrorType = errorType;
        }
    }
}