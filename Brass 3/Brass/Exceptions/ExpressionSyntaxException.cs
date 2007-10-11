using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	public class InvalidExpressionSyntaxExpection : CompilerExpection {

		public InvalidExpressionSyntaxExpection(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		public InvalidExpressionSyntaxExpection(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
