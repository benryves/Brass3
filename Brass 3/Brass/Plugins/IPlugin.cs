using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {

	/// <summary>
	/// Defines the basic interface for plugins.
	/// </summary>
	public interface IPlugin {

		/// <summary>
		/// Gets the name of the plugin.
		/// </summary>
		string Name { get; }

	}


}
