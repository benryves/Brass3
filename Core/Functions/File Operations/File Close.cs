using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fclose(handle)")]
	[Description("Closes a file.")]
	[Category("File Operations")]
	[SatisfiesAssignmentRequirement(true)]
	public class FileClose : IFunction {

		public string[] Names { get { return new string[] { "fclose" }; } }
		public string Name { get { return this.Names[0]; } }

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			FileStream S = FileOpen.GetFilestreamFromHandle(compiler, source);
			S.Dispose();
			Label L = new Label(compiler.Labels, 0);
			return L;			

		}
	}
}
