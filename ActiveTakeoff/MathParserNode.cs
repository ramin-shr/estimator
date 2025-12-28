using System;

namespace QuoterPlan
{
	internal class MathParserNode
	{
		internal MathParserNode(string _operator, MathParserNode _arg1, MathParserNode _arg2)
		{
			this._arg1 = _arg1;
			this._arg2 = _arg2;
			this._operator = _operator;
			this.args = 2;
			this.type = MathParserNode.TYPE_EXPRESSION;
		}

		internal MathParserNode(string _operator, MathParserNode _arg1)
		{
			this._arg1 = _arg1;
			this._operator = _operator;
			this.args = 1;
			this.type = MathParserNode.TYPE_EXPRESSION;
		}

		internal MathParserNode(string variable)
		{
			this.variable = variable;
			this.type = MathParserNode.TYPE_VARIABLE;
		}

		internal MathParserNode(double value)
		{
			this.value = value;
			this.type = MathParserNode.TYPE_CONSTANT;
		}

		internal string getOperator()
		{
			return this._operator;
		}

		internal double getValue()
		{
			return this.value;
		}

		internal string getVariable()
		{
			return this.variable;
		}

		internal int arguments()
		{
			return this.args;
		}

		internal int getType()
		{
			return this.type;
		}

		internal MathParserNode arg1()
		{
			return this._arg1;
		}

		internal MathParserNode arg2()
		{
			return this._arg2;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static MathParserNode()
		{
		}

		internal static int TYPE_VARIABLE = 1;

		internal static int TYPE_CONSTANT = 2;

		internal static int TYPE_EXPRESSION = 3;

		internal static int TYPE_END = 4;

		internal static int TYPE_UNDEFINED = -1;

		private string _operator = "";

		private MathParserNode _arg1;

		private MathParserNode _arg2;

		private int args;

		private int type = MathParserNode.TYPE_UNDEFINED;

		private double value = double.NaN;

		private string variable = "";
	}
}
