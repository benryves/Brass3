using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	public class IsString : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(0, 1)[0]).IsString ? 1 : 0);		
		}
	}
}
