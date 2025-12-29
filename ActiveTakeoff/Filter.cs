using System;
using System.Collections;

namespace QuoterPlan
{
	public class Filter
	{
		public Report.ReportOrderEnum Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		public Filter()
		{
			this.filterList = new ArrayList();
		}

		public FilterElement this[int index]
		{
			get
			{
				if (index < 0 || index >= this.filterList.Count)
				{
					return null;
				}
				return (FilterElement)this.filterList[index];
			}
		}

		public ArrayList Collection
		{
			get
			{
				return this.filterList;
			}
		}

		public int Count
		{
			get
			{
				return this.filterList.Count;
			}
		}

		public void Clear()
		{
			this.filterList.Clear();
		}

		public void LoadFromString(string filterString, Report.ReportOrderEnum order)
		{
			this.Clear();
			this.Order = order;
			string[] fields = Utilities.GetFields(filterString, '|');
			foreach (string originalString in fields)
			{
				string planName = "";
				string layerName = "";
				int groupID = -1;
				string[] fields2 = Utilities.GetFields(originalString, ';');
				foreach (string originalString2 in fields2)
				{
					string[] fields3 = Utilities.GetFields(originalString2, ':');
					try
					{
						bool flag = false;
						string text = fields3[0];
						string text2 = fields3[1];
						string a = fields3[2];
						string a2;
						if ((a2 = text) != null)
						{
							if (!(a2 == "#PLAN"))
							{
								if (!(a2 == "#LAYER"))
								{
									if (a2 == "#GROUP")
									{
										groupID = Utilities.ConvertToInt(text2);
										if (order == Report.ReportOrderEnum.ReportOrderByObjects)
										{
											planName = "";
										}
										flag = true;
									}
								}
								else if (order == Report.ReportOrderEnum.ReportOrderByPlans)
								{
									layerName = text2;
									groupID = -1;
									flag = true;
								}
							}
							else
							{
								planName = text2;
								if (order == Report.ReportOrderEnum.ReportOrderByPlans)
								{
									layerName = "";
									groupID = -1;
								}
								flag = true;
							}
						}
						if (flag && a == "*")
						{
							this.Add(planName, layerName, groupID);
						}
					}
					catch
					{
					}
				}
			}
		}

		private int Add(string planName, string layerName, int groupID)
		{
			return this.filterList.Add(new FilterElement(planName, layerName, groupID));
		}

        public bool QueryFilter(string planName, string layerName, int groupID, bool layerCanBeEmpty = false)
        {
            foreach (object item in this.filterList)
            {
                FilterElement filter = (FilterElement)item;

                bool planMatches = (planName == filter.PlanName);
                bool layerMatches = (layerName == filter.LayerName) || (layerName == "" && layerCanBeEmpty);
                bool groupMatches = (groupID == filter.GroupID);

                if (planMatches && layerMatches && groupMatches)
                    return true;
            }

            return false;
        }

        public bool QueryFilter(string planName, int groupID)
		{
			foreach (object obj in this.filterList)
			{
				FilterElement filterElement = (FilterElement)obj;
				if (planName == filterElement.PlanName && groupID == filterElement.GroupID)
				{
					return true;
				}
			}
			return false;
		}

		public static string Rename(string filterString, string objectType, string oldName, string newName, string planName = "")
		{
			string text = "";
			string a = "";
			string[] fields = Utilities.GetFields(filterString, '|');
			foreach (string originalString in fields)
			{
				string[] fields2 = Utilities.GetFields(originalString, ';');
				foreach (string originalString2 in fields2)
				{
					string[] fields3 = Utilities.GetFields(originalString2, ':');
					try
					{
						string text2 = fields3[1];
						if (fields3[0] == "#PLAN")
						{
							a = text2;
						}
						if (fields3[0] == objectType && (objectType == "#PLAN" || (objectType == "#LAYER" && a == planName)) && text2 == oldName)
						{
							text2 = newName;
						}
						string text3 = text;
						text = string.Concat(new string[]
						{
							text3,
							fields3[0],
							":",
							text2,
							":",
							fields3[2]
						});
					}
					catch
					{
					}
					text += ";";
				}
				text += "|";
			}
			return text;
		}

		public void PlanDelete()
		{
		}

		public void LayerDelete()
		{
		}

		public void GroupDelete()
		{
		}

		private ArrayList filterList;

		private Report.ReportOrderEnum order;
	}
}
