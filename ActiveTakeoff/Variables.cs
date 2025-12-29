using System;
using System.Collections;
using System.Reflection;

namespace QuoterPlan
{
    public class Variables
    {
        private ArrayList variableList;

        public ArrayList Collection
        {
            get
            {
                return this.variableList;
            }
        }

        public int Count
        {
            get
            {
                return this.variableList.Count;
            }
        }

        public Variable this[int index]
        {
            get
            {
                if (index < 0 || index >= this.variableList.Count)
                {
                    return null;
                }
                return (Variable)this.variableList[index];
            }
        }

        public Variables()
        {
            this.variableList = new ArrayList();
        }

        public int Add(Variable variable)
        {
            return this.variableList.Add(variable);
        }

        public void Clear()
        {
            foreach (Variable variable in this.variableList)
            {
                variable.Clear();
            }
            this.variableList.Clear();
        }

        public void Dump()
        {
            foreach (Variable variable in this.variableList)
            {
                variable.Dump();
            }
        }

        public Variable Find(string name)
        {
            Variable variable;
            IEnumerator enumerator = this.variableList.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Variable current = (Variable)enumerator.Current;
                    if (current.Name != name)
                    {
                        continue;
                    }
                    variable = current;
                    return variable;
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
            return variable;
        }

        public void Insert(int index, Variable variable)
        {
            this.variableList.Insert(index, variable);
        }

        public void Remove(Variable variable)
        {
            this.variableList.Remove(variable);
        }

        public void RemoveAt(int index)
        {
            this.variableList.RemoveAt(index);
        }
    }
}