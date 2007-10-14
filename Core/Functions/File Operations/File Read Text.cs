using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("freadtext(filename)")]
	[Description("Reads all text data from a file.")]
	[Category("File Operations")]
	[CodeExample("Display content of <c>file.txt</c>.", ".echoln freadtext(\"file.txt\")")]
	[SeeAlso(typeof(Directives.IncText))]
	public class FileReadText : IFunction {

		public string[] Names { get { return new string[] { "freadtext" }; } }
		public string Name { get { return this.Names[0]; } }

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, File.ReadAllText(source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.Filename })[0] as string));
		}
	}
}
