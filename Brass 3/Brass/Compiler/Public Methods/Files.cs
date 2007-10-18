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
				if (!Filenames.Contains(S.Filename)) Filenames.Add(S.Filename);
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


		/// <summary>
		/// Gets a tree structure of the assembled files and the directories that contain them.
		/// </summary>
		/// <param name="relative">True to return relative paths, false to return absolute paths.</param>
		/// <returns>Returns the root of the source tree.</returns>
		public SourceTreeEntry GetSourceTree(bool relative) {

			SourceTreeEntry Root = new SourceTreeEntry(null, true);

			Dictionary<string, SourceTreeEntry> DirectoryEntries = new Dictionary<string, SourceTreeEntry>();

			foreach (string File in this.GetAllFilenames(relative)) {
				SourceTreeEntry Current = Root;
				string[] Components = GetPathComponents(File);
				for (int i = 0; i < Components.Length; ++i) {
					Current = Current.GetOrCreate(Components[i], i != Components.Length - 1);
				}
			}
			return Root;
		}


		/// <summary>
		/// Defines a file or directory on the source tree.
		/// </summary>
		public class SourceTreeEntry : IComparable {

			private readonly string name;
			/// <summary>
			/// Gets the name of the source tree entry.
			/// </summary>
			public string Name {
				get { return this.name; }
			}

			private readonly bool isDirectory;
			/// <summary>
			/// Returns true if this entry is a directory, false if it's a file.
			/// </summary>
			public bool IsDirectory {
				get { return this.isDirectory; }
			}

			/// <summary>
			/// Gets an array of children underneath the source tree.
			/// </summary>
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
			
			/// <summary>
			/// Creates an instance of SourceTreeEntry.
			/// </summary>
			/// <param name="name">The name of the source tree entry.</param>
			/// <param name="isDirectory">True if it's a directory, false if it's a file.</param>
			public SourceTreeEntry(string name, bool isDirectory) {
				this.name = name;
				this.isDirectory = isDirectory;
				this.children = new Dictionary<string, SourceTreeEntry>();
			}

			/// <summary>
			/// Returns a formatted name representing this entry.
			/// </summary>
			public override string ToString() {
				return this.Name + (this.IsDirectory ? Path.DirectorySeparatorChar.ToString() : "");
			}

			/// <summary>
			/// Compare a source tree entry to another one for sorting purposes.
			/// </summary>
			public int CompareTo(object obj) {
				SourceTreeEntry that = obj as SourceTreeEntry;
				if (this.IsDirectory != that.IsDirectory) {
					return this.IsDirectory ? -1 : +1;
				} else {
					return this.Name.CompareTo(that.Name);
				}
			}

		}



		/// <summary>
		/// Split a path into its component parts.
		/// </summary>
		/// <param name="path">The path to split.</param>
		/// <returns>An array of strings, one for each component of the path.</returns>
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


		/// <summary>
		/// Gets a relative filename to either the project or the source file.
		/// </summary>
		/// <param name="filename">The absolute path to transform.</param>
		public string GetRelativeFilename(string filename) {
			if (this.project == null) {
				return Project.GetRelativeFilename(this.sourceFile, filename);
			} else {
				return Project.GetRelativeFilename(this.project.ProjectFilename, filename);
			}
		}

		/// <summary>
		/// Gets all source statements grouped by file.
		/// </summary>
		/// <param name="relativePaths">True to return relative filenames, false to return full filenames.</param>
		public KeyValuePair<string, SourceStatement[]>[] GetSourceStatementsByFile(bool relativePaths) {
			Dictionary<string, List<SourceStatement>> FileToStatements = new Dictionary<string, List<SourceStatement>>();

			foreach (SourceStatement Statement in this.statements) {
				List<SourceStatement> Statements;
				if (!FileToStatements.TryGetValue(Statement.Filename, out Statements)) {
					Statements = new List<SourceStatement>();
					FileToStatements.Add(Statement.Filename, Statements);
				}
				Statements.Add(Statement);
			}

			List<KeyValuePair<string, SourceStatement[]>> Result = new List<KeyValuePair<string, SourceStatement[]>>();
			foreach (KeyValuePair<string, List<SourceStatement>> Statement in FileToStatements) {
				Statement.Value.Sort(delegate(SourceStatement a, SourceStatement b) {
					return a.index.CompareTo(b.index);
				});
				Result.Add(new KeyValuePair<string, SourceStatement[]>(
					relativePaths ? this.GetRelativeFilename(Statement.Key) : Statement.Key,
					Statement.Value.ToArray()
				));
			}

			return Result.ToArray();
		}


	}
}
