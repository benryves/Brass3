using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	public class ParseErrorExpection : CompilerExpection {

		public ParseErrorExpection(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		public ParseErrorExpection(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
