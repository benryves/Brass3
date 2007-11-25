using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Plugins {
	
	/// <summary>
	/// Provides a simple wrapper around a single-byte .NET Encoding class.
	/// </summary>
	public class StringEncodingWrapper : IStringEncoder {

		private readonly Compiler compiler;
		/// <summary>
		/// Gets the compiler being used to build the current project.
		/// </summary>
		public Compiler Compiler { get { return this.compiler; } }

		private readonly string name;
		/// <summary>
		/// Gets the name of the encoding.
		/// </summary>
		public string Name { get { return this.name; } }

		private readonly Encoding encoding;
		/// <summary>
		/// Gets the .NET Encoding that this class wraps.
		/// </summary>
		public Encoding Encoding { get { return this.encoding; } }

		/// <summary>
		/// Creates a wrapper around a .NET Encoding.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <param name="name">The encoding name.</param>
		/// <param name="encoding">The .NET Encoding to wrap.</param>
		/// <remarks>Only single-byte encodings are supported.</remarks>
		public StringEncodingWrapper(Compiler compiler, string name, Encoding encoding) {
			if (!encoding.IsSingleByte) throw new NotSupportedException(Strings.ErrorAutomaticStringEncoderIsMultibyte);
			this.compiler = compiler;
			this.name = name;
			this.encoding = encoding;
		}

		/// <summary>
		/// Endodes a string into an array of bytes.
		/// </summary>
		/// <param name="toEncode">The string to encode.</param>
		public virtual byte[] GetData(string toEncode) {
			return this.encoding.GetBytes(toEncode);
		}

		/// <summary>
		/// Decodes a string from an array of bytes.
		/// </summary>
		/// <param name="data">The data to decode.</param>
		public virtual string GetString(byte[] data) {
			return this.Encoding.GetString(data);
		}

		/// <summary>
		/// Gets a Unicode character from an integer value.
		/// </summary>
		/// <param name="value">The character index to get a Unicode value from.</param>
		public virtual char GetChar(int value) {
			return this.encoding.GetString(new byte[] { (byte)value })[0];

		}

	}
}
