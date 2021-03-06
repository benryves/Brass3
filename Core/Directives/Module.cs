using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using System.ComponentModel;
using BeeDevelopment.Brass3.Attributes;

namespace Core.Directives {

	[Description("Declares the beginning or end of a module, or retrieves the current module name when used as a function.")]
	[Syntax(".module name")]
	[Syntax(".endmodule")]
	[Syntax("module()")]
	[CodeExample(".module People\r\n\r\n\tCount = 2\r\n\r\n\t.module Ben\r\n\t\tAge = 21\r\n\t.endmodule\r\n\r\n\t.module Steve\r\n\t\tAge = 22\r\n\t.endmodule\r\n\r\n.endmodule\r\n\r\n.echoln \"There are \", People.Count, \" people.\"\r\n.echoln \"Ben is \", People.Ben.Age, \" years old.\"\r\n.echoln \"Steve is \", People.Steve.Age, \" years old.\"\r\n")]
	[Remarks("Modules can be used to group labels into logical blocks.")]
	[Category("Code Structure")]
	[Warning("Unlike TASM and previous versions of Brass, all labels are automatically local in scope.")]
	[PluginName("module"), PluginName("endmodule")]
	public class Module : IDirective, IFunction {

		private Stack<string> ModuleNames;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			switch (directive) {
				case "module": {
						if (source.Tokens.Length != index + 2) throw new DirectiveArgumentException(source.Tokens[index], Strings.ErrorModuleNameExpected);
						ModuleNames.Push(compiler.Labels.CurrentModule);
						string NewModuleName = compiler.Labels.CurrentModule;
						if (!string.IsNullOrEmpty(NewModuleName)) NewModuleName += ".";
						NewModuleName += source.Tokens[index + 1].Data;
						compiler.Labels.CurrentModule = NewModuleName;
					} break;
				case "endmodule": {
						compiler.Labels.CurrentModule = ModuleNames.Pop();
					} break;
			} 
		}

		public Module(Compiler c) {
			this.ModuleNames = new Stack<string>();
			c.CompilationBegun += new EventHandler(delegate(object sender, EventArgs e) { this.ModuleNames.Clear(); });

		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			if (function == "endmodule") throw new CompilerException(source, "You may not use endmodule() as a function.");
			source.GetCommaDelimitedArguments(0, 0);
			return new Label(compiler.Labels, compiler.Labels.CurrentModule);	
		}

	}
}
