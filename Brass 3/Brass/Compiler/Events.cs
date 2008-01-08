using System;

namespace BeeDevelopment.Brass3 {
	public partial class Compiler {

		#region Events

		/// <summary>
		/// Event raised at the beginning of compilation.
		/// </summary>
		public event EventHandler CompilationBegun;
		/// <summary>
		/// Event raised at the beginning of compilation.
		/// </summary>
		protected virtual void OnCompilationBegun(EventArgs e) { 
			this.IsCompiling = true;
			if (CompilationBegun != null) CompilationBegun(this, e); 
		}

		/// <summary>
		/// Event raised at the end of compilation.
		/// </summary>
		public event EventHandler CompilationEnded;
		/// <summary>
		/// Event raised at the end of compilation.
		/// </summary>
		protected virtual void OnCompilationEnded(EventArgs e) {
			if (CompilationEnded != null) CompilationEnded(this, e);
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
			/// <param name="sourceException">A <see cref="CompilerException"/> to create a notification for.</param>
			public NotificationEventArgs(Compiler compiler, CompilerException sourceException)
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

		/// <summary>Raise a warning.</summary>
		/// <param name="message">A message describing the warning.</param>
		public void OnWarningRaised(string message) { this.OnWarningRaised(new NotificationEventArgs(this, message)); }

		/// <summary>Raise a warning.</summary>
		/// <param name="message">A message describing the warning.</param>
		/// <param name="statement">The statement that the warning relates to.</param>
		public void OnWarningRaised(string message, SourceStatement statement) { this.OnWarningRaised(new NotificationEventArgs(this, message, statement)); }

		/// <summary>Raise a warning.</summary>
		/// <param name="message">A message describing the warning.</param>
		/// <param name="source">The source that the warning relates to.</param>
		public void OnWarningRaised(string message, TokenisedSource source) { this.OnWarningRaised(new NotificationEventArgs(this, new CompilerException(source, message))); }

		/// <summary>Raise a warning.</summary>
		/// <param name="message">A message describing the warning.</param>
		/// <param name="token">The token that the warning relates to.</param>
		public void OnWarningRaised(string message, TokenisedSource.Token token) { this.OnWarningRaised(new NotificationEventArgs(this, new CompilerException(token, message))); }

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

		/// <summary>Raise a error.</summary>
		/// <param name="message">A message describing the error.</param>
		public void OnErrorRaised(string message) { this.OnErrorRaised(new NotificationEventArgs(this, message)); }

		/// <summary>Raise a error.</summary>
		/// <param name="message">A message describing the error.</param>
		/// <param name="statement">The statement that the error relates to.</param>
		public void OnErrorRaised(string message, SourceStatement statement) { this.OnErrorRaised(new NotificationEventArgs(this, message, statement)); }

		/// <summary>Raise a error.</summary>
		/// <param name="message">A message describing the error.</param>
		/// <param name="source">The source that the error relates to.</param>
		public void OnErrorRaised(string message, TokenisedSource source) { this.OnErrorRaised(new NotificationEventArgs(this, new CompilerException(source, message))); }

		/// <summary>Raise a error.</summary>
		/// <param name="message">A message describing the error.</param>
		/// <param name="token">The token that the error relates to.</param>
		public void OnErrorRaised(string message, TokenisedSource.Token token) { this.OnErrorRaised(new NotificationEventArgs(this, new CompilerException(token, message))); }


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
