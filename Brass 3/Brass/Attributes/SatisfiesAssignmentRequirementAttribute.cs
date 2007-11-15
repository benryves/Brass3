using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	/// <summary>
	/// Marks a function as implicitly satisfying the requirement that an assignment should be made in the label assignment section of a statement.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class SatisfiesAssignmentRequirementAttribute : Attribute {


		private readonly bool satisfiesAssignmentRequirement;
		/// <summary>
		/// Returns true if the attribute indicates that the marked class satisfies the assignment requirement.
		/// </summary>
		public bool SatisfiesAssignmentRequirement {
			get { return this.satisfiesAssignmentRequirement; }
		}

		/// <summary>
		/// Creates a new instance of the SatisfiesAssignmentRequirementAttribute class.
		/// </summary>
		/// <param name="satisfiesAssignmentRequirement">True if the marked class satisfies the assignment requirement.</param>
		public SatisfiesAssignmentRequirementAttribute(bool satisfiesAssignmentRequirement) {
			this.satisfiesAssignmentRequirement = satisfiesAssignmentRequirement;
		}

	}
}
