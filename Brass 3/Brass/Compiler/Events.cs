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

		/// <summary>
		/// Event raised at the start of a pass.
		/// </summary>
		public event EventHandler PassBegun;
		/// <summary>
		/// Event raised at the start of a pass.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPassBegun(EventArgs e) { 
			this.IsCompiling = true;
			if (PassBegun != null) PassBegun(this, e); 
		}

		/// <summary>
		/// Event raised at the end of pass.
		/// </summary>
		public event EventHandler PassEnded;
		/// <summary>
		/// Event raised at the end of pass.
		/// </summary>
		protected virtual void OnPassEnded(EventArgs e) {
			if (PassEnded != null) PassEnded(this, e);
			this.IsCompiling = false;
		}

		/// <summary>
		/// Defines information raised by the compiler.
		/// </summary>
		public class NotificationEventArgs : EventArgs {

			private readonly Compiler compiler;
			/// <summary>
			/// Gets the <see cref="Compiler"/> that generated this notification.
			/// </summary>
			public Compiler Compiler {
				get { return this.compiler; }
			}

			private readonly string message;
			/// <summary>
			/// Gets the text message for the information.
			/// </summary>
			public string Message {
				get { return this.message; }
			}


			private readonly TokenisedSource sourceStatement;
			/// <summary>
			/// Gets the source statement that this notification refers to (if any).
			/// </summary>
			public TokenisedSource SourceStatement {
				get { return this.sourceStatement; }
			}

			private readonly TokenisedSource.Token sourceToken;
			/// <summary>
			/// Gets the source statement token that this notification refers to (if any).
			/// </summary>
			public TokenisedSource.Token SourceToken {
				get { return this.sourceToken; }
			}

			private readonly int linenumber;
			/// <summary>
			/// Gets the line number that the notification refers to, or zero.
			/// </summary>
			public int LineNumber {
				get { return this.linenumber; }
			}

			private readonly string filename;
			/// <summary>
			/// Gets the filename that the notification refers to, or null.
			/// </summary>
			public string Filename {
				get { return this.filename; }
			}

			/// <summary>
			/// Create a new instance of the NotificationEventArgs class.
			/// </summary>
			/// <param name="compiler">The compiler that is raising the notification.</param>
			/// <param name="message">The text message describing the notification event.</param>
			/// <param name="filename">The name of the filename that the notification refers to.</param>
			/// <param name="linenumber">The line number that the notification refers to.</param>
			public NotificationEventArgs(Compiler compiler, string message, string filename, int linenumber) {
				this.compiler = compiler;
				this.message = message;
				this.filename = filename;
				this.linenumber = linenumber;
			}

			/// <summary>
			/// Create a new instance of the NotificationEventArgs class.
			/// </summary>
			/// <param name="compiler">The compiler that is raising the notification.</param>
			/// <param name="message">The text message describing the notification event.</param>
			public NotificationEventArgs(Compiler compiler, string message)
				: this(compiler, message, compiler.CurrentFile, compiler.CurrentLineNumber) {
			}

			/// <summary>
			/// Create a new instance of the NotificationEventArgs class.
			/// </summary>
			/// <param name="compiler">The compiler that is raising the notification.</param>
			/// <param name="sourceException">A <see cref="CompilerExpection"/> to create a notification for.</param>
			public NotificationEventArgs(Compiler compiler, CompilerExpection sourceException)
				: this(compiler, sourceException.Message) {
				this.sourceToken = sourceException.Token;
				this.sourceStatement = sourceException.SourceStatement;
			}

			/// <summary>
			/// Create a new instance of the NotificationEventArgs class.
			/// </summary>
			/// <param name="compiler">The compiler that is raising the notification.</param>
			/// <param name="message">The text message describing the notification event.</param>
			/// <param name="statement">The <see cref="SourceStatement"/> that this notification refers to.</param>
			public NotificationEventArgs(Compiler compiler, string message, SourceStatement statement)
				: this(compiler, message, statement.Filename, statement.LineNumber) {
				this.sourceStatement = statement.Source;
			}

		}

		/// <summary>
		/// Defines the event handler for compiler notifications.
		/// </summary>
		/// <param name="sender">The object raising the notification.</param>
		/// <param name="e">Data describing the notification.</param>
		public delegate void CompilerNotificationEventHandler(object sender, NotificationEventArgs e);

		/// <summary>
		/// Event raised on compiler warnings.
		/// </summary>
		public event CompilerNotificationEventHandler WarningRaised;
		/// <summary>
		/// Event raised on compiler warnings.
		/// </summary>
		/// <param name="e">Data describing the warning.</param>
		public virtual void OnWarningRaised(NotificationEventArgs e) {
			if (WarningRaised != null) {
				this.allWarnings.Add(e);
				WarningRaised(this, e);
			}
		}

		/// <summary>
		/// Event raised on compiler errors.
		/// </summary>
		public event CompilerNotificationEventHandler ErrorRaised;
		/// <summary>
		/// Event raised on compiler errors.
		/// </summary>
		/// <param name="e">Data describing the error.</param>
		public virtual void OnErrorRaised(NotificationEventArgs e) {
			if (ErrorRaised != null) {
				this.allErrors.Add(e);
				ErrorRaised(this, e); 
			}
		}

		/// <summary>
		/// Event raised on compiler messages.
		/// </summary>
		public event CompilerNotificationEventHandler MessageRaised;
		/// <summary>
		/// Event raised on compiler messages.
		/// </summary>
		/// <param name="e">Data describing the message.</param>
		public virtual void OnMessageRaised(NotificationEventArgs e) {
			if (MessageRaised != null) {
				this.allInformation.Add(e);
				MessageRaised(this, e);
			}
		}



		#endregion

	}
}
