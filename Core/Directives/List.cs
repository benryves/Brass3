using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".nolist")]
	[Syntax(".list")]
	[Description("Disables or enables additions to the list file.")]
	[Category("List File")]
	public class NoList : IDirective {

		public string[] Names {
			get {
				return new string[] { "nolist", "list" };
			}
		}

		public string Name { get { return Names[0]; } }

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			source.GetCommaDelimitedArguments(index + 1, 0);
			
		}

	}
}
