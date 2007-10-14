using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {

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
		public void LoadPluginsFromAssembly(string assemblyName, string[] exclusions) {

			List<string> ExclusionList = new List<string>(
				Array.ConvertAll<string, string>(exclusions, delegate(string a) { return a.ToLowerInvariant(); })
			);

			Assembly PluginAssembly = Assembly.LoadFile(Path.GetFullPath(assemblyName));

			Type[] Plugins = PluginAssembly.GetExportedTypes();

			foreach (Type T in Plugins) {
				if (T.IsClass) {

					ConstructorInfo Constructor = null;
					object[] ConstructorParameters = null;
					bool IsAliased = false;

					bool IsAssembler = false;
					bool IsDirective = false;
					bool IsFunction = false;
					bool IsOutputWriter = false;
					bool IsOutputModifier = false;
					bool IsStringEncoder = false;
					bool IsListingWriter = false;
					bool IsNumberEncoding = false;

					foreach (Type I in T.GetInterfaces()) {
						if (I == typeof(IPlugin)) {
							Constructor = T.GetConstructor(new Type[] { typeof(Compiler) });
							ConstructorParameters = new object[] { this };
							if (Constructor == null) {
								Constructor = T.GetConstructor(Type.EmptyTypes);
								ConstructorParameters = null;
							}
						} else if (I == typeof(IAssembler)) {
							IsAssembler = true;
						} else if (I == typeof(IDirective)) {
							IsDirective = true;
						} else if (I == typeof(IFunction)) {
							IsFunction = true;
						} else if (I == typeof(IOutputWriter)) {
							IsOutputWriter = true;
						} else if (I == typeof(IOutputModifier)) {
							IsOutputModifier = true;
						} else if (I == typeof(IStringEncoder)) {
							IsStringEncoder = true;
						} else if (I == typeof(IListingWriter)) {
							IsListingWriter = true;
						} else if (I == typeof(INumberEncoder)) {
							IsNumberEncoding = true;
						} else if (I == typeof(IAliasedPlugin)) {
							IsAliased = true;
						}
					}

					if (Constructor != null) {

						// Create an instance of the plugin:
						object Plugin = Constructor.Invoke(ConstructorParameters);

						// Check if we can use it or if it's been blacklisted:
						if (IsAliased) {
							bool IsExcluded = false;
							foreach (string AliasedName in (Plugin as IAliasedPlugin).Names) {
								if (AliasedName == null) continue;
								if (ExclusionList.Contains(AliasedName.ToLowerInvariant())) {
									IsExcluded = true;
									break;
								}
							}
							if (IsExcluded) continue;
						} else {
							if (ExclusionList.Contains((Plugin as IPlugin).Name.ToLowerInvariant())) continue;
						}

						bool HasCategory = false;
						if (IsAssembler) { HasCategory = true; this.assemblers.Add((IAssembler)Plugin); }
						if (IsDirective) { HasCategory = true; this.directives.Add((IDirective)Plugin); }
						if (IsFunction) { HasCategory = true; this.functions.Add((IFunction)Plugin); }
						if (IsOutputWriter) { HasCategory = true; this.outputWriters.Add((IOutputWriter)Plugin); }
						if (IsOutputModifier) { HasCategory = true; this.outputModifiers.Add((IOutputModifier)Plugin); }
						if (IsStringEncoder) { HasCategory = true; this.StringEncoders.Add((IStringEncoder)Plugin); }
						if (IsListingWriter) { HasCategory = true; this.ListingWriters.Add((IListingWriter)Plugin); }
						if (IsNumberEncoding) { HasCategory = true; this.NumberEncoders.Add((INumberEncoder)Plugin); }

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
			return null;
		}

		public T GetPluginInstanceFromType<T>() where T : class {
			return GetPluginInstanceFromType(typeof(T)) as T;
		}

		/// <summary>
		/// Get a plugin instance from a GUID.
		/// </summary>
		/// <param name="type">The GUID of the plugin to retrieve.</param>
		/// <returns>An instance of the plugin, or null if one wasn't found matching the type.</returns>
		public IPlugin GetPluginInstanceFromGuid(Guid guid) {
			foreach (IDirective Plugin in this.directives) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IFunction Plugin in this.functions) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IOutputModifier Plugin in this.outputModifiers) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IOutputWriter Plugin in this.outputWriters) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IStringEncoder Plugin in this.stringEncoders) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (IListingWriter Plugin in this.listingWriters) if (Plugin.GetType().GUID == guid) return Plugin;
			foreach (INumberEncoder Plugin in this.numberEncoders) if (Plugin.GetType().GUID == guid) return Plugin;
			return null;
		}


	}
}
