using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".stringencoder encoder")]
	[Description("Sets the current string encoder to the specified encoder.")]
	[Remarks("If a particular encoding cannot represent a character properly, it is recommended that the encoder returns a question-mark (or similar suitable character) in its place.")]
	[CodeExample(".stringencoder utf8\r\n.db \"This will be encoded as UTF-8.\"")]
	[CodeExample(".stringencoder ascii\r\n.echoln \"é\" == \"?\"\r\n\r\n.stringencoder utf8\r\n.echoln \"é\" == \"?\"")]
	public class StringEncoder : IDirective {

		public string[] Names { get { return new string[] { "stringencoder" }; } }
		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);
			TokenisedSource T = source.GetExpressionTokens(Args[0]);
			if (T.Tokens.Length != 1) throw new CompilerExpection(source.Tokens[index], "Encoding name expected.");
			string EncodingName = T.Tokens[0].Data;
			if (compiler.StringEncoders.PluginExists(EncodingName)) {
				compiler.StringEncoder = compiler.StringEncoders[EncodingName];
			} else {
				throw new CompilerExpection(T.Tokens[0], "Encoding plugin '" + EncodingName + "' not loaded.");
			}
		}

	}
}
