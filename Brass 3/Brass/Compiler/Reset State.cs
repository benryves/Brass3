using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;
using System.Xml;

namespace Brass3 {

	public partial class Compiler {

		private void ResetState() {
			this.CurrentStatement = 0;
			this.SwitchOn();
			this.Reactivator = null;
			this.MacroLookup.Clear();
			this.endianness = Endianness.Little;

			this.CustomStringEncoderEnabled = true;

			this.labels.ProgramCounter.NumericValue = 0;
			this.labels.ProgramCounter.Page = 0;
			this.labels.OutputCounter.NumericValue = 0;
			this.labels.OutputCounter.Page = 0;

			this.labels.ImplicitCreationDefault = this.labels.ProgramCounter;
			this.labels.ExportLabels = true;
			this.JustRecalledPosition = false;
			this.Functions.ClearRuntimeAliases();
			this.Directives.ClearRuntimeAliases();
			this.dataStructures.Clear();
			this.emptyFill = 0;
			this.writingToListFile = true;
		}

	}
}
