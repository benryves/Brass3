using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Logic {

	[Syntax("and(expression, expression [, expression, [, ...]])")]
	[Description("Returns true if all of the arguments are true; false if any one is false.")]
	[Category("Logic")]
	[SeeAlso(typeof(Or))]
	[CodeExample("Output a truth table.", "/*\r\n+-+-+-+\r\n|b|a|q|\r\n+-+-+-+\r\n|0|0|0|\r\n|0|1|0|\r\n|1|0|0|\r\n|1|1|1|\r\n+-+-+-+\r\n*/\r\n\r\n.echoln '+-+-+-+'\r\n.echoln '|b|a|q|'\r\n.echoln '+-+-+-+'\r\n\r\n#for b is 0 to 1\r\n\t.for a is 0 to 1\r\n\t\t#echoln '|', b, '|', a, '|', and(a, b), '|'\r\n\t.loop\r\n#loop\r\n\r\n.echoln '+-+-+-+'")]
	public class And : IFunction {

		public string[] Names {
			get { return new string[] { "and" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 2, int.MaxValue);
			bool Result = true;
			for (int i = 0; i < Args.Length; ++i) {
				if (source.EvaluateExpression(compiler, Args[i]).Value == 0) {
					Result = false;
					break;
				}
			}
			return new Label(compiler.Labels, Result ? 1 : 0);
		}
	}
}
