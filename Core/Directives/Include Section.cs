using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Core.Directives {
	[Category("Code Structure")]
	[Description("Inserts a code section at the current location.")]
	[SeeAlso(typeof(Section))]
	public class IncSection : IDirective {

	
		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			Section S = compiler.GetPluginInstanceFromType<Section>();
			if (S == null) throw new CompilerExpection(source, ".section plugin not loaded.");

			string SectionName = source.GetCommaDelimitedArguments(compiler, index+1, TokenisedSource.StringOrTokenArgument)[0] as string;

			List<Section.SectionRange> SectionRange;
			if (!S.Sections.TryGetValue(SectionName.ToLowerInvariant(), out SectionRange)) throw new CompilerExpection(source, string.Format("Section '{0}' not defined.", SectionName));

			foreach (Section.SectionRange Range	in SectionRange) {
				compiler.RecompileRange(Range.FirstStatement.Next, Range.LastStatement);
			}

		}

	}
}
