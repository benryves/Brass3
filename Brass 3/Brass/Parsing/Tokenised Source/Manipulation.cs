using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using BeeDevelopment.Brass3.Attributes;

namespace BeeDevelopment.Brass3 {
	public partial class TokenisedSource {

		/// <summary>
		/// Replace a range of tokens between a certain range with an array of other tokens.
		/// </summary>
		/// <param name="start">The index of the first token to replace.</param>
		/// <param name="end">The index of the last token to replace.</param>
		/// <param name="tokens">The tokens to replace the range with.</param>
		public void ReplaceTokens(int start, int end, Token[] tokens) {
			List<Token> ReplacedTokens = new List<Token>();
			for (int i = 0; i < start; ++i) ReplacedTokens.Add(this.tokens[i]);
			foreach (Token T in tokens) {
				T.Source = this;
				T.SourcePosition = -1;
				ReplacedTokens.Add(T);
			}
			for (int i = end + 1; i < this.tokens.Length; ++i) ReplacedTokens.Add(this.tokens[i]);
			this.tokens = ReplacedTokens.ToArray();
		}


		/// <summary>
		/// Replace a token at a specified index with an array of other tokens.
		/// </summary>
		/// <param name="index">The index of the token to replace.</param>
		/// <param name="tokens">The tokens to replace it with.</param>
		public void ReplaceToken(int index, Token[] tokens) {
			this.ReplaceTokens(index, index, tokens);
		}

		/// <summary>
		/// Gets a copy of the source, returning only the code.
		/// </summary>
		/// <param name="includeSeperators">True if you want to keep seperators in the output.</param>
		/// <returns>The source minus comments and whitespace.</returns>
		public TokenisedSource GetCode(bool includeSeperators) {
			List<Token> Result = new List<Token>(this.tokens.Length);
			foreach (Token T in this.tokens) {
				if (T.Type == Token.TokenTypes.WhiteSpace || T.Type == Token.TokenTypes.Comment || (!includeSeperators && T.Type == Token.TokenTypes.Seperator)) continue;
				Result.Add(T);
			}
			return new TokenisedSource(Result.ToArray(), this);
		}

		/// <summary>
		/// Gets a copy of the source, returning only the code.
		/// </summary>
		/// <returns>The source minus comments, whitespace and seperators.</returns>
		public TokenisedSource GetCode() {
			return this.GetCode(false);
		}

	}
}
