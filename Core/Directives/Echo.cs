using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".echo expression [, expression [, ... ]]")]
	[Description("Writes the text representation of the specified expressions to the console.\r\n<c>echoln</c> writes a new-line sequence at the end of the expression list.")]
	[Remarks("Expressions can be either numeric or string constants. This directive is only invoked during the second pass.")]
	[CodeExample(".echoln \"Program counter = \", $")]
	[SeeAlso(typeof(EchoChar))]
	public class Echo : IDirective {

		public string[] Names {
			get {
				return new string[] { "echo", "echoln" };
			}
		}

		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass != AssemblyPass.Pass2) return;
			foreach (int Expression in source.GetCommaDelimitedArguments(index + 1)) {
				if (source.ExpressionIsStringConstant(Expression)) {
					compiler.OutputString(source.GetExpressionStringConstant(Expression));
				} else {
					compiler.OutputString(source.EvaluateExpression(compiler, Expression).Value.ToString());
				}
			}
			if (directive == "echoln") compiler.OutputString(Environment.NewLine);
		}

	}
}
