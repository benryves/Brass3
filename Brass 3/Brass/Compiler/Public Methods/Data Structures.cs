using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Gets a data structure by its name.
		/// </summary>
		/// <param name="name">The name of the data structure to search for.</param>
		/// <returns>The data structure, a number encoder wrapped as a structure, or null if nothing could be found.</returns>
		public DataStructure GetStructureByName(string name) {
			if (this.dataStructures.PluginExists(name)) {
				return this.dataStructures[name] as DataStructure;
			} else if (this.NumberEncoders.PluginExists(name)) {
				return new DataStructure(this.NumberEncoders[name]);
			} else {
				return null;
			}
		}


	}
}
