using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Switch off the compiler.
		/// </summary>
		/// <param name="reactivator">The only type of directive to call.</param>
		/// <remarks>
		/// If you switch the compiler off, you should pass a type of directive as a reactivator.
		/// This directive will still be invoked even when the compiler is off, so you can switch it back on again.
		/// </remarks>
		public void SwitchOff(Type reactivator) {
			this.Reactivator = reactivator;
			this.switchedOn = false;
		}

		/// <summary>
		/// Switch the compiler on.
		/// </summary>
		public void SwitchOn() {
			this.Reactivator = null;
			this.switchedOn = true;
		}

		public int RememberPosition() {
			return CurrentStatement - 1;

		}

		bool JustRecalledPosition = false;
		public void RecallPosition(int position) {
			RecallPosition(position, true);
		}
		public void RecallPosition(int position, bool doNotReparseLabel) {
			CurrentStatement = position;
			if (doNotReparseLabel) JustRecalledPosition = true;
		}
	
	}
}
