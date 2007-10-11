using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace TexasInstruments.Brass {
	[Description("Defines a floating-point number that forms one of the two components of a complex number.")]
	[Warning("A complex number is made up of two components - this encoding only creates one of those two components for you.")]
	[CodeExample("Defines the complex number -234.5+0.25i.", "ComplexNumber: .data ticomplex -234.5, +0.25\r\n\r\n/*\r\n\r\nThis outputs the data:\r\n\r\n\t8C 82 23 45 00 00 00 00 00\r\n\t0C 7F 25 00 00 00 00 00 00\r\n\r\nTo copy the components to OP1 and OP2 you could\r\ndo something like this:\r\n\r\n\tld hl,ComplexNumber[1]\r\n\trst rMov9ToOP1\r\n\tbcall(_OP1toOP2)\r\n\t\r\n\tld hl,ComplexNumber[0]\r\n\trst rMov9ToOP1\r\n\r\n*/")]
	[SeeAlso(typeof(TiReal))]
	public class TiComplex : TiReal {

		public override string[] Names {
			get { return new string[] { "ticomplex" }; }
		}

		public override byte[] GetBytes(Compiler c, double d) {
			byte[] Data = Types.Real.GetBytes((Types.Real)d);
			Data[0] |= 0x0C;
			return Data;
		}
		
	}
}
