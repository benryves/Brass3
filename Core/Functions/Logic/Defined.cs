using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("defined(label)")]
	[Syntax("undefined(label)")]
	[Description("<c>defined()</c> returns true if a label or macro is defined, false otherwise; <c>undefined()</c> returns true if the label or macro is undefined.")]
	[Remarks("Constant values are implicitly defined. Macro definitions are checked before label definitions.")]
	[CodeExample("Automatically setting <c>DebugLevel</c> to 10 if previously undefined when <c>Debug</c> is defined.", "#define Debug\r\n\r\n#if defined(Debug) && undefined(DebugLevel)\r\n\tDebugLevel = 10\r\n\t.echoln \"Debug level set to 10.\"\r\n#endif")]
	[Category("Logic")]
	public class Defined : IFunction {

		public string[] Names {
			get { return new string[] { "defined", "undefined" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			TokenisedSource Args = source.GetExpressionTokens(source.GetCommaDelimitedArguments(0, 1)[0]);
			if (Args.Tokens.Length != 1) throw new DirectiveArgumentException(source, "Expected a label name or constant.");
			return new Label(compiler.Labels, (compiler.IsDefined(Args.Tokens[0]) ^ (function == "undefined")) ? 1 : 0);
		}

	}
}