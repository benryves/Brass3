using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".undefine name")]
	public class Undefine : IDirective {

		public string[] Names {
			get { return new string[] { "undef" }; }
		}

		public string Name { get { return Names[0]; } }


		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			TokenisedSource Argument = source.GetExpressionTokens(source.GetCommaDelimitedArguments(index + 1, 1)[0]);
			if (Argument.Tokens.Length != 1) throw new CompilerExpection(source, "Expected label or macro name.");
			if (compiler.LabelIsDefined(Argument.Tokens[0])) {
				compiler.Labels.Remove(compiler.Labels[Argument.Tokens[0].Data]);
			}

		}

	}
}
