using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Brass3.Attributes;

namespace Brass3 {
	public partial class TokenisedSource {

		private interface ISyntaxColourRule {
			ConsoleColor GetForeground(Token.TokenTypes type);
			ConsoleColor GetBackground(Token.TokenTypes type);
		}

		private class SyntaxColourNormal : ISyntaxColourRule {
			public ConsoleColor GetBackground(Token.TokenTypes type) {
				return ConsoleColor.Black;
			}
			public ConsoleColor GetForeground(Token.TokenTypes type) {
				switch (type) {
					case Token.TokenTypes.Comment:
						return ConsoleColor.DarkGreen;
					case Token.TokenTypes.Label:
						return ConsoleColor.DarkMagenta;
					case Token.TokenTypes.Seperator:
						return ConsoleColor.Magenta;
					case Token.TokenTypes.String:
						return ConsoleColor.Red;
					case Token.TokenTypes.Directive:
						return ConsoleColor.DarkCyan;
					case Token.TokenTypes.Punctuation:
						return ConsoleColor.DarkGray;
					default:
						return ConsoleColor.White;
				}
			}
		}

		private class SyntaxColourInverse : ISyntaxColourRule {
			public ConsoleColor GetBackground(Token.TokenTypes type) {
				return ConsoleColor.White;
			}
			public ConsoleColor GetForeground(Token.TokenTypes type) {
				switch (type) {
					case Token.TokenTypes.Comment:
						return ConsoleColor.DarkGreen;
					case Token.TokenTypes.Label:
						return ConsoleColor.DarkMagenta;
					case Token.TokenTypes.Seperator:
						return ConsoleColor.Magenta;
					case Token.TokenTypes.String:
						return ConsoleColor.Red;
					case Token.TokenTypes.Directive:
						return ConsoleColor.DarkCyan;
					case Token.TokenTypes.Punctuation:
						return ConsoleColor.DarkGray;
					default:
						return ConsoleColor.Black;
				}
			}
		}
	}
}
