using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3.Plugins;

namespace Brass3 {

	/// <summary>
	/// Defines a collection of directives.
	/// </summary>
	public class NamedPluginCollection<T> : ICollection<T> where T : class, IPlugin {

		protected Dictionary<string, T> Plugins;
		protected Dictionary<string, T> RuntimeAliases;
		protected List<T> UniquePlugins;

		private readonly Compiler Compiler;

		public NamedPluginCollection(Compiler compiler) {
			this.Compiler = compiler;
			this.Plugins = new Dictionary<string, T>();
			this.RuntimeAliases = new Dictionary<string, T>();
			this.UniquePlugins = new List<T>();
		}

		public virtual T this[string name] {
			get {
				name = name.ToLowerInvariant();

				T Result;
				if (this.Plugins.TryGetValue(name, out Result)) return Result;
				if (this.RuntimeAliases.TryGetValue(name, out Result)) return Result;
				if (typeof(T) == typeof(IStringEncoder)) {
					return new StringEncodingWrapper(this.Compiler, name, Encoding.GetEncoding(name)) as T;
				} else {
					throw new InvalidOperationException("Handler for '" + name + "' not found.");
				}
			}
		}

		public virtual void Add(T plugin) {			
			foreach (string Name in Compiler.GetPluginNames(plugin)) {
				if (this.Plugins.ContainsKey(Name)) throw new InvalidOperationException("Plugin " + Name + " already loaded.");
				this.Plugins.Add(Name, plugin);
			}
			UniquePlugins.Add(plugin);
		}

		public bool PluginExists(string name) {
			name = name.ToLowerInvariant();
			return this.Plugins.ContainsKey(name) || this.RuntimeAliases.ContainsKey(name);
		}

		public IEnumerator<T> GetEnumerator() {
			return this.UniquePlugins.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.UniquePlugins.GetEnumerator();
		}


		public void Clear() {
			this.Plugins.Clear();
			this.UniquePlugins.Clear();
		}

		public bool Contains(T item) {
			return this.UniquePlugins.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex) {
			this.UniquePlugins.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return this.UniquePlugins.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(T item) {
			List<string> ToRemove = new List<string>();
			foreach (KeyValuePair<string, T> Remove in this.Plugins) {
				if (Remove.Value.Equals(item)) {
					ToRemove.Add(Remove.Key);
				}
			}
			foreach (string Remove in ToRemove) {
				this.Plugins.Remove(Remove);
			}
			return this.UniquePlugins.Remove(item);

		}

		public void ClearRuntimeAliases() {
			RuntimeAliases.Clear();
		}

		public void AddRuntimeAlias(T function, string name) {
			name = name.ToLowerInvariant();
			if (this.Plugins.ContainsKey(name) || this.RuntimeAliases.ContainsKey(name)) throw new InvalidOperationException("Plugin '" + name + "' already loaded.");
			RuntimeAliases.Add(name, function);
		}
	}
}