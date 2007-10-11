using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Represents the two passes of the assembler.
	/// </summary>
	public enum AssemblyPass {
		None,
		Pass1,
		Pass2,
	}

	/// <summary>
	/// Represents the endianness of a platform.
	/// </summary>
	public enum Endianness {
		Little,
		Big,
	}

}
