using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using BeeDevelopment.Brass3.Attributes;
using System.ComponentModel;

namespace Core.Directives {

	[ParserBehaviourChange(ParserBehaviourChangeAttribute.ParserBehaviourModifiers.IgnoreFirstNewLine)]
	public class Struct : IDirective {

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			int[] Args = source.GetCommaDelimitedArguments(index + 2);

			TokenisedSource.Token StructName = source.Tokens[index + 1];

			int CurrentFieldOffset = 0;

			DataStructure NewStruct = new DataStructure(StructName.Data);

			foreach (int Arg in Args) {
				TokenisedSource Field = source.GetExpressionTokens(Arg).Clone() as TokenisedSource;
				int ArraySize = 1;

				if (Field.Tokens.Length < 2) throw new CompilerException(Field, Strings.ErrorStructInvalidFieldDeclaration);
				if (Field.Tokens.Length > 2) {
					if (Field.Tokens[1].Data != "[") throw new CompilerException(Field, Strings.ErrorStructInvalidArrayDeclaration);

					Field.ResetExpressionIndices();
					for (int i = 2; i < Field.Tokens.Length; ++i) {
						if (Field.Tokens[i].Data == "]") {
							if (i != Field.Tokens.Length - 2) throw new CompilerException(Field, Strings.ErrorStructInvalidArrayDeclaration);
							break;
						}
						if (Field.Tokens[i].Data == "[") throw new CompilerException(Field, Strings.ErrorStructInvalidArrayDeclaration);
						Field.Tokens[i].ExpressionGroup = 1;
					}

					ArraySize = (int)(Field.EvaluateExpression(compiler, 1).NumericValue);
				}

				string FieldName = Field.Tokens[Field.Tokens.Length - 1].Data;
				DataStructure FieldType = compiler.GetStructureByName(Field.Tokens[0].Data);
				if (FieldType == null) throw new CompilerException(Field.Tokens[0], string.Format(Strings.ErrorDataTypeNotDefined, Field.Tokens[0].Data));


				DataStructure.Field F = new DataStructure.Field(FieldName, FieldType, CurrentFieldOffset, ArraySize);
				NewStruct.Fields.Add(F);

				CurrentFieldOffset += F.Size;
			}

			compiler.DataStructures.AddRuntimeAlias(NewStruct, NewStruct.Name);


		}

	}
}
