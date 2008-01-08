using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	[Syntax("align([start], boundary)")]
	public class Align : IFunction {
		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			var Args = Array.ConvertAll<object, int>(source.GetCommaDelimitedArguments(compiler, 0, new[] { TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Positive, TokenisedSource.ArgumentType.Value | TokenisedSource.ArgumentType.Optional | TokenisedSource.ArgumentType.Positive }), v => (int)(double)v);
			var Start = Args.Length == 2 ? Args[1] : (int)compiler.Labels.ProgramCounter.NumericValue;
			var Alignment = Args[Args.Length - 1];
			return new Label(compiler.Labels, GetAlignment(Start, Alignment));
		}

		public static int GetAlignment(int start, int boundary) {
			return ((start + boundary - 1) / boundary) * boundary;
		}

	}
}
