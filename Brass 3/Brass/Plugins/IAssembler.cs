using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BeeDevelopment.Brass3.Plugins {

	/// <summary>
	/// Defines the interface that assemblers need to implement.
	/// </summary>
	public interface IAssembler : IPlugin {

		/// <summary>
		/// Try and match a line of source code.
		/// </summary>
		/// <param name="compiler">The compiler currently assembling the project.</param>
		/// <param name="source">The source line statement.</param>
		/// <param name="index">The first token to look at in the source.</param>
		/// <returns>True if the source was matched, false otherwise.</returns>
		//TODO: Document what the method actually needs to DO.
		bool TryMatchSource(Compiler compiler, TokenisedSource source, int index);

		/// <summary>
		/// Assemble a line of source code.
		/// </summary>
		/// <param name="compiler">The compiler currently assembling the project.</param>
		/// <param name="source">The source line statement.</param>
		/// <param name="index">The first token to look at in the source.</param>
		void Assemble(Compiler compiler, TokenisedSource source, int index);

	}


}
