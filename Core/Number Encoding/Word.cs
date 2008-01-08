using System;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Core.NumberEncoding {

	[Description("Defines a 16-bit word.")]
	[PluginName("word"), PluginName("uword")]
	public class Word : INumberEncoder {
		
		public int Size { get { return 2; } }

		public byte[] GetBytes(Compiler c, double d) {
			ushort value = (ushort)d;

			byte[] Result = new byte[this.Size];

			switch (c.Endianness) {
				case Endianness.Little:
					Result[0] = (byte)value;
					Result[1] = (byte)(value >>8);
					break;
				case Endianness.Big:
					Result[1] = (byte)value;
					Result[0] = (byte)(value >> 8);
					break;
				default:
					throw new InvalidOperationException();
			}

			return Result;
		}

		public double GetDouble(Compiler c, byte[] b) {

			switch (c.Endianness) {
				case Endianness.Little:
					return (double)(b[0] + (b[1] << 8));
				case Endianness.Big:
					return (double)(b[1] + (b[0] << 8));
				default:
					throw new InvalidOperationException();
			}

		}
	}
}
