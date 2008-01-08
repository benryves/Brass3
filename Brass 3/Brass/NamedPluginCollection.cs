using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BeeDevelopment.Brass3.Plugins;

namespace BeeDevelopment.Brass3 {

	/// <summary>
	/// Defines a collection of directives.
	/// </summary>
	public class PluginCollection<T> : ICollection<T> where T : class, IPlugin {

		private Dictionary<string, T> Plugins;
		private Dictionary<string, T> RuntimeAliases;
		private List<T> UniquePlugins;

		private readonly Compiler Compiler;

		/// <summary>
		/// Creates an instance of a plugin collection.
		/// </summary>
		/// <param name="compiler">The compiler that contains the collection.</param>
		public PluginCollection(Compiler compiler) {
			this.Compiler = compiler;
			this.Plugins = new Dictionary<string, T>();
			this.RuntimeAliases = new Dictionary<string, T>();
			this.UniquePlugins = new List<T>();
		}

		/// <summary>
		/// Gets a plugin by its name.
		/// </summary>
		/// <param name="name">The name of the plugin to retrieve.</param>
		public virtual T this[string name] {
			get {
				name = name.ToLowerInvariant();

				T Result;
				if (this.Plugins.TryGetValue(name, out Result)) return Result;
				if (this.RuntimeAliases.TryGetValue(name, out Result)) return Result;
				if (typeof(T) == typeof(IStringEncoder)) {
					return new StringEncodingWrapper(this.Compiler, name, Encoding.GetEncoding(name)) as T;
				} else {
					throw new InvalidOperationException(string.Format(Strings.ErrorPluginNotFound, name));
				}
			}
		}

		/// <summary>
		/// Adds a plugin to the collection.
		/// </summary>
		/// <param name="plugin">The plugin to add.</param>
		public virtual void Add(T plugin) {			
			foreach (string Name in Compiler.GetPluginNames(plugin)) {
				if (this.Plugins.ContainsKey(Name)) {
					this.Plugins.Remove(Name);
					this.Compiler.OnWarningRaised(new Compiler.NotificationEventArgs(this.Compiler, string.Format(Strings.WarningPluginLoadedTwice, Name)));
				}
				this.Plugins.Add(Name, plugin);
			}
			UniquePlugins.Add(plugin);
		}

		/// <summary>
		/// Check whether a plugin exists by name.
		/// </summary>
		/// <param name="name">The name of the plugin to check.</param>
		/// <returns>True if the plugin exists, false otherwise.</returns>
		public bool Contains(string name) {
			if (string.IsNullOrEmpty(name)) return false;
			name = name.ToLowerInvariant();
			return this.Plugins.ContainsKey(name) || this.RuntimeAliases.ContainsKey(name);
		}

		/// <summary>
		/// Try and get a plugin by its name.
		/// </summary>
		/// <param name="name">The name of the plugin to retrieve.</param>
		/// <param name="plugin">If found, an instance of the requested plugin.</param>
		/// <returns>True if the plugin was found, false otherwise.</returns>
		public bool TryGetPlugin(string name, out T plugin) {
			if (string.IsNullOrEmpty(name)) {
				plugin = default(T);
				return false;
			}
			return this.Plugins.TryGetValue(name.ToLowerInvariant(), out plugin);
		}

		/// <summary>
		/// Try and get a plugin by its name.
		/// </summary>
		/// <param name="name">The name of the plugin to retrieve.</param>
		/// <param name="plugin">If found, an instance of the requested plugin.</param>
		/// <returns>True if the plugin was found, false otherwise.</returns>
		public bool TryGetPlugin(string name, out IPlugin plugin) {

			plugin = default(IPlugin);

			if (string.IsNullOrEmpty(name)) return false;

			T Plugin;
			if (!this.Plugins.TryGetValue(name.ToLowerInvariant(), out Plugin)) return false;

			plugin = (IPlugin)Plugin;

			return true;
		}

		/// <summary>
		/// Gets an enumerator for the collection.
		/// </summary>
		public IEnumerator<T> GetEnumerator() {
			return this.UniquePlugins.GetEnumerator();
		}

		/// <summary>
		/// Gets an enumerator for the collection.
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.UniquePlugins.GetEnumerator();
		}


		/// <summary>
		/// Remove all plugins from the collection.
		/// </summary>
		public void Clear() {
			this.Plugins.Clear();
			this.UniquePlugins.Clear();
		}

		/// <summary>
		/// Check if a plugin exists within the collection.
		/// </summary>
		/// <param name="item">The plugin to search for.</param>
		/// <returns>True if the collection contains the plugin, false otherwise.</returns>
		public bool Contains(T item) {
			return this.UniquePlugins.Contains(item);
		}

		/// <summary>
		/// Copy the plugins to an array.
		/// </summary>
		/// <param name="array">The array to copy the plugins to.</param>
		/// <param name="arrayIndex">The index to start copying the plugins to.</param>
		public void CopyTo(T[] array, int arrayIndex) {
			this.UniquePlugins.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Gets the number of unique plugins inside the collection.
		/// </summary>
		public int Count {
			get { return this.UniquePlugins.Count; }
		}

		/// <summary>
		/// Returns false.
		/// </summary>
		public bool IsReadOnly {
			get { return false; }
		}

		/// <summary>
		/// Remove a plugin from the collection.
		/// </summary>
		/// <param name="item">The plugin to remove.</param>
		/// <returns>True if the plugin was removed, false otherwise.</returns>
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

		/// <summary>
		/// Clear all runtime aliases.
		/// </summary>
		public void ClearRuntimeAliases() {
			RuntimeAliases.Clear();
		}

		/// <summary>
		/// Adds a runtime alias.
		/// </summary>
		/// <param name="plugin">The plugin to add an alias for.</param>
		/// <param name="name">The aliased name.</param>
		/// <remarks>An alias can be used to reference a single plugin by multiple names.</remarks>
		public void AddRuntimeAlias(T plugin, string name) {
			name = name.ToLowerInvariant();
			if (this.Plugins.ContainsKey(name) || this.RuntimeAliases.ContainsKey(name)) throw new InvalidOperationException(string.Format(Strings.ErrorPluginAlreadyAliased, name));
			RuntimeAliases.Add(name, plugin);
		}
	}
}