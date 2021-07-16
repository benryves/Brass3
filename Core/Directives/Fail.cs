using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".fail expression [, expression [, ... ]]")]
	[Description("Forces an error in the source code and displays an error message. It follows the same general rules as <c>.echo</c>")]
	[Remarks("Expressions can be either numeric or string constants. This directive is only invoked during the second pass.")]
	[CodeExample(".if # != 3\r\n.fail \"This module will only run from page 3, not \", #, \"!\"\r\n.endif")]
	[PluginName("fail")]
	public class Fail : Echo {
		protected override void OutputMessage(Compiler compiler, string message) {
			compiler.OnErrorRaised(new Compiler.NotificationEventArgs(compiler, message));
		}

	}
}
