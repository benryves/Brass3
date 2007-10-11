using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	public class LabelExpection : CompilerExpection {

		public LabelExpection(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		public LabelExpection(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
