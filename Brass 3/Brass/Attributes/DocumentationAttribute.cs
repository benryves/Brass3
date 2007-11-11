using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Defines a plugin documentation attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class DocumentationUsageAttribute : Attribute {
		
		/// <summary>
		/// Defines the way that the plugin relates to documentation.
		/// </summary>
		public enum DocumentationType {
			/// <summary>
			/// The plugin provides some functionality and has documentation.
			/// </summary>
			FunctionalityAndDocumentation,
			/// <summary>
			/// The plugin provides some functionality but has no documentation and should not be displayed in the help viewer.
			/// </summary>
			FunctionalityOnly,
			/// <summary>
			/// The plugin provides some documentation but has no functionality.
			/// </summary>
			DocumentationOnly,
		}

		private readonly DocumentationType documentation;
		/// <summary>
		/// Gets the <see cref="DocumentationType"/> that this attribute represents.
		/// </summary>
		public DocumentationType Documentation {
			get { return this.documentation; }
		}

		/// <summary>
		/// Creates an instance of the <see cref="DocumentationUsageAttribute"/> attribute class.
		/// </summary>
		/// <param name="documentation">The <see cref="DocumentationType"/> that this attribute represents.</param>
		public DocumentationUsageAttribute(DocumentationType documentation) {
			this.documentation = documentation;
		}

	}
}
