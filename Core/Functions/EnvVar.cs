using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	public class EnvVar : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, Environment.GetEnvironmentVariable(source.GetCommaDelimitedArguments(compiler, 0, TokenisedSource.StringArgument)[0] as string) ?? "");
		}
	}
}
