using System;

namespace QuoterPlan
{
	public class PermissionException : TurboActivateException
	{
		public PermissionException() : base("Insufficient system permission. Either start your process as an admin / elevated user or call the function again with the TA_USER flag.")
		{
		}
	}
}
