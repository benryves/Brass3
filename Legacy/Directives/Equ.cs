using System;
using System.Collections.Generic;
using System.Text;

using Brass3.Attributes;
using Brass3.Plugins;

using System.ComponentModel;
using Brass3;

namespace Legacy.Directives {
	[Syntax("label .equ value")]
	[Description("Sets the value <c>label</c> to <c>value</c>.")]
	[Remarks("<c>label = value</c> is functionally equivalent to <c>label .equ value</c>. It is recommended that you do not use this directive and use the assignment operator instead.")]
	[Category("Labels")]
	public class Equ : IDirective {
		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.LabelEvaluationResult == null) throw new CompilerException(source, "No label in the statement to assign to.");

			int[] Args = source.GetCommaDelimitedArguments(index + 1);
			if (Args.Length != 1) throw new DirectiveArgumentException(source, "Expected one argument.");

			Label L = source.EvaluateExpression(compiler, Args[0]);
			compiler.LabelEvaluationResult.NumericValue = L.NumericValue;
			compiler.LabelEvaluationResult.Page = L.Page;


		}

	}
}
