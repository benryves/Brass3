using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Core.Functions.StringManipulation {
	
	[Description("Retrieves a substring.")]
	[Syntax("strreplace(original, search, replacement)")]
	[CodeExample("Replace <c>hlp</c> with <c>help</c>.", "errstring = \"This hlpfile is hlping you to hlp yourself with hlpful hlp.\"\r\n.echoln \"Original:  \", errstring\r\n\r\nerrstring = strreplace(errstring, \"hlp\", \"help\")\r\n.echoln \"Corrected: \", errstring\r\n")]
	public class StrReplace : IFunction {

		public string[] Names { get { return new string[] { "strreplace" }; } }
		public string Name { get { return Names[0]; } }
		
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] {
				TokenisedSource.ArgumentType.String,
				TokenisedSource.ArgumentType.String,
				TokenisedSource.ArgumentType.String
			});

			return new Label(compiler.Labels, (Args[0] as string).Replace(Args[1] as string, Args[2] as string));
		}



		

	}
}