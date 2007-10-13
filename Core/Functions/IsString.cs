using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	public class IsString : IFunction {

		public string[] Names {
			get { return new string[] { "isstring" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(0, 1)[0]).IsString ? 1 : 0);
				
			
		}
	}
}
