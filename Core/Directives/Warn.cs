using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Syntax(".warn expression [, expression [, ... ]]")]
	[Description("Displays a warning. It follows the same general rules as <c>.echo</c>")]
	[Remarks("Expressions can be either numeric or string constants. This directive is only invoked during the second pass.")]
	[CodeExample(".if $-start > 8*1024 && platform == ti8x\r\n.warn \"Beware the 8KB limit!\"\r\n.endif")]
	[PluginName("warn")]
	public class Warn : Echo {
		protected override void OutputMessage(Compiler compiler, string message) {
			compiler.OnWarningRaised(new Compiler.NotificationEventArgs(compiler, message));
		}

	}
}
