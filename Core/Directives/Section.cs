using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Core.Directives {
	[PluginName("section"), PluginName("endsection")]
	public class Section : IDirective {

		internal class SectionRange {
			public int FirstStatement;
			public int LastStatement;
			public SectionRange(int firstStatement, int lastStatement) {
				this.FirstStatement = firstStatement; this.LastStatement = lastStatement;
			}
			public SectionRange(int statement)
				: this(statement, statement) {
			}
		}

		internal Dictionary<string, List<SectionRange>> Sections;

		private string CurrentSection;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			List<SectionRange> SectionRangeData;
			switch (directive) {
				case "section":
					if (compiler.CurrentPass == AssemblyPass.Pass1) {
						if (CurrentSection != null) throw new CompilerExpection(source, string.Format("Currently inside section '{0}'.", this.CurrentSection));
						this.CurrentSection = (source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.StringOrTokenArgument)[0] as string).ToLowerInvariant();
						if (!this.Sections.TryGetValue(this.CurrentSection, out SectionRangeData)) {
							SectionRangeData = new List<SectionRange>();
							this.Sections.Add(this.CurrentSection, SectionRangeData);
						}
						SectionRangeData.Add(new SectionRange(compiler.RememberPosition() + 1));
					}
					compiler.SwitchOff(typeof(Section));
					break;
				case "endsection":
					if (compiler.CurrentPass == AssemblyPass.Pass1) {
						if (CurrentSection == null) throw new CompilerExpection(source, "No section to end.");
						SectionRangeData = this.Sections[this.CurrentSection];
						SectionRangeData[SectionRangeData.Count - 1].LastStatement = compiler.RememberPosition() - 1;
						this.CurrentSection = null;
					}
					compiler.SwitchOn();
					break;
			}
		}

		public Section(Compiler compiler) {

			// Create storage for sections.
			this.Sections = new Dictionary<string, List<SectionRange>>();
			
			// Clear sections at start of pass 1.
			compiler.PassBegun += delegate(object sender, EventArgs e) {
				this.CurrentSection = null;
				if (compiler.CurrentPass == AssemblyPass.Pass1) {
					this.Sections.Clear();
				}
			};

		}

	}
}
