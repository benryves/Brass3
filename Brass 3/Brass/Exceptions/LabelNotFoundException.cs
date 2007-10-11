using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {
	public class LabelNotFoundExpection : LabelExpection {

		public LabelNotFoundExpection(TokenisedSource.Token token, string message)
			: base(token, message) {
		}

		public LabelNotFoundExpection(TokenisedSource source, string message)
			: base(source, message) {
		}

	}
}
