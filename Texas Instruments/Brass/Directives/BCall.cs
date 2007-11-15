using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace TexasInstruments.Brass.Directives {
	[Category("Texas Instruments")]
	[SeeAlso(typeof(Functions.BCall))]
	[Syntax(".bcall _target")]
	[Syntax(".bcall z, _target"), Syntax(".bcall nz, _target")]
	[Syntax(".bcall c, _target"), Syntax(".bcall nc, _target")]
	[Syntax(".bcall pe, _target"), Syntax(".bcall po, _target")]
	[Syntax(".bcall p, _target"), Syntax(".bcall m, _target")]
	[Description("Calls or jumps to a function, switching page if required on compatible hardware.")]
	[Remarks("This is a directive version of the <c>bcall()</c> and <c>bjump()</c> function. Its only advantage is that it looks more like Z80 assembly rather than a macro or function call.")]
	[PluginName("bcall"), PluginName("bjump")]
	public class BCall : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			Functions.BCall BCaller = compiler.GetPluginInstanceFromType<Functions.BCall>();
			if (BCaller == null) {
				throw new CompilerExpection(source.Tokens[index], directive + "() function plugin not available.");
			}

			int[] Arguments = source.GetCommaDelimitedArguments(index + 1, 1, 2);
			string Name = directive;
			if (Arguments.Length == 2) {
				TokenisedSource.Token Condition = source.GetExpressionToken(Arguments[0]);
				string NameSuffix = Condition.DataLowerCase;
				switch (NameSuffix) {
					case "z":
					case "nz":
					case "c":
					case "nc":
					case "pe":
					case "po":
					case "p":
					case "m":
						break;
					default:
						throw new DirectiveArgumentException(Condition, "Invalid condition argument.");
				}
				Name += NameSuffix;
			}
			BCaller.RomCall(compiler, Name, (ushort)(compiler.CurrentPass == AssemblyPass.WritingOutput ? source.EvaluateExpression(compiler, Arguments[Arguments.Length - 1]).NumericValue : 0));
		}

	}
}
