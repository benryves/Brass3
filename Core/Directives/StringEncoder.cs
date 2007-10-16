using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".stringencoder encoder")]
	[Description("Sets the current string encoder to the one specified.")]
	[Remarks("If a particular encoding cannot represent a character properly, it is recommended that the encoder returns a question-mark (or similar suitable character) in its place.\r\nThe parameter <c>encoder</c> can also be a code page number or an internal encoding name.")]
	[Warning("If you specify the name or code page of a multibyte encoding not implemented by a Brass plugin be warned that these are not as reliable and rely on some guesswork (especially as far as endianness issues are concerned) so if you need a particular encoding consider writing a proper Brass plugin to handle it.")]
	[CodeExample(".stringencoder utf8\r\n.db \"This will be encoded as UTF-8.\"")]
	[CodeExample(".stringencoder ascii\r\n.echoln \"é\" == \"?\"\r\n\r\n.stringencoder utf8\r\n.echoln \"é\" == \"?\"")]
	[CodeExample(".stringencoder \"windows-1252\"\r\n.echoln char($80) ; Displays a € sign.")]
	public class StringEncoder : IDirective {

		public string[] Names { get { return new string[] { "stringencoder" }; } }
		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			//foreach (EncodingInfo EI in Encoding.GetEncodings()) Console.WriteLine("{0}\t{1}\t{2}\t{3}", EI.CodePage, EI.GetEncoding().IsSingleByte, EI.Name, EI.DisplayName);	

			int[] Args = source.GetCommaDelimitedArguments(index + 1, 1);
			TokenisedSource T = source.GetExpressionTokens(Args[0]);
			if (T.Tokens.Length == 0) throw new CompilerExpection(source, "Expected a string encoder name.");

			string EncodingName = T.Tokens[0].Data;
			Label EncodingLabel = null;
			if (T.Tokens.Length != 1 || T.Tokens[0].Type == TokenisedSource.Token.TokenTypes.String) {
				EncodingLabel = T.EvaluateExpression(compiler);
				EncodingName = EncodingLabel.StringValue;
			}
			
			if (compiler.StringEncoders.PluginExists(EncodingName)) {
				compiler.StringEncoder = compiler.StringEncoders[EncodingName];

			} else {
				if (EncodingLabel == null) EncodingLabel = T.EvaluateExpression(compiler);
				try {
					if (!EncodingLabel.IsString) {
						compiler.StringEncoder = new StringEncodingWrapper(compiler, EncodingName, Encoding.GetEncoding((int)EncodingLabel.NumericValue));
					} else {
						compiler.StringEncoder = new StringEncodingWrapper(compiler, EncodingName, Encoding.GetEncoding(EncodingLabel.StringValue));
					}
				} catch (Exception ex) {
					throw new CompilerExpection(T, ex.Message);
				}
			}
		}
	}
}
