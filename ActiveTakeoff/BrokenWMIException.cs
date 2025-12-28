using System;

namespace QuoterPlan
{
	public class BrokenWMIException : TurboActivateException
	{
		public BrokenWMIException() : base("The WMI repository on the computer is broken. To fix the WMI repository see the instructions here: https://wyday.com/limelm/help/faq/#fix-broken-wmi")
		{
		}
	}
}
