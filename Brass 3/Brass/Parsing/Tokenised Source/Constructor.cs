using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BeeDevelopment.Brass3.Attributes;

namespace BeeDevelopment.Brass3 {
	public partial class TokenisedSource {

		/// <summary>
		/// Gets an array of <see cref="TokenisedSource"/> data from a string.
		/// </summary>
		/// <param name="compiler">The compiler that contains the source.</param>
		/// <param name="s">The string to parse.</param>
		/// <returns>An array of tokenised source data.</returns>
		public static TokenisedSource[] FromString(Compiler compiler, string s) {
			MemoryStream MS = new MemoryStream(Encoding.Unicode.GetBytes(s));
			AssemblyReader AR = new AssemblyReader(compiler, MS);
			List<TokenisedSource> Result = new List<TokenisedSource>();

			while (AR.HasMoreData) {
				Result.Add(AR.ReadAssemblySource());
			}

			return Result.ToArray();
		}

		/// <summary>
		/// Join an array of <c>TokenisedSource</c> into a single array of tokens.
		/// </summary>
		public static Token[] Join(TokenisedSource[] source) {
			List<Token> Result = new List<Token>();
			for (int i = 0; i < source.Length; ++i) {
				if (i != 0) Result.Add(new Token("\\"));
				Result.AddRange(source[i].tokens);
			}
			return Result.ToArray();
		}

		private ParserBehaviourChangeAttribute parserBehaviourChanges = null;
		/// <summary>
		/// Gets any parser behaviour changes that were encountered when parsing this source statement.
		/// </summary>
		public ParserBehaviourChangeAttribute ParserBehaviourChanges {
			get { return this.parserBehaviourChanges; }
		}

		/// <summary>
		/// Create an instance of the TokenisedSource class by parsing source code from a stream.
		/// </summary>
		/// <param name="compiler">The compiler that contains the source.</param>
		/// <param name="s">The stream to parse data from.</param>
		/// <param name="lineNumberCounter">A reference to a line number counter that is incremented with each line of source read.</param>
		public TokenisedSource(Compiler compiler, Stream s, ref int lineNumberCounter) {
			BinaryReader LineReader = new BinaryReader(s, Encoding.Unicode);

			int OriginalLineNumber = lineNumberCounter;

			List<KeyValuePair<Token.TokenTypes, StringBuilder>> Tokens = new List<KeyValuePair<Token.TokenTypes, StringBuilder>>(64);

			// Used for keeping track of whether we're inside a comment or not:
			bool InToEndOfLineComment = false;
			bool InMultilineComment = false;

			// Used for keeping track of whether we're inside a string literal or not:
			bool InString = false;
			char StringChar = '"';
			int EscapingLength = 0;

			// Used for keeping track of "absolute" strings.
			bool InAbsolute = false;

			// Keep track of parser-mangling directives.
			bool FoundDirective = false;
			int DirectiveIndex = -1;

			// Most recent character:
			char LastChar = (char)0;

			Token.TokenTypes CurrentTokenType = Token.TokenTypes.None;
			StringBuilder CurrentToken = new StringBuilder(32);

			Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));

			int OriginalPosition = (int)s.Position;

			bool IgnoredNewline = false;

			while (s.Position != s.Length) {

				// Hunt for the last valid token type.
				Token.TokenTypes LastTokenType = Token.TokenTypes.WhiteSpace;
				int LastTokenIndex = -1;
				for (int i = Tokens.Count - 1; i >= 0; i--) {
					if (Tokens[i].Value.Length > 0) {
						LastTokenType = Tokens[i].Key;
						LastTokenIndex = i;
						break;
					}
				}

				char c = LineReader.ReadChar();

				if (c == '\n') {
					if (lineNumberCounter > 0) ++lineNumberCounter;
					if (InToEndOfLineComment) {
						InToEndOfLineComment = false;
						CurrentTokenType = Token.TokenTypes.None;
						CurrentToken = new StringBuilder(32);
						Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
					}
				}

				if (InToEndOfLineComment) {
					CurrentToken.Append(c);
				} else if (InMultilineComment) {
					CurrentToken.Append(c);
					if (c == '/' && LastChar == '*') {
						InMultilineComment = false;
						CurrentTokenType = Token.TokenTypes.None;
						CurrentToken = new StringBuilder(32);
						Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
					}
				} else if (InAbsolute) {
					CurrentToken.Append(c);
					if (c == '}') InAbsolute = false;
				} else if (InString) {
					CurrentToken.Append(c);

					if (EscapingLength > 0) { // Are we currently escaping some characters?
						--EscapingLength;
						if (EscapingLength == 0) {  // Check for extended escape sequences.
							if (c == 'x') {
								EscapingLength = 2;
							} else if (c == 'u') {
								EscapingLength = 4;
							} else if (c == 'c' || c == '^') {
								EscapingLength = 1;
							}
						}
					} else if (c == '\\') { // Are we about to start escaping?
						EscapingLength = 1;
					} else if (c == StringChar) { // Is it the end of a string literal?
						CurrentTokenType = Token.TokenTypes.None;
						CurrentToken = new StringBuilder(32);
						Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
						InString = false;
					}
				} else {


					bool CanBreak = true;
					bool IsNewLine = c == '\n';
					bool IsSeperator = c == '\\';

					if (IsNewLine) {
						for (int i = Tokens.Count - 1; i >= 0; i--) {
							if (Tokens[i].Value.Length == 0) continue;
							if (Tokens[i].Key == Token.TokenTypes.Comment || Tokens[i].Key == Token.TokenTypes.WhiteSpace) continue;
							if (Tokens[i].Key == Token.TokenTypes.Punctuation && Tokens[i].Value.ToString() == ",") {
								CanBreak = false;
							}
							break;
						}
					}

					if ((IsNewLine || IsSeperator) && FoundDirective) {
						// We have a directive on this line.
						// Will it prevent us from breaking?
						if (this.parserBehaviourChanges == null) {
							string DirectiveName = Tokens[DirectiveIndex].Value.ToString().Substring(1);
							if (compiler.Directives.Contains(DirectiveName)) {
								object[] o = compiler.Directives[DirectiveName].GetType().GetCustomAttributes(typeof(ParserBehaviourChangeAttribute), false);
								if (o.Length == 1) {
									this.parserBehaviourChanges = o[0] as ParserBehaviourChangeAttribute;
								}
								if (this.parserBehaviourChanges == null) this.parserBehaviourChanges = new ParserBehaviourChangeAttribute(ParserBehaviourChangeAttribute.ParserBehaviourModifiers.None);
							} else {
								this.parserBehaviourChanges = new ParserBehaviourChangeAttribute(ParserBehaviourChangeAttribute.ParserBehaviourModifiers.None);
							}
						}
						if (IsSeperator &&
							((this.parserBehaviourChanges.Behaviour & ParserBehaviourChangeAttribute.ParserBehaviourModifiers.IgnoreStatementSeperator) != ParserBehaviourChangeAttribute.ParserBehaviourModifiers.None)) CanBreak = false;

						if (IsNewLine &&
							(
								((this.parserBehaviourChanges.Behaviour & ParserBehaviourChangeAttribute.ParserBehaviourModifiers.IgnoreAllNewLines) != ParserBehaviourChangeAttribute.ParserBehaviourModifiers.None) ||
								(!IgnoredNewline && (this.parserBehaviourChanges.Behaviour & ParserBehaviourChangeAttribute.ParserBehaviourModifiers.IgnoreFirstNewLine) != ParserBehaviourChangeAttribute.ParserBehaviourModifiers.None)
							)) {
							IgnoredNewline = true;
							CanBreak = false;
						}
					}


					if (Punctuation.IndexOf(c) != -1) { // Is it some punctuation?

						
						if (c == '*' && LastChar == '/') { // Multi-line comment?

							Tokens.RemoveAt(Tokens.Count - 1); // Purge the last token.
							KeyValuePair<Token.TokenTypes, StringBuilder> StartComment = Tokens[Tokens.Count - 1];
							Tokens.RemoveAt(Tokens.Count - 1); // Purge the old incorrectly read token.

							CurrentTokenType = Token.TokenTypes.Comment;
							CurrentToken = StartComment.Value;
							CurrentToken.Append(c);

							Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));

							InMultilineComment = true;
						} else {
							InAbsolute = c == '{';

							CurrentTokenType = InAbsolute ? Token.TokenTypes.None : Token.TokenTypes.Punctuation;
							CurrentToken = new StringBuilder(32);
							
							CurrentToken.Append(c);
							
							Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
							if (!InAbsolute) {
								CurrentTokenType = Token.TokenTypes.None;
								CurrentToken = new StringBuilder(32);
								Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
							}
						}
					} else if (c == ';') { // Is it a comment character?
						InToEndOfLineComment = true;
						CurrentTokenType = Token.TokenTypes.Comment;
						CurrentToken = new StringBuilder(32);
						CurrentToken.Append(c);
						Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
					} else if (CanBreak && (IsNewLine || IsSeperator)) { // Is it a termination character?
						CurrentTokenType = Token.TokenTypes.Seperator;
						CurrentToken = new StringBuilder(c.ToString());
						Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
						break; // Done!
					} else if ((c == '"' || c == '\'') && (CurrentTokenType == Token.TokenTypes.WhiteSpace || LastTokenType != Token.TokenTypes.None)) {
						// Is it the start of a string literal?
						InString = true;
						StringChar = c;
						CurrentTokenType = Token.TokenTypes.String;
						CurrentToken = new StringBuilder(32);
						CurrentToken.Append(c);
						Tokens.Add(new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken));
					} else {

						bool IsWhiteSpace = char.IsWhiteSpace(c);

						if (CurrentToken.Length > 0 && (CurrentTokenType == Token.TokenTypes.WhiteSpace) == IsWhiteSpace) {
							CurrentToken.Append(c);
						} else {

							// Kludge for %:
							if (LastTokenType == Token.TokenTypes.Punctuation && LastChar == '%' && (c == '0' || c == '1')) {

								while (Tokens.Count > LastTokenIndex) {
									Tokens.RemoveAt(LastTokenIndex);
								}

								CurrentToken = new StringBuilder(32);
								CurrentToken.Append("%" + c);
							} else {
								CurrentToken = new StringBuilder(32);
								CurrentToken.Append(c.ToString());
								bool IsDirective = !FoundDirective && (c == '.' || c == '#'); // Only one directive per line.
								if (IsDirective) {
									FoundDirective = true;
									DirectiveIndex = Tokens.Count;
								}
							}
							CurrentTokenType = (IsNewLine || IsSeperator) ? Token.TokenTypes.Seperator : (IsWhiteSpace ? Token.TokenTypes.WhiteSpace : (c == '.' || c == '#' ? Token.TokenTypes.Directive : Token.TokenTypes.None));
							KeyValuePair<Token.TokenTypes, StringBuilder> T = new KeyValuePair<Token.TokenTypes, StringBuilder>(CurrentTokenType, CurrentToken);
							Tokens.Add(T);
						}
					}
				}
				LastChar = c;
			}

			List<Token> FixedTokens = new List<Token>(Tokens.Count);
			foreach (KeyValuePair<Token.TokenTypes, StringBuilder> T in Tokens) {
				if (T.Value.Length > 0) {
					Token.TokenTypes TokenType = T.Key;
					string TokenData = T.Value.ToString();
					Token Tk = new Token(this, TokenType, TokenData, (int)s.Position);
					Tk.SourcePosition = OriginalPosition;
					OriginalPosition += Tk.Data.Length;
					FixedTokens.Add(Tk);
				}
			}

			#region Function Merging

			for (int i = 0; i < FixedTokens.Count - 1; ++i) {
				if (FixedTokens[i].Type == Token.TokenTypes.None && FixedTokens[i + 1].Data == "(") {
					FixedTokens[i].type = Token.TokenTypes.Function;
				}
			}

			#endregion

			#region Punctuation Merging

			LinkedList<Token> MergedPunctuation = new LinkedList<Token>(FixedTokens);
			foreach (string Mergable in MergablePunctuation) {

				string Start = Mergable[0].ToString();
				string End = Mergable[1].ToString();
				if (Mergable.Length == 3) {
					Start += End;
					End = Mergable[2].ToString();
				}

				LinkedListNode<Token> MergeNode = MergedPunctuation.First;
				while (MergeNode != null && MergeNode.Next != null) {

					if (MergeNode.Value.Type == Token.TokenTypes.Punctuation && MergeNode.Value.Data == Start &&
						MergeNode.Next.Value.Type == Token.TokenTypes.Punctuation && MergeNode.Next.Value.Data == End) {

						// Merge!
						MergeNode.Value.Data = Mergable;
						MergedPunctuation.Remove(MergeNode.Next);

					}

					MergeNode = MergeNode.Next;
				}
			}
			FixedTokens = new List<Token>(MergedPunctuation);

			#endregion

			this.tokens = FixedTokens.ToArray();

			if (this.parserBehaviourChanges == null) this.parserBehaviourChanges = new ParserBehaviourChangeAttribute(ParserBehaviourChangeAttribute.ParserBehaviourModifiers.None);

		}

		/// <summary>
		/// Create an instance of the TokenisedSource class from an array of tokens.
		/// </summary>
		/// <param name="tokens">The tokens that make up this source.</param>
		public TokenisedSource(Token[] tokens) {
			this.tokens = tokens;
		}

		/// <summary>
		/// Create an instance of the TokenisedSource class from an array of tokens and an original..
		/// </summary>
		/// <param name="tokens">The tokens that make up this source.</param>
		/// <param name="original">The original tokenised source.</param>
		private TokenisedSource(Token[] tokens, TokenisedSource original) {
			this.Original = original;
			this.tokens = tokens;
			this.parserBehaviourChanges = original.parserBehaviourChanges;
		}
	}
}
