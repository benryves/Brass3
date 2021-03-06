using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

namespace Legacy.Directives {

	[Description("Defines a custom character mapping table.")]
	[Syntax(".asciimap character, replacement")]
	[Syntax(".asciimap start, end, replacement")]
	[Warning("Any custom string encoders are disabled during the execution of this directive. That is, if the current custom string encoder maps <c>'A'</c> to 0, <c>'A'</c> will be interpreted as 65 instead as that is the value that it maps to under the default compiler encoding.")]
	[Remarks("(Taken from the Brass 1 documentation).\r\nDefines an ASCII mapping table. In English, this is a special table that can be used to translate strings from the ASCII you're dealing with on your PC to any special variations on the theme on the device you are assembling to. For example, the TI-83 Plus calculator has a θ symbol where the '[' is normally. Using an ASCII mapping table, you could automatically make any strings defined using the .db directive handle this oddity. Another possibility would be a font where A-Z is actually 0-25 rather than the usual 65-90.\r\nThe first two arguments define the range of characters you are translating, inclusive. You can miss out the second one if you are only redefining a single character. The final argument is a special rule telling Brass how to perform the translation. It is a standard expression, where the special code <c>{*}</c> specifies the current character being translated.")]
	[CodeExample("Force all strings UPPERCASE.", ".asciimap 'a', 'z', {*}+('A'-'a')")]
	[CodeExample("Reset to standard mapping.", ".asciimap $00, $FF, {*}")]
	[CodeExample("Turn spaces into underscores.", ".asciimap ' ', '_'")]
	[CodeExample("Shift each letter in the range A to Z by one (A to B, B to C, &c).", ".asciimap 'A', 'Z', {*}+1")]
	[CodeExample("Clear the most significant bit (limit to 7-bit ASCII).", ".asciimap 128, 255, {*}&%01111111")]
	public class AsciiMap : IStringEncoder, IDirective {

		private Dictionary<char, byte> CharacterMap;

		public byte[] GetData(string toEncode) {

			byte[] Result = new byte[toEncode.Length];
			
			for (int i = 0; i < Result.Length; ++i) {
				byte CharacterCode;
				if (!CharacterMap.TryGetValue(toEncode[i], out CharacterCode)) CharacterCode = (byte)toEncode[i];
				Result[i] = CharacterCode;
			}

			return Result;
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			// Preserve state of CustomStringEncoderEnabled.
			bool CustomStringEncoderEnabled = compiler.CustomStringEncoderEnabled;
			
			try {

				// Disable custom string encoding.
				compiler.CustomStringEncoderEnabled = false;

				// Between 2 and 3 args:
				int[] Args = source.GetCommaDelimitedArguments(index + 1, 2, 3);

				// Get start and end char:
				int StartChar = (int)source.EvaluateExpression(compiler, Args[0]).NumericValue;
				int EndChar = Args.Length == 2 ? StartChar : (int)source.EvaluateExpression(compiler, Args[1]).NumericValue;

				// Duplicate the expression;
				TokenisedSource Expression = source.GetExpressionTokens(Args[Args.Length - 1]).Clone() as TokenisedSource;

				// Find out which bits of the expression are the magic hack "{*}".
				List<int> MagicHackArgument = new List<int>();
				for (int i = 0; i < Expression.Tokens.Length; ++i) {
					if (Expression.Tokens[i].Data == "{*}") MagicHackArgument.Add(i);
				}

				// Iterate over all the characters in the specified range:
				for (int Character = StartChar; Character <= EndChar; Character++) {

					// Replace "{*}" with Character.
					foreach (int Hack in MagicHackArgument) {
						Expression.Tokens[Hack] = Expression.Tokens[Hack].Clone(Character.ToString(CultureInfo.InvariantCulture)) as TokenisedSource.Token;
					}

					// Evaluate, add to map.
					Label L = Expression.EvaluateExpression(compiler);
					if (this.CharacterMap.ContainsKey((char)Character)) this.CharacterMap.Remove((char)Character);
					this.CharacterMap.Add((char)Character, (byte)L.NumericValue);
				}
			} finally {
				compiler.CustomStringEncoderEnabled = CustomStringEncoderEnabled;
			}
		}


		public AsciiMap(Compiler compiler) {
			CharacterMap = new Dictionary<char, byte>(256);
			compiler.CompilationBegun += delegate(object sender, EventArgs e) {
				CharacterMap.Clear();
			};
		}

		public char GetChar(int value) {
			foreach (KeyValuePair<char, byte> KVP in this.CharacterMap) {
				if (KVP.Value == value) return KVP.Key;
			}
			return (char)value;
		}

		public string GetString(byte[] data) {
			StringBuilder Result = new StringBuilder(data.Length);
			for (int i = 0; i < data.Length; ++i) Result[i] = GetChar(data[i]);
			return Result.ToString();
		}
	}
}
