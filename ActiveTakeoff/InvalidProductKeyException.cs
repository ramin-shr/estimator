using System;

namespace QuoterPlan
{
	public class InvalidProductKeyException : TurboActivateException
	{
		public InvalidProductKeyException() : base("The product key is invalid or there's no product key.")
		{
		}
	}
}
