using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions.FileOperations {

	[Syntax("fclose(handle)")]
	[Description("Closes a file.")]
	[Category("File Operations")]
	[SatisfiesAssignmentRequirement(true)]
	public class FClose : IFunction {


		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			FileStream S = FOpen.GetFilestreamFromHandle(compiler, source);
			S.Dispose();
			Label L = new Label(compiler.Labels, 0);
			return L;			

		}
	}
}
