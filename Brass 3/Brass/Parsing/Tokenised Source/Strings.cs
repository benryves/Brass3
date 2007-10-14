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
		public bool ExpressionIsStringConstant(Compiler compiler, int index) {
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
		/// Gets a string constant from an expression.
		/// </summary>
		/// <param name="index">The index of the expression to retrieve as a string constant.</param>
		/// <param name="escape">Decode escape sequences in the literal.</param>
		/// <returns>The decoded string.</returns>
		public string GetExpressionStringConstant(Compiler compiler, int index) {
			return this.EvaluateExpression(compiler, index).StringValue;
		}

		/// <summary>
		/// Gets a string constant from an expression.
		/// </summary>
		/// <param name="index">The index of the expression to retrieve as a string constant.</param>
		/// <param name="escape">Decode escape sequences in the literal.</param>
		/// <returns>The decoded string.</returns>
		public string GetExpressionStringConstant(Compiler compiler, int index, bool escape) {
			if (escape) {
				return this.EvaluateExpression(compiler, index).StringValue;
			} else {
				TokenisedSource Src = this.Clone() as TokenisedSource;
				foreach (Token T in Src.tokens) {
					if (T.Type == Token.TokenTypes.String) {
						T.Data = T.Data[0] + T.Data.Substring(1, T.Data.Length - 2).Replace("\\", "\\\\").Replace("\"", "\\\"") + T.Data[T.Data.Length - 1];
					}					
				}
				return Src.EvaluateExpression(compiler, index).StringValue;

			}

		}
	}
}