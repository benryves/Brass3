using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	/// <summary>
	/// Represents an error that occurs during compilation.
	/// </summary>
	public class CompilerException : ApplicationException {

		private readonly TokenisedSource source;
		/// <summary>
		/// Gets the <see cref="TokenisedSource"/> that the error occurred in.
		/// </summary>
		/// /// <remarks>This can be null if the error doesn't affect a particular statement.</remarks>
		public TokenisedSource SourceStatement {
			get { return this.source; }
		}

		private readonly TokenisedSource.Token token;
		/// <summary>
		/// Gets the <see cref="TokenisedSource.Token"/> that the error occurred in.
		/// </summary>
		/// <remarks>This can be null if the error doesn't affect an individual token.</remarks>
		public TokenisedSource.Token Token {
			get { return this.token; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="CompilerException"/> class.
		/// </summary>
		/// <param name="token">The token that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public CompilerException(TokenisedSource.Token token, string message)
			: base(message) {
			this.token = token;
			this.source = token.Source;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="CompilerException"/> class.
		/// </summary>
		/// <param name="source">The source that the error occured in.</param>
		/// <param name="message">A description of the error.</param>
		public CompilerException(TokenisedSource source, string message)
			: base(message) {
			this.token = null;
			this.source = source;
		}

	}
}
