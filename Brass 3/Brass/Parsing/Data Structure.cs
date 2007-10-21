using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Brass3.Plugins;

namespace Brass3 {

	/// <summary>
	/// Defines a data structure.
	/// </summary>
	public class DataStructure : IDataStructure {

		#region Types

		/// <summary>
		/// Defines a field within the data structure.
		/// </summary>
		public class Field : ICloneable {

			private string name;
			/// <summary>
			/// Gets or sets the name of the field.
			/// </summary>
			public string Name {
				get { return this.name; }
				set { this.name = value; }
			}

			private int offset;
			/// <summary>
			/// Gets or sets the data offset of the field within the structure.
			/// </summary>
			public int Offset {
				get { return this.offset; }
				set { this.offset = value; }
			}
			
			private int elementCount = 1;
			/// <summary>
			/// Gets or sets the number of elements (for arrays).
			/// </summary>
			public int ElementCount {
				get { return this.elementCount; }
				set { this.elementCount = value; }
			}

			public IDataStructure dataType;
			/// <summary>
			/// Gets or sets the underlying data type.
			/// </summary>
			public IDataStructure DataType {
				get { return this.dataType; }
				set { this.dataType = value; }
			}

			/// <summary>
			/// Gets the total size of the field.
			/// </summary>
			public int Size {
				get { return this.elementCount * this.DataType.Size; }
			}

			/// <summary>
			/// Creates an instance of a data structure field.
			/// </summary>
			public Field(string name, IDataStructure type, int offset, int elementCount) {
				this.name = name;
				this.dataType = type;
				this.offset = offset;
				this.elementCount = elementCount;
			}

			/// <summary>
			/// Returns a string representation of the field.
			/// </summary>
			public override string ToString() {
				return string.Format("{{offset={0}, type={1}, size={2}}}", this.Offset, Compiler.GetPluginName(this.DataType), this.Size);
			}

			/// <summary>
			/// Creates an instance of a data structure field.
			/// </summary>
			public Field(IDataStructure type)
				: this(null, type, 0, 1) {
			}

			public object Clone() {
				return new Field(this.name, this.dataType, this.offset, this.elementCount);
			}
		}

		#endregion

		#region Properties

		private readonly List<Field> fields;
		/// <summary>
		/// Gets the list of fields making up the data structure.
		/// </summary>
		public List<Field> Fields {
			get { return this.fields; }
		}

		/// <summary>
		/// Gets the size of the entire structure.
		/// </summary>
		public int Size {
			get {
				int Size = 0;
				foreach (Field F in this.Fields) Size += F.Size;
				return Size;
			}
		}

		private readonly string[] names;
		/// <summary>
		/// Gets all of the names (and aliases) of the data structure.
		/// </summary>
		public string[] Names { get { return this.names; } }

		/// <summary>
		/// Gets the main name of the data structure.
		/// </summary>
		public string Name { get { return this.Names[0]; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a data structure declaration instance.
		/// </summary>
		/// <param name="names">Array of aliased names.</param>
		public DataStructure(string[] names) {
			this.fields = new List<Field>(8);
			if (names == null) throw new ArgumentNullException("You must specify a name.");
			if (names.Length == 0) throw new ArgumentException("You must specify a name.");
			this.names = names;

		}

		/// <summary>
		/// Creates a data structure declaration instance.
		/// </summary>
		/// <param name="name">Name of the structure.</param>
		public DataStructure(string name)
			: this(new string[] { name }) {
		}

		/// <summary>
		/// Creates a basic data structure declaration instance.
		/// </summary>
		/// <param name="type">The underlying data type.</param>
		public DataStructure(IDataStructure type)
			: this(Compiler.GetPluginNames(type)) {
			this.fields.Add(new Field(type));
		}

		/// <summary>
		/// Gets a field by its name.
		/// </summary>
		/// <param name="fieldName">The name of the field to search for.</param>
		public Field this[string fieldName] {
			get {
				string SearchName = fieldName.ToLowerInvariant();
				foreach (Field F in this.fields) {
					if (F.Name.ToLowerInvariant() == SearchName) return F;
				}
				throw new IndexOutOfRangeException("Field '" + fieldName + "' not defined.");
			}
		}

		#endregion

		private KeyValuePair<string, Field>[] GetAllFields(int offset, string prefix) {
			List<KeyValuePair<string, Field>> Result = new List<KeyValuePair<string, Field>>();

			foreach (Field F in this.Fields) {
				if (F.Name != null) {
					string FieldLabelName = LabelCollection.ModuleCombine(prefix, F.Name);

					Field ClonedField = F.Clone() as Field;
					ClonedField.Offset += offset;

					Result.Add(new KeyValuePair<string, Field>(FieldLabelName, ClonedField));
					if (F.DataType.GetType() == typeof(DataStructure)) {
						Result.AddRange((F.DataType as DataStructure).GetAllFields(F.Offset, FieldLabelName));
					}
				}
			}

			return Result.ToArray();
		}
		/// <summary>
		/// Gets all of the fields underneath a particular data structure.
		/// </summary>
		public KeyValuePair<string, Field>[] GetAllFields() {
			return this.GetAllFields(0, "");
		}


	}
}
