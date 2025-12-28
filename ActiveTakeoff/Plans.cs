using System;
using System.Collections;

namespace QuoterPlan
{
	public class Plans
	{
		public Plans()
		{
			this.planList = new ArrayList();
		}

		public Plan this[int index]
		{
			get
			{
				if (index < 0 || index >= this.planList.Count)
				{
					return null;
				}
				return (Plan)this.planList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.planList;
			}
		}

		public int Count
		{
			get
			{
				return this.planList.Count;
			}
		}

		public void Clear()
		{
			foreach (object obj in this.planList)
			{
				Plan plan = (Plan)obj;
				plan.Clear();
			}
			this.planList.Clear();
		}

		public int Add(Plan plan)
		{
			return this.planList.Add(plan);
		}

		public void Insert(int index, Plan plan)
		{
			this.planList.Insert(index, plan);
		}

		public void Remove(Plan plan)
		{
			plan.Clear();
			this.planList.Remove(plan);
		}

		public void RemoveAt(int index)
		{
			try
			{
				this[index].Clear();
			}
			catch
			{
			}
			this.planList.RemoveAt(index);
		}

		public Plan FindPlan(string name)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Name == name)
				{
					return this[i];
				}
			}
			return null;
		}

		public int PlanIndex(Plan plan)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Equals(plan))
				{
					return i;
				}
			}
			return -1;
		}

		public string FindFreePlanName(string prefix)
		{
			string text = "";
			return this.FindFreePlanName(prefix, ref text);
		}

		public string FindFreePlanName(string prefix, ref string suffix)
		{
			int num = 0;
			string arg = prefix + " ";
			do
			{
				num++;
			}
			while (this.FindPlan(arg + num) != null);
			suffix = num.ToString();
			return arg + num;
		}

		private ArrayList planList;
	}
}
