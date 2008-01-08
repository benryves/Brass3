using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BeeDevelopment.Brass3.Plugins {

	/// <summary>
	/// Defines the interface for plugins with that handle string encoding.
	/// </summary>
	public interface IStringEncoder : IPlugin {

		/// <summary>
		/// Encode a string to using the string encoding.
		/// </summary>
		byte[] GetData(string toEncode);

		/// <summary>
		/// Decodes a string from an array of bytes.
		/// </summary>
		/// <param name="data">The data to decode.</param>
		string GetString(byte[] data);

		/// <summary>
		/// Gets a Unicode character from a character value.
		/// </summary>
		/// <param name="value">The value to encode.</param>
		char GetChar(int value);

	}
}
