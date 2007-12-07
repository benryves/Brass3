using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Gets or sets a an assembly snippet header for the entire compilation process.
		/// </summary>
		public string Header { get; set; }

		/// <summary>
		/// Gets or sets a an assembly snippet footer for the entire compilation process.
		/// </summary>
		public string Footer { get; set; }


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

		#region Private Members

		private LinkedListNode<SourceStatement> nextStatementToCompile;
		
		/// <summary>
		/// Gets or sets the next source statement to compile.
		/// </summary>
		public LinkedListNode<SourceStatement> NextStatementToCompile {
			get { return this.nextStatementToCompile; }
			set {
				if (!this.AllowPositionToChange) throw new InvalidOperationException(Strings.ErrorFlowControlDisabled); 
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

		/// <summary>
		/// Gets the index of the current source statement.
		/// </summary>
		public int CurrentStatementIndex {
			get {
				int Result = 0;
				var CountStatements = this.currentStatement;
				while (CountStatements.Previous != null) {
					CountStatements = CountStatements.Previous;
					++Result;
				}
				return Result;
			}
		}

		private bool IsCompiling = false;

		private bool IsAssembling = false;

		#endregion

		#region Public Methods

		/// <summary>
		/// Register a text-replacement macro.
		/// </summary>
		/// <param name="name">The text string to run the macro on.</param>
		/// <param name="function">The macro replacement to perform.</param>
		public void RegisterMacro(string name, PreprocessMacro function) {
			string Name = name.ToLowerInvariant();
			if (this.MacroLookup.ContainsKey(Name)) throw new InvalidOperationException(string.Format(Strings.ErrorMacroAlreadyDefined, name));
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
				this.Breakpoints.Clear();
				this.ResetState();

				// Set the assembler:
				if (this.assembler == null) {
					if (this.assemblers.Count == 1) {
						IEnumerator<IAssembler> IEA = assemblers.GetEnumerator();
						IEA.MoveNext();
						this.assembler = IEA.Current;
						this.OnWarningRaised(new NotificationEventArgs(this, string.Format(Strings.ErrorAssemblerNotSetAssumeDefault, Compiler.GetPluginName(this.assembler))));
					} else {
						this.OnErrorRaised(new NotificationEventArgs(this, Strings.ErrorAssemblerNotSet));
						return false;
					}
				}

				// Clear state:
				this.statements.Clear();
				this.WorkingOutputData.Clear();
				this.output.Clear();

				// Assemble.
				this.OnCompilationBegun(new EventArgs());

				this.IsAssembling = true;

				if (!string.IsNullOrEmpty(this.Header)) this.CompileStream(new MemoryStream(Encoding.Unicode.GetBytes(this.Header)), null);

				this.CompileFile(this.SourceFile);

				if (!string.IsNullOrEmpty(this.Footer)) this.CompileStream(new MemoryStream(Encoding.Unicode.GetBytes(this.Footer)), null);

				this.IsAssembling = false;

				this.OnCompilationEnded(new EventArgs());


				
				// Insert dynamic data.
				foreach (var DataItem in this.WorkingOutputData) {

					DynamicOutputData DynamicDataItem = DataItem as DynamicOutputData;

					if (DynamicDataItem != null) {

						// Restore program/output counter states.
						this.Labels.ProgramCounter.NumericValue = DataItem.ProgramCounter;
						this.Labels.OutputCounter.NumericValue = DataItem.OutputCounter;
						this.Labels.ProgramCounter.Page = DataItem.Page;
						this.Labels.OutputCounter.Page = DataItem.Page;
						
						// Restore module state.
						this.Labels.CurrentModule = DynamicDataItem.Module;

						// Restore number of compiled statements.
						this.compiledStatements = DynamicDataItem.CompiledStatements;
		
						// Populate the dynamic data.
						(DataItem as DynamicOutputData).Generator.Invoke(DataItem as DynamicOutputData);
					}

					// Alert plugins that we're about to modify output data (and set state accordingly!)
					this.OnBeforeOutputDataModified(this, new OutputDataEventArgs(DataItem));

					byte[] ExpandedData = DataItem.Data;

					// Iterate over all output modifiers.
					foreach (var Modifier in this.OutputModifiers) {
						var WorkingExpanded = new List<byte>();
						foreach (var b in ExpandedData) {
							WorkingExpanded.AddRange(Modifier.ModifyOutput(this, b));	
						}
						ExpandedData = WorkingExpanded.ToArray();
					}

					// Add the finally calculated (static) data to the output.
					this.output.Add(new StaticOutputData(DataItem.Source, DataItem.Page, DataItem.ProgramCounter, DataItem.OutputCounter, ExpandedData, DataItem.Background));
				}

				if (this.allErrors.Count > 0) {
					this.OnErrorRaised(new NotificationEventArgs(this,string.Format(Strings.ErrorCancellingBuild, allErrors.Count)));
					return false; // An error!
				}
				// Done!
				this.RemoveRedundantOutputData();

				// Do we need to write output?
				if (writeOutput && this.OutputWriter != null) {
					string Filename = this.DestinationFile;
					if (string.IsNullOrEmpty(Filename) && this.OutputWriter != null) {
						string Basename = this.SourceFile;
						if (this.Project != null && !string.IsNullOrEmpty(this.Project.ProjectFilename)) Basename = this.Project.ProjectFilename;
						Filename = Path.Combine(Path.GetDirectoryName(Basename), Path.GetFileNameWithoutExtension(Basename) + "." + this.OutputWriter.DefaultExtension);
					}
					if (string.IsNullOrEmpty(Filename)) {
						this.OnErrorRaised(new NotificationEventArgs(this, Strings.ErrorOutputFilenameNotSet));
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
			
			} catch (CompilerExpection ex) {
				this.OnErrorRaised(new NotificationEventArgs(this, ex));
				return false;
			} catch (Exception ex) {				
				this.OnErrorRaised(new NotificationEventArgs(this, ex.Message, this.currentStatement.Value));
				return false;
			} finally {
				this.IsCompiling = false;
				this.IsAssembling = false;
			}
		}

		/// <summary>
		/// Parse and compile data from a stream.
		/// </summary>
		/// <param name="stream">The stream containing source to compile.</param>
		/// <param name="filename">The filename associated with a stream.</param>
		/// <remarks>The parsed statements are cached, so you can only call this method during the initial pass.</remarks>
		public void CompileStream(Stream stream, string filename) {
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
