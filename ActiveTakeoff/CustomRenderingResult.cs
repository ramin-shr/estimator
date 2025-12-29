using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class CustomRenderingResult
    {
        public string Name
        {
            get;
            protected set;
        }

        public double Result
        {
            get;
            set;
        }

        public ExtensionResult.ExtensionResultTypeEnum ResultType
        {
            get;
            protected set;
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
    }
}