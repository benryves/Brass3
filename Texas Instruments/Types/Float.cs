using System;
using System.Collections.Generic;
using System.Text;

namespace TexasInstruments.Types {
	public struct Real {

		private decimal Value;

		#region Operators

		public static explicit operator decimal(Real value) { return value.Value; }
		public static explicit operator Real(decimal value) { return new Real(value); }
		public static explicit operator double(Real value) { return (double)value.Value; }
		public static explicit operator Real(double value) { return new Real((decimal)value); }

		#endregion

		public Real(decimal value) { this.Value = value; }

		public Real(double value)
			: this((decimal)value) {
		}


		public byte[] GetBytes() {
			return Real.GetBytes(this);
		}

		public static byte[] GetBytes(Real value) {
			byte[] Result = new byte[9];

			// Object type:
			Result[0] = (byte)(value.Value < 0 ? 0x80 : 0x00);

			// Calculate the mantissa and exponent:
			decimal Mantissa = Math.Abs(value.Value);
			int Exponent = -1;

			while (Math.Truncate(Mantissa) != 0m) {
				Mantissa /= 10m;
				++Exponent;
			}
			while (Math.Truncate(Mantissa * 10m) == 0m) {
				Mantissa *= 10m;
				--Exponent;
			}


			if (Exponent < -128 || Exponent > 127) throw new ArgumentOutOfRangeException();

			// Exponent:
			Result[1] = (byte)(0x80 + Exponent);

			// Mantissa:
			byte[] AsAscii = Array.ConvertAll<byte, byte>(Encoding.ASCII.GetBytes(Mantissa.ToString(System.Globalization.CultureInfo.InvariantCulture).Substring(2).PadRight(14, '0')), delegate(byte b) { return (byte)((b >= '0' && b <= '9') ? (b - '0') : 0); });
			for (int i = 0; i < 7; ++i) {
				Result[i + 2] = (byte)(((AsAscii[(i * 2)] & 0x0F) << 4) | (AsAscii[(i * 2) + 1] & 0x0F));
			}

			return Result;

		}

		public static Real GetReal(byte[] data) {
			if (data == null) throw new ArgumentNullException();
			if (data.Length != 9) throw new ArgumentException();

			decimal Result = 0;
			for (int i = 0; i < 7; ++i) {
				Result += (decimal)(Math.Pow(10, i * 2 + 0) * (data[8 - i] & 0xF));
				Result += (decimal)(Math.Pow(10, i * 2 + 1) * (data[8 - i] >> 04));
			}
			Result *= (decimal)Math.Pow(10, data[1] - 0x80 - 14 + 1);

			if ((data[0] & 0x80) != 0) Result *= -1;

			return new Real(Result);

		}

		public override string ToString() {
			return this.Value.ToString();
		}

	}
}
