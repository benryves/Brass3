using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace TexasInstruments.Brass {
	[Description("Defines a floating-point real number.")]
	[SeeAlso(typeof(TiComplex))]
	public class TiReal : INumberEncoder {

		public int Size { get { return 9; } }

		public virtual byte[] GetBytes(Compiler c, double d) {
			return Types.Real.GetBytes((Types.Real)d);
		}

		public double GetDouble(Compiler c, byte[] b) {
			return (double)Types.Real.GetReal(b);
		}
		
	}
}
