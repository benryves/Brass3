using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace TexasInstruments.Brass.Directives {
	public class IonHeader : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.GetOutputDataOnPage(compiler.Labels.ProgramCounter.Page).Length > 0) {
				compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new DirectiveArgumentException(source, "Data has already been output before the Ion header.")));
			}

			// Determine current machine type.
			bool Is83Plus = false;
			ushort Origin = 0x9327;
			if (compiler.OutputWriter is TexasInstruments.Brass.Output.TI8X) {
				Is83Plus = true;
				Origin = 0x9D93;
			} else if (!(compiler.OutputWriter is TexasInstruments.Brass.Output.TI83)) {
				compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, new DirectiveArgumentException(source, "Output writer is neither TI83 nor TI8X; assuming TI-83.")));
			}

			// Set program counters.
			compiler.Labels.ProgramCounter.NumericValue = Origin;
			compiler.Labels.OutputCounter.NumericValue = Origin;

			byte[] ProgramName = compiler.StringEncoder.GetData(source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.StringArgument)[0] as string);

			/*
			 * ret \ jr nc,+         // 3 bytes.
			 * .db "Description", 0  // Length of description + 1 bytes.
			 */

			switch (compiler.CurrentPass) {
				case AssemblyPass.Pass1:
					compiler.IncrementProgramAndOutputCounters(ProgramName.Length + (Is83Plus ? 6 : 4));
					break;
				case AssemblyPass.Pass2:
					if (Is83Plus) compiler.WriteOutput((ushort)0x6DBB);
					compiler.WriteOutput((byte)Functions.BCall.Z80Instruction.Ret);
					compiler.WriteOutput((byte)Functions.BCall.Z80Instruction.JrNC);
					compiler.WriteOutput((byte)(ProgramName.Length + 1));
					compiler.WriteOutput(ProgramName);
					compiler.WriteOutput((byte)0x00);
					break;
			}
			
		}
	}
}
