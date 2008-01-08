using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fsize(handle)")]
	[Syntax("fsize(name)")]
	[Description("Returns the size of a file.")]
	[CodeExample("; fsize() on a filename:\r\n.echoln fsize(\"file.ext\")\r\n\r\n; fsize() on an already-opened file:\r\nf = fopen(\"file.ext\")\r\n.echoln fsize(f)\r\nfclose(f)")]
	[Category("File Operations")]
	public class FSize : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			if (source.ExpressionIsStringConstant(compiler, Args[0])) {
				using (FileStream S = new FileStream(compiler.ResolveFilename(source.GetExpressionStringConstant(compiler, Args[0])), FileMode.Open)) {
					return new Label(compiler.Labels, S.Length);
				}
			} else {
				FileStream S = FOpen.GetFilestreamFromHandle(compiler, source);
				return new Label(compiler.Labels, S.Length);
			}
			

		}
	}
}
