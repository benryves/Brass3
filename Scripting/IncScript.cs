using System;
using System.Collections.Generic;
using System.Text;

using System.CodeDom.Compiler;
using System.Reflection;
using System.ComponentModel;
using System.IO;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.Globalization;

namespace Scripting {

	[Category("Scripting")]
	[Syntax(".incscript \"source.cs\" [, \"reference\" [, \"reference\" [, ...]]]")]
	[Description("Loads a script file.")]
	[Remarks(
@"Script files can be written in any .NET-compatible language, such as C# or Visual Basic.
Script files should contain at least one public class containing public static (<c>Shared</c> in Visual Basic) methods.
The following argument and return value types are valid:
<table>
	<tr>
		<th>Arguments and Return</th>
		<td><c>String</c>.
<c>Double</c>, <c>Float</c>, <c>Int32</c>, <c>UInt32</c>, <c>Int16</c>, <c>UInt16</c>, <c>Byte</c>, <c>SByte</c>.
<c>Bool</c>.</td>
	</tr>
	<tr>
		<th>Arguments Only</th>
		<td><c>Brass3.Compiler</c>, <c>Brass3.Label</c>.</td>
	</tr>
	<tr>
		<th>Return Only</th>
		<td><c>void</c> (<c>Sub</c> in Visual Basic).</td>
	</tr>
</table>
Brass itself only understands double-precision floats and strings, so data types are converted before your function is called and converted again when returned.
The <c>Brass3.Compiler</c> argument is a special case. If you specify it, do not pass a value for it from your assembly source file. It will be populated with the instance of the compiler object building the current file so that your script file can control the compiler directly if need be.")]
	[CodeExample("C# script.",
@"/* File: Script.cs

public class ScriptSample {

	public static double Multiply(double a, double b) {
		return a * b; 
	}

} */

.incscript ""Script.cs""
.echoln Multiply(4, 5) ; Outputs 20.")]

	[CodeExample("Visual Basic script.",
@"/* File: Script.vb

Public Class ScriptSample

	Public Shared Function RepeatString( _
		ByVal str As String, _
		ByVal amount As Integer) As String
		
		RepeatString = String.Empty
		
		For i As Integer = 1 to amount
			RepeatString &= str
		Next i
		
	End Function

End Class */

.incscript ""Script.vb""
.echoln ""Pot"" + RepeatString(""o"", 8)")]

	[CodeExample("Passing a <c>Label</c> to a directive.",
@"/* File: Script.cs

using Brass3;
public class ScriptSample {

	public static void IncrementLabel(Label label) {
		++label.NumericValue;
	}

} */

.incscript ""Script.cs""

x = 10            ; Initialise to 10.
.echoln x         ; Outputs 10.
#incrementlabel x ; Increments X.
.echoln x         ; Outputs 11.")]

	[CodeExample("Using Windows Forms.", 
@"/* File: WinForms.cs

using Brass3;
using System.Windows.Forms;
using System.Collections.Generic;

public class ConfirmBox {

	#region Private Fields
	private readonly Compiler Compiler;
	private Queue<bool> Results;
	#endregion
	
	#region Constructor
	public ConfirmBox(Compiler compiler) {
		this.Compiler = compiler;
		this.Results = new Queue<bool>();
	}
	#endregion

	#region Public Methods
	
	public void Alert(string prompt) {
		if (this.Compiler.CurrentPass == AssemblyPass.CreatingLabels) {
			MessageBox.Show(
				prompt,
				""Information"",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
		}
	}
	
	public bool Confirm(string prompt) {
		if (this.Compiler.CurrentPass == AssemblyPass.CreatingLabels) {
			
			bool Result = MessageBox.Show(
				prompt,
				""Question"",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question
			) == DialogResult.Yes;
			
			Results.Enqueue(Result);
			return Result;
		} else {
			return Results.Dequeue();
		}
	}
	
	#endregion

} */

.incscript ""WinForms.cs"",
           ""System.dll"", ""System.Windows.Forms.dll""

ClickCount = 0
.while Confirm(""Would you like to increment "" + ClickCount + ""?""))
	++ClickCount
.loop

.Alert ""Final value: "" + ClickCount + "".""")]

	public class IncScript : IDirective {

		private Queue<List<ScriptWrapper>> WrappedFunctions;

		public IncScript(Compiler compiler) {
			this.WrappedFunctions = new Queue<List<ScriptWrapper>>();
			compiler.PassBegun += delegate(object sender, EventArgs e) {
				if (compiler.CurrentPass == AssemblyPass.CreatingLabels) this.WrappedFunctions.Clear();
			};
		}

		private static string ResolveAssemblyName(string name) {
			string LocalPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), name);
			return (File.Exists(LocalPath)) ? LocalPath : name;
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.CurrentPass == AssemblyPass.CreatingLabels) {

				// Runtime-created wrapper function around the .NET method.
				List<ScriptWrapper> Wrappers = new List<ScriptWrapper>();


				// Try - if we throw an exception we'll add a dummy anyway to keep the queue happy.
				try {

					object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1,
						new TokenisedSource.ArgumentType[] {
							TokenisedSource.ArgumentType.Filename,
							TokenisedSource.ArgumentType.String | TokenisedSource.ArgumentType.Optional | TokenisedSource.ArgumentType.RepeatForever,
						}
					);

					string ScriptFile = Args[0] as string;

					// Hunt through all available compilers and dig out one with a matching extension.
					CodeDomProvider Provider = null;
					string Extension = Path.GetExtension(ScriptFile).ToLowerInvariant();
					if (Extension.Length > 0 && Extension[0] == '.') Extension = Extension.Substring(1);
					foreach (CompilerInfo Info in CodeDomProvider.GetAllCompilerInfo()) {
						if (Info.IsCodeDomProviderTypeValid) {
							CodeDomProvider TestProvider = Info.CreateProvider();
							if (TestProvider.FileExtension.ToLowerInvariant() == Extension) {
								Provider = TestProvider;
								break;
							}
						}
					}

					if (Provider == null) throw new CompilerExpection(source, "Script language not found.");

					// Compile the bugger:
					CompilerParameters Parameters = new CompilerParameters();
					Parameters.GenerateExecutable = false;
					Parameters.GenerateInMemory = true;
					Parameters.TreatWarningsAsErrors = false;


					Parameters.ReferencedAssemblies.Add(ResolveAssemblyName("Brass.exe")); // Goes without saying, eh? :)

					for (int i = 1; i < Args.Length; ++i) Parameters.ReferencedAssemblies.Add(ResolveAssemblyName(Args[i] as string));

					CompilerResults Results = Provider.CompileAssemblyFromFile(Parameters, ScriptFile);

					// Errors?
					foreach (CompilerError Error in Results.Errors) {
						Compiler.NotificationEventArgs Notification = new Compiler.NotificationEventArgs(compiler, Error.ErrorText, Error.FileName, Error.Line);
						if (Error.IsWarning) {
							compiler.OnWarningRaised(Notification);
						} else {
							compiler.OnErrorRaised(Notification);
						}
					}

					// Do nothing if there were errors.
					if (Results.Errors.HasErrors) return;


					// Grab the public classes from the script.
					foreach (Type T in Results.CompiledAssembly.GetExportedTypes()) {
						if (!T.IsClass) continue;

						// Try and create an instance of the class.
						object ClassInstance = null;

						// Dig out a constructor.
						ConstructorInfo InstanceConstructor = null;
						object[] ConstructorArgs = new object[]{compiler};
						if ((InstanceConstructor = T.GetConstructor(new Type[] { typeof(Compiler) })) == null) {
							InstanceConstructor = T.GetConstructor(Type.EmptyTypes);
							ConstructorArgs = new object[] { };
						}

						if (ConstructorArgs != null) {
							ClassInstance = InstanceConstructor.Invoke(BindingFlags.Default, null, ConstructorArgs, CultureInfo.InvariantCulture);
						}

						List<Type> ValidTypes = new List<Type>(
							new Type[] { typeof(string), typeof(double), typeof(float), typeof(int), typeof(uint), typeof(short), typeof(ushort), typeof(byte), typeof(sbyte), typeof(bool) }
						);

						// Hunt public static methods.
						foreach (MethodInfo Method in T.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)) {

							// Valid type?
							if (!(Method.ReturnType == typeof(void) || ValidTypes.Contains(Method.ReturnType))) continue;

							// Create an array of method parameters.
							List<TokenisedSource.ArgumentType> MethodParameters = new List<TokenisedSource.ArgumentType>();

							// Iterate over all of them...
							bool IsValid = true;

							foreach (ParameterInfo Parameter in Method.GetParameters()) {

								if (Parameter.ParameterType == typeof(Compiler)) {
									// ... :)
								} else if (Parameter.ParameterType == typeof(Label)) {
									MethodParameters.Add(TokenisedSource.ArgumentType.Label);
								} else if (Parameter.ParameterType == typeof(string)) {
									MethodParameters.Add(TokenisedSource.ArgumentType.String);
								} else if (ValidTypes.Contains(Parameter.ParameterType)) {
									MethodParameters.Add(TokenisedSource.ArgumentType.Value);
								} else {
									IsValid = false;
									break;
								}
							}

							if (IsValid) {
								// Create the wrapper!
								if (Method.ReturnType == typeof(void)) {
									Wrappers.Add(new ScriptDirectiveWrapper(ClassInstance, Method, MethodParameters.ToArray()));
								} else {
									Wrappers.Add(new ScriptFunctionWrapper(ClassInstance, Method, MethodParameters.ToArray()));
								}
							}

						}

					}
				} finally {
					this.WrappedFunctions.Enqueue(Wrappers);
					foreach (ScriptWrapper Wrapper in Wrappers) AddWrapperToCompiler(compiler, Wrapper);
				}
			} else {
				List<ScriptWrapper> Functions = this.WrappedFunctions.Dequeue();
				foreach (ScriptWrapper Function in Functions) AddWrapperToCompiler(compiler, Function);
			}
		}

		private void AddWrapperToCompiler(Compiler compiler, ScriptWrapper wrapper) {
			if (wrapper is IDirective) {
				compiler.Directives.AddRuntimeAlias(wrapper as IDirective, wrapper.Name);
			} else if (wrapper is IFunction) {
				compiler.Functions.AddRuntimeAlias(wrapper as IFunction, wrapper.Name);
			} else {
				throw new InvalidOperationException();
			}
		}


	}
}
