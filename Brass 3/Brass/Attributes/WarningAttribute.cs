using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a warning attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class WarningAttribute : Attribute {

		private string warning;
		/// <summary>
		/// Gets or sets the remarks for the type.
		/// </summary>
		public string Warning {
			get { return this.warning; }
			set { this.warning = value; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="WarningAttribute"/> class.
		/// </summary>
		/// <param name="warning">The warning for the type.</param>
		public WarningAttribute(string warning) {
			this.warning = warning;

		}

	}
}
