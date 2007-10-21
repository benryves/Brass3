using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a syntax example attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class SeeAlsoAttribute : Attribute {

		private Type typeToSeeAlso;
		/// <summary>
		/// Gets or sets the type to see also.
		/// </summary>
		public Type TypeToSeeAlso {
			get { return this.typeToSeeAlso; }
			set { this.typeToSeeAlso = value; }
		}

		/// <summary>
		/// Creates a new instance of the SyntaxAttribute class.
		/// </summary>
		/// <param name="typeToSeeAlso">The type to see also.</param>
		public SeeAlsoAttribute(Type typeToSeeAlso) {
			this.typeToSeeAlso = typeToSeeAlso;

		}

	}
}
