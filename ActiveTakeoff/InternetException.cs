using System;

namespace QuoterPlan
{
	public class InternetException : TurboActivateException
	{
		public InternetException() : base("Connection to the servers failed.")
		{
		}

		public InternetException(string message) : base(message)
		{
		}
	}
}
