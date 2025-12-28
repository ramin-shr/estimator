using System;

namespace QuoterPlan
{
	public class AlreadyActivatedException : TurboActivateException
	{
		public AlreadyActivatedException() : base("You can't use a product key because your app is already activated with a product key. To use a new product key, then first deactivate using either the Deactivate() or DeactivationRequestToFile().")
		{
		}
	}
}
