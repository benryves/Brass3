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
	[SeeAlso(typeof(Functions.FileOperations.FileReadText))]
	public class IncText : IDirective {

		public string[] Names { get { return new string[] { "inctext" }; } }
		public string Name { get { return Names[0]; } }

		private Queue<byte[]> PrecompiledData;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.CurrentPass == AssemblyPass.Pass1) {
				object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] {
					TokenisedSource.ArgumentType.Filename
				});
				PrecompiledData.Enqueue(compiler.StringEncoder.GetData(File.ReadAllText(Args[0] as string)));
			} else {
				compiler.WriteOutput(PrecompiledData.Dequeue());
			}

						
			
		}

		public IncText(Compiler compiler) {
			this.PrecompiledData = new Queue<byte[]>();
			compiler.PassBegun += delegate(object sender, EventArgs e) {
				if (compiler.CurrentPass == AssemblyPass.Pass1) {
					this.PrecompiledData.Clear();
				}
			};
			compiler.PassEnded += delegate(object sender, EventArgs e) {
				if (compiler.CurrentPass == AssemblyPass.Pass2) {
					this.PrecompiledData.Clear();
				}
			};
		}

	}
}