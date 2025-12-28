using System;
using System.Collections;
using System.Text;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public class MathParser
	{
		public MathParser()
		{
			this.ops = new Hashtable(52);
			this.spconst = new Hashtable(12);
			this.trees = new Hashtable(101);
			this.ops.Add("^", new MathParserOperator("^", 2, 3));
			this.ops.Add("+", new MathParserOperator("+", 2, 6));
			this.ops.Add("-", new MathParserOperator("-", 2, 6));
			this.ops.Add("/", new MathParserOperator("/", 2, 4));
			this.ops.Add("*", new MathParserOperator("*", 2, 4));
			this.ops.Add("$cos", new MathParserOperator("cos", 1, 2));
			this.ops.Add("$sin", new MathParserOperator("sin", 1, 2));
			this.ops.Add("$exp", new MathParserOperator("exp", 1, 2));
			this.ops.Add("$ln", new MathParserOperator("ln", 1, 2));
			this.ops.Add("$tan", new MathParserOperator("tan", 1, 2));
			this.ops.Add("$acos", new MathParserOperator("acos", 1, 2));
			this.ops.Add("$asin", new MathParserOperator("asin", 1, 2));
			this.ops.Add("$atan", new MathParserOperator("atan", 1, 2));
			this.ops.Add("$cosh", new MathParserOperator("cosh", 1, 2));
			this.ops.Add("$sinh", new MathParserOperator("sinh", 1, 2));
			this.ops.Add("$tanh", new MathParserOperator("tanh", 1, 2));
			this.ops.Add("$sqrt", new MathParserOperator("sqrt", 1, 2));
			this.ops.Add("$cotan", new MathParserOperator("cotan", 1, 2));
			this.ops.Add("$fpart", new MathParserOperator("fpart", 1, 2));
			this.ops.Add("$acotan", new MathParserOperator("acotan", 1, 2));
			this.ops.Add("$round", new MathParserOperator("round", 1, 2));
			this.ops.Add("$ceil", new MathParserOperator("ceil", 1, 2));
			this.ops.Add("$floor", new MathParserOperator("floor", 1, 2));
			this.ops.Add("$fac", new MathParserOperator("fac", 1, 2));
			this.ops.Add("$sfac", new MathParserOperator("sfac", 1, 2));
			this.ops.Add("$abs", new MathParserOperator("abs", 1, 2));
			this.ops.Add("$log", new MathParserOperator("log", 1, 2));
			this.ops.Add("%", new MathParserOperator("%", 2, 4));
			this.ops.Add(">", new MathParserOperator(">", 2, 7));
			this.ops.Add("<", new MathParserOperator("<", 2, 7));
			this.ops.Add("&&", new MathParserOperator("&&", 2, 8));
			this.ops.Add("==", new MathParserOperator("==", 2, 7));
			this.ops.Add("!=", new MathParserOperator("!=", 2, 7));
			this.ops.Add("||", new MathParserOperator("||", 2, 9));
			this.ops.Add("!", new MathParserOperator("!", 1, 1));
			this.ops.Add(">=", new MathParserOperator(">=", 2, 7));
			this.ops.Add("<=", new MathParserOperator("<=", 2, 7));
			this.spconst.Add("euler", 2.718281828459045);
			this.spconst.Add("pi", 3.141592653589793);
			this.spconst.Add("nan", double.NaN);
			this.spconst.Add("infinity", double.PositiveInfinity);
			this.spconst.Add("true", 1.0);
			this.spconst.Add("false", 0.0);
			this.maxoplength = 6;
			this.sb_init = 50;
			this.bRequireParentheses = true;
			this.bImplicitMultiplication = true;
		}

		public bool RequireParentheses
		{
			get
			{
				return this.bRequireParentheses;
			}
			set
			{
				this.bRequireParentheses = value;
			}
		}

		public bool ImplicitMultiplication
		{
			get
			{
				return this.bImplicitMultiplication;
			}
			set
			{
				this.bImplicitMultiplication = value;
			}
		}

		public double Parse(string exp, Hashtable tbl)
		{
			if (exp == null || exp.Equals(""))
			{
				throw new MathParserException(Resources.First_argument_to_method_Parse_is_null_or_empty);
			}
			if (tbl == null)
			{
				return this.Parse(exp, new Hashtable());
			}
			this.htbl = tbl;
			string text = this.skipSpaces(exp);
			this.sb_init = text.Length;
			double result;
			try
			{
				double num;
				if (this.trees.ContainsKey(text))
				{
					num = this.toValue((MathParserNode)this.trees[text]);
				}
				else
				{
					this.Syntax(text);
					MathParserNode mathParserNode = this.parse(this.putMult(this.parseE(text)));
					num = this.toValue(mathParserNode);
					this.trees.Add(text, mathParserNode);
				}
				result = num;
			}
			catch (Exception ex)
			{
				throw new MathParserException(ex.Message);
			}
			return result;
		}

		private void Syntax(string exp)
		{
			int i = 0;
			if (!this.matchParant(exp))
			{
				throw new MathParserException(Resources.Unbalanced_parenthesis);
			}
			int length = exp.Length;
			while (i < length)
			{
				try
				{
					string op;
					if ((op = this.getOp(exp, i)) != null)
					{
						int length2 = op.Length;
						i += length2;
						if (this.bRequireParentheses && !this.isTwoArgOp(op) && exp[i] != '(')
						{
							throw new MathParserException(Resources.Paranthesis_required_for_arguments + " " + exp.Substring(i - length2));
						}
						string op2 = this.getOp(exp, i);
						if (op2 != null && this.isTwoArgOp(op2) && !op2.Equals("+") && !op2.Equals("-"))
						{
							throw new MathParserException(Resources.Syntax_error_near + " " + exp.Substring(i - length2));
						}
					}
					else
					{
						if (!this.isAlpha(exp[i]) && !this.isConstant(exp[i]) && !this.isAllowedSym(exp[i]))
						{
							throw new MathParserException(Resources.Syntax_error_near + " " + exp.Substring(i));
						}
						i++;
					}
				}
				catch (IndexOutOfRangeException)
				{
					i++;
				}
			}
		}

		private string putMult(string exp)
		{
			if (!this.bImplicitMultiplication)
			{
				return exp;
			}
			int i = 0;
			int num = 0;
			string text = null;
			StringBuilder stringBuilder = new StringBuilder(exp);
			int length = exp.Length;
			while (i < length)
			{
				try
				{
					if ((text = this.getOp(exp, i)) != null && !this.isTwoArgOp(text) && i - 1 >= 0 && this.isAlpha(exp[i - 1]))
					{
						stringBuilder.Insert(i + num, "*");
						num++;
					}
					else if (this.isAlpha(exp[i]) && i - 1 >= 0 && this.isConstant(exp[i - 1]))
					{
						stringBuilder.Insert(i + num, "*");
						num++;
					}
					else if (exp[i] == '(' && i - 1 >= 0 && this.isConstant(exp[i - 1]))
					{
						stringBuilder.Insert(i + num, "*");
						num++;
					}
					else if (this.isAlpha(exp[i]) && i - 1 >= 0 && exp[i - 1] == ')')
					{
						stringBuilder.Insert(i + num, "*");
						num++;
					}
					else if (exp[i] == '(' && i - 1 >= 0 && exp[i - 1] == ')')
					{
						stringBuilder.Insert(i + num, "*");
						num++;
					}
					else if (exp[i] == '(' && i - 1 >= 0 && this.isAlpha(exp[i - 1]) && this.backTrack(exp.Substring(0, i)) == null)
					{
						stringBuilder.Insert(i + num, "*");
						num++;
					}
				}
				catch
				{
				}
				if (text != null)
				{
					i += text.Length;
				}
				else
				{
					i++;
				}
				text = null;
			}
			return stringBuilder.ToString();
		}

		private string parseE(string exp)
		{
			StringBuilder stringBuilder = new StringBuilder(exp);
			int i;
			int num = i = 0;
			int length = exp.Length;
			while (i < length)
			{
				try
				{
					if (exp[i] == 'e' && i - 1 >= 0 && char.IsDigit(exp[i - 1]) && ((i + 1 < length && char.IsDigit(exp[i + 1])) || (i + 2 < length && (exp[i + 1] == '-' || exp[i + 1] == '+') && char.IsDigit(exp[i + 2]))))
					{
						stringBuilder[i + num] = '*';
						stringBuilder.Insert(i + num + 1, "10^");
						num += 3;
					}
				}
				catch
				{
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		private MathParserNode parse(string exp)
		{
			MathParserNode mathParserNode = null;
			int num = 0;
			int length = exp.Length;
			if (length == 0)
			{
				throw new MathParserException(Resources.Wrong_number_of_arguments_to_operator);
			}
			int num2;
			if (exp[0] == '(' && (num2 = this.match(exp, 0)) == length - 1)
			{
				return this.parse(exp.Substring(1, num2 - 1));
			}
			if (this.isVariable(exp))
			{
				if (this.spconst.ContainsKey(exp))
				{
					return new MathParserNode((double)this.spconst[exp]);
				}
				return new MathParserNode(exp);
			}
			else
			{
				if (!this.isAllNumbers(exp))
				{
					goto IL_2F3;
				}
				try
				{
					return new MathParserNode(double.Parse(exp));
				}
				catch (FormatException)
				{
					throw new MathParserException(string.Concat(new string[]
					{
						Resources.Syntax_error,
						" ",
						exp,
						" ",
						Resources.not_using_regional_decimal_separator
					}));
				}
				IL_E5:
				string op;
				if ((op = this.getOp(exp, num)) == null)
				{
					string text = this.arg(null, exp, num);
					op = this.getOp(exp, num + text.Length);
					if (op == null)
					{
						throw new Exception(Resources.Missing_operator);
					}
					if (this.isTwoArgOp(op))
					{
						string text2 = this.arg(op, exp, num + text.Length + op.Length);
						if (text2.Equals(""))
						{
							throw new Exception(Resources.Wrong_number_of_arguments_to_operator + " " + op);
						}
						mathParserNode = new MathParserNode(op, this.parse(text), this.parse(text2));
						num += text.Length + op.Length + text2.Length;
					}
					else
					{
						if (text.Equals(""))
						{
							throw new Exception(Resources.Wrong_number_of_arguments_to_operator + " " + op);
						}
						mathParserNode = new MathParserNode(op, this.parse(text));
						num += text.Length + op.Length;
					}
				}
				else if (this.isTwoArgOp(op))
				{
					string text = this.arg(op, exp, num + op.Length);
					if (text.Equals(""))
					{
						throw new Exception(Resources.Wrong_number_of_arguments_to_operator + " " + op);
					}
					if (mathParserNode == null)
					{
						if (!op.Equals("+") && !op.Equals("-"))
						{
							throw new Exception(Resources.Wrong_number_of_arguments_to_operator + " " + op);
						}
						mathParserNode = new MathParserNode(0.0);
					}
					mathParserNode = new MathParserNode(op, mathParserNode, this.parse(text));
					num += text.Length + op.Length;
				}
				else
				{
					string text = this.arg(op, exp, num + op.Length);
					if (text.Equals(""))
					{
						throw new Exception(Resources.Wrong_number_of_arguments_to_operator + " " + op);
					}
					mathParserNode = new MathParserNode(op, this.parse(text));
					num += text.Length + op.Length;
				}
				IL_2F3:
				if (num >= length)
				{
					return mathParserNode;
				}
				goto IL_E5;
			}
		}

		private double toValue(MathParserNode tree)
		{
			if (tree.getType() == MathParserNode.TYPE_CONSTANT)
			{
				return tree.getValue();
			}
			if (tree.getType() != MathParserNode.TYPE_VARIABLE)
			{
				string @operator = tree.getOperator();
				MathParserNode tree2 = tree.arg1();
				if (tree.arguments() == 2)
				{
					MathParserNode tree3 = tree.arg2();
					if (@operator.Equals("+"))
					{
						return this.toValue(tree2) + this.toValue(tree3);
					}
					if (@operator.Equals("-"))
					{
						return this.toValue(tree2) - this.toValue(tree3);
					}
					if (@operator.Equals("*"))
					{
						return this.toValue(tree2) * this.toValue(tree3);
					}
					if (@operator.Equals("/"))
					{
						return this.toValue(tree2) / this.toValue(tree3);
					}
					if (@operator.Equals("^"))
					{
						return Math.Pow(this.toValue(tree2), this.toValue(tree3));
					}
					if (@operator.Equals("%"))
					{
						return this.toValue(tree2) % this.toValue(tree3);
					}
					if (@operator.Equals("=="))
					{
						if (this.toValue(tree2) != this.toValue(tree3))
						{
							return 0.0;
						}
						return 1.0;
					}
					else if (@operator.Equals("!="))
					{
						if (this.toValue(tree2) == this.toValue(tree3))
						{
							return 0.0;
						}
						return 1.0;
					}
					else if (@operator.Equals("<"))
					{
						if (this.toValue(tree2) >= this.toValue(tree3))
						{
							return 0.0;
						}
						return 1.0;
					}
					else if (@operator.Equals(">"))
					{
						if (this.toValue(tree2) <= this.toValue(tree3))
						{
							return 0.0;
						}
						return 1.0;
					}
					else if (@operator.Equals("&&"))
					{
						if (this.toValue(tree2) != 1.0 || this.toValue(tree3) != 1.0)
						{
							return 0.0;
						}
						return 1.0;
					}
					else if (@operator.Equals("||"))
					{
						if (this.toValue(tree2) != 1.0 && this.toValue(tree3) != 1.0)
						{
							return 0.0;
						}
						return 1.0;
					}
					else if (@operator.Equals(">="))
					{
						if (this.toValue(tree2) < this.toValue(tree3))
						{
							return 0.0;
						}
						return 1.0;
					}
					else if (@operator.Equals("<="))
					{
						if (this.toValue(tree2) > this.toValue(tree3))
						{
							return 0.0;
						}
						return 1.0;
					}
				}
				else
				{
					if (@operator.Equals("$sqrt"))
					{
						return Math.Sqrt(this.toValue(tree2));
					}
					if (@operator.Equals("$sin"))
					{
						return Math.Sin(this.toValue(tree2));
					}
					if (@operator.Equals("$cos"))
					{
						return Math.Cos(this.toValue(tree2));
					}
					if (@operator.Equals("$tan"))
					{
						return Math.Tan(this.toValue(tree2));
					}
					if (@operator.Equals("$asin"))
					{
						return Math.Asin(this.toValue(tree2));
					}
					if (@operator.Equals("$acos"))
					{
						return Math.Acos(this.toValue(tree2));
					}
					if (@operator.Equals("$atan"))
					{
						return Math.Atan(this.toValue(tree2));
					}
					if (@operator.Equals("$ln"))
					{
						return Math.Log(this.toValue(tree2));
					}
					if (@operator.Equals("$log"))
					{
						return Math.Log10(this.toValue(tree2));
					}
					if (@operator.Equals("$exp"))
					{
						return Math.Exp(this.toValue(tree2));
					}
					if (@operator.Equals("$cotan"))
					{
						return 1.0 / Math.Tan(this.toValue(tree2));
					}
					if (@operator.Equals("$acotan"))
					{
						return 1.5707963267948966 - Math.Atan(this.toValue(tree2));
					}
					if (@operator.Equals("$ceil"))
					{
						return Math.Ceiling(this.toValue(tree2));
					}
					if (@operator.Equals("$round"))
					{
						return Math.Round(this.toValue(tree2));
					}
					if (@operator.Equals("$floor"))
					{
						return Math.Floor(this.toValue(tree2));
					}
					if (@operator.Equals("$fac"))
					{
						return this.fac(this.toValue(tree2));
					}
					if (@operator.Equals("$abs"))
					{
						return Math.Abs(this.toValue(tree2));
					}
					if (@operator.Equals("$fpart"))
					{
						return this.fpart(this.toValue(tree2));
					}
					if (@operator.Equals("$sfac"))
					{
						return this.sfac(this.toValue(tree2));
					}
					if (@operator.Equals("$sinh"))
					{
						double d = this.toValue(tree2);
						return (Math.Exp(d) - 1.0 / Math.Exp(d)) / 2.0;
					}
					if (@operator.Equals("$cosh"))
					{
						double d = this.toValue(tree2);
						return (Math.Exp(d) + 1.0 / Math.Exp(d)) / 2.0;
					}
					if (@operator.Equals("$tanh"))
					{
						double d = this.toValue(tree2);
						return (Math.Exp(d) - 1.0 / Math.Exp(d)) / 2.0 / ((Math.Exp(d) + 1.0 / Math.Exp(d)) / 2.0);
					}
					if (@operator.Equals("!"))
					{
						if (this.toValue(tree2) == 1.0)
						{
							return 0.0;
						}
						return 1.0;
					}
				}
				throw new MathParserException(Resources.Unknown_operator);
			}
			string variable = tree.getVariable();
			string text = this.get(variable);
			if (this.trees.ContainsKey(text))
			{
				return this.toValue((MathParserNode)this.trees[text]);
			}
			if (this.isConstant(text))
			{
				return double.Parse(text);
			}
			text = this.skipSpaces(text);
			this.htbl[variable] = text;
			this.Syntax(text);
			tree = this.parse(this.putMult(this.parseE(text)));
			this.trees.Add(text, tree);
			return this.toValue(tree);
		}

		private bool matchParant(string exp)
		{
			int num = 0;
			int length = exp.Length;
			for (int i = 0; i < length; i++)
			{
				if (exp[i] == '(')
				{
					num++;
				}
				else if (exp[i] == ')')
				{
					num--;
				}
			}
			return num == 0;
		}

		private bool isAlpha(char ch)
		{
			return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
		}

		private bool isVariable(string str)
		{
			int length = str.Length;
			if (this.isAllNumbers(str))
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (this.getOp(str, i) != null || this.isAllowedSym(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		private bool isConstant(char ch)
		{
			return char.IsDigit(ch);
		}

		private bool isConstant(string exp)
		{
			double d = 0.0;
			bool flag = double.TryParse(exp, out d);
			return flag && !double.IsNaN(d);
		}

		private bool isAllNumbers(string str)
		{
			int i = 0;
			bool flag = false;
			char c = str[0];
			if (c == '-' || c == '+')
			{
				i = 1;
			}
			int length = str.Length;
			while (i < length)
			{
				c = str[i];
				if (!char.IsDigit(c) && ((c != '.' && c != ',') || flag))
				{
					return false;
				}
				flag = (c == '.' || c == ',');
				i++;
			}
			return true;
		}

		private bool isOperator(string str)
		{
			return this.ops.ContainsKey(str);
		}

		private bool isTwoArgOp(string str)
		{
			if (str == null)
			{
				return false;
			}
			object obj = this.ops[str];
			return obj != null && ((MathParserOperator)obj).arguments() == 2;
		}

		private bool isInteger(double a)
		{
			return a - (double)((int)a) == 0.0;
		}

		private bool isEven(int a)
		{
			return this.isInteger((double)(a / 2));
		}

		private bool isAllowedSym(char s)
		{
			return s == ',' || s == '.' || s == ')' || s == '(' || s == '>' || s == '<' || s == '&' || s == '=' || s == '|';
		}

		private string skipSpaces(string str)
		{
			int i = 0;
			int length = str.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			while (i < length)
			{
				if (str[i] != ' ')
				{
					stringBuilder.Append(str[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		private int match(string exp, int index)
		{
			int length = exp.Length;
			int i = index;
			int num = 0;
			while (i < length)
			{
				if (exp[i] == '(')
				{
					num++;
				}
				else if (exp[i] == ')')
				{
					num--;
				}
				if (num == 0)
				{
					return i;
				}
				i++;
			}
			return index;
		}

		private string getOp(string exp, int index)
		{
			int length = exp.Length;
			for (int i = 0; i < this.maxoplength; i++)
			{
				if (index >= 0 && index + this.maxoplength - i <= length)
				{
					string text = exp.Substring(index, this.maxoplength - i);
					if (this.isOperator(text))
					{
						return text;
					}
				}
			}
			return null;
		}

		private string arg(string _operator, string exp, int index)
		{
			int length = exp.Length;
			StringBuilder stringBuilder = new StringBuilder(this.sb_init);
			int i = index;
			int num;
			if (_operator == null)
			{
				num = -1;
			}
			else
			{
				num = ((MathParserOperator)this.ops[_operator]).precedence();
			}
			while (i < length)
			{
				string op;
				if (exp[i] == '(')
				{
					int num2 = this.match(exp, i);
					stringBuilder.Append(exp.Substring(i, num2 + 1 - i));
					i = num2 + 1;
				}
				else if ((op = this.getOp(exp, i)) != null)
				{
					if (stringBuilder.Length != 0 && !this.isTwoArgOp(this.backTrack(stringBuilder.ToString())) && ((MathParserOperator)this.ops[op]).precedence() >= num)
					{
						return stringBuilder.ToString();
					}
					stringBuilder.Append(op);
					i += op.Length;
				}
				else
				{
					stringBuilder.Append(exp[i]);
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		private string backTrack(string str)
		{
			int length = str.Length;
			try
			{
				for (int i = 0; i <= this.maxoplength; i++)
				{
					string op;
					if ((op = this.getOp(str, length - 1 - this.maxoplength + i)) != null && length - this.maxoplength - 1 + i + op.Length == length)
					{
						return op;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		private string get(string key)
		{
			object obj = this.htbl[key.ToLower()];
			string result = null;
			if (obj == null)
			{
				throw new MathParserException(Resources.No_value_associated_with + " " + key);
			}
			try
			{
				result = (string)obj;
			}
			catch
			{
				throw new MathParserException(string.Concat(new string[]
				{
					Resources.Wrong_type_value_for,
					" ",
					key,
					" ",
					Resources.expected_String
				}));
			}
			return result;
		}

		private double fac(double val)
		{
			if (!this.isInteger(val))
			{
				return double.NaN;
			}
			if (val < 0.0)
			{
				return double.NaN;
			}
			if (val <= 1.0)
			{
				return 1.0;
			}
			return val * this.fac(val - 1.0);
		}

		private double sfac(double val)
		{
			if (!this.isInteger(val))
			{
				return double.NaN;
			}
			if (val < 0.0)
			{
				return double.NaN;
			}
			if (val <= 1.0)
			{
				return 1.0;
			}
			return val * this.sfac(val - 2.0);
		}

		private double fpart(double val)
		{
			if (val >= 0.0)
			{
				return val - Math.Floor(val);
			}
			return val - Math.Ceiling(val);
		}

		private Hashtable ops;

		private Hashtable trees;

		private Hashtable htbl;

		private Hashtable spconst;

		private int maxoplength;

		private int sb_init;

		private bool bRequireParentheses;

		private bool bImplicitMultiplication;
	}
}
