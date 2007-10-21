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

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			
			object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] {
				TokenisedSource.ArgumentType.String | TokenisedSource.ArgumentType.SingleToken
			});
			compiler.StringEncoder = compiler.StringEncoders[Args[0] as string];

		}
	}
}
