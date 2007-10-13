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

		[Flags()]
		public enum ArgumentType {
			None = 0x0000,
			Optional = 0x8000,
			ImplicitLabelCreation = 0x4000,
			Value = 0x0001,
			String = 0x0002,
			UnescapedString = 0x0004,
			Filename = 0x0008,
		}

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
				ArgumentType CurrentArgument = types[i] & ~(ArgumentType.Optional | ArgumentType.ImplicitLabelCreation);
				switch (CurrentArgument) {
					case ArgumentType.Value:
						Label L = this.EvaluateExpression(compiler, Arguments[i]);
						if (!L.Created && ((types[i] & ArgumentType.ImplicitLabelCreation) == ArgumentType.ImplicitLabelCreation)) L.SetImplicitlyCreated();
						Result[i] = L.NumericValue;
						break;
					case ArgumentType.String:
					case ArgumentType.UnescapedString:
					case ArgumentType.Filename:
						Result[i] = this.GetExpressionStringConstant(compiler, Arguments[i], CurrentArgument == ArgumentType.String);
						if (CurrentArgument == ArgumentType.Filename) Result[i] = compiler.ResolveFilename(Result[i] as string);
						break;
					default:
						throw new ArgumentException("Argument type " + types[0].ToString() + " not recognised.");
				}
			}

			return Result;

		}

	}
}
