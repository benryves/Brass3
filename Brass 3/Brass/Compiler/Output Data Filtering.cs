using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;
using System.Xml;

namespace Brass3 {

	public partial class Compiler {

		/// <summary>
		/// Gets the unique page indices in the output data.
		/// </summary>
		public int[] GetUniquePageIndices() {
			//TODO: Raise error if accessed after bad build.
			List<int> PageIndices = new List<int>();
			foreach (OutputData O in this.Output) {
				if (!PageIndices.Contains(O.Page)) PageIndices.Add(O.Page);
			}
			return PageIndices.ToArray();
		}

		
		/// <summary>
		/// Gets the output data for a particular page.
		/// </summary>
		/// <param name="page">The page index to retrieve data from.</param>
		public OutputData[] GetOutputDataOnPage(int page) {
			//TODO: Raise error if accessed after bad build.
			List<OutputData> Data = new List<OutputData>(this.Output.Length);
			foreach (OutputData O in this.output) {
				if (O.Page == page) Data.Add(O);
			}
			return Data.ToArray();
		}

	}
}
