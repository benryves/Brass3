using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".function name([arg1 [, arg2 [, ...]]])\r\n\t[statements]\r\n.endfunction")]
	[Description("Defines a function.")]
	[Warning("If a return value is not explicitly set, the function returns NaN.")]
	[Remarks(
@"The function can contain any code, including mathematical expressions, directives and assembly source.
To return a value from the function, simply assign a label with the name of the function itself.
If you put the <c>macro</c> keyword in front of an argument name the passed argument is substituted into the function via a macro replacement rather than an as an evaluated label.
If you develop a complex function that is frequently used in your source file consider converting it to a function plugin, as they execute significantly faster and have much better control over the compilation process.")]
	[CodeExample("A recursive function that calculates n!", "#function f(n)\r\n\t.if n == 0\r\n\t\tf = 1\r\n\t.else\r\n\t\tf = n * f(n - 1)\r\n\t.endif\r\n#endfunction\r\n\r\n.echoln f(30) ; Outputs 2.65252859812191E+32.")]
	[CodeExample("Distance function using the Pythagorean theorem.", ".function distance(x,y)\r\n\tdistance = sqrt(x * x + y * y)\r\n.endfunction\r\n\r\n.echoln distance(3, 4) ; Outputs 5.")]
	[CodeExample("Z80 TI calculator <c>bcall()</c> implementation.", "/* Enumeration for different calculator models */\r\n#enum Model\r\n\tTI8X, ; TI-83 Plus\r\n\tTI83  ; TI-83\r\n\r\n/* Set to TI8X or TI83 */\r\nModel = Model.TI83\r\n\r\n/* Use bcall() to invoke ROM calls */\r\n#function bcall(label)\r\n\t.if Model == Model.TI8X\r\n\t\trst 28h\r\n\t\t.dw label\r\n\t.else\r\n\t\tcall label\r\n\t.endif\r\n#endfunction")]
	[CodeExample("Binary and hexadecimal formatting <c>echo</c> functions.", "/* Output the eight bits of value as 1s or 0s */\r\n.function echobinary(value)\r\n\t.for bit is 7 to 0\r\n\t\t.if value & 1 << bit\r\n\t\t\t.echo 1\r\n\t\t.else\r\n\t\t\t.echo 0\r\n\t\t.endif\r\n\t.loop\r\n.endfunction\r\n\r\n/* Output the eight bits of value as 1s or 0s with a % prefix */\r\n.function echobinarybyte(value)\r\n\t.echo '%' \\ echobinary(value)\r\n.endfunction\r\n\r\n/* Output the sixteen bits of value as 1s or 0s with a % prefix */\r\n.function echobinaryword(value)\r\n\t.echo '%'\r\n\techobinary(value >> 8)\r\n\techobinary(value)\r\n.endfunction\r\n\r\n/* Output the four bits of value as a hex digit */\r\n.function echohexnybble(value)\r\n\t.echo choose(1 + (value & %1111),\r\n\t\t'0', '1', '2', '3', '4', '5', '6', '7',\r\n\t\t'8', '9', 'A', 'B', 'C', 'D', 'E', 'F')\r\n.endfunction\r\n\r\n/* Output the eight bits of value as two hex digits */\r\n.function echohex(value)\r\n\techohexnybble(value >> 4)\r\n\techohexnybble(value)\r\n.endfunction\r\n\r\n/* Output the eight bits of value as two hex digits */\r\n.function echohexbyte(value)\r\n\t.echo '$' \\ echohex(value)\r\n.endfunction\r\n\r\n/* Output the eight bits of value as two hex digits */\r\n.function echohexword(value)\r\n\t.echo '$'\r\n\techohex(value >> 8)\r\n\techohex(value)\r\n.endfunction\r\n\r\nechobinaryword(1234) ; Outputs %0000010011010010\r\n.echoln\r\n\r\nechohexword(%1011111011101111) ; Outputs $BEEF\r\n.echoln")]
	[CodeExample("Passing a string via the <c>macro</c> keyword.", ".function repeatstring(macro str, repeat)\r\n\t.for i = 0, i < repeat, ++i\r\n\t\t.db str\r\n\t.loop\r\n.endfunction\r\n\r\nrepeatstring(\"Hello\", 3)")]
	[CodeExample("Passing by reference.", ".function increment(macro label)\r\n\t++label\r\n.endfunction\r\n\r\nx = 1\r\n.echoln \"x=\", x\r\n\r\nincrement(x)\r\n.echoln \"x=\", x")]
	[Category("Functions")]
	[DisplayName("function/macro")]
	[PluginName("function"), PluginName("endfunction")]
	[PluginName("macro"), PluginName("endmacro")]
	public class Function : IDirective {

		internal class FunctionDeclaration {
			public TokenisedSource.Token Name;
			public LinkedListNode<Compiler.SourceStatement> EntryPoint;
			public LinkedListNode<Compiler.SourceStatement> ExitPoint;
			public TokenisedSource.Token[] Arguments;
			public enum ArgumentType {
				Value,
				Macro,
			}
			public ArgumentType[] ArgumentTypes;
		}

		internal Dictionary<string, List<FunctionDeclaration>> UserDefinedFunctions;

		public Function(Compiler c) {
			this.UserDefinedFunctions = new Dictionary<string, List<FunctionDeclaration>>();
			c.PassBegun += delegate(object sender, EventArgs e) {
				this.DeclaringFunction = null;
				this.UserDefinedFunctions.Clear();
			};
		}


		private FunctionDeclaration DeclaringFunction = null;

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			switch (directive) {
				case "function":
				case "macro":

					if (!compiler.IsSwitchedOn) return;

					TokenisedSource Declaration = source.GetExpressionTokens(source.GetCommaDelimitedArguments(index + 1, 1)[0]);
					if (Declaration.Tokens.Length < 3) throw new DirectiveArgumentException(source, "Invalid function declaration.");
					if (Declaration.Tokens[0].Type != TokenisedSource.Token.TokenTypes.Function) throw new DirectiveArgumentException(Declaration.Tokens[0], "Function declaration needs to start with name().");


					for (int i = 0; i < Declaration.Tokens.Length; ++i) Declaration.Tokens[i].ExpressionGroup = 0;

					// Create a new FunctionDeclaration and set its name.
					this.DeclaringFunction = new FunctionDeclaration();
					this.DeclaringFunction.Name = Declaration.Tokens[0];

					
					// Decode the argument names from the f(...) list:
					int[] ArgumentIndices = Declaration.GetCommaDelimitedArguments(2);
					this.DeclaringFunction.Arguments = new TokenisedSource.Token[ArgumentIndices.Length];
					this.DeclaringFunction.ArgumentTypes = new FunctionDeclaration.ArgumentType[ArgumentIndices.Length];
					Declaration.Tokens[Declaration.Tokens.Length - 1].ExpressionGroup = 0;
					for (int i = 0; i < ArgumentIndices.Length; ++i) {

						TokenisedSource Argument = Declaration.GetExpressionTokens(ArgumentIndices[i]);

						if (Argument.Tokens.Length == 0 && ArgumentIndices.Length == 1) {
							Array.Resize<TokenisedSource.Token>(ref this.DeclaringFunction.Arguments, 0);
							Array.Resize<FunctionDeclaration.ArgumentType>(ref this.DeclaringFunction.ArgumentTypes, 0);
							break;
						}

						if (Argument.Tokens.Length == 1 || Argument.Tokens.Length == 2) {
							this.DeclaringFunction.Arguments[i] = Argument.Tokens[Argument.Tokens.Length - 1];
							this.DeclaringFunction.ArgumentTypes[i] = directive == "macro" ? FunctionDeclaration.ArgumentType.Macro : FunctionDeclaration.ArgumentType.Value;
							if (Argument.Tokens.Length == 2) {
								switch (Argument.Tokens[0].DataLowerCase) {
									case "value":
										this.DeclaringFunction.ArgumentTypes[i] = FunctionDeclaration.ArgumentType.Value;
										break;
									case "macro":
										this.DeclaringFunction.ArgumentTypes[i] = FunctionDeclaration.ArgumentType.Macro;
										break;
									default:
										throw new CompilerExpection(Argument.Tokens[0], "Invalid argument type specifier.");
								}
							}
						} else {
							throw new CompilerExpection(source, "Invalid argument declaration.");
						}
					}
		
					
					// Set entry point;
					this.DeclaringFunction.EntryPoint = compiler.RememberPosition().Next;

					// Don't compile the contents of the function!
					compiler.SwitchOff(typeof(Function));
					break;
				case "endfunction":
				case "endmacro":
					if (this.DeclaringFunction != null) {
						// We've finished declaring a function.
						string LowerCaseName = this.DeclaringFunction.Name.Data.ToLowerInvariant();
						this.DeclaringFunction.ExitPoint = compiler.RememberPosition();

						List<FunctionDeclaration> Declarations;
						if (!this.UserDefinedFunctions.TryGetValue(LowerCaseName, out Declarations)) {
							Declarations = new List<FunctionDeclaration>();
							this.UserDefinedFunctions.Add(LowerCaseName, Declarations);
						}

						Declarations.Add(this.DeclaringFunction);

						if (compiler.Functions.Contains(LowerCaseName)) {
							if (compiler.Functions[LowerCaseName].GetType() != typeof(Functions.UserFunction)) throw new InvalidOperationException("Function " + this.DeclaringFunction.Name.Data + " already natively defined.");
						} else {
							compiler.Functions.AddRuntimeAlias((IFunction)compiler.GetPluginInstanceFromType(typeof(Functions.UserFunction)), LowerCaseName);
						}						

						this.DeclaringFunction = null;
						compiler.SwitchOn();
					}
					break;
			}
			
		}
	}
}
