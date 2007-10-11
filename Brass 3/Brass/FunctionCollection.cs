using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3.Plugins;

namespace Brass3 {

	/// <summary>
	/// Defines a collection of functions.
	/// </summary>
	public class FunctionCollection : IEnumerable<IFunction> {

		private Dictionary<string, IFunction> Functions;
		private List<IFunction> UniqueFunctions;

		public FunctionCollection() {
			this.Functions = new Dictionary<string, IFunction>();
			this.UniqueFunctions = new List<IFunction>();
		}

		public IFunction this[string name] {
			get {
				return this.Functions[name.ToLowerInvariant()];
			}
		}

		public void Add(IFunction function) {
			foreach (string name in function.Names) {
				string Name = name.ToLowerInvariant();
				if (this.Functions.ContainsKey(Name)) throw new InvalidOperationException("Function " + Name + "() already loaded.");
				this.Functions.Add(Name, function);	
			}
			UniqueFunctions.Add(function);
		}

		public bool FunctionExists(string name) {
			return this.Functions.ContainsKey(name.ToLowerInvariant());
		}

		public IEnumerator<IFunction> GetEnumerator() {
			return this.UniqueFunctions.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.UniqueFunctions.GetEnumerator();
		}

	}
}
