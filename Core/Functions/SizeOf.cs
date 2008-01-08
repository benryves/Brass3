using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	public class SizeOf : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int Arg = source.GetCommaDelimitedArguments(0, 1)[0];
			TokenisedSource NamedArgs = source.GetExpressionTokens(Arg);
			if (NamedArgs.Tokens.Length == 1) {
				DataStructure DS = compiler.GetStructureByName(NamedArgs.Tokens[0].Data);
				if (DS != null) return new Label(compiler.Labels, DS.Size);
			}
			return new Label(compiler.Labels, source.EvaluateExpression(compiler, Arg).Size);
		}
	}
}
