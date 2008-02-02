using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
namespace TexasInstruments.Brass.Directives {
	
	[Category("Texas Instruments")]
	[Description("Generates a branch table entry for ROM calls.")]
	[Syntax(".branch label")]
	[CodeExample(@".page 0

	.branch OffPageCall

	.bcall _OffPageCall ; Note the _underscore prefix.

.page 1

OffPageCall
	ret")]
	[SeeAlso(typeof(AppHeader))]
	public class Branch : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			// First: align up to the next 3-byte boundary;
			while (((int)compiler.Labels.OutputCounter.NumericValue - 0x4000) % 3 != 0) compiler.IncrementProgramAndOutputCounters(1);

			Label BranchLabel = compiler.Labels.Create(new TokenisedSource.Token("_" + source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.TokenArgument)[0] as string));
			BranchLabel.NumericValue -= 0x4000;

			compiler.WriteDynamicOutput(3, Branch => {
				Label Target = (Label)source.GetCommaDelimitedArguments(compiler, index + 1, new[] { TokenisedSource.ArgumentType.Label })[0];
				Branch.Data = new byte[]{
					(byte)(int)Target.NumericValue,
					(byte)((int)Target.NumericValue >> 8),
					(byte)Target.Page,
				};
			});
		}

	}
}
