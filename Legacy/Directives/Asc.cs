using System;
using System.Collections.Generic;
using System.Text;

using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

using System.ComponentModel;
using BeeDevelopment.Brass3;

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

			compiler.CompilationBegun += delegate(object sender, EventArgs e) {
				compiler.RegisterMacro(".asc", delegate(Compiler c, ref TokenisedSource src, int index) {
					src.ReplaceToken(index, new TokenisedSource.Token[] { new TokenisedSource.Token(".db") });
				});
			};
		}
	}
}
