using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a syntax example attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class RemarksAttribute : Attribute {

		private string remarks;
		/// <summary>
		/// Gets or sets the remarks for the type.
		/// </summary>
		public string Remarks {
			get { return this.remarks; }
			set { this.remarks = value; }
		}

		/// <summary>
		/// Creates a new instance of the RemarksAttribute class.
		/// </summary>
		/// <param name="syntax">The remarks for the type.</param>
		public RemarksAttribute(string remarks) {
			this.remarks = remarks;

		}
	}
}
