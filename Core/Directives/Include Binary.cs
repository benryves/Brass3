using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Directives {

	[Syntax(".incbin \"file\"")]
	[Description("Insert all data from a binary file into the output at the current program counter position.")]
	[Remarks("Use this to import precompiled resources from other sources into your project.")]
	[CodeExample("MonsterSprite:\r\n.incbin \"Resources/Sprites/Monster.spr\"")]
	[Category("Data")]
	public class IncBin : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);

			string Filename = source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.FilenameArgument)[0] as string;

			if (!File.Exists(Filename)) throw new DirectiveArgumentException(source.Tokens[index], string.Format(Strings.ErrorFileNotFound, Filename));

			try {
				using (FileStream FS = File.OpenRead(Filename)) {
					switch (compiler.CurrentPass) {
						case AssemblyPass.CreatingLabels:
							compiler.IncrementProgramAndOutputCounters((int)FS.Length);
							break;
						case AssemblyPass.WritingOutput:
							int Data = 0;
							while ((Data = FS.ReadByte()) != -1) compiler.WriteOutput((byte)Data);
							break;
					}
				}
			} catch (Exception ex) {
				throw new CompilerExpection(source, ex.Message);
			}
		}

	}
}
