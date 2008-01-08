using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

namespace Core.Functions.StringManipulation {
	
	[Description("Retrieves a substring.")]
	[Syntax("strsub(string, start [, length])")]
	[Category("String Manipulation")]
	[Remarks("If the start index is a negative value then the substring is returned <c>start</c> characters from the end of the string rather than from the beginning.")]
	[CodeExample("A function to reverse a string.", ".function rev(s)\r\n\trev = \"\"\r\n\t.for i = 1, i <= strlength(s), ++i\r\n\t\trev += strsub(s, -i, 1)\r\n\t.loop\r\n.endfunction\r\n\r\n; Displays \"Hello!\"\r\n.echoln rev(\"!olleH\")")]
	public class StrSub : IFunction {
		
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] {
				TokenisedSource.ArgumentType.String,
				TokenisedSource.ArgumentType.Value,
				TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional
			});

			string Operand = Args[0] as string;
			int Start = (int)(double)Args[1];
			if (Start < 0) Start = Operand.Length + Start;
			int Length = Args.Length > 2 ? (int)(double)Args[2] : Operand.Length - Start;

			return new Label(compiler.Labels, Operand.Substring(Start, Length));
		}



		

	}
}
