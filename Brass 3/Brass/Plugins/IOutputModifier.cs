using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BeeDevelopment.Brass3.Plugins {

	/// <summary>
	/// Defines the basic interface for output-modifying plugins.
	/// </summary>
	public interface IOutputModifier : IPlugin {

		/// <summary>
		/// Writes the output data from a compiler to a stream.
		/// </summary>
		/// <param name="compiler">The working compiler.</param>
		/// <param name="data">The data to modify.</param>		
		byte[] ModifyOutput(Compiler compiler, byte data);

	}


}
