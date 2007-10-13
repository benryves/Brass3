using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Legacy.Directives {
	[Syntax(".inclabels \"filename.ext\"")]
	[Description("Include a labels file to add labels quickly.")]
	[Remarks("This Brass 1 directive loaded a file of labels. This was to partially compensate for a slow parser; it also reduces the size of include files significantly.\r\nThe file format is just a long list of label definitions, in the form <c>[label&nbsp;name&nbsp;length&nbsp;in&nbsp;characters][label&nbsp;name][page&nbsp;(ushort)][value&nbsp;(ushort)]</c>.\r\nFor example, the label fish, value $1234, would be <c>.db 4, \"fish\", $34, $12, $00, $00.</c>")]
	[Category("Labels")]
	public class IncLabels : IDirective {

		public string[] Names { get { return new string[] { "inclabels" }; } }
		public string Name { get { return this.Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			// Only invoke this bugger in pass 1:
			if (compiler.CurrentPass != AssemblyPass.Pass1) return;

			// Fetch the filename.
			string Filename = compiler.ResolveFilename(
				source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.UnescapedString })[0] as string
			);

			// Read the label file:
			using (BinaryReader BR = new BinaryReader(new FileStream(Filename, FileMode.Open))) {
				while (BR.BaseStream.Position < BR.BaseStream.Length) {
					string LN = new string(BR.ReadChars(BR.ReadByte()));
					uint LV = BR.ReadUInt16();
					uint LP = BR.ReadUInt16();
					Label L = compiler.Labels.Create(new TokenisedSource.Token(LN));
					L.NumericValue = LV;
					L.Page = (int)LP;
					L.SetConstant();
					L.SetImplicitlyCreated();
				}
			}


		}

	}
}