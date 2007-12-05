using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Core.Directives {
	
	[Category("Code Structure")]
	[Description("Defines a code section.")]
	[Remarks(@"There are instances when you need to control the structure of the program. For example, hardware constraints might dictate that all executable code must fit in the first 8KB of the binary, but data resources can appear after this point.
Code inside sections isn't compiled immediately. To compile it, you need to use the <see cref=""incsection""/> directive.")]
	[CodeExample(@"/* Main.asm */

.include ""File1.asm""
.include ""File2.asm""
.incsection Code
.incsection Data

/* File1.asm */

.section Code
.include ""Code1.asm""
.endsection

.section Data
.include ""Data1.inc""
.endsection

/* File2.asm */

.section Code
.include ""Code2.asm""
.endsection

.section Data
.include ""Data2.inc""
.endsection

/*
   This would assemble as the following:
   
   .include ""Code1.asm""
   .include ""Code2.asm""
   .include ""Data1.inc""
   .include ""Data2.inc""
   
*/")]
	[PluginName("section"), PluginName("endsection")]
	[SeeAlso(typeof(IncSection))]
	public class Section : IDirective {

		internal class SectionRange {
			public LinkedListNode<Compiler.SourceStatement> FirstStatement;
			public LinkedListNode<Compiler.SourceStatement> LastStatement;
			public SectionRange(LinkedListNode<Compiler.SourceStatement> firstStatement, LinkedListNode<Compiler.SourceStatement> lastStatement) {
				this.FirstStatement = firstStatement; this.LastStatement = lastStatement;
			}
			public SectionRange(LinkedListNode<Compiler.SourceStatement> statement)
				: this(statement, statement) {
			}
		}

		internal Dictionary<string, List<SectionRange>> Sections;

		private string CurrentSection;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			List<SectionRange> SectionRangeData;
			switch (directive) {
				case "section":
					if (CurrentSection != null) throw new CompilerExpection(source, string.Format(Strings.ErrorSectionAlreadyInsideSection, this.CurrentSection));
					this.CurrentSection = (source.GetCommaDelimitedArguments(compiler, index + 1, TokenisedSource.StringOrTokenArgument)[0] as string).ToLowerInvariant();
					if (!this.Sections.TryGetValue(this.CurrentSection, out SectionRangeData)) {
						SectionRangeData = new List<SectionRange>();
						this.Sections.Add(this.CurrentSection, SectionRangeData);
					}
					SectionRangeData.Add(new SectionRange(compiler.CurrentStatement));
					compiler.SwitchOff(typeof(Section));
					break;
				case "endsection":
					if (CurrentSection == null) throw new CompilerExpection(source, Strings.ErrorSectionNoSectionToEnd);
					SectionRangeData = this.Sections[this.CurrentSection];
					SectionRange Range = SectionRangeData[SectionRangeData.Count - 1];
					Range.LastStatement = compiler.CurrentStatement.Previous;
					if (Range.LastStatement == Range.FirstStatement) {
						SectionRangeData.Remove(Range);
					}
					this.CurrentSection = null;
					compiler.SwitchOn();
					break;
			}
		}

		public Section(Compiler compiler) {

			// Create storage for sections.
			this.Sections = new Dictionary<string, List<SectionRange>>();
			
			// Clear sections at start of pass 1.
			compiler.CompilationBegun += delegate(object sender, EventArgs e) {
				this.CurrentSection = null;
			};

		}

	}
}
