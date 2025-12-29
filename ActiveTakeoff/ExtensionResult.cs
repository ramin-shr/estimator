using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ExtensionResult
    {
        public string Caption
        {
            get;
            protected set;
        }

        public string Condition
        {
            get;
            protected set;
        }

        public string Formula
        {
            get;
            protected set;
        }

        public bool IsEstimatingItem
        {
            get;
            protected set;
        }

        public int ItemID
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        public ExtensionResult.ExtensionResultTypeEnum ResultType
        {
            get;
            protected set;
        }

        public int SectionID
        {
            get;
            protected set;
        }

        public bool ShowInLegend
        {
            get;
            protected set;
        }

        public int SubSectionID
        {
            get;
            protected set;
        }

        public string Unit
        {
            get;
            protected set;
        }

        public ExtensionResult()
        {
        }

        public ExtensionResult(string name, string caption, string unit, int itemID, int sectionID, int subSectionID, string formula, string condition, ExtensionResult.ExtensionResultTypeEnum resultType, bool showInLegend, bool isEstimatingItem)
        {
            this.Name = name;
            this.Caption = caption;
            this.Unit = unit;
            this.ItemID = itemID;
            this.SectionID = sectionID;
            this.SubSectionID = subSectionID;
            this.Formula = formula;
            this.Condition = condition;
            this.ResultType = resultType;
            this.ShowInLegend = showInLegend;
            this.IsEstimatingItem = (isEstimatingItem ? true : itemID != -1);
        }

        public void Clear()
        {
            this.Name = "";
            this.Caption = "";
            this.Unit = "";
            this.ItemID = -1;
            this.SectionID = -1;
            this.SubSectionID = -1;
            this.Formula = "";
            this.Condition = "";
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("Caption = ", this.Caption));
            Console.WriteLine(string.Concat("Unit = ", this.Unit));
            Console.WriteLine(string.Concat("ItemID = ", this.ItemID));
            Console.WriteLine(string.Concat("SectionID = ", this.SectionID));
            Console.WriteLine(string.Concat("SubSectionID = ", this.SubSectionID));
            Console.WriteLine(string.Concat("Formula= ", this.Formula));
            Console.WriteLine(string.Concat("Condition= ", this.Condition));
            Console.WriteLine(string.Concat("ResultType = ", this.ResultType));
            Console.WriteLine(string.Concat("ShowInLegend = ", this.ShowInLegend));
            Console.WriteLine(string.Concat("IsEstimatingItem = ", this.IsEstimatingItem));
        }

        public enum ExtensionResultTypeEnum
        {
            ResultTypeUnit,
            ResultTypeLength,
            ResultTypeArea,
            ResultTypeVolume,
            ResultTypeCurrency,
            ResultTypeCustom,
            ResultTypeEnumCount
        }
    }
}