using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Brass3.Documentation {
	/// <summary>
	/// Operator documentation.
	/// </summary>
	[Category("Source Syntax")]
	[Description("Operators are used to perform operations on labels in expressions.")]
	[Remarks(
@"Operators are evaluated in a particular order, referred to as operator precedence. Brass uses the same order of precedence that C# uses, documented in the following table. The higher up the table an operator is, the sooner it is evaluated.
<table>
	<tr><th>Label Access</th><td>:</td></tr>
	<tr><th>Unary</th><td>+, -, !, ~, ++x, --x</td></tr>
	<tr><th>Power</th><td>**</td></tr>
	<tr><th>Multiplicative</th><td>*, /, %</td></tr>
	<tr><th>Additive</th><td>+, -</td></tr>
	<tr><th>Shift</th><td>&lt;&lt;, &gt;&gt;</td></tr>
	<tr><th>Relational</th><td>&lt;, &gt;, &lt;=, &gt;=</td></tr>
	<tr><th>Equality</th><td>==, !=</td></tr>
	<tr><th rowspan=""3"">Logical</th><td>&</td></tr>
	<tr><td>^</td></tr>
	<tr><td>|</td></tr>
	<tr><th rowspan=""2"">Conditional</th><td>&&</td></tr>
	<tr><td>||</td></tr>
	<tr><th>Assignment</th><td>=, +=, -=, *=, /=, %=, &=, |=, ^=, &lt;&lt;=, &gt;&gt;=</td></tr>
	<tr><th>Indexing</th><td>a[x]</td></tr>
</table>
If two operators in the same group appear in the expression, the order in which they are evaluated depends on their associativity. Most operators are left-associative; that is they are evaluated in order from left to right. However, the assignment operators are right-associative and are thus evaluated in order from right to left.
You can use parentheses to cause an inner expression to be evaluated before its peers.")]
	[Warning("Some assemblers do not take operator precedence into account and simply evaluate from left to right.")]
	[CodeExample("Using parentheses to boost the precedence of an operator.", ".echoln 2 + 3 * 2   ; Outputs 8.\r\n.echoln (2 + 3) * 2 ; Outputs 10.")]
	[DisplayName("Operators")]
	[DocumentationUsage(DocumentationUsageAttribute.DocumentationType.DocumentationOnly)]
	public class Operators : IPlugin { }
}
