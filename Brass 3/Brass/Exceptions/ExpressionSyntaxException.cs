using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Represents an error that occurs in malformed mathematical expressions.
	/// </summary>
	public class InvalidExpressionSyntaxExpection : CompilerExpection {

		/// <summary>
		/// Creates a new instance of the <see cref="InvalidExpressionSyntaxExpection"/> class.
		/// </summary>
		/// <param name="token">The token that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public InvalidExpressionSyntaxExpection(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		/// <summary>
		/// Creates a new instance of the <see cref="InvalidExpressionSyntaxExpection"/> class.
		/// </summary>
		/// <param name="source">The source that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public InvalidExpressionSyntaxExpection(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
