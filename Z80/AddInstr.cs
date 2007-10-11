using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using System.IO;
using Brass3.Plugins;

namespace Z80 {
	public class AddInstr : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) { 
		}

		public string[] Names {
			get { return new string[] { "addinstr" }; }
		}

		public string Name {
			get { return this.Names[0]; }
		}
	}

}