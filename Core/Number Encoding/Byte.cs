using System;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;

namespace Core.NumberEncoding {

	[Description("Defines an 8-bit byte.")]
	public class Byte : INumberEncoder {
		
		public string[] Names { get { return new string[] { "byte" }; } }
		public string Name { get { return this.Names[0]; } }

		public int Size { get { return 1; } }

		public byte[] GetBytes(Compiler c, double d) {
			return new byte[] { (byte)d };
		}

		public double GetDouble(Compiler c, byte[] b) {
			return (double)b[0];
		}
	}
}
