using System;
using System.Collections.Generic;
using System.Text;

namespace BeeDevelopment.Brass3 {

	/// <summary>
	/// Represents an error that relates to a problem with a named label.
	/// </summary>
	public class LabelException : CompilerException {

		/// <summary>
		/// Creates a new instance of the <see cref="LabelException"/> class.
		/// </summary>
		/// <param name="token">The token that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public LabelException(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		/// <summary>
		/// Creates a new instance of the <see cref="LabelException"/> class.
		/// </summary>
		/// <param name="source">The source that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public LabelException(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
