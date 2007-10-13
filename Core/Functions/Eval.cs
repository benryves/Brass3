using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	[Description("Evaluates a snippet of code contained in a string.")]
	[Syntax("eval(\"expression\")")]
	[Remarks("The expression can contain any ")]
	[SatisfiesAssignmentRequirement(true)]
	public class Eval : IFunction {

		public string[] Names {
			get { return new string[] { "eval" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			object[] Source = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.String });

			Label Result = null;
			foreach (TokenisedSource TS in TokenisedSource.FromString(compiler, Source[0] as string)) {
				Compiler.SourceStatement S = new Compiler.SourceStatement(compiler, TS.GetCode(), compiler.CurrentFile, compiler.CurrentLineNumber);
				Result = S.Compile(false);
			}
			if (Result != null) {
				return Result.Clone() as Label;
			} else {
				return new Label(compiler.Labels, double.NaN);
			}
		}
	}
}
