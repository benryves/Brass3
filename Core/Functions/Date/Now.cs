using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Core.Functions.Date {
	[Description("Returns the current date and time.")]
	[Remarks("Dates and times are represented by the number of 100ns ticks since midnight, 1st January 0001.")]
	[Category("Date and Time")]
	public class Now : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { });
			return new Label(compiler.Labels, (double)DateTime.Now.Ticks);
		}

		public string[] Names {
			get { return new string[] { "now" }; }
		}

		public string Name { get { return Names[0]; } }

	}
}
