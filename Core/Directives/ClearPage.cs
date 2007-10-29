using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".clearpage page")]
	[Description("Clears the data output on a particular page.")]
	[Remarks("This directive can be useful to switch to a temporary page, do some assembling, then remove the output data so it doesn't appear in the output file.")]
	[CodeExample("Use the smallest of two source snippets.", "/* Returns the output size (in bytes) of compiled source */\r\n.function size_of_source(src)\r\n\r\n\t/* Preserve $ and @ */\r\n\told_$_p = :$ \\ old_$_v = $:\r\n\told_@_p = :@ \\ old_@_v = @:\r\n\t\r\n\t/* Move to a temporary page (-1) */\r\n\t:$ = -1\r\n\t\r\n\t/* Evaluate the source */\r\n\teval(src)\r\n\t\r\n\tsize_of_source = @ - old_@_v\r\n\t\r\n\t/* Clear all output data from temporary page */\r\n\t.clearpage -1\r\n\t\r\n\t/* Restore $ and @ */\r\n\t:$ = old_$_p \\ $: = old_$_v\r\n\t:@ = old_@_p \\ @: = old_@_v\r\n\r\n.endfunction\r\n\r\n/* Uses the smallest of two pieces of source code */\r\n.function use_smallest(src_a, src_b)\r\n\t\r\n\t/* Get the size of both snippets */\r\n\tsize_of_src_a = size_of_source(src_a)\r\n\tsize_of_src_b = size_of_source(src_b)\r\n\t\r\n\t.if size_of_src_a <= size_of_src_b\r\n\t\tuse_smallest = size_of_src_a\r\n\t\teval(src_a)\r\n\t.else\r\n\t\tuse_smallest = size_of_src_b\r\n\t\teval(src_b)\r\n\t.endif\r\n\r\n.endfunction\r\n\r\n/* Uses \"xor a\" and displays 1: */\r\n.echoln use_smallest(\"ld a,0\", \"xor a\")")]
	[Category("Output Manipulation")]
	public class ClearPage : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			int Page = (int)(double)source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.Value })[0];
			compiler.ClearPage(Page);

		}

	}
}
