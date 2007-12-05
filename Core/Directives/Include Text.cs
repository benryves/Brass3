using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Directives {

	[Syntax(".inctext \"file\"")]
	[Description("Insert all data from a text file into the output, converting it to the current text encoding.")]
	[Category("Data")]
	[CodeExample("Convert <c>file.txt</c> to big-endian UTF-32.", ".big\r\n.stringencoder utf32\r\n.inctext \"file.txt\"")]
	[SeeAlso(typeof(Functions.FileOperations.FReadText))]
	public class IncText : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] {
					TokenisedSource.ArgumentType.Filename
				});
			compiler.WriteStaticOutput(compiler.StringEncoder.GetData(File.ReadAllText(Args[0] as string)));
		}

	}
}
