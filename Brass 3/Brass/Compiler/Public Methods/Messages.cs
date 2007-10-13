using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {

		public void OutputString(string toOutput) {
			this.OnInformationRaised(new NotificationEventArgs(this, toOutput));
		}

	}
}
