using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fpos(handle)")]
	[Description("Returns the current position in the file.")]
	[Category("File Operations")]
	public class FilePos : IFunction {

		public string[] Names { get { return new string[] { "fpos" }; } }
		public string Name { get { return this.Names[0]; } }

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			FileStream S = FileOpen.GetFilestreamFromHandle(compiler, source);

			return new Label(compiler.Labels, S.Position);
			

		}
	}
}
