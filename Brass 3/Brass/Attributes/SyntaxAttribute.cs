using System;
using System.Collections.Generic;
using System.Text;

namespace BeeDevelopment.Brass3.Attributes {

	/// <summary>
	/// Defines a syntax example attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class SyntaxAttribute : Attribute {

		private string syntax;
		/// <summary>
		/// Gets or sets the syntax example for the type.
		/// </summary>
		public string Syntax {
			get { return this.syntax; }
			set { this.syntax = value; }
		}

		/// <summary>
		/// Creates a new instance of the SyntaxAttribute class.
		/// </summary>
		/// <param name="syntax">The syntax example for the type.</param>
		public SyntaxAttribute(string syntax) {
			this.syntax = syntax;

		}

	}
}
