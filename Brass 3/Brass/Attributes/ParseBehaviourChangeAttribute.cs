using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a parser behaviour-changing attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ParserBehaviourChangeAttribute : Attribute {


		/// <summary>
		/// Defines the possible parser modifications that are available.
		/// </summary>
		[Flags()]
		public enum ParserBehaviourModifiers {
			/// <summary>
			/// The behaviour of the parser is not modified.
			/// </summary>
			None = 0,
			/// <summary>
			/// The statement seperator character <c>\</c> will be not be permitted to terminate the current statement.
			/// </summary>
			IgnoreStatementSeperator = 1,
			/// <summary>
			/// The newline character will not be permitted to terminate the current statement at all.
			/// </summary>
			IgnoreAllNewLines = 2,
			/// <summary>
			/// The first encountered character will not be permitted to terminate the current statement at all.
			/// </summary>
			IgnoreFirstNewLine = 4,
			/// <summary>
			/// The macro preprocessor will not be run on this statement.
			/// </summary>
			SkipMacroPreprocessor = 8,
		}

		private readonly ParserBehaviourModifiers behaviour;
		/// <summary>
		/// Gets the parser behaviour modifiers this attribute represents.
		/// </summary>
		public ParserBehaviourModifiers Behaviour {
			get { return this.behaviour; }
		}

		/// <summary>
		/// Creates a new instance of the ParserBehaviourChangeAttribute class.
		/// </summary>
		public ParserBehaviourChangeAttribute(ParserBehaviourModifiers change) {
			this.behaviour = change;
		}

		/// <summary>
		/// Returns a formatted string representing the attribute.
		/// </summary>
		public override string ToString() {
			return this.Behaviour.ToString();
		}

	}
}
