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

		public class NotificationEventArgs : EventArgs {

			private readonly string message;
			public string Message {
				get { return this.message; }
			}


			private readonly TokenisedSource sourceStatement;
			public TokenisedSource SourceStatement {
				get { return this.sourceStatement; }
			}

			private readonly TokenisedSource.Token sourceToken;
			public TokenisedSource.Token SourceToken {
				get { return this.sourceToken; }
			}

			private readonly int linenumber;
			public int LineNumber {
				get { return this.linenumber; }
			}

			private readonly string filename;
			public string Filename {
				get { return this.filename; }
			}

			public NotificationEventArgs(Compiler c, string message, string filename, int linenumber) {
				this.message = message;
				this.filename = filename;
				this.linenumber = linenumber;
			}

			public NotificationEventArgs(Compiler c, string message)
				: this(c, message, c.CurrentFile, c.CurrentLineNumber) {
			}

			public NotificationEventArgs(Compiler c, CompilerExpection sourceException)
				: this(c, sourceException.Message) {
				this.sourceToken = sourceException.Token;
				this.sourceStatement = sourceException.SourceStatement;
			}

			public NotificationEventArgs(Compiler c, string message, Compiler.SourceStatement statement)
				: this(c, message, statement.Filename, statement.LineNumber) {
				this.sourceStatement = statement.Source;
			}

		}

		public delegate void CompilerNotificationEventHandler(object sender, NotificationEventArgs e);

		public event CompilerNotificationEventHandler WarningRaised;
		public virtual void OnWarningRaised(NotificationEventArgs e) {
			if (WarningRaised != null) {
				this.allWarnings.Add(e);
				WarningRaised(this, e);
			}
		}

		public event CompilerNotificationEventHandler ErrorRaised;
		public virtual void OnErrorRaised(NotificationEventArgs e) {
			if (ErrorRaised != null) {
				this.allErrors.Add(e);
				ErrorRaised(this, e); 
			}
		}

		public event CompilerNotificationEventHandler InformationRaised;
		public virtual void OnInformationRaised(NotificationEventArgs e) {
			if (InformationRaised != null) {
				this.allInformation.Add(e);
				InformationRaised(this, e);
			}
		}



		#endregion

	}
}
