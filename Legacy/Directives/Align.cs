using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Legacy.Directives {

	[Syntax(".align boundary")]
	public class Align : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.Value
			});

			int Boundary = (int)(double)Args[0];
			if ((double)Args[0] < 1 || Boundary != (double)Args[0]) throw new DirectiveArgumentException(source, "You can only align to positive integral boundaries.");

			int Source = (int)compiler.Labels.ProgramCounter.NumericValue;
			int Destination = (((Source) + (Boundary - 1)) / (Boundary)) * Boundary;

			int AlignOffset = Destination - Source;

			if (compiler.CurrentPass == AssemblyPass.Pass1) {
				compiler.IncrementProgramAndOutputCounters(AlignOffset);
			} else {
				compiler.WriteEmptyFill(AlignOffset);
			}
		}
	}
}
