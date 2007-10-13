using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;
using System.Globalization;

namespace SegaMasterSystem.Directives {
	[Description("Sets the product number in the Sega header.")]
	[Remarks("The product number needs be between 0 and 159999.")]
	[Syntax(".segaproduct product")]
	[Category("Sega 8-bit")]
	[SeeAlso(typeof(SegaRegion))]
	[SeeAlso(typeof(SegaVersion))]
	[SeeAlso(typeof(Output.SmsRom))]
	[SeeAlso(typeof(Output.GameGearRom))]
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
			c.PassBegun += delegate(object sender, EventArgs e) {
				if (c.CurrentPass == AssemblyPass.Pass1) {
					this.ProductNumber = 0;
				}
			};			
		}

		public string Name { get { return this.Names[0]; } }
		public string[] Names { get { return new string[] { "segaproduct" }; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass == AssemblyPass.Pass2) {
				this.ProductNumber = (int)source.EvaluateExpression(compiler, source.GetCommaDelimitedArguments(index + 1, 1)[0]).NumericValue;
			}
		}


	}
}
