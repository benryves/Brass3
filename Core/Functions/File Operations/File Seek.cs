using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fseek(handle, position)")]
	[Description("Seeks to a position within the file and returns the new position.")]
	[Category("File Operations")]
	public class FileSeek : IFunction {

		public string[] Names { get { return new string[] { "fseek" }; } }
		public string Name { get { return this.Names[0]; } }

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 2);
			FileStream S = FileOpen.GetFilestreamFromHandle(compiler, source);
			long Offset = (long)source.EvaluateExpression(compiler, Args[1]).Value;
			S.Seek(Offset, SeekOrigin.Begin);
			return new Label(compiler.Labels, S.Position);
			

		}
	}
}
