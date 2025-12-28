using System;

namespace QuoterPlan
{
	internal class MathParserOperator
	{
		internal MathParserOperator(string _operator, int arguments, int precedence)
		{
			this.op = _operator;
			this.args = arguments;
			this.prec = precedence;
		}

		internal int precedence()
		{
			return this.prec;
		}

		internal string getOperator()
		{
			return this.op;
		}

		internal int arguments()
		{
			return this.args;
		}

		private string op = "";

		private int args;

		private int prec = int.MaxValue;
	}
}
