using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {

	/// <summary>
	/// Defines the basic interface for listing file plugins.
	/// </summary>
	public interface IListingWriter : IPlugin {

		/// <summary>
		/// Writes the listing from a compiler to a stream.
		/// </summary>
		/// <param name="compiler">The compiler to get the output data from.</param>
		/// <param name="stream">The stream to write the output to.</param>
		void WriteListing(Compiler compiler, Stream stream);

	}


}
