using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".echochar expression [, expression [, ... ]]")]
	[Description("Writes unicode characters for each expression to the console, rather than their value as a number.")]
	[CodeExample("Difference between <c>.echo</c> and <c>.echochar</c>.", "; Outputs 66:\r\n.echo \"A\" + 1, \"\\n\"\r\n\r\n; Outputs B:\r\n.echochar \"A\" + 1, \"\\n\"")]
	[SeeAlso(typeof(Echo))]
	public class EchoChar : IDirective {

		public string[] Names {
			get {
				return new string[] { "echochar" };
			}
		}

		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass != AssemblyPass.Pass2) return;
			foreach (int Expression in source.GetCommaDelimitedArguments(index + 1)) {
				compiler.OutputString(((char)(int)source.EvaluateExpression(compiler, Expression).NumericValue).ToString());
			}
		}

	}
}
