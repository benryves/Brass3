using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Directives {
	[Category("Code Structure")]
	[Description("Inserts a code section at the current location.")]
	[SeeAlso(typeof(Section))]
	public class IncSection : IDirective {

	
		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			Section S = compiler.GetPluginInstanceFromType<Section>();
			if (S == null) throw new CompilerException(source, string.Format(Strings.ErrorPluginNotLoaded, "section"));

			string SectionName = source.GetCommaDelimitedArguments(compiler, index+1, TokenisedSource.StringOrTokenArgument)[0] as string;

			List<Section.SectionRange> SectionRange;
			if (!S.Sections.TryGetValue(SectionName.ToLowerInvariant(), out SectionRange)) throw new CompilerException(source, string.Format(Strings.ErrorSectionNotDefined, SectionName));

			foreach (Section.SectionRange Range	in SectionRange) {
				compiler.RecompileRange(Range.FirstStatement.Next, Range.LastStatement);
			}

		}

	}
}
