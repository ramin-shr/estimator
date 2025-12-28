using System;

namespace QuoterPlan
{
	public class StringLogicalComparer
	{
		public static int Compare(string s1, string s2)
		{
			if (s1 == null && s2 == null)
			{
				return 0;
			}
			if (s1 == null)
			{
				return -1;
			}
			if (s2 == null)
			{
				return 1;
			}
			if (s1.Equals(string.Empty) && s2.Equals(string.Empty))
			{
				return 0;
			}
			if (s1.Equals(string.Empty))
			{
				return -1;
			}
			if (s2.Equals(string.Empty))
			{
				return -1;
			}
			bool flag = char.IsLetterOrDigit(s1, 0);
			bool flag2 = char.IsLetterOrDigit(s2, 0);
			if (flag && !flag2)
			{
				return 1;
			}
			if (!flag && flag2)
			{
				return -1;
			}
			int num = 0;
			int num2 = 0;
			int num3;
			for (;;)
			{
				bool flag3 = char.IsDigit(s1, num);
				bool flag4 = char.IsDigit(s2, num2);
				if (!flag3 && !flag4)
				{
					bool flag5 = char.IsLetter(s1, num);
					bool flag6 = char.IsLetter(s2, num2);
					if ((flag5 && flag6) || (!flag5 && !flag6))
					{
						if (flag5 && flag6)
						{
							num3 = char.ToLower(s1[num]).CompareTo(char.ToLower(s2[num2]));
						}
						else
						{
							num3 = s1[num].CompareTo(s2[num2]);
						}
						if (num3 != 0)
						{
							break;
						}
					}
					else
					{
						if (!flag5 && flag6)
						{
							return -1;
						}
						if (flag5 && !flag6)
						{
							return 1;
						}
					}
				}
				else if (flag3 && flag4)
				{
					num3 = StringLogicalComparer.CompareNum(s1, ref num, s2, ref num2);
					if (num3 != 0)
					{
						return num3;
					}
				}
				else
				{
					if (flag3)
					{
						return -1;
					}
					if (flag4)
					{
						return 1;
					}
				}
				num++;
				num2++;
				if (num >= s1.Length && num2 >= s2.Length)
				{
					return 0;
				}
				if (num >= s1.Length)
				{
					return -1;
				}
				if (num2 >= s2.Length)
				{
					return -1;
				}
			}
			return num3;
		}

		private static int CompareNum(string s1, ref int i1, string s2, ref int i2)
		{
			int num = i1;
			int num2 = i2;
			int num3 = i1;
			int num4 = i2;
			StringLogicalComparer.ScanNumEnd(s1, i1, ref num3, ref num);
			StringLogicalComparer.ScanNumEnd(s2, i2, ref num4, ref num2);
			int num5 = i1;
			i1 = num3 - 1;
			int num6 = i2;
			i2 = num4 - 1;
			int num7 = num3 - num;
			int num8 = num4 - num2;
			if (num7 < num8)
			{
				return -1;
			}
			if (num7 > num8)
			{
				return 1;
			}
			int j = num;
			int num9 = num2;
			while (j <= i1)
			{
				int num10 = s1[j].CompareTo(s2[num9]);
				if (num10 != 0)
				{
					return num10;
				}
				j++;
				num9++;
			}
			int num11 = num3 - num5;
			int num12 = num4 - num6;
			if (num11 == num12)
			{
				return 0;
			}
			if (num11 > num12)
			{
				return -1;
			}
			return 1;
		}

		private static void ScanNumEnd(string s, int start, ref int end, ref int nzStart)
		{
			nzStart = start;
			end = start;
			bool flag = true;
			while (char.IsDigit(s, end))
			{
				if (flag && s[end].Equals('0'))
				{
					nzStart++;
				}
				else
				{
					flag = false;
				}
				end++;
				if (end >= s.Length)
				{
					return;
				}
			}
		}

		public StringLogicalComparer()
		{
		}
	}
}
