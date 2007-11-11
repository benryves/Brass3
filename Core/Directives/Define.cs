using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".define name [substitution]")]
	[Syntax(".define name(args) substitution")]
	[Remarks("Macros perform 'stupid' text substitution. This can cause problems with operator order of precedence if you try and use macros as functions.")]
	[CodeExample("A problem with text-replacement macros.", ".define product(x, y) x * y\r\n\r\n; This outputs six as expected.\r\n.echoln product(2, 3)\r\n\r\n; This outputs four, incorrectly!\r\n.echoln product(1 + 1, 1 + 1 + 1)\r\n\r\n; By adding extra parentheses, this 'works'.\r\n.define product2(x, y) (x) * (y)\r\n.echoln product2(1 + 1, 1 + 1 + 1)")]
	[ParserBehaviourChange(ParserBehaviourChangeAttribute.ParserBehaviourModifiers.IgnoreStatementSeperator | ParserBehaviourChangeAttribute.ParserBehaviourModifiers.SkipMacroPreprocessor)]
	public class Define : IDirective {

		private static TokenisedSource GetArguments(TokenisedSource s, int i) {
			if (s.Tokens[i + 1].Data != "(") throw new CompilerExpection(s, "Macro expects parameters.");

			List<TokenisedSource.Token> ArgumentitiveTokens = new List<TokenisedSource.Token>();
			int Start = i + 2; int End;
			int BraceDepth = 1;
			for (End = Start; End < s.Tokens.Length; ++End) {
				switch (s.Tokens[End].Data) {
					case "(":
						++BraceDepth;
						break;
					case ")":
						if (--BraceDepth == 0) goto FoundArgumentitiveTokens;
						break;
				}
				ArgumentitiveTokens.Add(s.Tokens[End]);
			}
		FoundArgumentitiveTokens: ;
			return new TokenisedSource(ArgumentitiveTokens.ToArray());
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			
			List<TokenisedSource.Token> Replacement = new List<TokenisedSource.Token>();
			List<string> ParameterNames = new List<string>();

			bool HasParameters = false;

			TokenisedSource SourceAllInclusive = source.OutermostTokenisedSource.GetCode(true);

			if (index == source.Tokens.Length - 2) {
				Replacement.Add(SourceAllInclusive.Tokens[index + 1]);
			} else {
				for (int i = index + 2; i < SourceAllInclusive.Tokens.Length; ++i) {
					if (SourceAllInclusive.Tokens[i].Type == TokenisedSource.Token.TokenTypes.Seperator && SourceAllInclusive.Tokens[i].Data.Trim() == "") continue;
					Replacement.Add(SourceAllInclusive.Tokens[i]);
				}
				if (SourceAllInclusive.Tokens[index + 1].Type == TokenisedSource.Token.TokenTypes.Function) {
					HasParameters = true;
					TokenisedSource Parameters = GetArguments(new TokenisedSource(Replacement.ToArray()), -1);
					int[] ParameterIndices = Parameters.GetCommaDelimitedArguments(0);
					foreach (int Parameter in ParameterIndices) {
						TokenisedSource ParameterName = Parameters.GetExpressionTokens(Parameter);
						if (ParameterName.Tokens.Length != 1) throw new CompilerExpection(source, "Macro parameter declarations must be a single token.");
						ParameterNames.Add(ParameterName.Tokens[0].Data.ToLowerInvariant());
					}
				}
			}

			string MacroName = SourceAllInclusive.Tokens[index + 1].Data;
			if (compiler.MacroIsDefined(MacroName)) compiler.UnregisterMacro(MacroName);

			compiler.RegisterMacro(MacroName, delegate(Compiler c, ref TokenisedSource s, int i) {
				if (!HasParameters) {
					s.ReplaceToken(i, Replacement.ToArray());
				} else {

					// Check that we're invoking this with name():
					if (!s.Tokens[i + 1].IsOpenBracket) return;

					int CloseBracketIndex = s.GetCloseBracketIndex(i + 1);

					// Pull out the arguments from the parentheses:
					TokenisedSource RawArguments = s.GetTokensInBrackets(i + 1);

					// Get argument indices:
					int[] Arguments = RawArguments.GetCommaDelimitedArguments(0, ParameterNames.Count);

					TokenisedSource[] PassedArguments = new TokenisedSource[ParameterNames.Count];
					for (int a = 0; a < PassedArguments.Length; ++a) {
						PassedArguments[a] = RawArguments.GetExpressionTokens(Arguments[a]);
					}

					List<TokenisedSource.Token> Result = new List<TokenisedSource.Token>();


					for (int ReplaceIndex = 2 + Math.Max(0, ((2 * ParameterNames.Count) - 1)); ReplaceIndex < Replacement.Count; ++ReplaceIndex) {

						TokenisedSource.Token T = Replacement[ReplaceIndex];

						bool MatchedArgument = false;
						for (int ParameterIndex = 0; ParameterIndex < ParameterNames.Count; ++ParameterIndex) {
							if (T.DataLowerCase == ParameterNames[ParameterIndex]) {
								foreach (TokenisedSource.Token MatchedParameter in PassedArguments[ParameterIndex].Tokens) {
									Result.Add(MatchedParameter);
								}
								MatchedArgument = true;
								break;
							}
						}
						if (!MatchedArgument) Result.Add(T);
					}
					s.ReplaceTokens(i, CloseBracketIndex, Result.ToArray()); // +2 for <macro name> <(>*/
					return;

				}
			});

		}

	}
}
