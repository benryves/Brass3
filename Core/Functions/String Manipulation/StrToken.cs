using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Core.Functions.StringManipulation {
	
	
	[Category("String Manipulation")]
	[Description("Returns a string made up of the passed tokens.")]
	[Syntax("strtoken(tokens)")]
	[CodeExample("expression = strtoken(1+abs(sin(toradians(-90))))\r\n.echoln strformat(\"{0} = {1}\", expression, eval(expression))\r\n/* Outputs \"1+abs(sin(toradians(-90))) = 2\". */")]
	public class StrToken : IFunction {
	
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			return new Label(compiler.Labels, source.ToString());
		}
	}
}
