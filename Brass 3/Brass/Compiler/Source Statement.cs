using System;
using System.Collections.Generic;
using System.Text;
using Brass3.Plugins;
using System.IO;

namespace Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Represents a statement that can be compiled.
		/// </summary>
		public class SourceStatement : ICloneable {

			#region Types

			/// <summary>
			/// Represents the function of the statement.
			/// </summary>
			public enum StatementType {
				/// <summary>
				/// This statement only contains an expression.
				/// </summary>
				Expression,
				/// <summary>
				/// This statement contains a directive.
				/// </summary>
				Directive,
				/// <summary>
				/// This statement contains some assembly source.
				/// </summary>
				Assembly,
			}

			#endregion

			#region Properties


			private bool writtenToListFile;
			/// <summary>
			/// Gets whether the statement should be written to the list file or not.
			/// </summary>
			private bool WrittenToListFile {
				get { return this.writtenToListFile; }
			}

			private readonly Compiler compiler;
			/// <summary>
			/// Gets the compiler that created and controls this statement.
			/// </summary>
			public Compiler Compiler {
				get { return this.compiler; }
			}

			private string sourceFile;
			/// <summary>
			/// Gets the name of the source file.
			/// </summary>
			public string SourceFile {
				get { return this.sourceFile; }
			}

			private int lineNumber;
			/// <summary>
			/// Gets the line number of the start of this source.
			/// </summary>
			public int LineNumber {
				get { return this.lineNumber; }
			}

			private readonly TokenisedSource source;
			/// <summary>
			/// Gets the source tokens for this statement.
			/// </summary>
			public TokenisedSource Source {
				get { return this.source; }
			}

			private StatementType type;
			/// <summary>
			/// Gets the type of this statement.
			/// </summary>
			public StatementType Type {
				get { return this.type; }
			}

			private int expressionStatementSplit;
			/// <summary>
			/// Gets the index of the first token that forms the directive or assembly statement.
			/// </summary>
			public int ExpressionStatementSplit {
				get { return this.expressionStatementSplit; }
			}

			/// <summary>
			/// Gets the output data generated by this statement.
			/// </summary>
			public OutputData[] GeneratedOutputData {
				get {
					List<OutputData> Result = new List<OutputData>(256);
					foreach (OutputData D in this.compiler.output) {
						if (D.Source == this) Result.Add(D);						
					}
					Result.Sort();
					return Result.ToArray();
				}
			}

			private bool wasCompiled = true;
			/// <summary>
			/// Gets whether the compiler was switched on when the statement was compiled.
			/// </summary>
			public bool WasCompiled {
				get {
					return this.wasCompiled;
				}
			}

			private bool compilerWasOnBefore;
			public bool CompilerWasOnBefore {
				get { return this.compilerWasOnBefore; }
			}

			private bool compilerWasOnAfterwards;
			public bool CompilerWasOnAfterwards {
				get { return this.compilerWasOnAfterwards; }
			}

			#endregion


			/// <summary>
			/// Compiles the statement.
			/// </summary>
			public Label Compile() {
				return this.Compile(true);
			}

			/// <summary>
			/// Compiles the statement.
			/// </summary>
			/// <param name="mustMakeAssignment">True if an assignment must be made; false otherwise.</param>
			public Label Compile(bool mustMakeAssignment) {

				if (this.source.Tokens.Length == 0) return null;

				Label Result = null;

				try {

					this.compilerWasOnBefore = this.compiler.IsSwitchedOn;
					this.wasCompiled = false;

					/*if (compiler.currentPass == AssemblyPass.Pass2) {
						Console.Write(compiler.IsSwitchedOn ? "*" : " ");
						Console.Write("{0:X4}:", (int)compiler.Labels.ProgramCounter.Value);
						Console.WriteLine(this.Source);
					}*/

					//if (compiler.CurrentPass == AssemblyPass.Pass2) Console.WriteLine(((int)compiler.Labels.ProgramCounter.Value).ToString("X4") + ":" + this.Source);

					// OK, so we first execute the "label" bit:
					if (!compiler.JustRecalledPosition && compiler.IsSwitchedOn) {
						if (expressionStatementSplit != 0) {

							compiler.labelEvaluationResult = null;
							for (int i = 0; i < expressionStatementSplit; ++i) Source.Tokens[i].ExpressionGroup = -1;
							Label L = Source.EvaluateExpression(compiler, -1, true);
							if (L.IsConstant && mustMakeAssignment) throw new CompilerExpection(Source, "An assignment must be made.");

							Result = compiler.labelEvaluationResult = L;

							// Check for (implicit) duplicate label creation:
							if (compiler.CurrentPass == AssemblyPass.Pass1 && expressionStatementSplit == 1 && !L.IsConstant && L.Created) throw new CompilerExpection(Source.Tokens[0], "Duplicate label '" + L.Name + "'.");
							L.Created = true;
							//L.NumericValue = L.NumericValue; ?
						}
					}
					compiler.JustRecalledPosition = false;

					// ...and now we execute the "other" bit:
					if (expressionStatementSplit != Source.Tokens.Length) {
						if (this.type == StatementType.Directive) {
							string DirectiveName = Source.Tokens[expressionStatementSplit].Data.Substring(1).ToLowerInvariant();
							IDirective Directive = compiler.Directives[DirectiveName];
							if (compiler.IsSwitchedOn || Directive.GetType() == compiler.Reactivator) {
								this.wasCompiled = true;
								Directive.Invoke(compiler, Source, expressionStatementSplit, DirectiveName);
							}
						} else {
							if (compiler.IsSwitchedOn) {
								this.wasCompiled = true;
								compiler.CurrentAssembler.Assemble(compiler, Source, expressionStatementSplit);
							}
						}
						compiler.labelEvaluationResult = null;
					}
				} catch (CompilerExpection ex) {
					compiler.OnErrorRaised(new NotificationEventArgs(compiler, ex.Message, ex));
				} catch (Exception ex) {
					compiler.OnErrorRaised(new NotificationEventArgs(compiler, ex.Message, null));
				} finally {
					this.compilerWasOnAfterwards = this.compiler.IsSwitchedOn;
				}

				return Result;
			}


			/// <summary>
			/// Creates a clone of the statement.
			/// </summary>
			/// <remarks>The source statements are also cloned.</remarks>
			public object Clone() {
				SourceStatement Clone = new SourceStatement(this.compiler, (TokenisedSource)this.Source.Clone(), this.SourceFile, this.LineNumber);
				return Clone;
			}

			public SourceStatement(Compiler compiler, TokenisedSource source, string filename, int lineNumber) {

				this.compiler = compiler;
				this.source = source;
				this.sourceFile = filename;
				this.lineNumber = lineNumber;

				this.writtenToListFile = compiler.writingToListFile;

				// Now, we have (potentially) two parts:  [label] [directive | assembly code]

				int LabelInstructionSplit = Source.Tokens.Length;

				for (int i = 0; i < Source.Tokens.Length; ++i) {
					// Is this a directive?
					if (Source.Tokens[i].Type == TokenisedSource.Token.TokenTypes.Directive && compiler.Directives.PluginExists(Source.Tokens[i].Data.Substring(1))) {
						LabelInstructionSplit = i;
						this.type = StatementType.Directive;
						break;
					}
					// Is this some assembly code?
					if (compiler.CurrentAssembler != null) {
						for (int j = 0; j < Source.Tokens.Length; ++j) Source.Tokens[j].TypeLocked = false;
						if (compiler.CurrentAssembler.TryMatchSource(this.compiler, Source, i)) {
							LabelInstructionSplit = i;
							for (int j = 0; j < Source.Tokens.Length; ++j) Source.Tokens[j].TypeLocked = true;
							this.type = StatementType.Assembly;
							break;
						}
						for (int j = 0; j < Source.Tokens.Length; ++j) Source.Tokens[j].TypeLocked = true;
					}
				}
				this.expressionStatementSplit = LabelInstructionSplit;

			}


			public override string ToString() {
				return this.Source.ToString();
			}

		}

	}
}
