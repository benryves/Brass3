using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.IO;

namespace Core.Functions {

	
	[SatisfiesAssignmentRequirement(true)]
	[DocumentationUsage(DocumentationUsageAttribute.DocumentationType.FunctionalityOnly)]
	public class UserFunction : IFunction {

		int ModuleAllocation = 0;

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {
			
			// Get the .function directive plugin:
			Directives.Function FunctionDirective = (Directives.Function)compiler.GetPluginInstanceFromType(typeof(Directives.Function));

			// Rip out its function declarations for "function":
			List<Directives.Function.FunctionDeclaration> FunctionDeclarations = FunctionDirective.UserDefinedFunctions[function];

			// Get the arguments passed:
			int[] PassedArguments = source.GetCommaDelimitedArguments(0);

			List<Directives.Function.FunctionDeclaration> MatchedFunctionDeclarations = new List<Core.Directives.Function.FunctionDeclaration>();


			foreach (Directives.Function.FunctionDeclaration Def in FunctionDeclarations) {
				if (Def.Arguments.Length == PassedArguments.Length) {
					MatchedFunctionDeclarations.Add(Def);
				}
			}


			if (MatchedFunctionDeclarations.Count == 0) throw new CompilerException(source, Strings.ErrorFunctionUnmatchedSignature);
			if (MatchedFunctionDeclarations.Count > 1) throw new CompilerException(source, Strings.ErrorFunctionAmbiguousSignature);

			Directives.Function.FunctionDeclaration FunctionDeclaration = MatchedFunctionDeclarations[0];			
			

			try {

				compiler.Labels.EnterModule("USER_FUNCTION_" + (ModuleAllocation++));

				// Just in case (for macro arguments):
				List<KeyValuePair<TokenisedSource.Token, TokenisedSource>> MacroArguments = new List<KeyValuePair<TokenisedSource.Token, TokenisedSource>>();

				// Set arguments:
				for (int i = 0; i < FunctionDeclaration.Arguments.Length; ++i) {

					switch (FunctionDeclaration.ArgumentTypes[i]) {
						case Directives.Function.FunctionDeclaration.ArgumentType.Value: {

								// Evaluate the passed argument:
								Label PassedArgument = source.EvaluateExpression(compiler, PassedArguments[i]);

								// Create a new label:
								Label CreatedLabel = compiler.Labels.Create(FunctionDeclaration.Arguments[i]);

								// Copy the value!
								if (PassedArgument.IsString) {
									CreatedLabel.StringValue = PassedArgument.StringValue;
								} else {
									CreatedLabel.NumericValue = PassedArgument.NumericValue;
								}

							} break;
						case Core.Directives.Function.FunctionDeclaration.ArgumentType.Macro: {

								MacroArguments.Add(new KeyValuePair<TokenisedSource.Token, TokenisedSource>(
									FunctionDeclaration.Arguments[i],
									source.GetExpressionTokens(PassedArguments[i])
								));

							} break;
						default:
							throw new NotImplementedException();
					}


				}

				// Execute the function:
				compiler.RecompileRange(FunctionDeclaration.EntryPoint.Next, FunctionDeclaration.ExitPoint.Previous, MacroArguments.ToArray());

				Label Result;
				if (compiler.Labels.TryParse(FunctionDeclaration.Name, out Result)) {
					return (Label)Result.Clone();
				} else {
					return new Label(compiler.Labels, double.NaN);
				}
			} finally {
				compiler.Labels.LeaveModule();
			}
		}
	}
}
