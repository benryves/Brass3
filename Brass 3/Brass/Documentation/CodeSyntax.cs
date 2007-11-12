using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Brass3.Documentation {
	/// <summary>
	/// Documents the syntax of source code.
	/// </summary>
	[Description("Source code is made up of individual compilable statements.")]
	[Category("Source Syntax")]
	[Remarks(
@"Source code is made up of statements. Typically a statement either ends at the end of the line or at the \ token.
Source code is read from files a token at a time. A token is a grouped sequence of characters such as an assembler instruction, numeric value, punctuation, whitespace, or a comment. Comments are discarded and whitespace is stripped.
If a directive is encountered as source is read, it is examined to see if it marked as affecting the parser. Directives can, for example, temporarily prevent \ from being interpreted as a seperator. This is not common.
Each statement is made up of two parts; a label assignment followed by either a directive or some assembly code. Once a sequence of tokens has been read, the compiler scans it from left to right trying to find a assembly instruction or directive.
As the compiler ignores whitespace between tokens, this means that indentation isn't as strictly regulated as it might be in other assemblers. Labels can be indented at any level, and assembly instructions don't need to be indented at all.")]
	[DisplayName("Statement Syntax")]
	[DocumentationUsage(DocumentationUsageAttribute.DocumentationType.DocumentationOnly)]
	public class CodeSyntax : IPlugin { }
}
