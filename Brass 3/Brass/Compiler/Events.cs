using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;
using System.Xml;

namespace Brass3 {
	public partial class Compiler {

		#region Events

		public event EventHandler PassBegun;
		/// <summary>
		/// Event raised at the start of a pass.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPassBegun(EventArgs e) { 
			this.IsCompiling = true;
			if (PassBegun != null) PassBegun(this, e); 
		}

		public event EventHandler PassEnded;
		/// <summary>
		/// Event raised at the end of pass.
		/// </summary>
		protected virtual void OnPassEnded(EventArgs e) {
			if (PassEnded != null) PassEnded(this, e);
			this.IsCompiling = false;
		}

		public event EventHandler EnteringSourceFile;
		/// <summary>
		/// Event raised when a source file has been entered.
		/// </summary>
		protected virtual void OnEnteringSourceFile(EventArgs e) {
			if (EnteringSourceFile != null) EnteringSourceFile(this, e);
		}

		public event EventHandler LeavingSourceFile;
		/// <summary>
		/// Event raised when a source file is about to be left.
		/// </summary>
		protected virtual void OnLeavingSourceFile(EventArgs e) {
			if (LeavingSourceFile != null) LeavingSourceFile(this, e);
		}

		public class CompilerNotificationEventArgs : EventArgs {

			private readonly string message;
			public string Message {
				get { return this.message; }
			}

			private readonly CompilerExpection sourceException;
			public CompilerExpection SourceException {
				get { return this.sourceException; }
			}


			public CompilerNotificationEventArgs(string message) {
				this.message = message;
			}

			public CompilerNotificationEventArgs(string message, CompilerExpection sourceException)
				: this(message) {
				this.sourceException = sourceException;
			}

		}

		public delegate void CompilerNotificationEventHandler(object sender, CompilerNotificationEventArgs e);

		public event CompilerNotificationEventHandler WarningRaised;
		public virtual void OnWarningRaised(CompilerNotificationEventArgs e) { if (WarningRaised != null) WarningRaised(this, e); }

		public event CompilerNotificationEventHandler ErrorRaised;
		public virtual void OnErrorRaised(CompilerNotificationEventArgs e) { if (ErrorRaised != null) ErrorRaised(this, e); }

		public event CompilerNotificationEventHandler InformationRaised;
		public virtual void OnInformationRaised(CompilerNotificationEventArgs e) { if (InformationRaised != null) InformationRaised(this, e); }



		#endregion

	}
}
