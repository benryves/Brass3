using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Variables {

	[Syntax(".varloc address, size")]
	[Description("Declares an area of memory for variable allocation.")]
	[SeeAlso(typeof(Var))]
	[Category("Variables")]
	public class VarLoc : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			
			if (compiler.CurrentPass == AssemblyPass.Pass2) return;

			Var Vars = compiler.GetPluginInstanceFromType(typeof(Var)) as Var;

			int[] Args = source.GetCommaDelimitedArguments(index + 1, 2);

			Vars.VariableLocations.Add(new Var.VariableAllocationRegion(
				(int)source.EvaluateExpression(compiler, Args[0]).NumericValue,
				(int)source.EvaluateExpression(compiler, Args[1]).NumericValue
			));

		}
	}
}
