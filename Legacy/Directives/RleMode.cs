using System;
using System.Collections.Generic;
using System.Text;

using BeeDevelopment.Brass3.Attributes;
using BeeDevelopment.Brass3.Plugins;

using System.ComponentModel;
using BeeDevelopment.Brass3;

namespace Legacy.Directives {
	[Syntax(".rlemode run_indicator [, value_first]")]
	[Description("Sets the current RLE mode.")]
	[Remarks("Sets the current RLE mode - first, the byte value used to represent a run (defaults to $91), followed by a flag to set whether the value or the length is written first after the run indicator (defaults to true).")]
	[Category("Data")]
	[Warning("This legacy directive only affects the behaviour of the <c>.incbmp</c> plugin.")]
	[SeeAlso(typeof(IncBmp))]
	public class RleMode : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			IncBmp Parent = compiler.GetPluginInstanceFromType<IncBmp>();
			if (Parent == null) throw new CompilerException(source.Tokens[index], ".incbmp plugin not loaded.");

			object[] o = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { 
				TokenisedSource.ArgumentType.Value,
				TokenisedSource.ArgumentType.Value |  TokenisedSource.ArgumentType.Optional
			});

			Parent.RLE_Flag = (byte)(double)o[0];
			if (o.Length > 1) Parent.RLE_ValueFirst = (double)o[1] != 0d;
		}

		
	}
}
