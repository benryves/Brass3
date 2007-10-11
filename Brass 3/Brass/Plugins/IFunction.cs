using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Brass3.Plugins {

	/// <summary>
	/// Defines the interface that functions need to implement.
	/// </summary>
	public interface IFunction : IAliasedPlugin {

		/// <summary>
		/// Invoke the function.
		/// </summary>
		/// <param name="compiler">The compiler being used to build the project.</param>
		/// <param name="source">The entirety of the function's arguments.</param>
		/// <param name="function">The name of the function.</param>
		Label Invoke(Compiler compiler, TokenisedSource source, string function);

	}
}
