using System;
using System.Collections;

namespace QuoterPlan
{
	public class Variables
	{
		public Variables()
		{
			this.variableList = new ArrayList();
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

		public ArrayList Collection
		{
			get
			{
				return this.variableList;
			}
		}

		public int Add(Variable variable)
		{
			return this.variableList.Add(variable);
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

		public int Count
		{
			get
			{
				return this.variableList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.variableList)
			{
				Variable variable = (Variable)obj;
				variable.Clear();
			}
			this.variableList.Clear();
		}

		public Variable Find(string name)
		{
			foreach (object obj in this.variableList)
			{
				Variable variable = (Variable)obj;
				if (variable.Name == name)
				{
					return variable;
				}
			}
			return null;
		}

		public void Dump()
		{
			foreach (object obj in this.variableList)
			{
				Variable variable = (Variable)obj;
				variable.Dump();
			}
		}

		private ArrayList variableList;
	}
}
