using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Brass3.Attributes;

namespace Brass3 {
	public partial class TokenisedSource {

		/// <summary>
		/// Gets an array of comma-delimited arguments, raising an error if the number of arguments doesn't match the expected value.
		/// </summary>
		/// <param name="index">The index to start searching for.</param>
		/// <param name="arguments">The number of arguments.</param>
		/// <returns>An array of expression indices, one for each expression between commas.</returns>
		public int[] GetCommaDelimitedArguments(int index, int arguments) {
			return this.GetCommaDelimitedArguments(index, arguments, arguments);
		}

		/// <summary>
		/// Gets an array of comma-delimited arguments, raising an error if the number of arguments is out of range.
		/// </summary>
		/// <param name="index">The index to start searching for.</param>
		/// <param name="min">The minimum number of arguments.</param>
		/// <param name="max">The maximum number of arguments.</param>
		/// <returns>An array of expression indices, one for each expression between commas.</returns>
		public int[] GetCommaDelimitedArguments(int index, int min, int max) {
			int[] Result = this.GetCommaDelimitedArguments(index);
			if (Result.Length < min || Result.Length > max) throw new DirectiveArgumentException(this.OutermostTokenisedSource, "Invalid number of arguments.");
			return Result;
		}

		/// <summary>
		/// Gets an array of comma-delimited arguments.
		/// </summary>
		/// <param name="index">The index to start searching for.</param>
		/// <returns>An array of expression indices, one for each expression between commas.</returns>
		public int[] GetCommaDelimitedArguments(int index) {
			this.ResetExpressionIndices();
			List<int> Indices = new List<int>(this.tokens.Length);

			int BraceDepth = 0;
			if (index < this.tokens.Length) {
				for (int i = index; i < this.tokens.Length; ++i) {

					if (this.tokens[i].IsOpenBracket) {
						++BraceDepth;
					} else if (this.tokens[i].IsCloseBracket) {
						--BraceDepth;
					}

					if ((this.tokens[i].Data == "," || this.tokens[i].Type == Token.TokenTypes.Seperator) && BraceDepth == 0) {
						this.tokens[i].ExpressionGroup = 0;
						Indices.Add(Indices.Count + 1);
					} else {
						this.tokens[i].ExpressionGroup = Indices.Count + 1;
					}

				}
				Indices.Add(Indices.Count + 1);
			}

			return Indices.ToArray();
		}


		/// <summary>
		/// Reset all the expression indices for all tokens.
		/// </summary>
		public void ResetExpressionIndices() {
			foreach (Token T in this.Tokens) T.ExpressionGroup = 0;
		}
	}
}
