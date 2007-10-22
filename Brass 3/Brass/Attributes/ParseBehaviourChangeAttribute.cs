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
			None = 0,
			IgnoreStatementSeperator = 1,
			IgnoreAllNewLines = 2,
			IgnoreFirstNewLine = 4,
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
