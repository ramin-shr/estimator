using System;

namespace QuoterPlan
{
	public class DateTimeException : TurboActivateException
	{
		public DateTimeException() : base("The activation has expired or the system time has been tampered with. Ensure your time, timezone, and date settings are correct. After fixing them restart your computer.")
		{
		}
	}
}
