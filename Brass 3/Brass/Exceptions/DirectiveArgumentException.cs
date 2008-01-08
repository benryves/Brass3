using System;
using System.Collections.Generic;
using System.Text;

namespace BeeDevelopment.Brass3 {

	/// <summary>
	/// Represents an error in an argument passed to a directive or a function.
	/// </summary>
	public class DirectiveArgumentException : CompilerException {

		/// <summary>
		/// Creates an instance of <see cref="DirectiveArgumentException"/>.
		/// </summary>
		/// <param name="token">The token that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public DirectiveArgumentException(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		/// <summary>
		/// Creates an instance of <see cref="DirectiveArgumentException"/>.
		/// </summary>
		/// <param name="source">The source that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public DirectiveArgumentException(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
