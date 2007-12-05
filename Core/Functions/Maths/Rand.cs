using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Functions.Maths {

	
	[Syntax("rand()")]
	[Syntax("rand(max)")]
	[Syntax("rand(min, max)")]
	[Description("Returns a random number between range specified.")]
	[Remarks("<c>rand()</c> returns a floating-point value between 0 and 1; <c>rand(max)</c> and <c>rand(min, max)</c> both return integral values.\r\nThe random number generator is seeded to 0 at the start of each pass.")]
	[Category("Maths")]
	[SeeAlso(typeof(SRand))]
	public class Rand : IFunction {
		
		private Random RandomNumberSource;

		public void Seed(int seed) {
			this.RandomNumberSource = new Random(seed);
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			double Result = 0;

			int[] Args = source.GetCommaDelimitedArguments(0, 0, 2);
			double[] ArgValues = Array.ConvertAll<int, double>(Args, delegate(int i) { return source.EvaluateExpression(compiler,i).NumericValue; });


			switch (Args.Length) {
				case 0:
					Result = RandomNumberSource.NextDouble();
					break;
				case 1:
					Result = RandomNumberSource.Next((int)ArgValues[0]);
					break;
				case 2:
					Result = RandomNumberSource.Next((int)ArgValues[0], (int)ArgValues[1]);
					break;
			}

			return new Label(compiler.Labels, Result);
		}

		public Rand(Compiler c) {
			c.CompilationBegun += new EventHandler(delegate(object sender, EventArgs e) {
				this.Seed(0);
			});

		}


	}
}
