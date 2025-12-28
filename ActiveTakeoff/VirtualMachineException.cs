using System;

namespace QuoterPlan
{
	public class VirtualMachineException : TurboActivateException
	{
		public VirtualMachineException() : base("The function failed because this instance of your program is running inside a virtual machine / hypervisor and you've prevented the function from running inside a VM.")
		{
		}
	}
}
