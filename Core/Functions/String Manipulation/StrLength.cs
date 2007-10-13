using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Core.Functions.StringManipulation {
	
	[Description("Returns the number of characters of a string.")]
	[Syntax("strlength(string)")]
	[Category("String Manipulation")]
	public class StrLength : IFunction {

		public string[] Names { get { return new string[] { "strlength" }; } }
		public string Name { get { return Names[0]; } }
		
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] {
				TokenisedSource.ArgumentType.String
			});

			string Operand = Args[0] as string;
			

			return new Label(compiler.Labels, Operand.Length);
		}



		

	}
}
