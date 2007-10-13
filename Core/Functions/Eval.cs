using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	[Description("Returns the result of an expression in a string.")]
	[Syntax("eval(\"expression\")")]
	public class Eval : IFunction {

		public string[] Names {
			get { return new string[] { "eval" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int Expression = source.GetCommaDelimitedArguments(0, 1)[0];
			if (!source.ExpressionIsStringConstant(Expression)) throw new CompilerExpection(source, "Expected a single string expression.");
			double Result = double.NaN;
			foreach (TokenisedSource TS in TokenisedSource.FromString(compiler, source.GetExpressionStringConstant(Expression))) {
				Result = TS.EvaluateExpression(compiler).NumericValue;
			}
			return new Label(compiler.Labels, Result);
		}
	}
}
