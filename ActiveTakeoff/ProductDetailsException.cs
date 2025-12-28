using System;

namespace QuoterPlan
{
	public class ProductDetailsException : TurboActivateException
	{
		public ProductDetailsException() : base("The product details file \"TurboActivate.dat\" failed to load. It's either missing or corrupt.")
		{
		}
	}
}
