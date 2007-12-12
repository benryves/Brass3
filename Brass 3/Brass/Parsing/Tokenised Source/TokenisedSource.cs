using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Brass3.Attributes;

namespace Brass3 {

	/// <summary>
	/// Defines a tokenised line of source code.
	/// </summary>
	public partial class TokenisedSource : ICloneable {

		#region Constants

		private const string Punctuation = ",+-*/!%^&|()[]{}~<>=?:";
		private static string[] MergablePunctuation = new string[] { "++", "--", "**", "<<", ">>", ">=", "<=", "!=", "==", "&&", "||", "+=", "-=", "*=", "/=", "%=", "&=", "|=", "^=", "<<=", ">>=" };

		#endregion

		#region Public Properties

		private Token[] tokens;
		/// <summary>
		/// Gets the tokens that make up this line of source code.
		/// </summary>
		public Token[] Tokens {
			get { return this.tokens; }
			private set { this.tokens = value; }
		}

		private int matchedItem;
		/// <summary>
		/// Gets or sets the index of the matched item.
		/// </summary>
		/// <remarks>The matched item could be an assembly instruction or a directive. This index is used internally by the assembler.</remarks>
		public int MatchedItem {
			get { return this.matchedItem; }
			set { this.matchedItem = value; }
		}

		/// <summary>
		/// Returns a string representation of the source.
		/// </summary>
		public override string ToString() {
			StringBuilder Result = new StringBuilder(this.tokens.Length * 4);
			for (int i = 0; i < this.tokens.Length; ++i) {
				if (i > 0 && !this.tokens[i].IsTouching(this.tokens[i-1])) {
					Result.Append(" ");
				}
				Result.Append(this.tokens[i].Data);
			}
			return Result.ToString();
		}

		#endregion

		#region Internal Properties

		private TokenisedSource Original = null;
		/// <summary>
		/// Gets the outermost tokenised source that contains this source range.
		/// </summary>
		public TokenisedSource OutermostTokenisedSource {
			get {
				TokenisedSource Source = this;
				while (Source.Original != null) {
					Source = Source.Original;
				}
				return Source;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Write the source to the console with syntax highlighting.
		/// </summary>
		public void WriteColouredConsoleOutput(bool highlightToken, Token tokenToHighlight, bool inverse) {
			ConsoleColor[] OriginalColours = new ConsoleColor[] { Console.ForegroundColor, Console.BackgroundColor };

			ISyntaxColourRule ColourRule = (inverse ? new SyntaxColourInverse() as ISyntaxColourRule : new SyntaxColourNormal() as ISyntaxColourRule);

			foreach (TokenisedSource.Token T in this.Tokens) {
				if (highlightToken && (T == tokenToHighlight || tokenToHighlight == null)) {
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.Yellow;
				} else {
					Console.ForegroundColor = ColourRule.GetForeground(T.Type);
					Console.BackgroundColor = ColourRule.GetBackground(T.Type);
				}
				Console.Write(T.Data.Replace("\r", "").Replace("\n", ""));
			}


			Console.ForegroundColor = OriginalColours[0];
			Console.BackgroundColor = OriginalColours[1];
		}


		/// <summary>
		/// Get the tokens for an entire subexpression.
		/// </summary>
		/// <param name="index">The index of the expression.</param>
		public TokenisedSource GetExpressionTokens(int index) {
			List<Token> Result = new List<Token>();
			foreach (Token T in this.tokens) if (T.ExpressionGroup == index) Result.Add(T);
			return new TokenisedSource(Result.ToArray(), this);
		}

		/// <summary>
		/// Get the single token for a subexpression.
		/// </summary>
		/// <param name="index">The index of the expression.</param>
		public TokenisedSource.Token GetExpressionToken(int index) {
			TokenisedSource Source = this.GetExpressionTokens(index);
			if (Source.Tokens.Length == 1) return Source.Tokens[0];
			throw new CompilerException(Source, Strings.ErrorExpectedSingleToken);
		}

		/// <summary>
		/// Get the single token for a subexpression and a index.
		/// </summary>
		/// <param name="expressionIndex">The index of the expression.</param>
		/// <param name="indexExpression">Outputs the subexpression containing the index.</param>
		public TokenisedSource.Token GetExpressionTokenAndIndex(int expressionIndex, out TokenisedSource indexExpression) {
			TokenisedSource Source = this.GetExpressionTokens(expressionIndex);
			indexExpression = null;
			if (Source.Tokens.Length == 1) return Source.Tokens[0];
			if (Source.Tokens[1].Data == "[" && Source.Tokens[Source.Tokens.Length - 1].Data == "]") {
				TokenisedSource.Token[] IndexExpression = new TokenisedSource.Token[Source.Tokens.Length - 3];
				Array.Copy(this.Tokens, 2, IndexExpression, 0, IndexExpression.Length);
				indexExpression = new TokenisedSource(IndexExpression, this);
				return Source.Tokens[0];
			}
			throw new CompilerException(Source, Strings.ErrorArgumentExpectedSingleTokenAndIndex);
		}

		/// <summary>
		/// Gets the index of a matching close bracket from the index of the first brace index.
		/// </summary>
		/// <param name="firstBraceIndex">The index of the opening bracket you wish to match.</param>
		public int GetCloseBracketIndex(int firstBraceIndex) {
			if (!tokens[firstBraceIndex].IsOpenBracket) throw new CompilerException(tokens[firstBraceIndex], Strings.ErrorBracketStartIndexOutOfBounds);
			int BraceDepth = 1;
			Stack<string> OpenBraces = new Stack<string>();
			OpenBraces.Push(tokens[firstBraceIndex].Data);
			for (++firstBraceIndex; firstBraceIndex < this.tokens.Length && BraceDepth > 0; ++firstBraceIndex) {
				if (this.tokens[firstBraceIndex].IsOpenBracket) {
					OpenBraces.Push(this.tokens[firstBraceIndex].Data);
					++BraceDepth;
				}
				if (this.tokens[firstBraceIndex].IsCloseBracket) {
					if (this.tokens[firstBraceIndex].MatchingBracket == OpenBraces.Pop()) {
						--BraceDepth;
					} else {
						throw new CompilerException(this.tokens[firstBraceIndex], Strings.ErrorBracketMismatch);
					}
				}
			}
			if (BraceDepth == 0) return firstBraceIndex - 1;
			throw new CompilerException(tokens[firstBraceIndex], Strings.ErrorBracketNotClosed);
		}

		/// <summary>
		/// Gets an array of token types and strings.
		/// </summary>
		/// <remarks>Use this to group like tokens together for optimal output in listing files.</remarks>
		public KeyValuePair<Token.TokenTypes, string>[] GetTypesAndStrings() {
			List<KeyValuePair<Token.TokenTypes, string>> Result = new List<KeyValuePair<Token.TokenTypes, string>>();

			LinkedList<TokenisedSource.Token> MergableTokens = new LinkedList<Token>();
			foreach (TokenisedSource.Token T in this.Tokens) {
				MergableTokens.AddLast(T.Clone() as TokenisedSource.Token);
			}


			LinkedListNode<TokenisedSource.Token> MergerPointer = MergableTokens.First;
			/*while (MergerPointer.Next != null) {
				if (MergerPointer.Next.Value.Type == MergerPointer.Value.Type) {
					TokenisedSource.Token MergedToken = MergerPointer.Value.Clone(MergerPointer.Value.Data + MergerPointer.Next.Value.Data) as TokenisedSource.Token;
					MergerPointer = MergerPointer.Next;
					MergableTokens.Remove(MergerPointer.Previous); // Remove old.
					MergableTokens.AddBefore(MergerPointer, MergedToken); // Insert new.
					MergableTokens.Remove(MergerPointer.Next); // Remove old.
				} else {
					MergerPointer = MergerPointer.Next;
				}
			}*/

			foreach (TokenisedSource.Token T in MergableTokens) {
				Result.Add(new KeyValuePair<Token.TokenTypes, string>(T.Type, T.Data));				
			}
			return Result.ToArray();
		}

		#endregion

		
		/// <summary>
		/// Clones the source and all of its tokens.
		/// </summary>
		public object Clone() {
			Token[] DuplicatedTokens = new Token[this.Tokens.Length];
			for (int i = 0; i < this.tokens.Length; ++i) DuplicatedTokens[i] = (Token)this.Tokens[i].Clone();
			TokenisedSource Clone = new TokenisedSource(DuplicatedTokens, this.Original);
			Clone.matchedItem = this.MatchedItem;
			Clone.parserBehaviourChanges = this.parserBehaviourChanges;
			return Clone;
		}

	}
}
