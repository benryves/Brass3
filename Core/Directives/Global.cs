using System.ComponentModel;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

namespace Core.Directives {

	[Description("resources://Core.Documentation/GlobalDescription")]
	[Syntax(".global expression [, expression [, ... ]]")]
	[Syntax(".global\r\n\texpression\r\n\t[expression]\r\n\t[...]\r\n.endglobal")]
	[PluginName("global"), PluginName("endglobal")]
	[DisplayName("global")]
	[Category("Labels")]
	public class Global : IDirective {

		/// <summary>Stores the module we were in when .global was called.</summary>
		private string EntryModule;

		public Global(Compiler compiler) {
			compiler.CompilationBegun += (sender, e) => this.EntryModule = null;
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			var Arguments = source.GetCommaDelimitedArguments(index + 1);

			switch (directive) {
				
				case "global":

					if (Arguments.Length == 0) {
						if (this.EntryModule != null) throw new CompilerException(source.Tokens[index], "You may not nest .global directives.");
						this.EntryModule = compiler.Labels.CurrentModule;
						compiler.Labels.CurrentModule = "";
					} else {
						var PreviousModule = compiler.Labels.CurrentModule;
						try {
							compiler.Labels.CurrentModule = "";
							foreach (var Argument in Arguments) source.EvaluateExpression(compiler, Argument, true, true).SetImplicitlyCreated();
						} finally {
							compiler.Labels.CurrentModule = PreviousModule;
						}
					}

					break;

				case "endglobal":
					// Sanity check:
					if (Arguments.Length != 0) throw new CompilerException(source, "Unexpected arguments for .endglobal directive.");
					if (this.EntryModule == null) throw new CompilerException(source.Tokens[index], "No global block to end.");
					if (!string.IsNullOrEmpty(compiler.Labels.CurrentModule)) throw new CompilerException(source.Tokens[index], "Scope is not global at this point.");
					// Exit the global scope.
					compiler.Labels.CurrentModule = this.EntryModule;
					this.EntryModule = null;
					break;
			}

		


		}

	}
}
