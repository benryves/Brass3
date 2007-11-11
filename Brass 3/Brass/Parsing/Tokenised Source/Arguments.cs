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
		/// Gets a <see cref="TokenisedSource"/> contained within brackets.
		/// </summary>
		/// <param name="firstBracketIndex">The index of the first bracket.</param>
		public TokenisedSource GetTokensInBrackets(int firstBracketIndex) {
			int lastBracketIndex = this.GetCloseBracketIndex(firstBracketIndex);
			Token[] SubRange = new Token[lastBracketIndex - firstBracketIndex - 1];
			Array.Copy(this.tokens, firstBracketIndex + 1, SubRange, 0, SubRange.Length);
			return new TokenisedSource(SubRange, this);
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
			if (Result.Length < min || Result.Length > max) {
				string Expected = 
					max == int.MaxValue 
					?  (
						string.Format("{0} or more", min)
					)
					: string.Format(
						min == max ? "{0}" : (min + 1 == max ? "{0} or {1}" : "{0} to {2}"), 
						(min == 0 ? "no" : min.ToString()), 
						(max == 0 ? "no" : max.ToString())
					);
				string Arguments = min == 1 ? "argument" : "arguments";
				throw new DirectiveArgumentException(this.OutermostTokenisedSource, string.Format("Expected {0} {1} (you passed {2}).", Expected, Arguments, Result.Length == 0 ? "none" : Result.Length.ToString()));
			}
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

		/// <summary>
		/// Defines an argument type in comma-delimited expressions.
		/// </summary>
		[Flags()]
		public enum ArgumentType {
			/// <summary>
			/// No argument type.
			/// </summary>
			None = 0x0000,
			
			/// <summary>
			/// A numeric value (double).
			/// </summary>
			Value = 0x0001,
			/// <summary>
			/// A string value.
			/// </summary>
			String = 0x0002,
			/// <summary>
			/// An unescaped string (escape characters are ignored).
			/// </summary>
			UnescapedString = 0x0004,
			/// <summary>
			/// A filename (filenames are resolved and returned as a full path).
			/// </summary>
			Filename = 0x0008,
			/// <summary>
			/// A single token (string).
			/// </summary>
			SingleToken = 0x0010,

			/// <summary>
			/// All of the various types available.
			/// </summary>
			Types = Value | String | UnescapedString | Filename | SingleToken,
			
			/// <summary>
			/// Allow the argument to cause an implicit label creation.
			/// </summary>
			ImplicitLabelCreation = 0x0100,
			/// <summary>
			/// The argument is optional.
			/// </summary>
			Optional = 0x0200,
			/// <summary>
			/// The numeric value of the argument must be positive.
			/// </summary>
			Positive = 0x0400,

			/// <summary>
			/// All of the available argument modifiers.
			/// </summary>
			Modifiers = Optional | ImplicitLabelCreation | Positive,

		}

		/// <summary>
		/// Gets an array of argument types for retrieving a single numeric value from a comma-delimited expression.
		/// </summary>
		public static ArgumentType[] ValueArgument { get { return new ArgumentType[] { ArgumentType.Value }; } }
		/// <summary>
		/// Gets an array of argument types for retrieving a single string from a comma-delimited expression.
		/// </summary>
		public static ArgumentType[] StringArgument { get { return new ArgumentType[] { ArgumentType.String }; } }
		/// <summary>
		/// Gets an array of argument types for retrieving a single filename string from a comma-delimited expression.
		/// </summary>
		public static ArgumentType[] FilenameArgument { get { return new ArgumentType[] { ArgumentType.Filename }; } }
		/// <summary>
		/// Gets an array of argument types for retrieving a single token string from a comma-delimited expression.
		/// </summary>
		public static ArgumentType[] TokenArgument { get { return new ArgumentType[] { ArgumentType.SingleToken }; } }
		/// <summary>
		/// Gets an array of argument types for retrieving a single token (or string) string from a comma-delimited expression.
		/// </summary>
		public static ArgumentType[] StringOrTokenArgument { get { return new ArgumentType[] { ArgumentType.SingleToken | ArgumentType.String }; } }

		/// <summary>
		/// Get the results of some comma-delimited arguments.
		/// </summary>
		/// <param name="compiler">The compiler being used to compile the current project.</param>
		/// <param name="index">The start index of the comma-delimited argument list.</param>
		/// <param name="types">An array of types for each argument.</param>
		/// <returns>An array of objects (either doubles or strings), one for each evaluated argument.</returns>
		public object[] GetCommaDelimitedArguments(Compiler compiler, int index, ArgumentType[] types) {

			// Work out the number of arguments:
			bool HitOptional = false;
			int MinArgs = 0;
			int MaxArgs = types.Length;
			
			foreach (ArgumentType AT in types) {
				if ((AT & ArgumentType.Optional) == ArgumentType.Optional) {
					HitOptional = true;
				} else {
					if (HitOptional) throw new ArgumentException("Argument types cannot have mandatory arguments following optional arguments.");
					++MinArgs;
				}
			}

			// Get expression indices:
			int[] Arguments = this.GetCommaDelimitedArguments(index, MinArgs, MaxArgs);

			// Calculate the result:
			object[] Result = new object[Arguments.Length];
			for (int i = 0; i < Arguments.Length; ++i) {
				ArgumentType CurrentArgument = types[i] & (ArgumentType.Types);

				bool StringExpected = (CurrentArgument & (ArgumentType.String | ArgumentType.UnescapedString | ArgumentType.Filename)) != ArgumentType.None;

				if ((CurrentArgument & ArgumentType.SingleToken) == ArgumentType.SingleToken) {
					TokenisedSource SingleToken = this.GetExpressionTokens(Arguments[i]);
					if (SingleToken.Tokens.Length == 1 && (!StringExpected || SingleToken.Tokens[0].Type != Token.TokenTypes.String)) {
						Result[i] = SingleToken.Tokens[0].Data;
						continue;
					}
					CurrentArgument &= ~ArgumentType.SingleToken;
					if (CurrentArgument == ArgumentType.None) throw new DirectiveArgumentException(this, "Expected a single token value for argument " + (i + 1) + ".");
				}

				

				switch (CurrentArgument) {
					case ArgumentType.Value:
						Label L = this.EvaluateExpression(compiler, Arguments[i]);
						if (!L.Created && ((types[i] & ArgumentType.ImplicitLabelCreation) == ArgumentType.ImplicitLabelCreation)) L.SetImplicitlyCreated();
						Result[i] = L.NumericValue;
						if (((types[i] & ArgumentType.Positive) != ArgumentType.None) && L.NumericValue < 0) throw new DirectiveArgumentException(this, "Argument " + (i + 1) + " must be positive.");
						break;
					case ArgumentType.String:
					case ArgumentType.UnescapedString:
					case ArgumentType.Filename:
						Result[i] = this.GetExpressionStringConstant(compiler, Arguments[i], CurrentArgument == ArgumentType.String);
						if (CurrentArgument == ArgumentType.Filename) Result[i] = compiler.ResolveFilename(Result[i] as string);
						break;
					default:
						throw new ArgumentException("Argument type " + types[0].ToString() + " not supported.");
				}
			}

			return Result;

		}

	}
}
