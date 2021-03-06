using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	
	[Syntax("srand(seed)")]
	[Description("Seeds the random number generator.")]
	[Category("Maths")]
	[SeeAlso(typeof(Rand))]
	public class SRand : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			int[] Args = source.GetCommaDelimitedArguments(0, 1);
			int Seed = (int)source.EvaluateExpression(compiler, Args[0]).NumericValue;

			Rand RandomSource = compiler.GetPluginInstanceFromType(typeof(Rand)) as Rand;
			if (RandomSource == null) throw new InvalidOperationException("rand() function plugin not loaded.");


			RandomSource.Seed(Seed);

			return new Label(compiler.Labels, Seed);
		}

	}
}
