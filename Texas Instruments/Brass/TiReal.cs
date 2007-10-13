using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace TexasInstruments.Brass {
	[Description("Defines a floating-point real number.")]
	[SeeAlso(typeof(TiComplex))]
	public class TiReal : INumberEncoder {

		public virtual string[] Names { get { return new string[] { "tireal" }; } }
		public string Name { get { return this.Names[0]; } }

		public int Size { get { return 9; } }

		public virtual byte[] GetBytes(Compiler c, double d) {
			return Types.Real.GetBytes((Types.Real)d);
		}

		public double GetDouble(Compiler c, byte[] b) {
			return (double)Types.Real.GetReal(b);
		}
		
	}
}
