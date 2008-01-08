using System;
using System.Collections.Generic;
using System.Text;

namespace BeeDevelopment.Brass3 {

	/// <summary>
	/// Represents an error that occurs when a named label cannot be found.
	/// </summary>
	public class LabelNotFoundException : LabelException {

		/// <summary>
		/// Creates a new instance of the <see cref="LabelNotFoundException"/> class.
		/// </summary>
		/// <param name="token">The token that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public LabelNotFoundException(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		/// <summary>
		/// Creates a new instance of the <see cref="LabelNotFoundException"/> class.
		/// </summary>
		/// <param name="source">The source that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public LabelNotFoundException(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
