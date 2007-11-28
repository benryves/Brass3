using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Directives {

	[Syntax(".include \"file\"")]
	[Description("Loads and compiles <c>file</c> at the current source file position.")]
	[Remarks("Use this break up a project into multiple source files. This greatly aids code readability and allows you to easily incorporate source from other projects into your own without having to manually copy and paste it in.")]
	[CodeExample(".include \"graphics.asm\"")]
	[Category("Code Structure")]
	public class Include : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.CurrentPass != AssemblyPass.CreatingLabels) return; // ONLY include extra source files during pass 1.

			string Filename = source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.FilenameArgument)[0] as string;

			if (!File.Exists(Filename)) throw new DirectiveArgumentException(source.Tokens[index], string.Format(Strings.ErrorFileNotFound, Filename));

			compiler.CompileFile(Filename);
		}

	}
}
