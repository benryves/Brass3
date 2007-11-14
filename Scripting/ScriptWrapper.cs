using System;
using System.Collections.Generic;
using System.Text;

using System.CodeDom.Compiler;
using System.Reflection;
using System.Globalization;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Scripting {
	
	public class ScriptWrapper {

		private readonly MethodInfo WrappedMethod;
		private readonly TokenisedSource.ArgumentType[] BrassArguments;
		private readonly ParameterInfo[] DotNetArguments;
		private readonly object ContainerInstance;

		public string Name {
			get { return this.WrappedMethod.Name; }
		}

		public ScriptWrapper(object containerInstance, MethodInfo wrappedMethod, TokenisedSource.ArgumentType[] arguments) {
			this.ContainerInstance = containerInstance;
			this.WrappedMethod = wrappedMethod;
			this.BrassArguments = arguments;
			this.DotNetArguments = wrappedMethod.GetParameters();
		}


		protected object Execute(Compiler compiler, TokenisedSource source, int index) {

			object[] ArgumentsFromBrass = source.GetCommaDelimitedArguments(compiler, index, this.BrassArguments);
			object[] ArgumentsToDotNet = new object[DotNetArguments.Length];

			for (int i = 0, j = 0; i < this.DotNetArguments.Length; ++i) {
				if (this.DotNetArguments[i].ParameterType == typeof(Compiler)) {
					ArgumentsToDotNet[i] = compiler;
				} else {
					ArgumentsToDotNet[i] = Convert.ChangeType(ArgumentsFromBrass[j++], this.DotNetArguments[i].ParameterType, CultureInfo.InvariantCulture);
				}
			}

			try {
				return WrappedMethod.Invoke(this.ContainerInstance, BindingFlags.Default, null, ArgumentsToDotNet, CultureInfo.InvariantCulture);
			} catch (Exception ex) {
				throw ex.InnerException;				
			}
	
		}
	}

	[DocumentationUsage(DocumentationUsageAttribute.DocumentationType.FunctionalityOnly)]
	public class ScriptFunctionWrapper : ScriptWrapper, IFunction {

		public ScriptFunctionWrapper(object containerInstance, MethodInfo wrappedMethod, TokenisedSource.ArgumentType[] arguments)
			: base(containerInstance, wrappedMethod, arguments) {
		}

		public Label Invoke(Compiler compiler, TokenisedSource source, string function) {

			object Result = this.Execute(compiler, source, 0);

			if (Result is string) {
				return new Label(compiler.Labels, Result as string);
			} else {
				return new Label(compiler.Labels, (double)Convert.ChangeType(Result, typeof(double)));
			}

		}
	}

	[DocumentationUsage(DocumentationUsageAttribute.DocumentationType.FunctionalityOnly)]
	public class ScriptDirectiveWrapper : ScriptWrapper, IDirective {

		public ScriptDirectiveWrapper(object containerInstance, MethodInfo wrappedMethod, TokenisedSource.ArgumentType[] arguments)
			: base(containerInstance, wrappedMethod, arguments) {
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			this.Execute(compiler, source, index + 1);
		}

	}

}
