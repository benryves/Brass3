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
	[Description("Returns a string with formatted numeric results.")]
	[Syntax("format(string [, arg0 [, arg1, [arg2]]])")]
	[Remarks("This uses the .NET <c>String.Format</c> method in the invariant culture.\r\nIf an argument is a string it is passed as a string; if it is numeric and an integral value it is passed as an integer, otherwise it is passed as a double.")]
	[CodeExample("Formatting as hexadecimal with the <c>X</c> format specifier.", ".org $9D93\r\n.echoln format(\"Program counter = ${0:X4}\", $)")]
	public class Format : IFunction {

		public string[] Names { get { return new string[] { "format" }; } }
		public string Name { get { return Names[0]; } }
		
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1, int.MaxValue);
			if (!source.ExpressionIsStringConstant(Args[0])) throw new DirectiveArgumentException(source, "First argument must be a string.");
			string FormatString = source.GetExpressionStringConstant(Args[0]);
			object[] FormatArgs = new object[Args.Length - 1];
			for (int i = 1; i < Args.Length; ++i) {
				Label L = source.EvaluateExpression(compiler, Args[i]);
				FormatArgs[i - 1] = L.IsString ? (object)L.StringValue : (object)(L.NumericValue == (int)L.NumericValue ? (object)(int)L.NumericValue : (object)L.NumericValue);
			}
			return new Label(compiler.Labels, string.Format(CultureInfo.InvariantCulture, FormatString, FormatArgs));
		}
	}
}
