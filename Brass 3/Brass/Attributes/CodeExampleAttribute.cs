using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a syntax example attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class CodeExampleAttribute : Attribute {

		private string example;
		/// <summary>
		/// Gets or sets the example for the type.
		/// </summary>
		public string Example {
			get { return this.example; }
			set { this.example = value; }
		}

		private string caption;
		/// <summary>
		/// Gets or sets the caption for the type.
		/// </summary>
		public string Caption {
			get { return this.caption; }
			set { this.caption = value; }
		}

		/// <summary>
		/// Creates a new instance of the CodeExampleAttribute class.
		/// </summary>
		/// <param name="example">The example for the type.</param>
		public CodeExampleAttribute(string example) {
			this.example = example;
		}


		/// <summary>
		/// Creates a new instance of the CodeExampleAttribute class.
		/// </summary>
		/// <param name="caption">The caption for the example.</param>
		/// <param name="example">The example for the type.</param>
		public CodeExampleAttribute(string caption, string example) {
			this.caption = caption;
			this.example = example;
		}
	}
}
