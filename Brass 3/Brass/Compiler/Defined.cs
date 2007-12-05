using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Returns true if the macro specified is defined.
		/// </summary>
		/// <param name="name">The name of the macro to check.</param>
		public bool MacroIsDefined(string name) {
			return this.MacroLookup.ContainsKey(name.ToLowerInvariant());
		}

		/// <summary>
		/// Returns true if the macro specified is defined.
		/// </summary>
		/// <param name="name">The name of the macro to check.</param>
		public bool MacroIsDefined(TokenisedSource.Token name) {
			return this.MacroIsDefined(name.Data);
		}

		/// <summary>
		/// Returns true if the label specified is defined.
		/// </summary>
		/// <param name="name">The name of the label to check.</param>
		/// <remarks>Constants implicitly return true.</remarks>
		public bool LabelIsDefined(TokenisedSource.Token name) {
			Label L;
			return (this.labels.TryParse(name, out L) && L.Created);
		}


		/// <summary>
		/// Returns true if the macro or label specified is defined.
		/// </summary>
		/// <param name="name">The name of the macro or label to check.</param>
		/// <remarks>Constants implicitly return true.</remarks>
		public bool IsDefined(TokenisedSource.Token name) {
			return this.MacroIsDefined(name) || this.LabelIsDefined(name);
		}

	}
}
