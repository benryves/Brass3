using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Defines a breakpoint.
	/// </summary>
	public class Breakpoint {

		/// <summary>
		/// Describes the event that triggers the breakpoint.
		/// </summary>
		[Flags()]
		public enum BreakpointTrigger {
			/// <summary>
			/// The breakpoint is never triggered.
			/// </summary>
			None = 0x00,
			/// <summary>
			/// The breakpoint halts program execution when the program counter reaches it.
			/// </summary>
			Execution = 0x01,
			/// <summary>
			/// The breakpoint halts program execution when the memory address it points to is read from.
			/// </summary>
			MemoryRead = 0x02,
			/// <summary>
			/// The breakpoint halts program execution when the memory address it points to is written to.
			/// </summary>
			MemoryWrite = 0x04,
		}

		/// <summary>
		/// Gets or sets the page that the breakpoint occurs on.
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// Gets or sets the address that the breakpoint refers to.
		/// </summary>
		public int Address { get; set; }

		/// <summary>
		/// Gets or sets the event that trigers the breakpoint.
		/// </summary>
		public BreakpointTrigger Trigger { get; set; }


	}
}