using System;
using System.Collections.Generic;
using System.Text;

namespace Brass3 {

	/// <summary>
	/// Defines a collection of labels.
	/// </summary>
	public class LabelCollection : IEnumerable<Label> {
		
		private Dictionary<string, Label> Lookup = new Dictionary<string,Label>();

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

		public void EnterTemporaryModule() {
			int ModuleSuffix = (ushort)DateTime.Now.Ticks;
			this.EnterModule(string.Format("TEMP_MODULE_#{0:X4}", ModuleSuffix));
			
		}
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

			return TemporaryLabels.ToArray();
		}

		private Label implicitCreationDefault;
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
				throw new InvalidOperationException();
			}
		}


		private bool TryGetByName(string name, out Label label) {
			string CheckModule = this.CurrentModule;
			bool Found = false;
			while (!(Found = this.Lookup.TryGetValue(ModuleCombine(CheckModule, name).ToLowerInvariant(), out label)) && !string.IsNullOrEmpty(CheckModule)) {
				CheckModule = ModuleGetParent(CheckModule);
			}
			return Found;
			
		}


		public Label Create(TokenisedSource.Token name) {
			return Create(name, null);
		}

		public Label Create(TokenisedSource.Token name, DataStructure type) {
			
			// Get the name and name only.
			bool AccessesPage;
			string LabelName = Label.GetNameWithoutColon(name.Data, out AccessesPage);

			Label L;
			if (this.TryParse(name, out L)) {
				if (L.IsConstant) {
					throw new LabelExpection(name, "Invalid name '" + name.Data + "' (looks like a number).");
				} else {
					if (this.Lookup.ContainsKey(name.DataLowerCase)) {
						throw new LabelExpection(name, "Label '" + name.Data + "' already defined.");
					}
				}
			}

			// Get the full label path name:
			string FullName = ModuleCombine(this.CurrentModule, LabelName);


			// Clone and create;
			Label Result = (Label)this.ImplicitCreationDefault.Clone();
			Result.Name = FullName;
			Result.Token = name;
			Result.AccessingPage = AccessesPage;
			Result.Type = type;

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
			if (this.Lookup.ContainsKey(SearchName)) throw new LabelExpection(label.Token, "Label '" + label.Name + "' already created.");
			this.Lookup.Add(SearchName, label);
		}


		public void Remove(Label L) {
			if (L == this.programCounter) throw new InvalidOperationException("You cannot remove the predefined program counter label.");
			this.Lookup.Remove(L.Name.ToLowerInvariant());
		}

		#endregion

		#region Parsing

		/// <summary>
		/// Try and parse a string into a label.
		/// </summary>
		/// <param name="value">The value to parse.</param>
		/// <param name="result">The place to store the resulting label.</param>
		/// <returns>True if the string was parsed successfully, false otherwise.</returns>
		public bool TryParse(TokenisedSource.Token token, out Label result) {


			double ParsedValue = double.NaN;


			string Value = token.Data.ToLowerInvariant();

			result = null;

			if (string.IsNullOrEmpty(Value)) return false;

			// Handle string constants:
			if (token.Type == TokenisedSource.Token.TokenTypes.String) {
				byte[] Data = compiler.StringEncoder.GetData(token.GetStringConstant(true));
				ParsedValue = 0;
				for (int i = Data.Length - 1; i >= 0; i--) {
					ParsedValue *= 256; 
					ParsedValue += (int)(Data[i]);
				}
			} else {

				bool AccessesPage;

				// Search for labels:
				/*foreach (string AttemptLookup in this.ResolveLabelName(token.Data, out AccessesPage)) {
					if (this.Lookup.TryGetValue(AttemptLookup.ToLowerInvariant(), out result)) {
						result.AccessingPage = AccessesPage;
						return true;
					}
				}*/

				foreach (string AttemptLookup in this.ResolveLabelName(token.Data, out AccessesPage)) {
					if (this.TryGetByName(AttemptLookup, out result)) {
						result.AccessingPage = AccessesPage;
						return true;
					}
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
					*/} else if (Suffix == 'o') {
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
		/// Gets an array of strings of the resolved label name.
		/// </summary>
		/// <param name="source">The source name. This can be any suspected label name, including colon prefix/suffix and module access.</param>
		/// <param name="accessesPage">Outputs true if you're trying at access the page of a label rather than its value.</param>
		public string[] ResolveLabelName(string source, out bool accessesPage) {
			List<string> Result = new List<string>();

			string OriginalName = Label.GetNameWithoutColon(source, out accessesPage);
			Result.Add(ModuleCombine(this.CurrentModule, OriginalName));
			Result.Add(OriginalName);

			return Result.ToArray();
		}

		/// <summary>
		/// Parse a string into a label.
		/// </summary>
		/// <param name="value">The value to parse.</param>
		/// <returns>The label.</returns>
		public Label Parse(TokenisedSource.Token value) {
			Label Result;
			if (this.TryParse(value, out Result)) return Result;
			throw new ParseErrorExpection(value, "Could not parse '" + value.Data + "'.");
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
					throw new InvalidOperationException("Must be base 2, 8 or 16.");
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

		public LabelCollection(Compiler compiler) {
			this.compiler = compiler;
			this.Lookup = new Dictionary<string, Label>(1024);
			this.programCounter = new Label(this, new TokenisedSource.Token(null, TokenisedSource.Token.TokenTypes.Label, "$", 0), false, 0d, 0, null);
			this.Lookup.Add("$", this.programCounter);
			this.outputCounter = new Label(this, new TokenisedSource.Token(null, TokenisedSource.Token.TokenTypes.Label, "@", 0), false, 0d, 0, null);
			this.Lookup.Add("@", this.programCounter);
		}

		#endregion

		#region IEnumerable<Label> Members

		public IEnumerator<Label> GetEnumerator() {
			return this.Lookup.Values.GetEnumerator();
		}

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

		public static string ModuleCombine(string module1, string module2) {
			return ((string.IsNullOrEmpty(module1)) ? "" : (module1 + ".")) + module2;
		}

		public static string ModuleGetParent(string path) {
			string[] ModulePath = path.Split('.');
			if (ModulePath.Length == 0) throw new ArgumentException("Already at the top module level.");
			Array.Resize<string>(ref ModulePath, ModulePath.Length - 1);
			return string.Join(".", ModulePath);
		}

		public static string ModuleGetName(string path) {
			string[] ModulePath = path.Split('.');
			return ModulePath[path.Length - 1];
		}

		public string ModuleGetFullPath(string name) {
			if (this.Lookup.ContainsKey(name.ToLower())) return name;
			Label L;
			if (this.TryGetByName(name, out L)) return L.Name;
			return ModuleCombine(this.CurrentModule, name);
		}

	}
}
