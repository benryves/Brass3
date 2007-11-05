using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".fill amount [, value]")]
	[Description("Writes a specified number of bytes into the output at the current location.")]
	[Remarks("If <c>value</c> is not specified, the current empty fill value is used instead.")]
	[CodeExample("; Output 100 space characters.\r\n.fill 100, ' '")]
	[Category("Output Manipulation")]
	[SeeAlso(typeof(Directives.Block))]
	public class Fill : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			
			object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Positive,
				TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional,
			});

			int Amount = (int)(double)Args[0];
			byte FillValue = Args.Length == 2 ? (byte)(double)Args[1] : compiler.EmptyFill;

			switch (compiler.CurrentPass) {
				case AssemblyPass.Pass1:
					compiler.IncrementProgramAndOutputCounters(Amount);
					break;
				case AssemblyPass.Pass2:
					for (int i = 0; i < Amount; ++i) {
						compiler.WriteOutput(FillValue);						
					}
					break;
			}
		}

	}
}
