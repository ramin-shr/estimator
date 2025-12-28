using System;

namespace QuoterPlan
{
	public class InternetTLSException : InternetException
	{
		public InternetTLSException() : base("The secure connection to the activation servers failed due to a TLS or certificate error. More information here: https://wyday.com/limelm/help/faq/#internet-error")
		{
		}
	}
}
