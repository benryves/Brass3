using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Brass3 {

	/// <summary>
	/// Provides a class to read assembly statements from a source file.
	/// </summary>
	public class AssemblyReader : StreamReader {

		private readonly Compiler Compiler;
		private readonly Queue<TokenisedSource> QueuedProcessedSource;

		private int lineNumber;
		/// <summary>
		/// Gets the current line number.
		/// </summary>
		public int LineNumber {
			get { return this.lineNumber; }
		}

		/// <summary>
		/// Creates an instance of the AssemblyReader to read from a stream.
		/// </summary>
		/// <param name="s">The stream to read from.</param>
		public AssemblyReader(Compiler c, Stream s)
			: base(s) {
			this.lineNumber = 1;
			this.Compiler = c;
			this.QueuedProcessedSource = new Queue<TokenisedSource>(16);
		}


		public bool HasMoreData {
			get { return this.QueuedProcessedSource.Count > 0 || this.BaseStream.Position != this.BaseStream.Length; }
		}

		/// <summary>
		/// Reads a single assembly source line from the stream.
		/// </summary>
		/// <returns>The source line ready for parsing.</returns>
		/// <remarks>This routine will strip comments and break up multiple lines of source automatically.</remarks>
		public TokenisedSource ReadAssemblySource() {
			// Is our queue empty?
			if (this.QueuedProcessedSource.Count == 0) {

				// Rip some stuff out of the source file:
				TokenisedSource FullSource = new TokenisedSource(this.Compiler, this.BaseStream, ref this.lineNumber);

				//TokenisedSource[] EnMacroed = this.Compiler.PreprocessMacros(FullSource);

				// Macro preprocess:
				bool AffectedByMacro = false;
				for (int i = 0; i < FullSource.Tokens.Length; ++i) {
					TokenisedSource.Token TokenToPreprocess = FullSource.Tokens[i];
					if (TokenToPreprocess.Type != TokenisedSource.Token.TokenTypes.WhiteSpace) {
						Compiler.PreprocessMacro MacroFunction;
						List<string> OldTokenValues = new List<string>(FullSource.Tokens.Length);
						foreach (TokenisedSource.Token T in FullSource.Tokens) OldTokenValues.Add(T.Data);

						int OldSize = FullSource.Tokens.Length;


						if (Compiler.MacroLookup.TryGetValue(TokenToPreprocess.DataLowerCase, out MacroFunction)) {
							bool AffectedByMacroThisLoop = false;
							MacroFunction(this.Compiler, ref FullSource, i);
							// Has anything changed?
							if (FullSource.Tokens.Length != OldSize) {
								AffectedByMacroThisLoop = true;
							} else {
								for (int j = 0; j < FullSource.Tokens.Length; ++j) {
									if (FullSource.Tokens[j].Data != OldTokenValues[j]) {
										AffectedByMacroThisLoop = true;
										break;
									}
								}
							}
							if (AffectedByMacroThisLoop) {
								AffectedByMacro = true;
								i = 0;
							}
						}
					}
				}

				if (AffectedByMacro) {
					//TODO: Remove this godawful parser hack.
					foreach (TokenisedSource TS in TokenisedSource.FromString(this.Compiler, FullSource.ToString())) {
						this.QueuedProcessedSource.Enqueue(TS);
					}
				} else {
					this.QueuedProcessedSource.Enqueue(FullSource);
				}

			}

			// Dequeue and return.
			return this.QueuedProcessedSource.Dequeue();
		}

	}
}
