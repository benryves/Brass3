using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3.Plugins;

namespace Brass3 {

	/// <summary>
	/// Defines a collection of directives.
	/// </summary>
	public class NamedPluginCollection<T> : ICollection<T> where T:IPlugin {

		protected Dictionary<string, T> Plugins;
		protected List<T> UniquePlugins;

		public NamedPluginCollection() {
			this.Plugins = new Dictionary<string, T>();
			this.UniquePlugins = new List<T>();
		}

		public T this[string name] {
			get {
				return this.Plugins[name.ToLowerInvariant()];
			}
		}

		public virtual void Add(T plugin) {
			if (plugin.Name == null) return;
			string Name = plugin.Name.ToLowerInvariant();
			if (this.Plugins.ContainsKey(Name)) throw new InvalidOperationException("Plugin " + plugin.Name + " already loaded.");
			this.Plugins.Add(Name, plugin);
			UniquePlugins.Add(plugin);
		}

		public bool PluginExists(string name) {
			return this.Plugins.ContainsKey(name.ToLowerInvariant());
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
	}
}
