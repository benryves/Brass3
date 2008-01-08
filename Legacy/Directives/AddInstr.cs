using System;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Legacy.Directives {

	[Description("Allows you to add an instruction to the Z80 instruction set at runtime.")]
	[Syntax(".addinstr instruction args opcode size rule class [shift [or]]")]
	[Remarks(@"This directive only affects the Z80 assembler.
As the bundled Z80 assembler only supports a small subset of TASM's instruction types (<c>NOP</c>, <c>R1</c>, <c>ZIX</c>, <c>ZBIT</c>) this directive is of limited usefulness.
See <c>TASMTABS.HTM</c> in TASM's zip file for more information.")]
	[SeeAlso(typeof(Z80.Z80))]
	[CodeExample("TI-83 Plus ROM call using <c>addinstr</c>.", @"/* ROM call equates: */
.include ""ti83plus.inc""

/* Instruction to invoke a ROM call: */
.addinstr B_CALL * EF 3 NOP 1

/* Using the new instruction: */
	b_call _puts")]
	public class AddInstr : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {


			Z80.Z80 Assembler = compiler.GetPluginInstanceFromType<Z80.Z80>();
			if (Assembler == null) throw new CompilerException(source.Tokens[index], ".addinstr needs the Z80 assembler plugin to be loaded.");

			StringBuilder AddInstrSyntax = new StringBuilder(32);

			foreach (int Arg in source.GetCommaDelimitedArguments(index + 1)) {
				AddInstrSyntax.Append(source.GetExpressionTokens(Arg).ToString());
			}

			Assembler.AddInstruction(new Z80.Z80.Instruction(AddInstrSyntax.ToString()), true);
		}
	}
}