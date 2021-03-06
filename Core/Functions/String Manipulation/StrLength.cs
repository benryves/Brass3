using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

namespace Core.Functions.StringManipulation {
	
	[Description("Returns the number of characters of a string.")]
	[Syntax("strlength(string)")]
	[Category("String Manipulation")]
	public class StrLength : IFunction {
	
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] {
				TokenisedSource.ArgumentType.String
			});

			string Operand = Args[0] as string;
			

			return new Label(compiler.Labels, Operand.Length);
		}



		

	}
}
