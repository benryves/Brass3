using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".block amount")]
	[Description("Increment the program counter and output counter by the specified amount.")]
	[Remarks("No data is written. This is functionally equivalent to <c>$+=amount \\ @+=amount</c>.")]
	[Category("Output Manipulation")]
	[SeeAlso(typeof(Directives.Fill))]
	public class Block : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			int Offset = (int)(double)source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.ValueArgument)[0];
			compiler.IncrementProgramAndOutputCounters(Offset);
		}

	}
}
