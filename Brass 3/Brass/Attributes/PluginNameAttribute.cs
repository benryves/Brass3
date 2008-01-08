using System;
using System.Collections.Generic;
using System.Text;

namespace BeeDevelopment.Brass3.Attributes {

	/// <summary>
	/// Defines a plugin name attribute.
	/// </summary>
	/// <remarks>
	/// By default a plugin is given the name of its class.
	/// However, if you want your plugin to respond to multiple aliases or give it a name that is invalid in C#, mark the plugin class with this attribute.
	/// </remarks>
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

		/// <summary>Creates an instance of the <see cref="PluginNameAttribute"/> attribute.</summary>
		/// <param name="name">The name for the plugin.</param>
		public PluginNameAttribute(string name) {
			this.name = name;
		}
	}
}
