using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	[Description("Returns the current line number in the source file.")]
	[Syntax("line()")]
	public class Line : IFunction {

		public string[] Names {
			get { return new string[] { "line" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			source.GetCommaDelimitedArguments(0, 0);
			return new Label(compiler.Labels, compiler.CurrentLineNumber);
		}
	}
}
