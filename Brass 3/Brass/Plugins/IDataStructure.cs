using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {
	/// <summary>
	/// Defines the interface that data structures need to implement.
	/// </summary>
	public interface IDataStructure : IPlugin {

		/// <summary>
		/// Returns the fixed size in bytes of the data structure.
		/// </summary>
		int Size { get; }

	}
}
