using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
	public class CustomRenderingResult
	{
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Name>k__BackingField = value;
			}
		}

		public double Result
		{
			[CompilerGenerated]
			get
			{
				return this.<Result>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Result>k__BackingField = value;
			}
		}

		public ExtensionResult.ExtensionResultTypeEnum ResultType
		{
			[CompilerGenerated]
			get
			{
				return this.<ResultType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ResultType>k__BackingField = value;
			}
		}

		public CustomRenderingResult(string name, double result, ExtensionResult.ExtensionResultTypeEnum resultType)
		{
			this.Name = name;
			this.Result = result;
			this.ResultType = resultType;
		}

		public CustomRenderingResult Clone()
		{
			return new CustomRenderingResult(this.Name, this.Result, this.ResultType);
		}

		[CompilerGenerated]
		private string <Name>k__BackingField;

		[CompilerGenerated]
		private double <Result>k__BackingField;

		[CompilerGenerated]
		private ExtensionResult.ExtensionResultTypeEnum <ResultType>k__BackingField;
	}
}
