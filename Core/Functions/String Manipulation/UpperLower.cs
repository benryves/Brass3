using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Core.Functions.StringManipulation {
	
	[Description("Converts all characters in a string to upper or lower case.")]
	[Syntax("upper(string)")]
	[Syntax("lower(string)")]
	[Remarks("The invariant culture is used.")]
	[Category("String Manipulation")]
	public class UpperLower : IFunction {

		public string[] Names { get { return new string[] { "upper", "lower" }; } }
		public string Name { get { return Names[0]; } }
		
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			string Arg = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.String })[0] as string;
			return new Label(compiler.Labels, function == "upper" ? Arg.ToUpperInvariant() : Arg.ToLowerInvariant());
		}



		

	}
}
