using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".db expression [, expression [, ... ] ]")]
	[Syntax(".data type expression, [, expression [, ... ] ]")]
	[Description("Defines bytes (<c>.db</c>/<c>.byte</c>), 16-bit words (<c>.dw</c>/<c>.word</c>) or 32-bit integers (<c>.di</c>/<c>.int</c>).\r\nYou may also define string literals using these directives.")]
	[Remarks("If you have a string, each character is treated as an individual expression, hence <c>.dw \"123\"</c> outputs six bytes of data for three characters.")]
	[CodeExample(".word 512, 4 ** 4, 1 << 10")]
	[CodeExample(".db \"Brass\", 0")]
	[Category("Data")]
	public class DataDeclaration : IDirective {
		
		private Core.NumberEncoding.Byte ByteEncoder;
		private Core.NumberEncoding.Word WordEncoder;
		private Core.NumberEncoding.Int IntEncoder;

		public string[] Names {
			get {
				return new string[] { "db", "byte", "dw", "word", "di", "int", "data" };
			}
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (directive == "data") {

				// Name of the type:
				TokenisedSource.Token Type = source.Tokens[index + 1];
				
				//  Get arguments;
				int[] Args = source.GetCommaDelimitedArguments(index + 2, 1, int.MaxValue);

				// Encoder for this data type:
				if (!compiler.NumberEncoders.PluginExists(Type.Data)) throw new CompilerExpection(Type, "Data type '" + Type.Data + "' not defined.");
				INumberEncoder NumberEncoder = compiler.NumberEncoders[Type.Data];

				// Set size of declaring label if applicable.
				if (compiler.LabelEvaluationResult != null) compiler.LabelEvaluationResult.Type = NumberEncoder;

				// Iterate over each argument;
				foreach (int Arg in Args) {

					// Is it a string?
					byte[] StringData = null;
					if (source.ExpressionIsStringConstant(Arg)) {
						StringData = compiler.StringEncoder.GetData(source.GetExpressionStringConstant(Arg));
					}

					// Which pass?
					switch (compiler.CurrentPass) {
						case AssemblyPass.Pass1: // Just $+=sizeof(data)
							if (StringData == null) {
								compiler.IncrementProgramAndOutputCounters(NumberEncoder.Size);
							} else {
								compiler.IncrementProgramAndOutputCounters(NumberEncoder.Size * StringData.Length);
							}
							break;
						case AssemblyPass.Pass2: // Evaluate and write the data!
							if (StringData == null) {
								compiler.WriteOutput(NumberEncoder.GetBytes(compiler, source.EvaluateExpression(compiler, Arg).NumericValue));
							} else {
								foreach (byte b in StringData) compiler.WriteOutput(NumberEncoder.GetBytes(compiler, b));
							}
							break;
					}
				}
			

			} else {

				int[] Args = source.GetCommaDelimitedArguments(index + 1);

				// Get the size of the data:
				int Size = 1;
				INumberEncoder Encoder = this.ByteEncoder;
				switch (directive) {
					case "dw":
					case "word":
						Size = 2;
						Encoder = this.WordEncoder;
						break;
					case "di":
					case "int":
						Size = 4;
						Encoder = this.IntEncoder;
						break;
				}

				if (compiler.LabelEvaluationResult != null) {
					compiler.LabelEvaluationResult.Type = Encoder;
				}

				switch (compiler.CurrentPass) {
					case AssemblyPass.Pass1:
						foreach (int i in Args) {
							if (source.ExpressionIsStringConstant(i)) {																
								compiler.IncrementProgramAndOutputCounters(compiler.StringEncoder.GetData(source.GetExpressionStringConstant(i)).Length * Size);
							} else {
								compiler.IncrementProgramAndOutputCounters(Size);
							}
						}
						break;
					case AssemblyPass.Pass2:
						foreach (int i in Args) {
							if (source.ExpressionIsStringConstant(i)) {
								foreach (byte c in compiler.StringEncoder.GetData(source.GetExpressionStringConstant(i))) {
									switch (Size) {
										case 1:
											compiler.WriteOutput((byte)c);
											break;
										case 2:
											compiler.WriteOutput((short)c);
											break;
										case 4:
											compiler.WriteOutput((int)c);
											break;

									}

								}
							} else {
								double Data = source.EvaluateExpression(compiler, i).NumericValue;
								switch (Size) {
									case 1:
										compiler.WriteOutput((byte)Data);
										break;
									case 2:
										compiler.WriteOutput((short)Data);
										break;
									case 4:
										compiler.WriteOutput((int)Data);
										break;

								}

							}
						}
						break;
				}
			}
		}


		public string Name { get { return Names[0]; } }

		public DataDeclaration() {
			ByteEncoder = new Core.NumberEncoding.Byte();
			WordEncoder = new Core.NumberEncoding.Word();
			IntEncoder = new Core.NumberEncoding.Int();
		}

	}
}
