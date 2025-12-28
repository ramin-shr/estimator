using System;

namespace QuoterPlan
{
	public class InternetTimeoutException : InternetException
	{
		public InternetTimeoutException() : base("The connection to the server timed out because a long period of time elapsed since the last data was sent or received.")
		{
		}
	}
}
