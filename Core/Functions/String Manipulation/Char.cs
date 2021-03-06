using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

namespace Core.Functions.StringManipulation {
	
	
	[Category("String Manipulation")]
	[Description("Returns a character corresponding to the character <c>code</c> according to the current string encoding.")]
	[Remarks("If a valid character can't be returned then <c>'?'</c> should be returned instead.")]
	[Syntax("char(code)")]
	[CodeExample(".stringencoder ascii\r\n.echoln char($03A3) ; ?\r\n\r\n.stringencoder utf16\r\n.echoln char($03A3) ; Greek Σ.\r\n\r\n/* If this example doesn't work, set\r\nyour console to a codepage that can\r\ndisplay Greek characters (CHCP 1253\r\non Windows). Using Western European\r\n(DOS) (code page 850) 'S' is displayed\r\ninstead of Σ. */")]
	public class Char : IFunction {
		
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.Value });
			return new Label(compiler.Labels, compiler.StringEncoder.GetChar((int)(double)Args[0]).ToString());
		}
	}
}
