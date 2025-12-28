using System;

namespace QuoterPlan
{
	public class ExtraDataTooLongException : TurboActivateException
	{
		public ExtraDataTooLongException() : base("The \"extra data\" was too long. You're limited to 255 UTF-8 characters. Or, on Windows, a Unicode string that will convert into 255 UTF-8 characters or less.")
		{
		}
	}
}
