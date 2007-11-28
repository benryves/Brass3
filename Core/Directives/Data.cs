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


		private Queue<bool> IsStringQueue;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			int[] Args;
			INumberEncoder NumberEncoder = this.ByteEncoder;

			if (directive == "data") {

				// Name of the type:
				TokenisedSource.Token Type = source.Tokens[index + 1];

				//  Get arguments;
				Args = source.GetCommaDelimitedArguments(index + 2, 1, int.MaxValue);

				// Encoder for this data type:
				if (!compiler.NumberEncoders.Contains(Type.Data)) throw new CompilerExpection(Type, string.Format(Strings.ErrorDataTypeNotDefined, Type.Data));
				NumberEncoder = compiler.NumberEncoders[Type.Data];

				// Set size of declaring label if applicable.
				if (compiler.LabelEvaluationResult != null) compiler.LabelEvaluationResult.Type = NumberEncoder;
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

			// Iterate over each argument;
			foreach (int Arg in Args) {

				Label Result = null;

				bool IsString = false;
				if (compiler.CurrentPass == AssemblyPass.CreatingLabels) {

					CompilerExpection ReasonForFailure;
					if(source.TryEvaluateExpression(compiler, Arg, out Result, out ReasonForFailure)) {
						IsString = Result.IsString;
					} else {
						IsString = false;
					}

					this.IsStringQueue.Enqueue(IsString);
				} else {
					IsString = this.IsStringQueue.Dequeue();
				}


				// Which pass?
				switch (compiler.CurrentPass) {
					case AssemblyPass.CreatingLabels: // Just $+=sizeof(data)
						if (IsString) {
							compiler.IncrementProgramAndOutputCounters(NumberEncoder.Size * Result.StringValue.Length);
						} else {
							compiler.IncrementProgramAndOutputCounters(NumberEncoder.Size);
						}
						break;
					case AssemblyPass.WritingOutput: // Evaluate and write the data!
						Result = source.EvaluateExpression(compiler, Arg);
						if (IsString) {
							foreach (byte b in compiler.StringEncoder.GetData(Result.StringValue)) compiler.WriteOutput(NumberEncoder.GetBytes(compiler, b));
						} else {
							compiler.WriteOutput(NumberEncoder.GetBytes(compiler, Result.NumericValue));
						}
						break;
				}
			}



		}

		public DataDeclaration(Compiler c) {
			this.IsStringQueue = new Queue<bool>();
			ByteEncoder = new Core.NumberEncoding.Byte();
			WordEncoder = new Core.NumberEncoding.Word();
			IntEncoder = new Core.NumberEncoding.Int();
			c.PassBegun += delegate(object sender, EventArgs e) {
				if (c.CurrentPass == AssemblyPass.CreatingLabels) {
					this.IsStringQueue.Clear();
				}
			};
		}

	}
}
