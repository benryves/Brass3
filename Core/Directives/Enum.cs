using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[Description("Declare an enumeration, a module containing a list of named constants as labels.")]
	[Syntax(".enum name\r\n\tlabel [= expression],\r\n\tlabel [= expression],\r\n\t...\r\n\tlabel [= expression]")]
	[ParserBehaviourChange(ParserBehaviourChangeAttribute.ParserBehaviourModifiers.IgnoreFirstNewLine)]
	[Category("Labels")]
	[CodeExample("Defines an enumeration for the days of the week.", ".enum Days\r\n\tSunday,\r\n\tMonday,\r\n\tTuesday,\r\n\tWednesday,\r\n\tThursday,\r\n\tFriday,\r\n\tSaturday\r\n\\\r\n\r\n/*\r\nThis creates the following labels:\r\nDays.Sunday   = 0\r\nDays.Monday   = 1\r\nDays.Tuesday  = 2\r\n...\r\nDays.Saturday = 6\r\n*/")]
	[CodeExample("Initialisers can be used to manually control enumeration values.", ".enum Numbers\r\n\tEleven = 11,\r\n\tTwelve,\r\n\tThirteen,\r\n\tTwenty = 20,\r\n\tVingt = 20,\r\n\tZwanzig = 20,\r\n\tTwentyOne,\r\n\tFour = 4,\r\n\tFive\r\n\\\r\n\r\n.echoln Numbers.Eleven    ; 11\r\n.echoln Numbers.Twelve    ; 12\r\n.echoln Numbers.Thirteen  ; 13\r\n.echoln Numbers.Twenty    ; 20\r\n.echoln Numbers.Vingt     ; 20\r\n.echoln Numbers.Zwanzig   ; 20\r\n.echoln Numbers.TwentyOne ; 21\r\n.echoln Numbers.Four      ; 4\r\n.echoln Numbers.Five      ; 5")]
	[Warning("You can manually assign two labels with the same value. This is intended behaviour.")]
	public class Enum : IDirective {

		public string[] Names {
			get { return new string[] { "enum" }; }
		}

		public string Name { get { return Names[0]; } }


		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {
			int[] Args = source.GetCommaDelimitedArguments(index + 2);

			TokenisedSource.Token EnumName = source.Tokens[index + 1];
			compiler.Labels.EnterModule(EnumName.Name);
			
			Label DefaultCreator = new Label(compiler.Labels, 0, false);
			compiler.Labels.ImplicitCreationDefault = DefaultCreator;
			
			foreach (int i in Args) {

				Label L = source.EvaluateExpression(compiler, i);
				if (L.Created) DefaultCreator.Value = double.MinValue;
				L.SetImplicitlyCreated();

				DefaultCreator.Value = Math.Max(DefaultCreator.Value, L.Value) + 1;
			}

			compiler.Labels.ImplicitCreationDefault = compiler.Labels.ProgramCounter;

			compiler.Labels.LeaveModule();

		}

	}
}
