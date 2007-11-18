using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
namespace TexasInstruments.Brass.Directives {
	
	[Category("Texas Instruments")]
	[Description("Sets a field in the application header to a particular value.")]
	[Syntax(".appfield field, value")]
	[Remarks(@"The application's name is set with the <see ref=""tivariablename""/> directive.
<table>
    <tr>
        <th>Field</th>
        <th>Name</th>
        <th>Description</th>
        <th>Type</th>
        <th>Default</th>
    </tr>
    <tr>
        <td><tt>revision</tt></td>
        <td>Program Revision</td>
        <td>The version of the application.</td>
        <td>Number (byte)</td>
        <td>1</td>
    </tr>
    <tr>
        <td><tt>build</tt></td>
        <td>Build Number</td>
        <td>Often used a sub-version. For example: v1.0 Build 10.</td>
        <td>Number (byte)</td>
        <td>1</td>
    </tr>
    <!--<tr>
        <td><tt>expires</tt></td>
        <td>Program Expiration Date</td>
        <td>Date the application is valid until.</td>
        <td>Date</td>
        <td>None</td>
    </tr>-->
    <tr>
        <td><tt>uses</tt></td>
        <td>Overuse Count</td>
        <td>How many times the application can be run. Must be at least 16.</td>
        <td>Number (byte)</td>
        <td>None</td>
    </tr>
    <tr>
        <td><tt>nosplash</tt></td>
        <td>Disable TI Spash Screen</td>
        <td>Ensures the TI splash screen isn't displayed.</td>
        <td>Boolean</td>
        <td>True</td>
    </tr>
    <tr>
        <td><tt>hardware</tt></td>
        <td>Maximum Hardware Revision</td>
        <td>Maximum hardware revision the application is valid on.</td>
        <td>Number (byte)</td>
        <td>None</td>
    </tr>
    <tr>
        <td><tt>basecode</tt></td>
        <td>Lowest Basecode</td>
        <td>The lowest basecode that the application can run on.</td>
        <td>OS Version (maj.minor)</td>
        <td>0.00</td>
    </tr>
</table>")]
	[SeeAlso(typeof(AppHeader))]
	public class AppField : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			if (compiler.CurrentPass == AssemblyPass.WritingOutput) {

				AppHeader Header = compiler.GetPluginInstanceFromType<AppHeader>();

				if (Header == null) throw new CompilerExpection(source, "appheader plugin not loaded.");

				object[] Args = source.GetCommaDelimitedArguments(compiler, index + 1, new TokenisedSource.ArgumentType[] { TokenisedSource.ArgumentType.SingleToken | TokenisedSource.ArgumentType.String, TokenisedSource.ArgumentType.Value });

				double Value = (double)Args[1];

				switch ((Args[0] as string).ToLowerInvariant()) {
					case "revision":
						Header.ProgramRevision = (byte)Value;
						break;
					case "build":
						Header.BuildNumber = (byte)Value;
						break;
					case "uses":
						Header.OveruseCount = (byte)Value;
						break;
					case "nosplash":
						Header.DisableTISplashScreen = (bool)Convert.ChangeType(Value, typeof(bool));
						break;
					case "hardware":
						Header.MaximumHardwareRevision = (byte)Value;
						break;
					case "basecode":
						Header.LowestBasecode = (decimal)Value;
						break;
					default:
						compiler.OnWarningRaised(string.Format("Application field '{0}' not recognised.", Args[0] as string), source);
						break;
				}

			}
		}

	}
}
