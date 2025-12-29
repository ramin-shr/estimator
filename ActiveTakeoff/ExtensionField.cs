using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ExtensionField
    {
        public string Caption
        {
            get;
            set;
        }

        public string Condition
        {
            get;
            protected set;
        }

        public ExtensionField.ExtensionFieldTypeEnum FieldType
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ExtensionField()
        {
        }

        public ExtensionField(string name, string caption, string condition, ExtensionField.ExtensionFieldTypeEnum fieldType)
        {
            this.Name = name;
            this.Caption = caption;
            this.Condition = condition;
            this.FieldType = fieldType;
        }

        public void Clear()
        {
            this.Name = "";
            this.Caption = "";
            this.Condition = "";
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("Caption = ", this.Caption));
            Console.WriteLine(string.Concat("Condition = ", this.Condition));
            Console.WriteLine(string.Concat("FieldType = ", this.FieldType));
        }

        public enum ExtensionFieldTypeEnum
        {
            FieldTypeDimension,
            FieldTypeInteger,
            FieldTypeDouble,
            FieldTypeCurrency,
            FieldTypeEnumCount
        }
    }
}