using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BeeDevelopment.Brass3.Plugins {

	/// <summary>
	/// Defines the basic interface for output plugins.
	/// </summary>
	public interface IOutputWriter : IPlugin {

		/// <summary>
		/// Writes the output data from a compiler to a stream.
		/// </summary>
		/// <param name="compiler">The compiler to get the output data from.</param>
		/// <param name="stream">The stream to write the output to.</param>
		void WriteOutput(Compiler compiler, Stream stream);

		/// <summary>
		/// Gets the default extension for this particular output type.
		/// </summary>
		string DefaultExtension { get; }

	}


}
