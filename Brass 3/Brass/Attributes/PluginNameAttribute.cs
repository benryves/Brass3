using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a syntax example attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class PluginNameAttribute : Attribute {

		private string name;
		/// <summary>
		/// Gets or sets the example for the type.
		/// </summary>
		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}

		/// <param name="syntax">The name for the plugin.</param>
		public PluginNameAttribute(string name) {
			this.name = name;
		}
	}
}
