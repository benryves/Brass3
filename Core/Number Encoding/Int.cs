using System;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Core.NumberEncoding {

	[Description("Defines a 32-bit integer.")]
	[PluginName("int"), PluginName("uint")]
	public class Int : INumberEncoder {
	
		public int Size { get { return 4; } }

		public byte[] GetBytes(Compiler c, double d) {
			ushort value = (ushort)d;

			byte[] Result = new byte[this.Size];

			switch (c.Endianness) {
				case Endianness.Little:
					Result[0] = (byte)value;
					Result[1] = (byte)(value >> 8);
					Result[2] = (byte)(value >> 16);
					Result[3] = (byte)(value >> 24);
					break;
				case Endianness.Big:
					Result[3] = (byte)value;
					Result[2] = (byte)(value >> 8);
					Result[1] = (byte)(value >> 16);
					Result[0] = (byte)(value >> 24);
					break;
				default:
					throw new InvalidOperationException();
			}

			return Result;
		}

		public double GetDouble(Compiler c, byte[] b) {

			switch (c.Endianness) {
				case Endianness.Little:
					return (double)(b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24));
				case Endianness.Big:
					return (double)(b[3] + (b[2] << 8) + (b[1] << 16) + (b[0] << 24));
				default:
					throw new InvalidOperationException();
			}

		}
	}
}
