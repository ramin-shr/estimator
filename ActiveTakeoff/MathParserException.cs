using System;

namespace QuoterPlan
{
	public class MathParserException : Exception
	{
		public MathParserException()
		{
		}

		public MathParserException(string message) : base(message)
		{
		}

		public MathParserException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
