using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	[Syntax("choose(index, value1, value2, ...)")]
	[Description("Chooses a value from a list of values based on an index number. The list has a base index of 1, not 0.")]
	[Remarks("If the index value is negative then the value from the end of the list is chosen instead.")]
	[CodeExample("; Define some prime numbers:\r\n#define primes 2, 3, 5, 7, 11, 13\r\n\r\n; Iterate over the list from start to end:\r\n.echoln \"Forwards:\"\r\n#for i is +1 to +6\r\n.echoln choose(i, primes)\r\n#loop\r\n\r\n; Iterate over the list backwards:\r\n.echoln \"Backwards:\"\r\n#for i is -1 to -6\r\n.echoln choose(i, primes)\r\n#loop")]
	public class Choose : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			int[] Args = source.GetCommaDelimitedArguments(0, 1, int.MaxValue);
			int Index = (int)source.EvaluateExpression(compiler, Args[0]).NumericValue;
			int AdjustedIndex = (Index > 0) ? (Index) : (Args.Length + Index);
			if (AdjustedIndex < 1 || AdjustedIndex >= Args.Length) throw new CompilerExpection(source, "Choice " + Index + " not available.");
			return new Label(compiler.Labels, source.EvaluateExpression(compiler, Args[AdjustedIndex]).NumericValue);
		}
	}
}
