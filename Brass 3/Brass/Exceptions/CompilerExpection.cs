using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	public class CompilerExpection : ApplicationException {

		private readonly TokenisedSource source;
		public TokenisedSource SourceStatement {
			get { return this.source == null ? null : this.source.OutermostTokenisedSource; }
		}

		private readonly TokenisedSource.Token token;
		public TokenisedSource.Token Token {
			get { return this.token; }
		}

		public CompilerExpection(TokenisedSource.Token token, string message)
			: base(message) {
			this.token = token;
			this.source = token.Source;
		}

		public CompilerExpection(TokenisedSource source, string message)
			: base(message) {
			this.token = null;
			this.source = source;
		}

	}
}
