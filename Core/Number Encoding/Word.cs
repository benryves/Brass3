using System;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;

namespace Core.NumberEncoding {

	[Description("Defines a 16-bit word.")]
	public class Word : INumberEncoder {
		
		public string[] Names { get { return new string[] { "word" }; } }
		public string Name { get { return this.Names[0]; } }

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
