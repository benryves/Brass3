using System;
using System.Collections.Generic;
using System.Text;
using Brass3.Plugins;
using System.IO;

namespace Brass3 {
	public partial class Compiler {

		private string header;
		/// <summary>
		/// Gets or sets a an assembly snippet header for the entire compilation process.
		/// </summary>
		public string Header {
			get { return this.header; }
			set { this.header = value; }
		}

		private string footer;
		/// <summary>
		/// Gets or sets a an assembly snippet footer for the entire compilation process.
		/// </summary>
		public string Footer {
			get { return this.footer; }
			set { this.footer = value; }
		}


		/// <summary>
		/// Recompiles a range of statements.
		/// </summary>
		/// <param name="firstStatement">The index of the first statement to recompile.</param>
		/// <param name="lastStatement">The index of the last statement to recompile.</param>
		/// <returns>The result of the last statement's label assignment.</returns>
		public Label RecompileRange(LinkedListNode<SourceStatement> firstStatement, LinkedListNode<SourceStatement> lastStatement) {
			return this.RecompileRange(firstStatement, lastStatement, null);
		}

		/// <summary>
		/// Recompiles a range of statements.
		/// </summary>
		/// <param name="firstStatement">The index of the first statement to recompile.</param>
		/// <param name="lastStatement">The index of the last statement to recompile.</param>
		/// <param name="tokenReplacements">An array of Token-&gt;TokenisedSource replacement macros that only apply for the duration of the recompilation stage.</param>
		/// <remarks>Use the <paramref name="tokenReplacements"/> parameter to register temporary macro replacements (for example: when recompiling a range as a function call).</remarks>
		/// <returns>The result of the last statement's label assignment.</returns>
		public Label RecompileRange(LinkedListNode<SourceStatement> firstStatement, LinkedListNode<SourceStatement> lastStatement, KeyValuePair<TokenisedSource.Token, TokenisedSource>[] tokenReplacements) {

			LinkedListNode<SourceStatement> PreservePosition = this.NextStatementToCompile;
			Label Result = null;
			this.NextStatementToCompile = firstStatement;

			do  {

				SourceStatement ToCompile = this.NextStatementToCompile.Value;

				if (tokenReplacements != null) {

					ToCompile = ToCompile.Clone() as SourceStatement;

					foreach (KeyValuePair<TokenisedSource.Token, TokenisedSource> MacroArguments in tokenReplacements) {
						for (int i = 0; i < ToCompile.Source.Tokens.Length; ++i) {
							if (ToCompile.Source.Tokens[i].DataLowerCase == MacroArguments.Key.DataLowerCase) {
								TokenisedSource.Token[] Replacement = MacroArguments.Value.Tokens;
								ToCompile.Source.ReplaceToken(i, Replacement);
							}
						}
					}
				}

				this.currentStatement = this.nextStatementToCompile;
				this.nextStatementToCompile = this.nextStatementToCompile.Next;
				Result = ToCompile.Compile();

			} while (this.currentStatement != lastStatement);

			this.NextStatementToCompile = PreservePosition;
			return Result;
		}

		#region Private Methods



		private void BeginPass(AssemblyPass pass) {
			this.currentPass = pass;
			this.ResetState();
			this.OnPassBegun(new EventArgs());
		}


		#endregion

		#region Private Members

		private LinkedListNode<SourceStatement> nextStatementToCompile;
		
		/// <summary>
		/// Gets or sets the next source statement to compile.
		/// </summary>
		public LinkedListNode<SourceStatement> NextStatementToCompile {
			get { return this.nextStatementToCompile; }
			set {
				if (!this.AllowPositionToChange) throw new InvalidOperationException("Flow control has been temporarily disabled."); 
				this.nextStatementToCompile = value;
			}
		}

		
		private LinkedListNode<SourceStatement> currentStatement;
		/// <summary>
		/// Gets the current statement.
		/// </summary>
		public LinkedListNode<SourceStatement> CurrentStatement {
			get { return this.currentStatement; }
		}


		private readonly LinkedList<SourceStatement> statements;
		/// <summary>
		/// Gets an array of all of the parsed assembly source statements.
		/// </summary>
		/// <remarks>During the first pass this will still be in a process of being populated, but it will remain constant during the second pass.</remarks>
		public SourceStatement[] Statements {
			get { return new List<SourceStatement>(this.statements).ToArray(); }
		}

		private int compiledStatements;
		/// <summary>
		/// Gets the number of compiled statements this pass.
		/// </summary>
		public int CompiledStatements {
			get { return this.compiledStatements; }
		}

		private bool IsCompiling = false;

		#endregion

		#region Public Methods

		/// <summary>
		/// Register a text-replacement macro.
		/// </summary>
		/// <param name="name">The text string to run the macro on.</param>
		/// <param name="function">The macro replacement to perform.</param>
		public void RegisterMacro(string name, PreprocessMacro function) {
			string Name = name.ToLowerInvariant();
			if (this.MacroLookup.ContainsKey(Name)) throw new InvalidOperationException("Macro " + name + " already defined.");
			this.MacroLookup.Add(Name, function);
		}

		/// <summary>
		/// Remove a macro from the list of available macros.
		/// </summary>
		/// <param name="name">The name of the macro to remove.</param>
		public void UnregisterMacro(string name) {
			this.MacroLookup.Remove(name.ToLowerInvariant());
		}


		#endregion

		/// <summary>
		/// Compile the source file.
		/// </summary>
		public bool Compile(bool writeOutput) {

			try {

				this.allWarnings.Clear();
				this.allErrors.Clear();
				this.allInformation.Clear();

				// Set the assembler:
				if (this.assembler == null) {
					if (this.assemblers.Count == 1) {

						this.assembler = this.assemblers.GetEnumerator().Current;
						this.OnWarningRaised(new NotificationEventArgs(this, "Assembler not explicitly set, so assuming " + Compiler.GetPluginName(this.assembler) + "."));
					} else {
						this.OnErrorRaised(new NotificationEventArgs(this, "No assembler set."));
						return false;
					}
				}

				// Clear state:
				this.statements.Clear();
				this.output.Clear();

				// Run pass 1:
				this.BeginPass(AssemblyPass.CreatingLabels);

				if (!string.IsNullOrEmpty(this.Header)) this.CompileStream(new MemoryStream(Encoding.Unicode.GetBytes(this.Header)), null);

				this.CompileFile(this.SourceFile);

				if (!string.IsNullOrEmpty(this.Footer)) this.CompileStream(new MemoryStream(Encoding.Unicode.GetBytes(this.Footer)), null);

				this.OnPassEnded(new EventArgs());

				// TODO: Warn if any output was written?
				this.output.Clear();

				// Clear variable variables:
				List<Label> ToRemove = new List<Label>();
				foreach (Label L in this.labels) {
					if (L != this.labels.ProgramCounter && L.NeedsClearingBetweenPasses) ToRemove.Add(L);
				}
				foreach (Label L in ToRemove) this.labels.Remove(L);

				if (this.allErrors.Count > 0) {
					this.OnErrorRaised(new NotificationEventArgs(this, string.Format("{0} error{1} found: Cancelling build.", allErrors.Count, allErrors.Count == 1 ? "" : "s")));
					return false; // An error!
				}

				// Run pass 2:
				this.BeginPass(AssemblyPass.WritingOutput);
				while (NextStatementToCompile != null) {
					this.CompileCurrentStatement();
				}
				this.OnPassEnded(new EventArgs());

				if (this.allErrors.Count > 0) {
					this.OnErrorRaised(new NotificationEventArgs(this, string.Format("{0} error{1} found: Cancelling build.", allErrors.Count, allErrors.Count == 1 ? "" : "s")));
					return false; // An error!
				}


				// Done!

				// Do we need to write output?
				if (writeOutput && this.OutputWriter != null) {
					string Filename = this.DestinationFile;
					if (string.IsNullOrEmpty(Filename) && this.OutputWriter != null) {
						string Basename = this.SourceFile;
						if (this.Project != null && !string.IsNullOrEmpty(this.Project.ProjectFilename)) Basename = this.Project.ProjectFilename;
						Filename = Path.Combine(Path.GetDirectoryName(Basename), Path.GetFileNameWithoutExtension(Basename) + "." + this.OutputWriter.DefaultExtension);
					}
					if (string.IsNullOrEmpty(Filename)) {
						this.OnErrorRaised(new NotificationEventArgs(this, "Output filename not specified."));
					} else {
						using (FileStream OutputStream = new FileStream(Filename, FileMode.Create)) {
							this.OutputWriter.WriteOutput(this, OutputStream);
							OutputStream.Flush();
						}
					}
					foreach (KeyValuePair<string, IListingWriter> Listing in this.listingFiles) {
						using (FileStream ListingStream = new FileStream(Listing.Key, FileMode.Create)) {
							Listing.Value.WriteListing(this, ListingStream);
							if (ListingStream.CanWrite) ListingStream.Flush();
						}
					}
				}
				return true;
			} finally {
				IsCompiling = false;
			}
		}

		/// <summary>
		/// Parse and compile data from a stream.
		/// </summary>
		/// <param name="stream">The stream containing source to compile.</param>
		/// <param name="filename">The filename associated with a stream.</param>
		/// <remarks>The parsed statements are cached, so you can only call this method during the initial pass.</remarks>
		public void CompileStream(Stream stream, string filename) {
			if (this.currentPass == AssemblyPass.WritingOutput) throw new InvalidOperationException("You can only load and compile a file during the initial pass.");

			using (AssemblyReader AR = new AssemblyReader(this, stream)) {

				while (AR.HasMoreData || NextStatementToCompile != null) {

					if (NextStatementToCompile == null) {

						// Source to compile:
						SourceStatement PAS;

						int StartLineNumber = AR.LineNumber;
						TokenisedSource FullSource = AR.ReadAssemblySource();


						// Strip it down to the code and code only:
						TokenisedSource Source = FullSource.GetCode();

						PAS = new SourceStatement(
							this,
							Source,
							filename,
							StartLineNumber
						);

						if (this.NextStatementToCompile == null) {
							statements.AddLast(PAS);
							this.nextStatementToCompile = statements.Last;
						} else {
							statements.AddAfter(NextStatementToCompile, PAS);
							this.nextStatementToCompile = nextStatementToCompile.Next;
						}

					}
					this.CompileCurrentStatement();
				}
			}
		}


		/// <summary>
		/// Compile the current statement and move on to the next one (if required).
		/// </summary>
		/// <returns>The value returned by the statement's label expression.</returns>
		private Label CompileCurrentStatement() {
			this.currentStatement = this.nextStatementToCompile;
			this.nextStatementToCompile = this.nextStatementToCompile.Next;
			return this.currentStatement.Value.Compile();
		}

		/// <summary>
		/// Load, parse, and compile a file.
		/// </summary>
		/// <param name="filename">The name of the file to compile.</param>
		/// <remarks>The parsed statements are cached, so you can only call this method during the initial pass.</remarks>
		public void CompileFile(string filename) {
			this.CompileStream(new MemoryStream(Encoding.Unicode.GetBytes(File.ReadAllText(filename))), filename);
		}

	}
}
