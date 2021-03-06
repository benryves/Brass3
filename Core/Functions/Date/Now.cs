using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Functions.Date {
	[Description("Returns the current date and time.")]
	[Remarks("Dates and times are represented by the number of 100ns ticks since midnight, 1st January 0001.")]
	[Category("Date and Time")]
	public class Now : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { });
			return new Label(compiler.Labels, (double)DateTime.Now.Ticks);
		}

	}
}
