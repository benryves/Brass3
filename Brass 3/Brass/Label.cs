using System;
using System.Collections.Generic;
using System.Text;
using Brass3.Plugins;

namespace Brass3 {

	/// <summary>
	/// Defines a value and a page. Can represent a variable value or a constant.
	/// </summary>
	public class Label : ICloneable {

		#region Properties

		private TokenisedSource.Token token;
		/// <summary>
		/// Gets the underlying token that created this label.
		/// </summary>
		public TokenisedSource.Token Token {
			get { return this.token; }
			internal set { this.token = value; }
		}

		private AssemblyPass lastAssignedPass;
		/// <summary>
		/// Gets the last pass that a label was assigned to this label.
		/// </summary>
		public AssemblyPass LastAssignedPass {
			get { return this.lastAssignedPass; }
		}

		private readonly LabelCollection collection;
		/// <summary>
		/// Gets the collection containing the label.
		/// </summary>
		public LabelCollection Collection {
			get { return this.collection; }
		}

		private string name;
		/// <summary>
		/// Gets the full name of the label.
		/// </summary>
		public string Name {
			get {
				return this.name;
			}
			internal set {
				this.name = value;
			}
		}

		internal int ChangeCount = 0;
		internal bool NeedsClearingBetweenPasses {
			get { return ChangeCount > 1; }
		}

		private IDataStructure type = null;
		public IDataStructure Type {
			get { return this.type; }
			set { this.type = value; }
		}

		/// <summary>
		/// Gets the size of the label's underlying type.
		/// </summary>
		public int Size {
			get { return this.type == null ? 1 : this.type.Size; }
		}


		private double value;
		/// <summary>
		/// Gets or sets the value of the label.
		/// </summary>
		/// <remarks>You cannot assign values to constants.</remarks>
		public double Value {
			get {
				if (AccessingPage) {
					return this.Page;
				} else {
					if (!this.Created) throw new LabelNotFoundExpection(this.Token, "Value for label '" + this.Name + "' not found.");
					return this.value;
				}
			}
			set {
				if (AccessingPage) {
					this.Page = (int)value;
				} else {
					if (this.IsConstant) throw new LabelExpection(this.token, "'" + this.Name + "' is a constant and cannot be assigned to.");
					this.lastAssignedPass = this.collection.Compiler.CurrentPass;
					if (this.created) ++ChangeCount;
					this.created = true;
					this.value = value;
				}
			}
		}

		private int page;
		/// <summary>
		/// Gets or sets the page the label is on.
		/// </summary>
		public int Page {
			get {
				if (!this.Created) throw new LabelExpection(this.token, "Page value for label '" + this.Name + "' not found.");
				return this.page;
			}
			set {
				if (this.IsConstant) throw new LabelExpection(this.token, "'" + this.Name + "' is a constant and cannot be assigned to.");
				this.lastAssignedPass = this.collection.Compiler.CurrentPass;
				this.created = true;
				this.page = value;
			}
		}

		private bool isConstant;
		/// <summary>
		/// Gets true if the label is a constant.
		/// </summary>
		public bool IsConstant {
			get { return this.isConstant; }
			internal set { this.isConstant = value; }
		}

		private bool created;
		/// <summary>
		/// Gets true if the label has been created and assigned.
		/// </summary>
		public bool Created {
			get { return this.created; }
			internal set { this.created = value; }
		}

		/// <summary>
		/// Returns a formatted string describing the label.
		/// </summary>
		public override string ToString() {
			return this.Name + "=" + this.Value + ":" + this.Page;
		}

		/// <summary>
		/// Returns true if the label is "defined".
		/// </summary>
		/// <remarks>A "defined" label is one that has been created and assigned to during the current pass.</remarks>
		public bool Defined {
			get { return this.lastAssignedPass == this.collection.Compiler.CurrentPass; }
		}


		private bool accessingPage = false;
		/// <summary>
		/// Returns true if the label is being accessed by page rather than by value.
		/// </summary>
		public bool AccessingPage {
			get { return this.accessingPage; }
			internal set { this.accessingPage = value; }
		}

		private bool exported;
		/// <summary>
		/// Gets or sets whether the label is to be exported or not.
		/// </summary>
		public bool Exported {
			get { return this.exported; }
			set { this.exported = value; }
		}


		#endregion

		#region Constructors

		/// <summary>
		/// Creates a valued instance of a label.
		/// </summary>
		/// <param name="name">The name of the label.</param>
		/// <param name="isConstant">True if this label is a constant.</param>
		/// <param name="value">The value of the label.</param>
		/// <param name="page">The page the label is on.</param>
		public Label(LabelCollection collection, TokenisedSource.Token token, bool isConstant, double value, int page, IDataStructure type) {
			this.exported = collection.ExportLabels;
			this.created = true;
			this.collection = collection;
			this.token = token;
			if (this.token != null) this.name = GetNameWithoutColon(token.Data, out accessingPage);
			this.value = value;
			this.page = page;
			this.type = type;
		}

		/// <summary>
		/// Creates an instance of a label.
		/// </summary>
		/// <param name="name">The name of the label.</param>
		/// <param name="isConstant">True if this label is a constant.</param>
		public Label(LabelCollection collection, TokenisedSource.Token token, bool isConstant) {
			this.exported = collection.ExportLabels;
			this.created = false;
			this.collection = collection;
			this.name = GetNameWithoutColon(token.Data, out accessingPage);
			
		}

		internal static string GetNameWithoutColon(string name, out bool accessingPage) {
			if (name[0] == ':') {
				accessingPage = true;
				return name.Substring(1);
			} else if (name[name.Length - 1] == ':') {
				accessingPage = false;
				return name.Remove(name.Length - 1);
			} else {
				accessingPage = false;
				return name;
			}
		}

		/// <summary>
		/// Creates a limited instance of a label.
		/// </summary>
		/// <param name="collection">The collection containing the label.</param>
		/// <param name="value">The value to set the label to.</param>
		/// <param name="isConstant">True if the label is a constant, false if it is a variable.</param>
		/// <remarks>This crude label is very limited in functionality, and it is recommended that you only use it to create temporary labels as return values from functions.</remarks>
		public Label(LabelCollection collection, double value, bool isConstant) {
			this.exported = collection.ExportLabels;
			this.collection = collection;
			this.created = true;
			this.value = value;
			this.page = collection.ProgramCounter.page;
			this.isConstant = isConstant;
		}

		/// <summary>
		/// Creates a limited instance of a label.
		/// </summary>
		/// <param name="collection">The collection containing the label.</param>
		/// <param name="value">The value to set the label to.</param>
		/// <remarks>This crude label is very limited in functionality, and it is recommended that you only use it to create temporary labels as return values from functions.</remarks>
		public Label(LabelCollection collection, double value)
			: this(collection, value, true) {
		}

		#endregion



		#region ICloneable Members

		/// <summary>
		/// Creates a copy of the label.
		/// </summary>
		public object Clone() {
			return new Label(this.collection, this.Token, this.IsConstant, this.Value, this.Page, this.type);
		}

		/// <summary>
		/// Creates a copy of the label.
		/// </summary>
		public object Clone(TokenisedSource.Token renamed) {
			return new Label(this.collection, renamed, this.IsConstant, this.Value, this.Page, this.type);
		}

		#endregion

		/// <summary>
		/// Checks if a label name is valid.
		/// </summary>
		/// <param name="name">The name of the label to check (minus any colon prefixes).</param>
		/// <returns>True if the label name is valid, false otherwise.</returns>
		public static bool IsValidLabelName(string name) {

			if (string.IsNullOrEmpty(name)) return false;

			if (name == "$") return true;

			// Bad characters cannot appear ANYWHERE in the label name.
			// These include operators and whitespace.
			const string BadChars = " \t\r\n\"%^&*()[]-=+/\\<>,~|!?:;";

			const string BadPrefix = "$%@";
			foreach (char c in BadPrefix) if (name[0] == c) return false;

			foreach (char c in name) if (BadChars.IndexOf(c) != -1) return false;

			bool IsAllDigits = true;
			for (int i = 0; i < name.Length; ++i) {

				// break for suffixes.
				if (i == name.Length - 1 && "hbo".IndexOf(char.ToLower(name[i])) != -1) break;

				if (!char.IsDigit(name, i)) {
					IsAllDigits = false;
					break;
				}
			}

			if (IsAllDigits) return false;

			// I think it might be valid!
			return true;
		}

		/// <summary>
		/// State that the label was implicitly created. Only call this if you are generating new labels and need to force implicit creation.
		/// </summary>
		public void SetImplicitlyCreated() {
			this.created = true;
		}

		/// <summary>
		/// State that the label is a constant. Only call this if you are generating new labels and want to force it to be constant.
		/// </summary>
		public void SetConstant() {
			this.isConstant = true;
		}

	}
}
