using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	public class EnvVar : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, Environment.GetEnvironmentVariable(source.GetCommaDelimitedArguments(compiler, 0, TokenisedSource.StringArgument)[0] as string) ?? "");
		}
	}
}
