using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

using TexasInstruments.Types;
using TexasInstruments.Types.Variables;


namespace TexasInstruments.Brass.Output {

	[Category("Texas Instruments")]
	[Description("Writes a TI-83 Plus program file (*.8xp).")]
	[Remarks(TI8X.Remarks)]
	[SeeAlso(typeof(Output.TI83)), SeeAlso(typeof(Output.TI82)), SeeAlso(typeof(Output.TI73)), SeeAlso(typeof(Output.TI86)), SeeAlso(typeof(Output.TI85))]
	public class TI8X : IOutputWriter {

		internal const string Remarks = "Data is output sequentially in order of the output counter. Gaps are not inserted between non-consecutive addresses; output modifiers that change the size of the output data are fully supported.\r\nA new program variable is created within the output file for each page - use this to create groups.";

		public virtual string DefaultExtension {
			get { return "8xp"; }
		}

		public virtual CalculatorModel Model {
			get { return CalculatorModel.TI83Plus; }
		}

		public void WriteOutput(Compiler compiler, Stream stream) {

			Directives.TIVariableName VarName = compiler.GetPluginInstanceFromType<Directives.TIVariableName>();

			GroupFile Group = new GroupFile(this.Model);

			foreach (int Page in compiler.GetUniquePageIndices()) {

				Program Prog = new Program();
				
				Prog.Name = "PROG" + Page;
				if (VarName != null) {
					string Name = null;
					if (VarName.VariableNames.TryGetValue(Page, out Name)) {
						Prog.Name = Name;
					}
				}

				Prog.Model = this.Model;


				List<byte> Data = new List<byte>(1024);
				foreach (Compiler.OutputData D in compiler.GetOutputDataOnPage(Page)) {
					Data.AddRange(D.Data);
				}

				Prog.Data = Data.ToArray();

				Group.Variables.Add(Prog);

			}

			Group.Save(stream);
			stream.Flush();
		}
		
	}
}
