using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Core.Directives {

	[PluginName("contiguous"), PluginName("endcontiguous")]
	[Description("Enables contiguous block output over an area of source code.")]
	[Remarks(
@"Output data simply has an address and value. This means that if you skip a block of data (for example, via the <c>.org</c> directive) without writing anything in the intervening space the object file will have holes in it.
Some output formats, such as Intel HEX files, accomodate this problem by giving each sequence of data an address field. Other output writers, such as the raw writer plugin, ignore the missing data.
This plugin will automatically add explicit output data to fill the holes. It will use the current empty fill value.")]
	[Warning(@"Try and keep the mixing and matching of contiguous regions with non-contiguous regions to a minimum.")]
	[Category("Output Manipulation")]
	[CodeExample(@"; Empty spaces will be filled with the space character.
.emptyfill "" ""

; Start at address 00.
.org 00

#contiguous

.org 10
	.db ""A"" ; Appears at 10.
	
.org 20
	.db ""B"" ; Appears at 20.

#endcontiguous

.org 30
	.db ""C"" ; Appears at 21.

/* This example assumes use of the raw    *\
 * output plugin. A and B, being declared *
 * inside contiguous regions, appear      *
 * at their absolute positions within the *
 * output binary file. C, being outside a *
 * contiguous region, is simply appended  *
 * after B as the plugin is not allowed   *
 * to insert the missing data between 21  *
\* and 30.                                */")]
	[SeeAlso(typeof(Output.Raw)), SeeAlso(typeof(Output.RawPages)), SeeAlso(typeof(Output.IntelHex)), SeeAlso(typeof(Directives.EmptyFill))]
	public class Contiguous : IDirective {

		/// <summary>
		/// Is output currently contiguous?
		/// </summary>
		private bool IsContiguous = false;

		private Dictionary<int, int> LastOutputCounterPerPage;

		private bool Filling;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass == AssemblyPass.Pass2) {
				bool WasContiguous = this.IsContiguous;
				this.IsContiguous = directive == "contiguous";
			}
		}

		public Contiguous(Compiler c) {

			this.LastOutputCounterPerPage = new Dictionary<int, int>();

			c.PassBegun += delegate(object sender, EventArgs e) {
				this.IsContiguous = false;
				this.LastOutputCounterPerPage.Clear();
				this.Filling = false;
			};

			c.Labels.OutputCounter.ValueChanged += delegate(object sender, Label.ValueChangedEventArgs e) {
				if (!Filling && c.CurrentPass == AssemblyPass.Pass2) {
					
					// Get the output counter:
					Label Label = sender as Label;
					
					// Have we encountered it on this page before?
					if (!LastOutputCounterPerPage.ContainsKey(Label.Page)) {
						LastOutputCounterPerPage.Add(Label.Page, (int)Label.NumericValue);
					}


					// How has the label jumped?
					int Start = LastOutputCounterPerPage[Label.Page];
					int End = (int)Label.NumericValue;

					if (IsContiguous) {

						if (End < Start) {
							throw new CompilerExpection(c.CurrentSourceStatement.Source, "Contiguous output can only handle increasing output counter values.");
						} else {

							this.Filling = true;

							try {

								for (int Address = Start; Address < End; ++Address) {
									if (!c.DataExists(Label.Page, Address)) {
										Label.NumericValue = Address;
										c.WriteEmptyFill(1);
									}
								}

								Label.NumericValue = End;
							} finally {
								this.Filling = false;
							}

						}

					}

					LastOutputCounterPerPage[Label.Page] = End;
					
				}
			};

			c.PassEnded += delegate(object sender, EventArgs e) {
				if (c.CurrentPass == AssemblyPass.Pass2) {
				}
			};
		}
	}
}
