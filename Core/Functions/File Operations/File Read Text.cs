using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("freadtext(filename)")]
	[Description("Reads all text data from a file.")]
	[Category("File Operations")]
	[CodeExample("Display content of <c>file.txt</c>.", ".echoln freadtext(\"file.txt\")")]
	[SeeAlso(typeof(Directives.IncText))]
	public class FReadText : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, File.ReadAllText(source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.Filename })[0] as string));
		}
	}
}
