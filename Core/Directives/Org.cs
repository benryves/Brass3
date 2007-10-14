using System;
using System.Collections.Generic;
using System.Text;

using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;
using Brass3;

namespace Core.Directives {
	[Syntax(".org address")]
	[Description("Sets the value of the current program counter and output counter.")]
	[CodeExample(".org $9D93")]
	public class Org : IDirective {

		public string[] Names {
			get { return new string[] { "org" }; }
		}
		public string Name {
			get { return this.Names[0]; }
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			int[] args = source.GetCommaDelimitedArguments(index + 1, 1);

			compiler.Labels.OutputCounter.NumericValue = (int)source.EvaluateExpression(compiler, args[0]).NumericValue;
			compiler.Labels.ProgramCounter.NumericValue = compiler.Labels.OutputCounter.NumericValue;
			

		}
	}
}
