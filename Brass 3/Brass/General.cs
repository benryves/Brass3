using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Represents the passes of the assembler.
	/// </summary>
	public enum AssemblyPass {
		/// <summary>
		/// Not in an assembly pass.
		/// </summary>
		None,
		/// <summary>
		/// Represents the initial pass, during which labels are created and assigned.
		/// </summary>
		CreatingLabels = 1,
		/// <summary>
		/// Represents the final pass, during which the output data is generated.
		/// </summary>
		WritingOutput = 2,
	}

	/// <summary>
	/// Represents the endianness of a platform.
	/// </summary>
	public enum Endianness {
		/// <summary>
		/// Represents a little-endian byte-order platform.
		/// </summary>
		Little,
		/// <summary>
		/// Represents a big-endian byte order platform.
		/// </summary>
		Big,
	}

}
