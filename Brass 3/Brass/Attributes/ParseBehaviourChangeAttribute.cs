using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a parser behaviour-changing attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ParserBehaviourChangeAttribute : Attribute {


		[Flags()]
		public enum ParserBehaviourModifiers {
			None = 0,
			IgnoreStatementSeperator = 1,
			IgnoreAllNewLines = 2,
			IgnoreFirstNewLine = 4,
			SkipMacroPreprocessor = 8,
		}

		private readonly ParserBehaviourModifiers behaviour;
		public ParserBehaviourModifiers Behaviour {
			get { return this.behaviour; }
		}

		/// <summary>
		/// Creates a new instance of the ParserBehaviourChangeAttribute class.
		/// </summary>
		public ParserBehaviourChangeAttribute(ParserBehaviourModifiers change) {
			this.behaviour = change;
		}

	}
}
