using System;

namespace QuoterPlan
{
	public class COMException : TurboActivateException
	{
		public COMException() : base("CoInitializeEx failed. Re-enable Windows Management Instrumentation (WMI) service. Contact your system admin for more information.")
		{
		}
	}
}
