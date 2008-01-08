using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BeeDevelopment.Brass3.Plugins {
	/// <summary>
	/// Defines the interface that directives need to implement.
	/// </summary>
	public interface IDirective : IPlugin {


		/// <summary>
		/// Invoke the directive.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the project.</param>
		/// <param name="source">The source line statement.</param>
		/// <param name="index">The first token to look at in the source.</param>
		/// <param name="directive">The name of the directive in use.</param>
		/// <remarks>The directive name will always be passed in lowercase.</remarks>
		void Invoke(Compiler compiler, TokenisedSource source, int index, string directive);

	}
}
