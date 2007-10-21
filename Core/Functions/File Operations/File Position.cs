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
	public class FPos : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			FileStream S = FOpen.GetFilestreamFromHandle(compiler, source);

			return new Label(compiler.Labels, S.Position);
			

		}
	}
}
