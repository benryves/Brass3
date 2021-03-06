using System;
using System.Collections.Generic;
using System.Text;

namespace BeeDevelopment.Brass3 {

	/// <summary>
	/// Defines a collection of labels.
	/// </summary>
	public class LabelCollection : IEnumerable<Label> {
		
		private Dictionary<string, Label> Lookup = new Dictionary<string,Label>();

		private Dictionary<int, Dictionary<int, Label>> ReusableBackwards;
		private Dictionary<int, Dictionary<int, Label>> ReusableForwards;

		private int MaxReusableIndex;

		#region Properties

		private string currentModule;
		/// <summary>
		/// Gets or sets the current module.
		/// </summary>
		public string CurrentModule {
			get { return this.currentModule; }
			set {
				//TODO: Verify syntax.
				this.currentModule = value;
			}
		}

		/// <summary>
		/// Enter a temporary module.
		/// </summary>
		public void EnterTemporaryModule() {
			int ModuleSuffix = (ushort)DateTime.Now.Ticks;
			this.EnterModule(string.Format("TEMP_MODULE_#{0:X4}", ModuleSuffix));
			
		}

		/// <summary>
		/// Leave a temporary module.
		/// </summary>
		/// <returns>An array of the <see cref="Label"/> objects created within the temporary module.</returns>
		public Label[] LeaveTemporaryModule() {
			List<Label> TemporaryLabels = new List<Label>();
			List<Label> ToPurge = new List<Label>();
			string TempName = this.CurrentModule.ToLowerInvariant();
			foreach (Label L in this) {
				if (ModuleGetParent(L.Name).ToLowerInvariant() == TempName) {
					ToPurge.Add(L);
				}
			}
			foreach (Label L in ToPurge) {
				TemporaryLabels.Add(L.Clone() as Label);
				this.Remove(L);				
			}
			this.LeaveModule();
			return TemporaryLabels.ToArray();
		}

		private Label implicitCreationDefault;
		/// <summary>
		/// Gets or sets the label that is implicitly assigned to new labels.
		/// </summary>
		/// <remarks>This defaults to the program counter label, <c>$</c>.</remarks>
		/// <warning>Changing this to your own label can cause serious bugs if your plugin is programmed incorrectly. Consider reverting it to its default value (ProgramCounter) in a <c>finally</c> block.</warning>
		public Label ImplicitCreationDefault {
			get { return this.implicitCreationDefault ?? this.programCounter; }
			set { this.implicitCreationDefault = value; }
		}
		

		private readonly Label programCounter;
		/// <summary>
		/// Gets the current program counter.
		/// </summary>
		public Label ProgramCounter {
			get { this.programCounter.AccessingPage = false; return this.programCounter; }
		}

		private readonly Label outputCounter;
		/// <summary>
		/// Gets the current output counter.
		/// </summary>
		public Label OutputCounter {
			get { this.outputCounter.AccessingPage = false; return this.outputCounter; }
		}

		private readonly Compiler compiler;
		/// <summary>
		/// Gets the compiler containing this label collection.
		/// </summary>
		public Compiler Compiler {
			get { return this.compiler; }
		}

		private bool exportLabels;
		/// <summary>
		/// Gets true if labels are to be exported by default, false otherwise.
		/// </summary>
		public bool ExportLabels {
			get { return this.exportLabels; }
			set { this.exportLabels = value; }
		}

		#endregion

		#region Label Access

		/// <summary>
		/// Gets a label by its name.
		/// </summary>
		/// <param name="name">The name of the label to retrieve.</param>
		public Label this[string name] {
			get {
				Label Label;
				if (TryGetByName(name, out Label)) return Label;
				throw new InvalidOperationException(string.Format(Strings.ErrorLabelNotFound, name));
			}
		}


		private bool TryGetByName(string name, out Label label) {
			string CheckModule = this.CurrentModule;
			bool Found = false;
			while (!((Found = this.Lookup.TryGetValue(ModuleCombine(CheckModule, name).ToLowerInvariant(), out label)) && label != null && label.Created)  && !string.IsNullOrEmpty(CheckModule)) {
				CheckModule = ModuleGetParent(CheckModule);
			}
			return Found;
			
		}


		/// <summary>
		/// Creates a reusable label from a name.
		/// </summary>
		/// <param name="reusable">The name of the reusable label.</param>
		/// <returns>A created reusable label.</returns>
		public Label CreateReusable(string reusable) {

			// Check there is *some* sort of name being passed.
			if (string.IsNullOrEmpty(reusable)) throw new ArgumentException(Strings.ErrorUnspecifiedName);

			// Verify that each character in the name is the same.
			char Class = reusable[0];
			foreach (char c in reusable) {
				if (c != Class) throw new ArgumentException(Strings.ErrorLabelReusableInvalidName);				
			}

			// Grab the dictionary to store the label in.
			Dictionary<int, Dictionary<int, Label>> ReusableDictionary;

			switch (Class) {
				case '-':
					ReusableDictionary = this.ReusableBackwards;
					break;
				case '+':
					ReusableDictionary = this.ReusableForwards;
					break;
				default:
					throw new ArgumentException(string.Format(Strings.ErrorLabelReusableInvalidClass, Class));
			}

			// Add!
			Dictionary<int, Label> Reusables;

			// Try to get the length-based dictionary. If not present, create a new one!
			if (!ReusableDictionary.TryGetValue(reusable.Length, out Reusables)) {
				Reusables = new Dictionary<int, Label>();
				ReusableDictionary.Add(reusable.Length, Reusables);
			}

			// Sanity check;
			if (Reusables.ContainsKey(this.Compiler.CompiledStatements)) throw new InvalidOperationException(Strings.ErrorLabelReusableAlreadyExistsAtPosition);

			// Create, add, and return the new label.
			Label CreatedLabel = this.ImplicitCreationDefault.Clone() as Label;
			Reusables.Add(this.Compiler.CompiledStatements, CreatedLabel);
			this.MaxReusableIndex = Math.Max(this.MaxReusableIndex, this.Compiler.CompiledStatements);
			CreatedLabel.Name = "{" + reusable + "}";
			return CreatedLabel;
		}


		/// <summary>
		/// Create a new <see cref="Label"/> with a particular name.
		/// </summary>
		/// <param name="name">The name of the label to create.</param>
		/// <returns>The newly created label.</returns>
		public Label Create(TokenisedSource.Token name) {
			return Create(name, null);
		}

		/// <summary>
		/// Create a new <see cref="Label"/> with a particular name and type.
		/// </summary>
		/// <param name="name">The name of the label to create.</param>
		/// <param name="type">The data structure of the new label.</param>
		/// <returns>The newly created label.</returns>
		public Label Create(TokenisedSource.Token name, DataStructure type) {
			
			// Get the name and name only.
			string LabelName = name.Data;

			// Get the full label path name:
			string FullName = ModuleCombine(this.CurrentModule, LabelName);

			Label L;
			if (this.TryParse(new TokenisedSource.Token(FullName), out L)) {
				if (L.IsConstant) {
					throw new LabelException(name, string.Format(Strings.ErrorLabelInvalidNameNumber, name.Data));
				} else {
					if (this.Lookup.ContainsKey(FullName.ToLowerInvariant())) {
						throw new LabelException(name, string.Format(Strings.ErrorLabelAlreadyDefined, name.Data));
					}
				}
			}




			// Clone and create;
			Label Result = (Label)this.ImplicitCreationDefault.Clone();
			Result.Name = FullName;
			Result.Token = name;
			Result.DataType = type;

			// Add to dictionary:
			this.Lookup.Add(FullName.ToLowerInvariant(), Result);
			return Result;
		}

		/// <summary>
		/// Add a label to the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		public void Add(Label label) {
			string SearchName = label.Name.ToLowerInvariant();
			if (this.Lookup.ContainsKey(SearchName)) throw new LabelException(label.Token, string.Format(Strings.ErrorLabelAlreadyDefined, label.Name));
			this.Lookup.Add(SearchName, label);
		}


		/// <summary>
		/// Remove a label from the collection.
		/// </summary>
		/// <param name="label">The label to remove.</param>
		public void Remove(Label label) {
			if (label == this.programCounter) throw new InvalidOperationException(Strings.ErrorLabelCannotRemoveProgramCounter);
			if (label == this.outputCounter) throw new InvalidOperationException(Strings.ErrorLabelCannotRemoveOutputCounter);
			this.Lookup.Remove(label.Name.ToLowerInvariant());
		}

		#endregion

		#region Parsing


		/// <summary>
		/// Try and parse a string into a label.
		/// </summary>
		/// <param name="token">The value to parse.</param>
		/// <param name="result">The place to store the resulting label.</param>
		/// <returns>True if the string was parsed successfully, false otherwise.</returns>
		public bool TryParse(TokenisedSource.Token token, out Label result) {

			// Handle $ and @:
			if (token.Data == "$") { result = this.programCounter; return true; }
			if (token.Data == "@") { result = this.outputCounter; return true; }

			double ParsedValue = double.NaN;


			string Value = token.Data.ToLowerInvariant();

			result = null;

			if (string.IsNullOrEmpty(Value)) return false;

			// Handle reusable labels:
			{
				string ReusableName = token.Data;
				if (ReusableName[0] == '{' && ReusableName[ReusableName.Length - 1] == '}') {
					ReusableName = ReusableName.Substring(1, ReusableName.Length - 2);
				}

				char ReusableClass = ReusableName[0];
				bool IsReusable = ReusableClass == '+' || ReusableClass == '-';

				if (IsReusable) {

					foreach (char c in ReusableName) {
						if (c != ReusableClass) {
							IsReusable = false;
							break;
						}
					}

					if (IsReusable) {

						Dictionary<int, Dictionary<int, Label>> ReusableClassDictionary;

						int Step;
						int Start = compiler.CompiledStatements;
						int End;

						switch (ReusableClass) {
							case '+':
								ReusableClassDictionary = this.ReusableForwards;
								Step = +1;
								End = this.MaxReusableIndex + 1;
								break;
							case '-':
								ReusableClassDictionary = this.ReusableBackwards;
								Step = -1;
								End = -1;
								break;
							default:
								throw new Exception(); // ?!
						}

						// Check that we're going to move the correct way through the source.
						if (Math.Sign(End - Start) != Step) return false;

						Dictionary<int, Label> ReusableOffsetDictionary;
						if (!ReusableClassDictionary.TryGetValue(ReusableName.Length, out ReusableOffsetDictionary)) {
							return false;
						} else {
							for (int i = Start; i != End; i += Step) {
								if (ReusableOffsetDictionary.TryGetValue(i, out result)) return true;
							}
							return false;
						}
					}
				}
			}


			// Handle string constants:
			if (token.Type == TokenisedSource.Token.TokenTypes.String) {
				byte[] Data = compiler.StringEncoder.GetData(token.GetStringConstant(true));
				ParsedValue = 0;
				for (int i = Data.Length - 1; i >= 0; i--) {
					ParsedValue *= 256;
					ParsedValue += (int)(Data[i]);
				}
			} else {

				// Search for labels:
				/*foreach (string AttemptLookup in this.ResolveLabelName(token.Data, out AccessesPage)) {
					if (this.Lookup.TryGetValue(AttemptLookup.ToLowerInvariant(), out result)) {
						result.AccessingPage = AccessesPage;
						return true;
					}
				}*/


				if (this.TryGetByName(token.Data, out result)) {
					return true;
				}



				char Prefix = Value[0];
				char Suffix = Value[Value.Length - 1];

				string Trimmed = "";
				int FromBase = 0;

				bool HasSuffixOrPrefix = Value.Length > 1;

				if (HasSuffixOrPrefix) {
					if (Prefix == '$') {
						Trimmed = Value.Substring(1);
						FromBase = 16;
					} else if (Suffix == 'h') {
						Trimmed = Value.Remove(Value.Length - 1);
						FromBase = 16;
					} else if (Prefix == '%') {
						Trimmed = Value.Substring(1);
						FromBase = 2;
					} else if (Suffix == 'b') {
						Trimmed = Value.Remove(Value.Length - 1);
						FromBase = 2;
						/*} else if (Prefix == '0') {
							Trimmed = Value.Substring(1);
							FromBase = 8;
						*/
					} else if (Suffix == 'o') {
						Trimmed = Value.Remove(Value.Length - 1);
						FromBase = 8;
					} else {
						if (!double.TryParse(Value, out ParsedValue)) return false;
					}
				} else {
					if (!double.TryParse(Value, out ParsedValue)) return false;
				}

				if (FromBase != 0) {
					int ParsedFromBaseResult;
					if (!TryParseBase(Trimmed, out ParsedFromBaseResult, FromBase)) return false;
					ParsedValue = ParsedFromBaseResult;
				}
			}

			result = new Label(this, token, false, 0, 0, null);
			result.NumericValue = ParsedValue;
			if (token.Type == TokenisedSource.Token.TokenTypes.String) result.IsString = true;
			result.IsConstant = true;
			return true;
		}


		/// <summary>
		/// Parse a string into a label.
		/// </summary>
		/// <param name="value">The value to parse.</param>
		/// <returns>The label.</returns>
		public Label Parse(TokenisedSource.Token value) {
			Label Result;
			if (this.TryParse(value, out Result)) return Result;
			throw new ParseErrorException(value, string.Format(Strings.ErrorLabelCannotParse, value.Data));
		}


		/// <summary>
		/// Try and parse a value from a base.
		/// </summary>
		/// <param name="input">The value to parse.</param>
		/// <param name="result">The place to store the result.</param>
		/// <param name="fromBase">The base to convert from.</param>
		/// <returns>True if the value could be converted, false otherwise.</returns>
		internal static bool TryParseBase(string input, out int result, int fromBase) {

			char[] Chars = input.ToLowerInvariant().ToCharArray();

			result = 0;
			int Shift = 1;
			switch (fromBase) {
				case 2:
					Shift = 1;
					break;
				case 8:
					Shift = 3;
					break;
				case 16:
					Shift = 4;
					break;
				default:
					throw new InvalidOperationException(Strings.ErrorBaseMustByTwoEightSixteen);
			}

			string BaseChars = "0123456789abcdef";
			foreach (char c in Chars) {
				result <<= Shift;
				int Value = BaseChars.IndexOf(c);
				if (Value < 0 || Value >= fromBase) return false;
				result += Value;
			}
			return true;

		}


		/*private string CombineWithModule(string name) {
			return ModuleCombine(this.currentModule, name);
		}*/

		#endregion

		#region Constructor

		/// <summary>
		/// Creates an instance of the <see cref="LabelCollection"/> class.
		/// </summary>
		/// <param name="compiler">The compiler that contains this collection.</param>
		public LabelCollection(Compiler compiler) {
			this.compiler = compiler;
			this.Lookup = new Dictionary<string, Label>(1024);
			this.ReusableForwards = new Dictionary<int, Dictionary<int, Label>>(8);
			this.ReusableBackwards = new Dictionary<int, Dictionary<int, Label>>(8);
			this.programCounter = new Label(this, new TokenisedSource.Token(null, TokenisedSource.Token.TokenTypes.Label, "$", 0), false, 0d, 0, null);
			this.Lookup.Add("$", this.programCounter);
			this.outputCounter = new Label(this, new TokenisedSource.Token(null, TokenisedSource.Token.TokenTypes.Label, "@", 0), false, 0d, 0, null);
			this.Lookup.Add("@", this.programCounter);
		}

		#endregion

		#region IEnumerable<Label> Members

		/// <summary>
		/// Gets an enumerator for the collection.
		/// </summary>
		public IEnumerator<Label> GetEnumerator() {
			return this.Lookup.Values.GetEnumerator();
		}

		/// <summary>
		/// Gets an enumerator for the collection.
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.Lookup.Values.GetEnumerator();
		}

		#endregion

		/// <summary>
		/// Enter a particular module.
		/// </summary>
		/// <param name="module">Name of the module to enter.</param>
		public void EnterModule(string module) {
			this.CurrentModule = ModuleCombine(this.CurrentModule, module);
		}

		/// <summary>
		/// Leave the current module.
		/// </summary>
		public void LeaveModule() {
			this.CurrentModule = ModuleGetParent(this.CurrentModule);
		}

		/// <summary>
		/// Combine the names of two modules.
		/// </summary>
		/// <param name="module1">The first module name.</param>
		/// <param name="module2">The second module name.</param>
		/// <returns>The combined path, taking into consideration module path rules.</returns>
		public static string ModuleCombine(string module1, string module2) {

			string[] ModuleComponents = ((string)(((string.IsNullOrEmpty(module1)) ? "" : (module1 + ".")) + module2)).Split('.');

			List<string> Result = new List<string>();

			for (int i = 0; i < ModuleComponents.Length; ++i) {
				if (ModuleComponents[i].ToLowerInvariant() == "parent") {
					Result.RemoveAt(Result.Count - 1);
				} else if (ModuleComponents[i].ToLowerInvariant() == "global") {
					Result.Clear();
				} else {
					Result.Add(ModuleComponents[i]);
				}
			}

			return string.Join(".", Result.ToArray());
		}

		/// <summary>
		/// Gets the name of a module's parent.
		/// </summary>
		/// <param name="path">The module path to get the parent of.</param>
		public static string ModuleGetParent(string path) {
			string[] ModulePath = path.Split('.');
			if (ModulePath.Length == 0) throw new ArgumentException(Strings.ErrorModuleAlreadyAtTopLevel);
			Array.Resize<string>(ref ModulePath, ModulePath.Length - 1);
			return string.Join(".", ModulePath);
		}

		/// <summary>
		/// Gets the name of a module.
		/// </summary>
		/// <param name="path">The module path to get the name of.</param>
		/// <returns>The name of the module with no path information.</returns>
		public static string ModuleGetName(string path) {
			string[] ModulePath = path.Split('.');
			return ModulePath[path.Length - 1];
		}

		/// <summary>
		/// Resolves the full path of a label's name.
		/// </summary>
		/// <param name="name">The relative name of the label to get a full path of.</param>
		/// <returns>The full path of the label.</returns>
		public string ModuleGetFullLabelPath(string name) {
			if (this.Lookup.ContainsKey(name.ToLower())) return name;
			Label L;
			if (this.TryGetByName(name, out L)) return L.Name;
			return ModuleCombine(this.CurrentModule, name);
		}

		/// <summary>
		/// Returns true if a label is in the current module.
		/// </summary>
		/// <param name="label">The label to check.</param>
		/// <returns>True if the label is in the current module, false otherwise.</returns>
		public bool LabelIsInCurrentModule(Label label) {
			if (label.Name.IndexOf('.') == -1) {
				return string.IsNullOrEmpty(this.currentModule);
			} else {
				return ModuleGetParent(label.Name).ToLowerInvariant() == this.currentModule.ToLowerInvariant();
			}
		}

	}
}
