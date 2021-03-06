using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;
using System.Globalization;

namespace SegaMasterSystem.Directives {
	[Description("Sets the product number in the Sega header.")]
	[Remarks("The product number needs be between 0 and 159999.")]
	[Syntax(".segaproduct product")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(SegaRegion))]
	[SeeAlso(typeof(SegaVersion))]
	[SeeAlso(typeof(Output.SmsRom))]
	[SeeAlso(typeof(Output.GGRom))]
	public class SegaProduct : IDirective {

		private int productNumber = 0;
		/// <summary>
		/// Gets or sets the part number.
		/// </summary>
		public int ProductNumber {
			get { return this.productNumber; }
			set {
				if (value < 0 || value > 159999) throw new ArgumentOutOfRangeException("Part number must be between 0 and 159999.");
				this.productNumber = value;
			}
		}

		public SegaProduct(Compiler c) {
			c.CompilationBegun += delegate(object sender, EventArgs e) {
				this.ProductNumber = 0;
			};
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			this.ProductNumber = (int)source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(index + 1, 1)[0]).NumericValue;
		}
	}
}
