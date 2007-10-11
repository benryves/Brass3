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
	public class Include : IDirective {

		public string[] Names { get { return new string[] { "include" }; } }
		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.CurrentPass == AssemblyPass.Pass2) return; // ONLY include extra source files during pass 1.

			int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);
			if (!source.ExpressionIsStringConstant(Args[0])) throw new DirectiveArgumentException(source, "Filename expected.");

			string Filename = compiler.ResolveFilename(source.GetExpressionStringConstant(Args[0], false));

			if (!File.Exists(Filename)) throw new DirectiveArgumentException(source.Tokens[index], "File '" + Filename + "' not found.");

			compiler.CompileFile(Filename);
		}

	}
}
