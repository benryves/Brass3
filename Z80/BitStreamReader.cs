using System;
using System.IO;
using System.Text;

namespace Z80 {

	/// <summary>
	/// Provides methods for reading data out of a bitstream.
	/// </summary>
	public class BitStreamReader : IDisposable {

		/// <summary>
		/// Gets or sets the underlying stream.
		/// </summary>
		private Stream BaseStream { get; set; }

		/// <summary>
		/// Creates an instance of the <see cref="BitStreamReader"/>.
		/// </summary>
		/// <param name="stream">The stream to read data from.</param>
		public BitStreamReader(Stream stream) {
			this.BaseStream = stream;
		}
		/// <summary>
		/// Disposes the <see cref="BitStreamReader"/> and its underlying stream.
		/// </summary>
		public void Dispose() {
			if (this.BaseStream != null) {
				this.BaseStream.Dispose();
				this.BaseStream = null;
			}
		}

		private byte CurrentByte;
		private int CurrentBitOffset = 7;

		public bool ReadBit() {
			CurrentBitOffset = (CurrentBitOffset + 1) & 7;
			if (CurrentBitOffset == 0) {
				var Byte = this.BaseStream.ReadByte();
				if (Byte == -1) throw new InvalidOperationException("Tried to read past the end of the file.");
				this.CurrentByte = (byte)Byte;
			}
			var Result = (this.CurrentByte & (0x80 >> CurrentBitOffset)) != 0;
			return Result;
		}

		#region Methods

		/// <summary>
		/// Aligns to the next byte boundary for subsequent reads.
		/// </summary>
		public void AlignToNextByte() {
			this.CurrentBitOffset = 7;
		}

		/// <summary>
		/// Reads a value from the <see cref="BitStreamReader"/>.
		/// </summary>
		/// <param name="bits">The width of the value in bits</param>
		/// <returns>The read value.</returns>
		/// <remarks>You may need to swap the endian-ness of the data read.</remarks>
		public int ReadValue(int bits) {
			int Result = 0;
			for (int i = 0; i < bits; ++i) {
				Result |= (this.ReadBit() ? ((1 << (bits - 1)) >> i) : 0x00);
			}
			return Result;
		}

		/// <summary>
		/// Reads a <see cref="Byte"/> from the <see cref="BitStreamReader"/>.
		/// </summary>
		/// <returns>The <see cref="Byte"/> read.</returns>
		public byte ReadByte() {
			return (byte)this.ReadValue(8);
		}

		/// <summary>
		/// Reads a <see cref="UInt16"/> from the <see cref="BitStreamReader"/>.
		/// </summary>
		/// <returns>The <see cref="UInt16"/> read.</returns>
		public ushort ReadUInt16() {
			var Result = this.ReadValue(16);
			return (ushort)(((Result & 0xFF) << 8) | (Result >> 8));
		}

		/// <summary>
		/// Reads an array of <see cref="Byte"/>s from the <see cref="BitStreamReader"/>.
		/// </summary>
		/// <param name="count">The number of <see cref="Byte"/>s to read.</param>
		/// <returns>The <see cref="Byte"/> array read.</returns>
		public byte[] ReadBytes(int count) {
			var Result = new byte[count];
			for (int i = 0; i < count; ++i) Result[i] = this.ReadByte();
			return Result;
		}

		/// <summary>
		/// Reads a string from the <see cref="BitStream"/>.
		/// </summary>
		/// <returns>The string has a three-bit length prefix and is assumed to be ASCII-encoded.</returns>
		public string ReadString() {
			return Encoding.ASCII.GetString(
				this.ReadBytes(this.ReadValue(3))
			);
		}

		#endregion
	}
}
