using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".align boundary")]
	[Description("Sets the current program counter value to the next multiple of <c>boundary</c>.")]
	[Remarks("If the program counter already sits on a boundary it will not be changed.")]
	[CodeExample("$ = 10\r\n.align 256\r\n.echoln $ ; Outputs 256.")]
	[CodeExample("$ = 512\r\n.align 256\r\n.echoln $ ; Outputs 512.")]
	public class Align : IDirective {

		public string[] Names {
			get {
				return new string[] { "align" };
			}
		}

		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			int[] Args = source.GetCommaDelimitedArguments(index + 1);
			if (Args.Length != 1) throw new DirectiveArgumentException(source, "Only one argument expected.");

			double Boundary = source.EvaluateExpression(compiler, Args[0]).Value;
			if (Boundary < 1 || (int)Boundary != Boundary) throw new DirectiveArgumentException(source, "You can only align to positive integral boundaries.");

			compiler.Labels.ProgramCounter.Value = ((((int)compiler.Labels.ProgramCounter.Value) + ((int)Boundary - 1)) / (int)(Boundary)) * Boundary;

		}

	}
}
