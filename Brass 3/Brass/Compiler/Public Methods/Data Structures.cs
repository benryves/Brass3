using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using BeeDevelopment.Brass3.Plugins;

namespace BeeDevelopment.Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Gets a data structure by its name.
		/// </summary>
		/// <param name="name">The name of the data structure to search for.</param>
		/// <returns>The data structure, a number encoder wrapped as a structure, or null if nothing could be found.</returns>
		public DataStructure GetStructureByName(string name) {
			if (this.dataStructures.Contains(name)) {
				return this.dataStructures[name] as DataStructure;
			} else if (this.NumberEncoders.Contains(name)) {
				return new DataStructure(this.NumberEncoders[name]);
			} else {
				return null;
			}
		}


	}
}
