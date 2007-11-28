using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".undefine name")]
	[PluginName("undef")]
	public class Undefine : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			TokenisedSource Argument = source.GetExpressionTokens(source.GetCommaDelimitedArguments(index + 1, 1)[0]);
			if (Argument.Tokens.Length != 1) throw new CompilerExpection(source, Strings.ErrorUndefExpectedLabelOrMacroName);
			//TODO: Undefine macros.
			if (compiler.LabelIsDefined(Argument.Tokens[0])) {
				compiler.Labels.Remove(compiler.Labels[Argument.Tokens[0].Data]);
			}

		}

	}
}
