using System;
using System.Collections.Generic;
using System.Text;

using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;
using Brass3;

namespace Legacy.Directives {
	[Syntax(".asc string")]
	[Description("Defines bytes (<c>.db</c> alias).")]
	[Warning("This directive isn't implemented fully; it just registers a macro to replace <c>.asc</c> with <c>.db</c>.")]
	[CodeExample(".asc \"Please don't use me.\"\r\n.db \"Use me instead!\"")]
	[Category("Data")]
	[SeeAlso(typeof(Core.Directives.DataDeclaration))]
	public class Asc : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			throw new InvalidOperationException();
		}

		public Asc(Compiler compiler) {

			compiler.PassBegun += delegate(object sender, EventArgs e) {
				if (compiler.CurrentPass == AssemblyPass.CreatingLabels) {
					compiler.RegisterMacro(".asc", delegate(Compiler c, ref TokenisedSource src, int index) {
						src.ReplaceToken(index, new TokenisedSource.Token[] { new TokenisedSource.Token(".db") });
					});
				}
			};

		}

	}
}
