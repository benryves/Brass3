using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fread(handle)")]
	[Syntax("freadw(handle)")]
	[Syntax("freadi(handle)")]
	[Description("Reads data from a file and advances the pointer.")]
	[Remarks("<c>fread</c> reads an unsigned byte, and returns -1 if you've tried to read past the end of the stream.\r\n<c>freadw</c> and <c>freadi</c> read signed 16-bit and 32-bit integers respectively.")]
	[Category("File Operations")]
	public class FileRead : IFunction {

		public string[] Names { get { return new string[] { "fread", "freadw", "freadi" }; } }
		public string Name { get { return this.Names[0]; } }

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			FileStream S = FileOpen.GetFilestreamFromHandle(compiler, source);

			BinaryReader BR = new BinaryReader(S);

			switch (function) {
				case "fread":
					return new Label(compiler.Labels, S.ReadByte());
				case "freadw":
					return new Label(compiler.Labels, BR.ReadInt16());
				case "freadi":
					return new Label(compiler.Labels, BR.ReadInt32());
				default:
					throw new InvalidOperationException();
			}
			

		}
	}
}
