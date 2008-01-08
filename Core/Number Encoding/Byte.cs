using System;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Core.NumberEncoding {

	[Description("Defines an 8-bit byte.")]
	[PluginName("byte"), PluginName("ubyte")]
	public class Byte : INumberEncoder {
		
		public int Size { get { return 1; } }

		public byte[] GetBytes(Compiler c, double d) {
			return new byte[] { (byte)d };
		}

		public double GetDouble(Compiler c, byte[] b) {
			return (double)b[0];
		}
	}
}
