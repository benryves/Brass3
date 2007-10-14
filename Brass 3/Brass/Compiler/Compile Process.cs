using System;
using System.Collections.Generic;
using System.Text;
using Brass3.Plugins;
using System.IO;

namespace Brass3 {
	public partial class Compiler {

		public Label RecompileRange(int firstStatement, int lastStatement) {
			return this.RecompileRange(firstStatement, lastStatement, null);
		}

		public Label RecompileRange(int firstStatement, int lastStatement, KeyValuePair<TokenisedSource.Token, TokenisedSource>[] tokenReplacements) {
			if (Math.Min(firstStatement, lastStatement) < 0 || Math.Max(firstStatement, lastStatement) >= this.statements.Count) {
				throw new InvalidOperationException("Cannot recompile a source range that hasn't been parsed.");
			}
			int PreservePosition = this.CurrentStatement;
			Label Result = null;
			for (this.CurrentStatement = firstStatement; this.CurrentStatement <= lastStatement; ++this.CurrentStatement) {
				if (tokenReplacements == null) {
					this.statements[this.CurrentStatement].Compile();
				} else {
					SourceStatement Duplicate = (SourceStatement)this.statements[this.CurrentStatement].Clone();

					foreach (KeyValuePair<TokenisedSource.Token, TokenisedSource> MacroArguments in tokenReplacements) {
						for (int i = 0; i < Duplicate.Source.Tokens.Length; ++i) {
							if (Duplicate.Source.Tokens[i].DataLowerCase == MacroArguments.Key.DataLowerCase) {
								TokenisedSource.Token[] Replacement = MacroArguments.Value.Tokens;
								Duplicate.Source.ReplaceToken(i, Replacement);
								//i = 0;
							}
						}
					}

					Result = Duplicate.Compile();

				}
			}
			this.CurrentStatement = PreservePosition;
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

		private List<SourceStatement> statements;
		public SourceStatement[] Statements {
			get { return this.statements.ToArray(); }
		}

		public bool CanAppendStatement {
			get {
				return this.CurrentPass == AssemblyPass.Pass1 && this.CurrentStatement == this.statements.Count - 1;
			}
		}

		public void AppendStatement(SourceStatement statement) {
			if (this.CurrentPass != AssemblyPass.Pass1) throw new InvalidOperationException("Source statements may only be appended during the first pass.");
			if (!CanAppendStatement) throw new InvalidOperationException("Source statements may only be appended to the end of a sequence of statements.");
			this.statements.Add(statement);
		}

		private int CurrentStatement = 0;

		private bool IsCompiling = false;

		public delegate void PreprocessMacro(Compiler compiler, ref TokenisedSource source, int index);

		internal Dictionary<string, PreprocessMacro> MacroLookup;

		#endregion

		#region Public Methods

		public void RegisterMacro(string name, PreprocessMacro function) {
			string Name = name.ToLowerInvariant();
			if (this.MacroLookup.ContainsKey(Name)) throw new InvalidOperationException("Macro " + name + " already defined.");
			this.MacroLookup.Add(Name, function);
		}

		public void UnregisterMacro(string name) {
			this.MacroLookup.Remove(name.ToLowerInvariant());
		}

		private bool FileStopPending = false;
		public void StopAssemblingCurrentFile() {
			FileStopPending = true;
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
						this.OnWarningRaised(new NotificationEventArgs(this, "Assembler not explicitly set, so assuming " + this.assembler.Name + "."));
					} else {
						this.OnErrorRaised(new NotificationEventArgs(this, "No assembler set."));
						return false;
					}
				}

				// Clear state:
				this.statements.Clear();
				this.output.Clear();

				// Run pass 1:
				this.BeginPass(AssemblyPass.Pass1);
				this.CompileFile(this.SourceFile);
				this.OnPassEnded(new EventArgs());

				// TODO: Warn if any output was written?
				this.output.Clear();

				// Clear variable variables:
				List<Label> ToRemove = new List<Label>();
				foreach (Label L in this.labels) {
					if (L != this.labels.ProgramCounter && L.NeedsClearingBetweenPasses) ToRemove.Add(L);
				}
				foreach (Label L in ToRemove) this.labels.Remove(L);

				// Run pass 2:
				this.BeginPass(AssemblyPass.Pass2);
				while (CurrentStatement < statements.Count) {
					SourceStatement PAS = statements[CurrentStatement++];
					PAS.Compile();
				}
				this.OnPassEnded(new EventArgs());


				// Done!

				// Do we need to write output?
				if (writeOutput) {
					using (FileStream OutputStream = new FileStream(this.destinationFile, FileMode.Create)) {
						this.OutputWriter.WriteOutput(this, OutputStream);
						OutputStream.Flush();
					}
					foreach (KeyValuePair<string, IListingWriter> Listing in this.listingFiles) {
						using (FileStream ListingStream = new FileStream(Listing.Key, FileMode.Create)) {
							Listing.Value.WriteListing(this, ListingStream);
							ListingStream.Flush();
						}
					}
				}

				return true;
			} finally {
				IsCompiling = false;
			}
		}

		/// <summary>
		/// Load, parse, and compile a file.
		/// </summary>
		/// <param name="file">The name of the file to compile.</param>
		/// <remarks>The parsed statements are cached, so you can only call this method during the initial pass.</remarks>
		public void CompileFile(string filename) {

			bool RaisedEnteredFileEvent = false;

			this.FileStopPending = false;

			if (this.currentPass == AssemblyPass.Pass2) throw new InvalidOperationException("You can only load and compile a file during the initial pass.");
			using (AssemblyReader AR = new AssemblyReader(this, new MemoryStream(Encoding.Unicode.GetBytes(File.ReadAllText(filename))))) {

				while ((CurrentStatement < this.statements.Count || AR.HasMoreData) && !FileStopPending) {

					// Source to compile:
					SourceStatement PAS;

					if (CurrentStatement >= this.statements.Count) {


						int StartLineNumber = AR.LineNumber;
						TokenisedSource FullSource = AR.ReadAssemblySource();


						// Strip it down to the code and code only:
						TokenisedSource Source = FullSource.GetCode();

						// If there are no tokens available, skip this statement:
						if (Source.Tokens.Length == 0) continue;

						PAS = new SourceStatement(
							this,
							Source,
							filename,
							StartLineNumber
						);

						statements.Add(PAS);
					} else {
						PAS = statements[CurrentStatement];
					}
					++CurrentStatement;

					// Raise event to say that "we've entered the source file!"
					if (!RaisedEnteredFileEvent) {
						this.OnEnteringSourceFile(new EventArgs());
						RaisedEnteredFileEvent = true;
					}

					PAS.Compile();
				}

			}

		}

	}
}
