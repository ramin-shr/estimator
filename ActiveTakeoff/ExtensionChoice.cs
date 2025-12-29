using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class ExtensionChoice
    {
        private string defaultChoice;

        public string Caption
        {
            get;
            private set;
        }

        public string Condition
        {
            get;
            protected set;
        }

        public string DefaultChoice
        {
            get
            {
                string str = (UnitScale.DefaultUnitSystem() == UnitScale.UnitSystem.imperial ? this.DefaultChoiceImperial : this.DefaultChoiceMetric);
                str = (str == "" ? this.defaultChoice : str);
                return str;
            }
            private set
            {
                this.defaultChoice = value;
            }
        }

        public string DefaultChoiceImperial
        {
            get;
            private set;
        }

        public string DefaultChoiceMetric
        {
            get;
            private set;
        }

        public ExtensionChoiceElements Elements
        {
            get;
            private set;
        }

        public bool Hidden
        {
            get;
            set;
        }

        public string Name
        {
            get;
            private set;
        }

        public ExtensionChoice(string name, string caption, string defaultChoice, string defaultChoiceImperial, string defaultChoiceMetric, string condition, bool hidden)
        {
            this.Name = name;
            this.Caption = caption;
            this.DefaultChoice = defaultChoice;
            this.DefaultChoiceImperial = defaultChoiceImperial;
            this.DefaultChoiceMetric = defaultChoiceMetric;
            this.Hidden = hidden;
            this.Condition = condition;
            this.Elements = new ExtensionChoiceElements();
        }

        public void Clear()
        {
            this.Name = "";
            this.Caption = "";
            this.DefaultChoice = "";
            this.DefaultChoiceImperial = "";
            this.DefaultChoiceMetric = "";
            this.Condition = "";
            this.Hidden = false;
            this.Elements.Clear();
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat("Name = ", this.Name));
            Console.WriteLine(string.Concat("Caption = ", this.Caption));
            Console.WriteLine(string.Concat("DefaultChoice= ", this.DefaultChoice));
            Console.WriteLine(string.Concat("DefaultChoiceImperial= ", this.DefaultChoiceImperial));
            Console.WriteLine(string.Concat("DefaultChoiceMetric= ", this.DefaultChoiceMetric));
            Console.WriteLine(string.Concat("Condition= ", this.Condition));
            Console.WriteLine(string.Concat("Hidden= ", this.Hidden));
            this.Elements.Dump();
        }

        public ExtensionChoiceElement FindElement(string name)
        {
            ExtensionChoiceElement extensionChoiceElement;
            IEnumerator enumerator = this.Elements.Collection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    ExtensionChoiceElement current = (ExtensionChoiceElement)enumerator.Current;
                    if (current.Name != name)
                    {
                        continue;
                    }
                    extensionChoiceElement = current;
                    return extensionChoiceElement;
                }
                return null;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return extensionChoiceElement;
        }

        public ExtensionChoiceElement GetDefaultElement()
        {
            ExtensionChoiceElement item;
            try
            {
                if (this.DefaultChoice != "")
                {
                    foreach (ExtensionChoiceElement collection in this.Elements.Collection)
                    {
                        if (collection.Name != this.DefaultChoice)
                        {
                            continue;
                        }
                        item = collection;
                        return item;
                    }
                    item = this.Elements[0];
                }
                else
                {
                    item = this.Elements[0];
                }
            }
            catch
            {
                item = null;
            }
            return item;
        }
    }
}