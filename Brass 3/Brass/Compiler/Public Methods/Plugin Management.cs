using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using BeeDevelopment.Brass3.Plugins;

namespace BeeDevelopment.Brass3 {
	public partial class Compiler {

		/// <summary>
		/// Gets all of the names for a plugin.
		/// </summary>
		/// <param name="plugin">The plugin to get the names of.</param>
		/// <returns>An array of the plugin's names.</returns>
		public static string[] GetPluginNames(Type plugin) {
			string[] Result = Array.ConvertAll<object, string>(plugin.GetCustomAttributes(typeof(Attributes.PluginNameAttribute), false), o=>(o as Attributes.PluginNameAttribute).Name.ToLowerInvariant());
			if (Result.Length == 0) {
				Result = new string[] { plugin.Name.ToLowerInvariant() };
			}
			return Result;
		}

		/// <summary>
		/// Gets all of the names for a plugin.
		/// </summary>
		/// <param name="plugin">The plugin to get the names of.</param>
		/// <returns>An array of the plugin's names.</returns>
		public static string[] GetPluginNames(IPlugin plugin) {
			return Compiler.GetPluginNames(plugin.GetType());
		}

		/// <summary>
		/// Gets the display name for a plugin.
		/// </summary>
		/// <param name="plugin">The plugin to get the display name of.</param>
		/// <returns>The plugin's display name.</returns>
		/// <remarks>If no display name is explicitly defined, the plugin's names are returned delimited by a slash.</remarks>
		public static string GetPluginDisplayName(Type plugin) {

			string[] Result = Array.ConvertAll<object, string>(plugin.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), false), o=>(o as System.ComponentModel.DisplayNameAttribute).DisplayName);

			if (Result.Length == 0) {
				Result = GetPluginNames(plugin);
			}
			return string.Join("/", Result);
		}


		/// <summary>
		/// Gets the display name for a plugin.
		/// </summary>
		/// <param name="plugin">The plugin to get the display name of.</param>
		/// <returns>The plugin's display name.</returns>
		/// <remarks>If no display name is explicitly defined, the plugin's names are returned delimited by a slash.</remarks>
		public static string GetPluginDisplayName(IPlugin plugin) {
			return Compiler.GetPluginDisplayName(plugin.GetType());
		}

		/// <summary>
		/// Gets the single identifying name for a plugin.
		/// </summary>
		/// <param name="plugin">The plugin to get the name of.</param>
		public static string GetPluginName(Type plugin) {
			return Compiler.GetPluginNames(plugin)[0];
		}

		/// <summary>
		/// Gets the single identifying name for a plugin.
		/// </summary>
		/// <param name="plugin">The plugin to get the name of.</param>
		public static string GetPluginName(IPlugin plugin) {
			return Compiler.GetPluginNames(plugin)[0];
		}

		/// <summary>
		/// Load all plugins from an assembly.
		/// </summary>
		/// <param name="assemblyName">The filename of the assembly to load.</param>
		public void LoadPluginsFromAssembly(string assemblyName) {
			this.LoadPluginsFromAssembly(assemblyName, new string[0]);
		}

		/// <summary>
		/// Load plugins from an assembly.
		/// </summary>
		/// <param name="assemblyName">The filename of the assembly to load.</param>
		/// <param name="exclusions">An array of strings corresponding to names of plugins that are not to be loaded.</param>
		public void LoadPluginsFromAssembly(string assemblyName, string[] exclusions) {

			List<string> ExclusionList = new List<string>(Array.ConvertAll<string, string>(exclusions, a => a.ToLowerInvariant()));

			string AssemblyName = Path.GetFullPath(assemblyName);
			if (!File.Exists(AssemblyName)) AssemblyName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.GetFileName(AssemblyName));
			Assembly PluginAssembly = Assembly.LoadFile(AssemblyName);

			Type[] Plugins = PluginAssembly.GetExportedTypes();

			foreach (Type T in Plugins) {
				if (T.IsClass) {

					ConstructorInfo Constructor = null;
					object[] ConstructorParameters = null;

					if (new List<Type>(T.GetInterfaces()).Contains(typeof(IPlugin))) {
						Constructor = T.GetConstructor(new Type[] { typeof(Compiler) });
						ConstructorParameters = new object[] { this };
						if (Constructor == null) {
							Constructor = T.GetConstructor(Type.EmptyTypes);
							ConstructorParameters = null;
						}
					}
					if (Constructor != null) {

						// Check if the plugin has been excluded:
						bool IsExcluded = false;
						foreach (string AliasedName in Compiler.GetPluginNames(T)) {
							if (ExclusionList.Contains(AliasedName)) {
								IsExcluded = true;
								break;
							}
						}
						if (IsExcluded) {
							continue;
						}

						// Create an instance of the plugin:
						object Plugin = Constructor.Invoke(ConstructorParameters);

						// Check if we can use it or if it's been blacklisted:

						
					
						bool HasCategory = false;
						if (Plugin is IAssembler) { HasCategory = true; this.assemblers.Add((IAssembler)Plugin); }
						if (Plugin is IDirective) { HasCategory = true; this.directives.Add((IDirective)Plugin); }
						if (Plugin is IFunction) { HasCategory = true; this.functions.Add((IFunction)Plugin); }
						if (Plugin is IOutputWriter) { HasCategory = true; this.outputWriters.Add((IOutputWriter)Plugin); }
						if (Plugin is IOutputModifier) { HasCategory = true; this.outputModifiers.Add((IOutputModifier)Plugin); }
						if (Plugin is IStringEncoder) { HasCategory = true; this.StringEncoders.Add((IStringEncoder)Plugin); }
						if (Plugin is IListingWriter) { HasCategory = true; this.ListingWriters.Add((IListingWriter)Plugin); }
						if (Plugin is INumberEncoder) { HasCategory = true; this.NumberEncoders.Add((INumberEncoder)Plugin); }
						if (Plugin is IDebugger) { HasCategory = true; this.Debuggers.Add((IDebugger)Plugin); }

						if (!HasCategory) this.InvisiblePlugins.Add((IPlugin)Plugin);

					}

				}
			}
		}


		/// <summary>
		/// Get a plugin instance from a type.
		/// </summary>
		/// <param name="type">The type of the plugin to retrieve.</param>
		/// <returns>An instance of the plugin, or null if one wasn't found matching the type.</returns>
		public IPlugin GetPluginInstanceFromType(Type type) {
			foreach (IDirective Plugin in this.directives) if (Plugin.GetType() == type) return Plugin;
			foreach (IFunction Plugin in this.functions) if (Plugin.GetType() == type) return Plugin;
			foreach (IOutputModifier Plugin in this.outputModifiers) if (Plugin.GetType() == type) return Plugin;
			foreach (IOutputWriter Plugin in this.outputWriters) if (Plugin.GetType() == type) return Plugin;
			foreach (IStringEncoder Plugin in this.stringEncoders) if (Plugin.GetType() == type) return Plugin;
			foreach (IListingWriter Plugin in this.listingWriters) if (Plugin.GetType() == type) return Plugin;
			foreach (INumberEncoder Plugin in this.numberEncoders) if (Plugin.GetType() == type) return Plugin;
			foreach (IAssembler Plugin in this.assemblers) if (Plugin.GetType() == type) return Plugin;
			foreach (IDebugger Plugin in this.debuggers) if (Plugin.GetType() == type) return Plugin;
			return null;
		}

		/// <summary>
		/// Get a plugin instance from its name.
		/// </summary>
		/// <param name="name">The name of the plugin to retrieve.</param>
		/// <returns>An instance of the plugin, or null if one wasn't found matching the name.</returns>
		public IPlugin GetPluginInstanceFromName(string name) {
			IPlugin Plugin;
			if (this.directives.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.functions.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.outputModifiers.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.outputWriters.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.stringEncoders.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.listingWriters.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.numberEncoders.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.assemblers.TryGetPlugin(name, out Plugin)) return Plugin;
			if (this.debuggers.TryGetPlugin(name, out Plugin)) return Plugin;
			
			return null;
		}

		/// <summary>
		/// Get a plugin instance from a type.
		/// </summary>
		/// <typeparam name="T">The type of the plugin to retrieve.</typeparam>
		public T GetPluginInstanceFromType<T>() where T : class, IPlugin {
			return GetPluginInstanceFromType(typeof(T)) as T;
		}

		/// <summary>
		/// Get a plugin instance from a GUID.
		/// </summary>
		/// <param name="guid">The GUID of the plugin to retrieve.</param>
		/// <returns>An instance of the plugin, or null if one wasn't found matching the type.</returns>
		public IPlugin GetPluginInstanceFromGuid(Guid guid) {
			foreach (IDirective Plugin in this.directives) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IFunction Plugin in this.functions) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IOutputModifier Plugin in this.outputModifiers) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IOutputWriter Plugin in this.outputWriters) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IStringEncoder Plugin in this.stringEncoders) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IListingWriter Plugin in this.listingWriters) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (INumberEncoder Plugin in this.numberEncoders) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IAssembler Plugin in this.assemblers) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IDebugger Plugin in this.debuggers) if (Plugin.GetType().GUID == guid) return Plugin;
			return null;
		}


	}
}
