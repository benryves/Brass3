using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Brass3.Attributes;

namespace Brass3 {

	public partial class TokenisedSource {

		/// <summary>
		/// Defines the atomic component of a line of source code: the token.
		/// </summary>
		public class Token : ICloneable {

			/// <summary>
			/// Defines a token's use.
			/// </summary>
			public enum TokenTypes {
				/// <summary>
				/// The token does not have any special functionality.
				/// </summary>
				None,
				/// <summary>
				/// The token is a string literal.
				/// </summary>
				String,
				/// <summary>
				/// The token contains whitespace.
				/// </summary>
				WhiteSpace,
				/// <summary>
				/// The token is a comment.
				/// </summary>
				Comment,
				/// <summary>
				/// The token is a directive name.
				/// </summary>
				Directive,
				/// <summary>
				/// The token is a label name.
				/// </summary>
				Label,
				/// <summary>
				/// The token is punctuation (such as an operator).
				/// </summary>
				Punctuation,
				/// <summary>
				/// The token is an instruction.
				/// </summary>
				Instruction,
				/// <summary>
				/// The token is a statement seperator.
				/// </summary>
				Seperator,
				/// <summary>
				/// The token is a function name.
				/// </summary>
				Function,
			}

			internal bool TypeLocked = true;

			internal TokenTypes type;
			/// <summary>
			/// Gets flags describing the token.
			/// </summary>
			public TokenTypes Type {
				get { return this.type; }
				set {
					if (TypeLocked || value != TokenTypes.Instruction) throw new CompilerExpection(this, "Type can only be set to TokenType.Instruction by assembler plugins.");
					this.type = value; 
				}
			}

			private string data;
			/// <summary>
			/// Gets the source token data.
			/// </summary>
			public string Data {
				get { return this.data; }
				internal set { this.data = value; this.lowerCaseData = null; }
			}

			private string lowerCaseData;
			/// <summary>
			/// Gets the source token in lowercase.
			/// </summary>
			public string DataLowerCase {
				get {
					if (lowerCaseData == null) lowerCaseData = data.ToLowerInvariant();
					return lowerCaseData;
				}
			}

			private TokenisedSource source;
			/// <summary>
			/// Gets the source statement that contains this token.
			/// </summary>
			public TokenisedSource Source {
				get { return this.source;}
				internal set { this.source = value; }
			}

			private int sourcePosition;
			/// <summary>
			/// The position from the start of the source file.
			/// </summary>
			public int SourcePosition {
				get { return this.sourcePosition; }
				internal set { this.sourcePosition = value; }
			}

			/// <summary>
			/// Creates an instance of a token.
			/// </summary>
			/// <param name="source">The <see cref="TokenisedSource"/> that contains the token.</param>
			/// <param name="flags">The token's type.</param>
			/// <param name="data">The string value of the token.</param>
			/// <param name="sourcePosition">The position of the token in the source file.</param>
			public Token(TokenisedSource source, TokenTypes flags, string data, int sourcePosition) {
				this.source = source;
				this.type = flags;
				this.data = data;
				this.sourcePosition = sourcePosition;
			}


			/// <summary>
			/// Creates an instance of a token.
			/// </summary>
			/// <param name="data">The string value of the token.</param>
			public Token(string data) {
				this.data = data;
			}

			/// <summary>
			/// Gets true if the token might be an expression operand.
			/// </summary>
			public bool IsExpressionOperand {
				get {
					return this.type == TokenTypes.None || this.type == TokenTypes.Label;
				}
			}

			/// <summary>
			/// Returns a string representation of the token.
			/// </summary>
			public override string ToString() {
				return this.ExpressionGroup + ":" + this.Type.ToString() + " [" + this.Data + "]";
			}


			/// <summary>
			/// Gets true if a token is an open bracket or parenthesis.
			/// </summary>
			public bool IsOpenBracket {
				get { return this.data == "(" || this.data == "["; }
			}


			/// <summary>
			/// Gets true if a token is a closing bracket or parenthesis.
			/// </summary>
			public bool IsCloseBracket {
				get { return this.data == ")" || this.data == "]"; }
			}

			/// <summary>
			/// Gets the matching bracket for the current token.
			/// </summary>
			public string MatchingBracket {
				get {
					if (this.IsOpenBracket) {
						switch (this.Data) {
							case "(": return ")";
							case "[": return "]";
							case "{": return "}";
							default: throw new InvalidOperationException();
						}
					} else if (this.IsCloseBracket) {
						switch (this.Data) {
							case ")": return "(";
							case "]": return "[";
							case "}": return "{";
							default: throw new InvalidOperationException();
						}
					} else {
						throw new CompilerExpection(this, "This token isn't an open or close bracket.");
					}
				}
			}

			/// <summary>The expression that this token falls into.</summary>
			public int ExpressionGroup;


			/// <summary>
			/// Returns true if the two tokens are touching.
			/// </summary>
			/// <param name="a">The first token to check.</param>
			/// <param name="b">The second token to check.</param>
			/// <returns>True if touching, false otherwise.</returns>
			public static bool IsTouching(Token a, Token b) {
				if (a.Source == null || b.Source == null) return false;
				if (a.Source != b.Source) return false;
				if (a.SourcePosition <= b.SourcePosition) {
					return b.SourcePosition == a.SourcePosition + a.Data.Length;
				} else {
					return a.SourcePosition == b.SourcePosition + b.Data.Length;
				}
			}

			/// <summary>
			/// Returns true if the token touches another token.
			/// </summary>
			/// <param name="token">The token to check against this one.</param>
			/// <returns>True if the tokens are touching, false otherwise.</returns>
			public bool IsTouching(Token token) {
				return Token.IsTouching(this, token);
			}

			/// <summary>
			/// Gets the value of the token as a string constant.
			/// </summary>
			/// <param name="escape">True to parse escape sequences.</param>
			public string GetStringConstant(bool escape) {

				StringBuilder Result = new StringBuilder(this.data.Length);

				char StringCharacter = this.data[0];
				int Escaping = 0;
				string EscapeString = "";

				for (int i = 1; i < this.data.Length - 1; ++i) {

					char c = this.data[i];

					// Handle escape characters:
					if (Escaping > 0) {
						EscapeString += c;
						--Escaping;
						if (c == 'x') {
							Escaping = 2;
						} else if (c == 'u') {
							Escaping = 4;
						} else if (c == 'c' || c == '^') {
							Escaping = 1;
						}
						if (Escaping == 0) {
							// Done!
							switch (EscapeString) {
								case "a": Result.Append('\a'); break;
								case "b": Result.Append('\b'); break;
								case "t": Result.Append('\t'); break;
								case "r": Result.Append('\r'); break;
								case "v": Result.Append('\v'); break;
								case "f": Result.Append('\f'); break;
								case "n": Result.Append('\n'); break;
								case "e": Result.Append('\u001B'); break;
								case "0": Result.Append('\0'); break;
								case "\\": Result.Append('\\'); break;
								case "\"": Result.Append('"'); break;
								default:
									int EscapeCode = 0;
									if ((EscapeString.Length == 3 && EscapeString[0] == 'x') || (EscapeString.Length == 5 && EscapeString[0] == 'u')) {
										if (LabelCollection.TryParseBase(EscapeString.Substring(1), out EscapeCode, 16)) {
											Result.Append((char)EscapeCode);
										} else {
											Result.Append("\\" + EscapeString);
										}
									} else if (EscapeString.Length == 2 && (EscapeString[0] == 'c' || EscapeString[0] == '^')) {
										int CharCode = (int)EscapeString[1] - 0x40;
										if (CharCode >= 0 && CharCode < 32) {
											Result.Append((char)CharCode);
										} else {
											Result.Append("\\" + EscapeString);
										}
									} else {
										Result.Append("\\" + EscapeString);
									}
									break;
							}
						}
					} else {
						if (escape && c == '\\') {
							EscapeString = "";
							Escaping = 1;
						} else {
							Result.Append(c);
						}
					}
				}
				return Result.ToString();
			}

			/// <summary>
			/// Returns true if a token contains a valid label name.
			/// </summary>
			public bool IsValidLabelName {
				get {
					return Label.IsValidLabelName(this.data);
				}
			}

			/// <summary>
			/// Gets the string contents of a comment token.
			/// </summary>
			public string GetCommentString() {
				if (this.Type != TokenTypes.Comment) throw new InvalidOperationException();
				if (this.data.Length == 0) return "";
				if (this.data[0] == ';') {
					return this.data.Substring(1);
				}
				return this.data.Substring(2, data.Length - 4);
			}


			/// <summary>
			/// Creates a copy of a token.
			/// </summary>
			public object Clone() {
				Token Clone = new Token(this.Source, this.Type, this.Data, this.SourcePosition);
				Clone.TypeLocked = this.TypeLocked;
				Clone.ExpressionGroup = this.ExpressionGroup;
				return Clone;
			}

			/// <summary>
			/// Creates a copy of a token and replaces its string value with another.
			/// </summary>
			/// <param name="data">The replacement string value.</param>
			public object Clone(string data) {
				Token T = this.Clone() as Token;
				T.data = data;
				return T;
			}
		}

	}
}
