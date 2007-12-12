using System;
using System.Collections.Generic;
using System.Text;
using Brass3.Plugins;
using System.Globalization;

namespace Brass3 {

	/// <summary>
	/// Defines a value and a page. Can represent a variable value or a constant.
	/// </summary>
	public class Label : ICloneable {

		#region Types

		/// <summary>
		/// Describes the usage of the label.
		/// </summary>
		public enum UsageType {
			/// <summary>
			/// The label defines an executable address (such as a function entry point).
			/// </summary>
			Execution,
			/// <summary>
			/// The label defines a variable (pointer to memory).
			/// </summary>
			Variable,
			/// <summary>
			/// The label defines a constant.
			/// </summary>
			Constant,
		}

		#endregion

		#region Events

		/// <summary>
		/// Defines the event arguments for a changed value.
		/// </summary>
		public class ValueChangedEventArgs : EventArgs {

			/// <summary>
			/// Defines which field of the label changed.
			/// </summary>
			public enum FieldChangedType {
				/// <summary>
				/// The numeric value of the label was changed.
				/// </summary>
				NumericValue,
				/// <summary>
				/// The string value of the label was changed.
				/// </summary>
				StringValue,
				/// <summary>
				/// The page value of the label was changed.
				/// </summary>
				Page,
			}

			private readonly FieldChangedType changedField;
			/// <summary>
			/// Gets the label field that was changed.
			/// </summary>
			public FieldChangedType ChangedField {
				get { return this.changedField; }				
			}

			private readonly object oldValue;
			/// <summary>
			/// Gets the old value of the changed field.
			/// </summary>
			public object OldValue {
				get { return this.oldValue; }
			}
			

			private readonly object newValue;
			/// <summary>
			/// Gets the new value of the changed field.
			/// </summary>
			public object NewValue {
				get { return this.newValue; }
			}

			/// <summary>
			/// Creates an instance of the <see cref="ValueChangedEventArgs"/> class.
			/// </summary>
			public ValueChangedEventArgs(FieldChangedType changedField, object oldValue, object newValue) {
				this.changedField = changedField;
				this.oldValue = oldValue;
				this.newValue = newValue;
			}
		}

		/// <summary>
		/// Represents the method that will handle the <see cref="ValueChanged"/> event of a <see cref="Label"/>.
		/// </summary>
		/// <param name="sender">The <see cref="Label"/> whose value has changed.</param>
		/// <param name="e">A <see cref="ValueChangedEventArgs"/> that contains the event data.</param>
		public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);

		/// <summary>
		/// Event fired when one of the label's values changes.
		/// </summary>
		public event ValueChangedEventHandler ValueChanged;
		
		/// <summary>
		/// Event fired when one of the label's values changes.
		/// </summary>
		protected virtual void OnValueChanged(ValueChangedEventArgs e) {
			if (ValueChanged != null) ValueChanged(this, e);
		}

		#endregion

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
		/// <summary>
		/// Gets or sets the underlying <see cref="IDataStructure"/> type defined by the label.
		/// </summary>
		public IDataStructure DataType {
			get { return this.type; }
			set { this.type = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="UsageType"/> of the label.
		/// </summary>
		public UsageType Usage { get; set; }

		/// <summary>
		/// Gets the size of the label's underlying type.
		/// </summary>
		public int Size {
			get { return this.type == null ? 1 : this.type.Size; }
		}

		private bool isString;
		/// <summary>
		/// Gets or sets whether to interpret the label as a string.
		/// </summary>
		public bool IsString {
			get { return this.isString; }
			set { this.isString = value; }
		}

		private string stringValue;
		/// <summary>
		/// Gets or sets the string value of the label.
		/// </summary>
		public string StringValue {
			get { return this.isString ? this.stringValue : this.NumericValue.ToString(CultureInfo.InvariantCulture); }
			set {
				this.isString = true;
				string Old = this.stringValue;
				this.stringValue = value;
				this.OnValueChanged(new ValueChangedEventArgs(ValueChangedEventArgs.FieldChangedType.StringValue, Old, this.stringValue));
			}
		}

		private double value;
		/// <summary>
		/// Gets or sets the value of the label.
		/// </summary>
		/// <remarks>You cannot assign values to constants.</remarks>
		public double NumericValue {
			get {
				if (AccessingPage) {
					return this.Page;
				} else {
					if (!this.Created) throw new LabelNotFoundException(this.Token, string.Format(Strings.ErrorLabelNotFound, this.Name));
					if (this.IsString) {
						byte[] Data = this.collection.Compiler.StringEncoder.GetData(this.StringValue);
						double ParsedValue = 0;
						for (int i = Data.Length - 1; i >= 0; i--) {
							ParsedValue *= 256;
							ParsedValue += (int)(Data[i]);
						}
						return ParsedValue;
					} else {
						return this.value;
					}
				}
			}
			set {
				if (AccessingPage) {
					int Old = this.page;
					this.Page = (int)value;
					this.OnValueChanged(new ValueChangedEventArgs(ValueChangedEventArgs.FieldChangedType.Page, Old, this.page));
				} else {
					if (this.IsConstant) throw new LabelException(this.token, string.Format(Strings.ErrorLabelIsConstant, this.Name));
					if (this.created) ++ChangeCount;
					this.created = true;
					this.isString = false;
					double Old = this.value;
					this.value = value;
					this.OnValueChanged(new ValueChangedEventArgs(ValueChangedEventArgs.FieldChangedType.NumericValue, Old, this.value));
				}
			}
		}

		private int page;
		/// <summary>
		/// Gets or sets the page the label is on.
		/// </summary>
		public int Page {
			get {
				if (!this.Created) throw new LabelException(this.token, string.Format(Strings.ErrorLabelPageNotFound, this.Name));
				return this.page;
			}
			set {
				if (this.IsConstant) throw new LabelException(this.token, string.Format(Strings.ErrorLabelIsConstant, this.Name));
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
			return this.Name + "=" + (this.IsString ? this.StringValue : this.NumericValue.ToString()) + ":" + this.Page;
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
		/// <param name="collection">The <see cref="LabelCollection"/> that will contain the label.</param>
		/// <param name="token">The name of the label.</param>
		/// <param name="isConstant">True if this label is a constant.</param>
		/// <param name="value">The value of the label.</param>
		/// <param name="page">The page the label is on.</param>
		/// <param name="type">The <see cref="IDataStructure"/> type of the label.</param>
		public Label(LabelCollection collection, TokenisedSource.Token token, bool isConstant, double value, int page, IDataStructure type) {
			this.exported = collection.ExportLabels;
			this.created = true;
			this.collection = collection;
			this.token = token;
			if (this.token != null) this.name = token.Data;
			this.value = value;
			this.page = page;
			this.type = type;
			if (token != null) {
				this.stringValue = token.Type == TokenisedSource.Token.TokenTypes.String ? token.GetStringConstant(true) : token.Data;
				this.isString = token.Type == TokenisedSource.Token.TokenTypes.String;
			}
		}

		/// <summary>
		/// Creates an instance of a label.
		/// </summary>
		/// <param name="collection">The <see cref="LabelCollection"/> that will contain the label.</param>
		/// <param name="token">The name of the label.</param>
		/// <param name="isConstant">True if this label is a constant.</param>
		public Label(LabelCollection collection, TokenisedSource.Token token, bool isConstant) {
			this.exported = collection.ExportLabels;
			this.created = false;
			this.collection = collection;
			this.name = token.Data;
			if (token != null) {
				this.stringValue = token.Type == TokenisedSource.Token.TokenTypes.String ? token.GetStringConstant(true) : token.Data;
				this.isString = token.Type == TokenisedSource.Token.TokenTypes.String;
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
			this.Usage = UsageType.Execution;
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

		/// <summary>
		/// Creates a limited instance of a label.
		/// </summary>
		/// <param name="collection">The collection containing the label.</param>
		/// <param name="value">The value to set the label to.</param>
		/// <remarks>This crude label is very limited in functionality, and it is recommended that you only use it to create temporary labels as return values from functions.</remarks>
		public Label(LabelCollection collection, string value)
			: this(collection, 0, true) {
			this.StringValue = value;
		}

		#endregion

		#region ICloneable Members

		/// <summary>
		/// Creates a copy of the label.
		/// </summary>
		public object Clone() {
			Label L = new Label(this.collection, this.Token, this.IsConstant, this.NumericValue, this.Page, this.type);
			L.isString = this.isString;
			L.stringValue = this.stringValue;
			return L;
		}

		/// <summary>
		/// Creates a copy of the label.
		/// </summary>
		public object Clone(TokenisedSource.Token renamed) {
			Label L = new Label(this.collection, renamed, this.IsConstant, this.NumericValue, this.Page, this.type);
			L.isString = this.isString;
			L.stringValue = this.stringValue;
			return L;
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

			const string BadPrefix = "$%";
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
