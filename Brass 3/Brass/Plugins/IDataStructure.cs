using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {
	/// <summary>
	/// Defines the interface that number encoders need to implement.
	/// </summary>
	public interface IDataStructure : IAliasedPlugin {

		/// <summary>
		/// Returns the fixed size in bytes of the number type.
		/// </summary>
		int Size { get; }

	}
}
