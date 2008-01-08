using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".db expression [, expression [, ... ] ]")]
	[Syntax(".data type expression, [, expression [, ... ] ]")]
	[Description("Defines bytes (<c>.db</c>/<c>.byte</c>), 16-bit words (<c>.dw</c>/<c>.word</c>) or 32-bit integers (<c>.di</c>/<c>.int</c>).\r\nYou may also define string literals using these directives.")]
	[Remarks("If you have a string, each character is treated as an individual expression, hence <c>.dw \"123\"</c> outputs six bytes of data for three characters.")]
	[Warning("The type of argument - string or number - is determined during the first pass. If the evaluation fails, the result is assumed to be a number. This can cause problems for forward-referenced strings.")]
	[CodeExample(".word 512, 4 ** 4, 1 << 10")]
	[CodeExample(".db \"Brass\", 0")]
	[DisplayName("data")]
	[Category("Data")]
	[PluginName("data"), PluginName("db"), PluginName("byte"), PluginName("dw"), PluginName("word"), PluginName("di"), PluginName("int")]
	public class DataDeclaration : IDirective {

		private Core.NumberEncoding.Byte ByteEncoder;
		private Core.NumberEncoding.Word WordEncoder;
		private Core.NumberEncoding.Int IntEncoder;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			int[] Args;
			INumberEncoder NumberEncoder = this.ByteEncoder;

			if (directive == "data") {

				// Name of the type:
				TokenisedSource.Token Type = source.Tokens[index + 1];

				//  Get arguments;
				Args = source.GetCommaDelimitedArguments(index + 2, 1, int.MaxValue);

				// Encoder for this data type:
				if (!compiler.NumberEncoders.Contains(Type.Data)) throw new CompilerException(Type, string.Format(Strings.ErrorDataTypeNotDefined, Type.Data));
				NumberEncoder = compiler.NumberEncoders[Type.Data];

				// Set size of declaring label if applicable.
				if (compiler.LabelEvaluationResult != null) compiler.LabelEvaluationResult.DataType = NumberEncoder;
			} else {

				Args = source.GetCommaDelimitedArguments(index + 1);

				// Get the size of the data:
				INumberEncoder Encoder = this.ByteEncoder;
				switch (directive) {
					case "dw":
					case "word":
						NumberEncoder = this.WordEncoder;
						break;
					case "di":
					case "int":
						NumberEncoder = this.IntEncoder;
						break;
				}

			}

			// Get the argument indices for the source.
			foreach (var ExpressionIndex in source.GetCommaDelimitedArguments(index + 1)) {


				Label Data; CompilerException Error;
				if (source.TryEvaluateExpression(compiler, ExpressionIndex, out Data, out Error)) {

					// We've managed to calculate the data. It's static!
					if (Data.IsString) {
						// For strings, dump out each character (encoded to a byte value)...
						foreach (var EncodedStringData in compiler.StringEncoder.GetData(Data.StringValue)) {
							compiler.WriteStaticOutput(NumberEncoder.GetBytes(compiler, EncodedStringData));
						}
					} else {
						// For numbers, just dump out a single value.
						compiler.WriteStaticOutput(NumberEncoder.GetBytes(compiler, Data.NumericValue));
					}

				} else {
					
					// We can't actually calculate the data just yet, so do something dynamic!
					compiler.WriteDynamicOutput(NumberEncoder.Size, G => G.Data = NumberEncoder.GetBytes(compiler, source.EvaluateExpression(compiler,ExpressionIndex).NumericValue));

				}

			}

		}

		public DataDeclaration(Compiler c) {
			ByteEncoder = new Core.NumberEncoding.Byte();
			WordEncoder = new Core.NumberEncoding.Word();
			IntEncoder = new Core.NumberEncoding.Int();
		}

	}
}
