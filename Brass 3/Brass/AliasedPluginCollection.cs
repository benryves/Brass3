using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3.Plugins;

namespace Brass3 {

	/// <summary>
	/// Defines a collection of directives.
	/// </summary>
	public class AliasedPluginCollection<T> : NamedPluginCollection<T> where T : IAliasedPlugin {

		public AliasedPluginCollection()
			: base() {
		}

		public override void Add(T plugin) {
			bool HasValidName = false;
			foreach (string name in plugin.Names) {
				if (name == null) continue;
				HasValidName = true;
				string Name = name.ToLowerInvariant();
				if (this.Plugins.ContainsKey(Name)) throw new InvalidOperationException("Plugin '" + plugin.Name + "' already loaded.");
				this.Plugins.Add(Name, plugin);
			}
			if (HasValidName) UniquePlugins.Add(plugin);
		}

		private List<string> RuntimeAliases = new List<string>();

		public void AddRuntimeAlias(T plugin, string alias) {
			string Name = alias.ToLowerInvariant();
			if (this.Plugins.ContainsKey(Name)) throw new InvalidOperationException("Plugin '" + plugin.Name + "' already loaded.");
			this.Plugins.Add(Name, plugin);
			RuntimeAliases.Add(Name);
		}

		internal void ClearRuntimeAliases() {
			foreach (string Name in RuntimeAliases) {
				this.Plugins.Remove(Name);				
			}
		}


	}
}
