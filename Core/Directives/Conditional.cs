using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".if condition1\r\n\t[evaluated if condition1 met]\r\n.elseif condition2\r\n\t[evaluated if condition2 met]\r\n.else\r\n\t[evaluated if no conditions met]\r\n.endif")]
	[Description("Selects statements for execution based on the results of a series of conditionals.")]
	[Remarks("You may nest conditional statements.\r\nStatements following an <c>else</c> or <c>elseif</c> directive are only executed if no previous conditions have evaluated to <c>true</c>.\r\n<c>ifdef</c>, <c>ifndef</c>, <c>elseifdef</c> and <c>elseifndef</c> check if the following symbol is defined (ie, <c>ifdef x</c> is functionally equivalent to <c>if defined(x)</c>).")]
	[CodeExample("x = 10 \\ y = 20\r\n\r\n.if x > y\r\n\t.echoln \"X is greater than Y.\"\r\n.endif")]
	[CodeExample("age = 18\r\n\r\n#if age > 300\r\n\t.echoln \"Sorry, we don't serve spirits.\"\r\n#elseif age < 18\r\n\t.echoln \"You are below the legal drinking age.\"\r\n#elseif age < 21\r\n\t.echoln \"Can I see some ID, please?\"\r\n#else\r\n\t.echoln \"Here, have a pint.\"\r\n#endif")]
	[Category("Flow Control")]
	[PluginName("if"), PluginName("elseif"), PluginName("else"), PluginName("endif"), PluginName("ifdef"), PluginName("ifndef"), PluginName("elseifdef"), PluginName("elseifndef")]
	public class Conditional : IDirective {

		private class ConditionalBlock {
			public bool ParentResult;
			public bool CurrentResult;
			public bool ConditionMetInPast;
		}

		private Stack<ConditionalBlock> ConditionalStack;

		private Compiler Compiler;

	
		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			bool CheckingDefinitions = directive.EndsWith("def");
			
			int ExpectedArgs = (directive == "endif" || directive == "else") ? 0 :1;
			int[] Args = source.GetCommaDelimitedArguments(index + 1, ExpectedArgs);

			ConditionalBlock CurrentBlock;
			if (directive == "if" || directive == "ifdef" || directive == "ifndef") {
				CurrentBlock = new ConditionalBlock();
				CurrentBlock.ParentResult = this.ConditionalStack.Peek().CurrentResult;
				ConditionalStack.Push(CurrentBlock);
			} else if (directive == "endif") {
				CurrentBlock = this.ConditionalStack.Pop();
			} else {
				CurrentBlock = this.ConditionalStack.Peek();
			}

			bool ConditionalResult = false;
			if (Args.Length > 0) {
				if (CheckingDefinitions) {
					bool Defined = compiler.IsDefined(source.Tokens[index + 1]) ^ (directive[directive.Length - 4] == 'n');
					
					TokenisedSource StuffAfterIfDef = source.GetExpressionTokens(Args[0]);
					StuffAfterIfDef.Tokens[0] = new TokenisedSource.Token(StuffAfterIfDef, TokenisedSource.Token.TokenTypes.None, Defined ? "1" : "0", -1);
					ConditionalResult = StuffAfterIfDef.EvaluateExpression(compiler).NumericValue != 0;
				} else {
					ConditionalResult = source.EvaluateExpression(compiler, Args[0]).NumericValue != 0;
				}
			}

			switch (directive) {
				case "if":
				case "ifdef":
				case "ifndef":
				case "elseif":
				case "elseifdef":
				case "elseifndef":
					CurrentBlock.CurrentResult = ConditionalResult && CurrentBlock.ParentResult && !CurrentBlock.ConditionMetInPast;
					break;
				case "else":
					CurrentBlock.CurrentResult = !CurrentBlock.ConditionMetInPast  && CurrentBlock.ParentResult;
					break;
				case "endif":
					CurrentBlock.CurrentResult = CurrentBlock.ParentResult;
					break;
			}

			CurrentBlock.ConditionMetInPast |= CurrentBlock.CurrentResult;
			if (CurrentBlock.CurrentResult) {
				this.Compiler.SwitchOn();
			} else {
				this.Compiler.SwitchOff(typeof(Conditional));
			}

		}

		public Conditional(Compiler compiler) {
			this.Compiler = compiler;
			this.Compiler.PassBegun += new EventHandler(Compiler_PassBegun);
		}

		void Compiler_PassBegun(object sender, EventArgs e) {
			this.ConditionalStack = new Stack<ConditionalBlock>();
			ConditionalBlock CB = new ConditionalBlock();
			CB.ConditionMetInPast = true;
			CB.CurrentResult = true;
			this.ConditionalStack.Push(CB); // Implicit "true" at top of stack.
		}

	}
}
