using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("feof(handle)")]
	[Description("Returns true if the file pointer is at the end of the file.")]
	[Category("File Operations")]
	[CodeExample("Reading all data from a file in a loop.", "f = fopen(\"file.ext\")\r\n\r\n#while !feof(f)\r\n\t.echoln fread(f)\r\n#loop")]
	public class FEOF: IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			FileStream S = FOpen.GetFilestreamFromHandle(compiler, source);
			return new Label(compiler.Labels, S.Position >= S.Length ? 1 : 0);
		}
	}
}
