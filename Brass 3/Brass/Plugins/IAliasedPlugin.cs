using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {

	/// <summary>
	/// Defines the interface for plugins with multiple aliased names.
	/// </summary>
	public interface IAliasedPlugin : IPlugin {

		/// <summary>
		/// Gets the name and aliases of the directive.
		/// </summary>
		string[] Names { get; }

	}


}
