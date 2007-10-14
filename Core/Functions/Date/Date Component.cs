using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Core.Functions.Date {
	[Description("Extract a component from a date.")]
	[Syntax("year([date])")]
	[Syntax("month([date])")]
	[Syntax("day([date])")]
	[Syntax("hour([date])")]
	[Syntax("minute([date])")]
	[Syntax("second([date])")]
	[Remarks("If no date is passed then the function returns the component from the current time.")]
	[CodeExample("Embedding a timestamp into a compiler binary.", ".db strformat(\"Built: {0:D2}:{1:D2}:{2:D2} {3}/{4}/{5}\",\r\n\thour(), minute(), second(),\r\n\tday(), month(), year()), 0")]
	[Category("Date and Time")]
	public class DateComponent : IFunction {

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			object[] Args = source.GetCommaDelimitedArguments(compiler, 0, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional });
			DateTime Date = DateTime.Now;
			if (Args.Length == 1) Date = new DateTime((long)(double)Args[0]);
			double Result = 0;
			switch (function) {
				case "year":
					Result = Date.Year;
					break;
				case "month":
					Result = Date.Month;
					break;
				case "day":
					Result = Date.Day;
					break;
				case "hour":
					Result = Date.Hour;
					break;
				case "minute":
					Result = Date.Minute;
					break;
				case "second":
					Result = Date.Second;
					break;
			}
			return new Label(compiler.Labels, Result);
		}

		public string[] Names {
			get { return new string[] { "year", "month", "day", "hour", "minute", "second" }; }
		}

		public string Name { get { return Names[0]; } }

	}
}