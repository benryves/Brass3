using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3.Attributes {

	[AttributeUsage(AttributeTargets.Class)]
	public class SatisfiesAssignmentRequirementAttribute : Attribute {


		private readonly bool satisfiesAssignmentRequirement;
		public bool SatisfiesAssignmentRequirement {
			get { return this.satisfiesAssignmentRequirement; }
		}

		/// <summary>
		/// Creates a new instance of the SatisfiesAssignmentRequirementAttribute class.
		/// </summary>
		public SatisfiesAssignmentRequirementAttribute(bool satisfiesAssignmentRequirement) {
			this.satisfiesAssignmentRequirement = satisfiesAssignmentRequirement;
		}

	}
}
