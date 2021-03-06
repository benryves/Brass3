using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Variables {

	[Syntax(".varloc address, size")]
	[Description("Declares an area of memory for variable allocation.")]
	[SeeAlso(typeof(Var))]
	[Category("Variables")]
	public class VarLoc : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			
			Var Vars = compiler.GetPluginInstanceFromType(typeof(Var)) as Var;

			int[] Args = source.GetCommaDelimitedArguments(index + 1, 2);

			Vars.VariableLocations.Add(new Var.VariableAllocationRegion(
				(int)source.EvaluateExpression(compiler, Args[0]).NumericValue,
				(int)source.EvaluateExpression(compiler, Args[1]).NumericValue
			));

		}
	}
}
