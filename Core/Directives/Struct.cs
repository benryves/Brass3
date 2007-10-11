using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[ParserBehaviourChange(ParserBehaviourChangeAttribute.ParserBehaviourModifiers.IgnoreFirstNewLine)]
	public class Struct : IDirective {

		public string[] Names {
			get { return new string[] { "struct" }; }
		}

		public string Name { get { return Names[0]; } }


		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			int[] Args = source.GetCommaDelimitedArguments(index + 2);

			TokenisedSource.Token StructName = source.Tokens[index + 1];

			int CurrentFieldOffset = 0;

			DataStructure NewStruct = new DataStructure(StructName.Data);

			foreach (int Arg in Args) {
				TokenisedSource Field = source.GetExpressionTokens(Arg).Clone() as TokenisedSource;
				int ArraySize = 1;

				if (Field.Tokens.Length < 2) throw new CompilerExpection(Field, "Invalid structure field declaration.");
				if (Field.Tokens.Length > 2) {
					if (Field.Tokens[1].Data != "[") throw new CompilerExpection(Field, "Invalid structure field array declaration.");

					Field.ResetExpressionIndices();
					for (int i = 2; i < Field.Tokens.Length; ++i) {
						if (Field.Tokens[i].Data == "]") {
							if (i != Field.Tokens.Length - 2) throw new CompilerExpection(Field, "Invalid structure field array declaration.");
							break;
						}
						if (Field.Tokens[i].Data == "[") throw new CompilerExpection(Field, "Invalid structure field array declaration.");
						Field.Tokens[i].ExpressionGroup = 1;
					}

					ArraySize = (int)(Field.EvaluateExpression(compiler, 1).Value);
				}

				string FieldName = Field.Tokens[Field.Tokens.Length - 1].Data;
				DataStructure FieldType = compiler.GetStructureByName(Field.Tokens[0].Data);
				if (FieldType == null) throw new CompilerExpection(Field.Tokens[0], "Undefined data type '" + Field.Tokens[0].Data + "'.");


				DataStructure.Field F = new DataStructure.Field(FieldName, FieldType, CurrentFieldOffset, ArraySize);
				NewStruct.Fields.Add(F);

				CurrentFieldOffset += F.Size;
			}

			compiler.DataStructures.Add(NewStruct);


		}

	}
}
