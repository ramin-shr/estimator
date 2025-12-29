using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class PresetChoice
    {
        public string ChoiceCaption
        {
            get;
            set;
        }

        public string ChoiceElementCaption
        {
            get;
            set;
        }

        public string ChoiceElementName
        {
            get;
            set;
        }

        public string ChoiceName
        {
            get;
            private set;
        }

        public Variables Variables
        {
            get;
            private set;
        }

        public PresetChoice(string choiceName, string choiceElementName)
        {
            this.ChoiceName = choiceName;
            this.ChoiceElementName = choiceElementName;
            this.Variables = new Variables();
        }

        public void Clear()
        {
            this.ChoiceName = "";
            this.ChoiceCaption = "";
            this.ChoiceElementName = "";
            this.ChoiceElementCaption = "";
            this.Variables.Clear();
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("ChoiceName = ", this.ChoiceName));
            Console.WriteLine(string.Concat("ChoiceCaption = ", this.ChoiceCaption));
            Console.WriteLine(string.Concat("ChoiceElementName = ", this.ChoiceElementName));
            Console.WriteLine(string.Concat("ChoiceElementCaption = ", this.ChoiceElementCaption));
            this.Variables.Dump();
        }
    }
}