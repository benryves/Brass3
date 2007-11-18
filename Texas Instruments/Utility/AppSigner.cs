using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TexasInstruments.Utility {
	/// <summary>
	/// AppSigner class provides a wrapper around the Wappsign COM object.
	/// </summary>
	class Wappsign {

		private Type WappsignComType;
		private object WappsignObj;

		private void CreateWappsignObject() {
			this.WappsignComType = Type.GetTypeFromProgID("Wappsign.Sign");
			if (WappsignComType == null) throw new Exception("Wappsign not found.");
			this.WappsignObj = Activator.CreateInstance(WappsignComType);
		}

		/// <summary>
		/// Create instance of the AppSigner class (wrapper around Wappsign COM object).
		/// </summary>
		public Wappsign() {
			CreateWappsignObject();
		}

		/// <summary>
		/// Create instance of the AppSigner class (wrapper around Wappsign COM object).
		/// </summary>
		/// <param name="mode">Wappsign mode flags.</param>
		public Wappsign(Mode mode) {
			CreateWappsignObject();
			this.Flags = mode;
		}

		/// <summary>
		/// Controls the way that Wappsign operates.
		/// </summary>
		[Flags]
		public enum Mode : int {
			/// <summary>Generates the log file.</summary>
			Verbose = 1,
			/// <summary>Affects FormatOutput(). Appends .8xk.</summary>
			Output83 = 2,
			/// <summary>Affects FormatOutput(). Appends .73k.</summary>
			Output73 = 4,
			/// <summary>Will append .hex to the output file and only run the Fillapp.</summary>
			FillOnly = 8,
			/// <summary>Detects the target calculator type (83P/73).</summary>
			DetectType = 4096
		}


		/// <summary>
		/// Mode flags for Wappsign's operation.
		/// </summary>
		public Mode Flags {
			get {
				return (Mode)this.WappsignComType.InvokeMember("Flags", BindingFlags.GetProperty, null, this.WappsignObj, null);
			}
			set {
				this.WappsignComType.InvokeMember("Flags", BindingFlags.SetProperty, null, this.WappsignObj, new object[] { value });
			}
		}

		/// <summary>
		/// Signs an application.
		/// </summary>
		/// <param name="hex">Filename of Intel hex source file.</param>
		/// <param name="key">Filename of key.</param>
		/// <param name="output">Filename used for output.</param>
		/// <returns>0 if signed correctly, otherwise an error code. Use GetErrorMessage() to retrieve the full text error message. If DetectType is used with the flags, then Wappsign detects target calculator type.</returns>
		public int Sign(string hex, string key, string output) {
			return (int)this.WappsignComType.InvokeMember("Sign", BindingFlags.InvokeMethod, null, this.WappsignObj, new object[] { hex, key, output });
		}


		/// <summary>
		/// Detects and retrieves the key file used by the application. Uses the search paths saved in the registry. These can be modified at run time using the directory functions.
		/// </summary>
		/// <param name="hex">Filename of the Intel hex source file.</param>
		/// <returns>A string with the full path name to the key file, otherwise "".</returns>
		public string GetKeyFile(string hex) {
			return (string)this.WappsignComType.InvokeMember("GetKeyFile", BindingFlags.InvokeMethod, null, this.WappsignObj, new object[] { hex });
		}


		/// <summary>
		/// Simply takes filename, removes the extension and adds either .8xk, 73k or .hex according to the Flags properties.
		/// </summary>
		/// <param name="filename">The filename to format.</param>
		/// <returns>Output filename, otherwise "".</returns>
		public string FormatOutput(string filename) {
			return (string)this.WappsignComType.InvokeMember("FormatOutput", BindingFlags.InvokeMethod, null, this.WappsignObj, new object[] { filename });
		}

		/// <summary>
		/// Returns the full error message given an error code. 
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <returns>Will return the error message. Passing 0 returns "WAPP0000: Successful sign!" and passing an invalid or unrecognized error will return "XXXX0000: An unspecificed error has occurred!"</returns>
		public string GetErrorMessage(int errorCode) {
			return (string)this.WappsignComType.InvokeMember("GetErrorMessage", BindingFlags.InvokeMethod, null, this.WappsignObj, new object[] { errorCode });
		}

		/// <summary>
		/// Adds a directory to the search path. All trailing slashes are removed.
		/// </summary>
		/// <param name="directory">Directory name to add.</param>
		/// <returns>Returns true if the path was successfully added. False if 10 directories are already in the search path.</returns>
		public bool AddDirectory(string directory) {
			return (bool)this.WappsignComType.InvokeMember("AddDirectory", BindingFlags.InvokeMethod, null, this.WappsignObj, new object[] { directory });
		}

		/// <summary>
		/// Removes a directory from the search path.
		/// </summary>
		/// <param name="directory">Directory path to remove.</param>
		/// <returns>True if directory was successfully removed. False if directory could not be found or removed.</returns>
		public bool RemoveDirectory(string directory) {
			return (bool)this.WappsignComType.InvokeMember("RemoveDirectory", BindingFlags.InvokeMethod, null, this.WappsignObj, new object[] { directory });
		}

		/// <summary>
		/// Retrieves the index-th directory from the search path.
		/// </summary>
		/// <param name="index">Directory index.</param>
		/// <returns>Directory path name. Returns "" if the index is out of range.</returns>
		public string GetDirectory(int index) {
			return (string)this.WappsignComType.InvokeMember("GetDirectory", BindingFlags.InvokeMethod, null, this.WappsignObj, new object[] { index });
		}

	}
}
