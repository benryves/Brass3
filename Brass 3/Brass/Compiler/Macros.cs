using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	public partial class Compiler {

		#region Types

		/// <summary>
		/// Defines a macro replacement delegate.
		/// </summary>
		/// <param name="compiler">The compiler that the macro is being run in.</param>
		/// <param name="source">The assembly source that needs to have the macro run on it.</param>
		/// <param name="index">The index of the matched token that needs to be replaced.</param>
		public delegate void PreprocessMacro(Compiler compiler, ref TokenisedSource source, int index);

		#endregion

		#region Private/Internal Members

		/// <summary>
		/// A dictionary mapping strings (macro names) -> macro functions.
		/// </summary>
		internal Dictionary<string, PreprocessMacro> MacroLookup;
		
		#endregion

		#region Public Methods


		/// <summary>
		/// Run the macro preprocessor on a line of source.
		/// </summary>
		/// <param name="source">The source to run the macro preprocessor on.</param>
		public TokenisedSource[] PreprocessMacros(TokenisedSource source) {

			string OldSource = source.ToString();
			string NewSource = OldSource;

			for (int i = 0; i < source.Tokens.Length; ++i) {
				PreprocessMacro MacroFunction;
				if (this.MacroLookup.TryGetValue(source.Tokens[i].DataLowerCase, out MacroFunction)) {
					
					MacroFunction(this, ref source, i);
					NewSource = source.ToString();

				}
			}

			if (OldSource == NewSource) {
				return new TokenisedSource[] { source };
			} else {
				return TokenisedSource.FromString(this, NewSource);
			}

		}

		#endregion

	}
}
