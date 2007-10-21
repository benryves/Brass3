using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {
	/// <summary>
	/// Defines the interface that number encoders need to implement.
	/// </summary>
	public interface INumberEncoder : IDataStructure {

		/// <summary>
		/// Encodes a double-precision value into an array of bytes.
		/// </summary>
		/// <param name="d">The number to encode.</param>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <returns>The encoded data.</returns>
		byte[] GetBytes(Compiler compiler, double d);

		/// <summary>
		/// Decodes a double-precision value from an array of bytes.
		/// </summary>
		/// <param name="b">The data to decode.</param>
		/// <param name="compiler">The compiler being used to build the current project.</param>
		/// <returns>The decoded data.</returns>
		double GetDouble(Compiler compiler, byte[] b);


	}
}
