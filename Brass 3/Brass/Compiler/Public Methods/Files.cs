using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Brass3.Plugins;

namespace Brass3 {
	public partial class Compiler {


		/// <summary>
		/// Gets an array of all of the files that were assembled.
		/// </summary>
		/// <param name="relative">True to return relative filenames, false to return full filenames.</param>
		public string[] GetAllFilenames(bool relative) {
			List<string> Filenames = new List<string>();
			foreach (SourceStatement S in this.statements) {
				if (!Filenames.Contains(S.SourceFile)) Filenames.Add(S.SourceFile);
			}
			Filenames.Sort();
			if (relative) {
				return Array.ConvertAll<string, string>(Filenames.ToArray(), delegate(string s) { return this.GetRelativeFilename(s); });
			} else {
				return Filenames.ToArray();
			}
		}

		/// <summary>
		/// Gets an array of all of the directories referenced in an array of filenames.
		/// </summary>
		/// <param name="filenames">Array of filenames to search through.</param>
		public string[] GetDirectories(string[] filenames) {
			List<string> Directories = new List<string>();
			foreach (string S in filenames) {
				string Dir = Path.GetDirectoryName(S);
				if (!Directories.Contains(Dir)) Directories.Add(Dir);
			}
			Directories.Sort();
			return Directories.ToArray();
		}


		public class SourceTreeEntry : IComparable {

			private readonly string name;
			public string Name {
				get { return this.name; }
			}

			private readonly bool isDirectory;
			public bool IsDirectory {
				get { return this.isDirectory; }
			}

			public SourceTreeEntry[] Children {
				get {
					List<SourceTreeEntry> Result = new List<SourceTreeEntry>(this.children.Count);
					foreach (KeyValuePair<string, SourceTreeEntry> Child in this.children) Result.Add(Child.Value);
					Result.Sort();
					return Result.ToArray();
				}
			}
						
			internal Dictionary<string, SourceTreeEntry> children;

			internal SourceTreeEntry GetOrCreate(string name, bool isDirectory) {
				SourceTreeEntry Result;
				if (!this.children.TryGetValue(name, out Result)) {
					Result = new SourceTreeEntry(name, isDirectory);
					this.children.Add(name, Result);
				}
				return Result;
			}
			
			public SourceTreeEntry(string name, bool isDirectory) {
				this.name = name;
				this.isDirectory = isDirectory;
				this.children = new Dictionary<string, SourceTreeEntry>();
			}

			public override string ToString() {
				return this.Name + (this.IsDirectory ? Path.DirectorySeparatorChar.ToString() : "");
			}

			public int CompareTo(object obj) {
				SourceTreeEntry that = obj as SourceTreeEntry;
				if (this.IsDirectory != that.IsDirectory) {
					return this.IsDirectory ? -1 : +1;
				} else {
					return this.Name.CompareTo(that.Name);
				}
			}

		}

		public SourceTreeEntry GetSourceTree(bool relative) {

			SourceTreeEntry Root = new SourceTreeEntry(null, true);

			Dictionary<string, SourceTreeEntry> DirectoryEntries = new Dictionary<string,SourceTreeEntry>();

			foreach (string File in this.GetAllFilenames(relative)) {
				SourceTreeEntry Current = Root;
				string[] Components = GetPathComponents(File);
				for (int i = 0; i < Components.Length; ++i) {
					Current = Current.GetOrCreate(Components[i], i != Components.Length - 1);
				}
			}
			return Root;
		}


		private string[] GetPathComponents(string path) {
			List<string> Result = new List<string>();
			string Filename;
			while (!string.IsNullOrEmpty(Filename = Path.GetFileName(path))) {
				Result.Add(Filename);
				path = Path.GetDirectoryName(path);
			}
			Result.Reverse();
			return Result.ToArray();
		}


		public string GetRelativeFilename(string filename) {
			if (this.project == null) {
				return Project.GetRelativeFilename(this.sourceFile, filename);
			} else {
				return Project.GetRelativeFilename(this.project.ProjectFilename, filename);
			}
		}


	}
}
