using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Defines a breakpoint.
	/// </summary>
	public class Breakpoint {

		#region Types

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

		#endregion

		#region Properties

		/// <summary>
		/// Gets the compiler that created this breakpoint.
		/// </summary>
		public Compiler Compiler { get; private set; }

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

		/// <summary>
		/// Gets or sets the description of the breakpoint.
		/// </summary>
		public string Description { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of a breakpoint.
		/// </summary>
		/// <param name="compiler">The compiler that created the breakpoint.</param>
		/// <param name="trigger">The event that triggers the breakpoint.</param>
		/// <param name="address">The address that the breakpoint refers to</param>
		/// <param name="page">The page that the breakpoint occurs on.</param>
		/// <param name="description">A description of the breakpoint.</param>
		public Breakpoint(Compiler compiler, BreakpointTrigger trigger, int address, int page, string description) {
			this.Compiler = compiler;
			this.Trigger = trigger;
			this.Address = address;
			this.Page = page;
			this.Description = description;
		}

		/// <summary>
		/// Creates an instance of a breakpoint.
		/// </summary>
		public Breakpoint(Compiler compiler)
			: this(compiler, BreakpointTrigger.Execution, (int)compiler.Labels.ProgramCounter.NumericValue, compiler.Labels.ProgramCounter.Page, null) {
		}

		#endregion
	}
}