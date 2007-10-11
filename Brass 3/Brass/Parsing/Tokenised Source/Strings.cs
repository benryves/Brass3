using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Brass3.Attributes;

namespace Brass3 {
	public partial class TokenisedSource {

		/// <summary>
		/// Check whether an expression is a string constant.
		/// </summary>
		/// <param name="index">The expression string to check.</param>
		/// <returns>True if it's a string constant, false otherwise.</returns>
		public bool ExpressionIsStringConstant(int index) {
			bool HasSeenString = false;
			foreach (Token T in this.Tokens) {
				if (T.ExpressionGroup == index) {
					if (T.Type == Token.TokenTypes.String) {
						HasSeenString = true;
					} else {
						return false;
					}
				}
			}
			return HasSeenString;
		}

		/// <summary>
		/// Gets an escaped string constant from an expression.
		/// </summary>
		/// <param name="index">The index of the expression to retrieve as a string constant.</param>
		/// <returns>The decoded string.</returns>
		public string GetExpressionStringConstant(int index) {
			return this.GetExpressionStringConstant(index, true);
		}

		/// <summary>
		/// Gets a string constant from an expression.
		/// </summary>
		/// <param name="index">The index of the expression to retrieve as a string constant.</param>
		/// <param name="escape">Decode escape sequences in the literal.</param>
		/// <returns>The decoded string.</returns>
		public string GetExpressionStringConstant(int index, bool escape) {
			StringBuilder Result = new StringBuilder(128);
			foreach (Token T in this.Tokens) {
				if (T.ExpressionGroup == index) {
					if (T.Type == Token.TokenTypes.String) {
						Result.Append(T.GetStringConstant(escape));

					}
				}
			}
			return Result.ToString();
		}
	}
}