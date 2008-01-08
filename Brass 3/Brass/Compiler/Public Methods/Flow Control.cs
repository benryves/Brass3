using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using BeeDevelopment.Brass3.Plugins;

namespace BeeDevelopment.Brass3 {
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

		/// <summary>
		/// Returns a statement node that can be used to later jump back to the current statement.
		/// </summary>
		public LinkedListNode<SourceStatement> RememberPosition() {
			return this.currentStatement;
		}

		bool allowPositionToChange;
		/// <summary>
		/// Gets or sets a flag that specifies whether a plugin can change the current position.
		/// </summary>
		/// <remarks>Use this to disable jumping when executing your own code.</remarks>
		public bool AllowPositionToChange {
			get { return this.allowPositionToChange; }
			set { this.allowPositionToChange = value; }		
		}

		bool JustRecalledPosition = false;
		/// <summary>
		/// Return to a position in the source file and recompile from that point.
		/// </summary>
		/// <param name="position">The position to jump back to, retrieved by <seealso cref="RememberPosition"/>.</param>
		/// <remarks>The label assignment component of the statement jumped to will not be re-evaluated.</remarks>
		public void RecallPosition(LinkedListNode<SourceStatement> position) {
			if (!this.AllowPositionToChange) throw new InvalidOperationException(Strings.ErrorFlowControlDisabled);
			RecallPosition(position, true);
		}

		/// <summary>
		/// Return to a position in the source file and recompile from that point.
		/// </summary>
		/// <param name="position">The position to jump back to, retrieved by <seealso cref="RememberPosition"/>.</param>
		/// <param name="doNotReparseLabel">Set to true to stop the label assignment component of the remembered statement from being re-evaluated.</param>
		public void RecallPosition(LinkedListNode<SourceStatement> position, bool doNotReparseLabel) {
			if (!this.AllowPositionToChange) throw new InvalidOperationException(Strings.ErrorFlowControlDisabled);
			NextStatementToCompile = position;
			if (doNotReparseLabel) JustRecalledPosition = true;
		}
	
	}
}
