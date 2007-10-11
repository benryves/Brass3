using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;

namespace Variables {
	[Syntax(".var type name [, name [, ... ]]")]
	[Description("Declares a variable.")]
	[SeeAlso(typeof(VarLoc))]
	[Category("Variables")]
	public class Var : IDirective {

		public string Name { get { return Names[0]; } }
		public string[] Names { get { return new string[] { "var" }; } }

		private List<VariableToAllocate> ToAllocate;
		private List<string> AlreadyAllocated;
		internal List<VariableAllocationRegion> VariableLocations;

		public class VariableAllocationRegion : IComparable<VariableAllocationRegion> {

			public int Size;
			public int Offset;

			public int CurrentOffset;

			public int FreeSpace {
				get { return this.Size - (this.CurrentOffset - this.Offset); }
			}

			public int CompareTo(VariableAllocationRegion that) {
				return that.FreeSpace.CompareTo(this.FreeSpace);
			}

			public VariableAllocationRegion(int offset, int size) {
				this.Offset = offset;
				this.Size = size;
				this.CurrentOffset = Offset;
			}
		}

		public class VariableToAllocate : IComparable<VariableToAllocate> {
			
			public TokenisedSource.Token Name;
			public DataStructure DataType;
			public int ArraySize = 1;


			public VariableToAllocate(TokenisedSource.Token name, DataStructure dataType, int arraySize) {
				this.Name = name;
				this.DataType = dataType;
				this.ArraySize = arraySize;
			}

			public int Size {
				get { return this.ArraySize * this.DataType.Size; }
			}

			public int CompareTo(VariableToAllocate that) {
				return that.Size.CompareTo(this.Size);
			}
		}

		public void Invoke(Compiler compiler, TokenisedSource source, int index, string directive) {

			if (compiler.CurrentPass == AssemblyPass.Pass2) return;

			if (index > source.Tokens.Length - 3) throw new DirectiveArgumentException(source.Tokens[index], "Variable declarations require at least a type and a name.");

			source.GetCommaDelimitedArguments(index + 1);

			DataStructure VarType = compiler.GetStructureByName(source.Tokens[index + 1].Data);
			if (VarType == null) throw new CompilerExpection(source.Tokens[index + 1], string.Format("Unrecognised variable type '{0}'.", source.Tokens[index + 1].Data));

			int NameListIndex = index + 2;
			int ElementCount = 1;
			if (source.Tokens[NameListIndex].Data == "[") {
				NameListIndex = source.GetCloseBracketIndex(NameListIndex);
				source.ResetExpressionIndices();
				for (int i = index + 3; i < NameListIndex; i++) {
					source.Tokens[i].ExpressionGroup = 1;
				}

				ElementCount = (int)source.EvaluateExpression(compiler, 1).Value;
				++NameListIndex;
			}

			foreach (int NameArg in source.GetCommaDelimitedArguments(NameListIndex, 1, int.MaxValue)) {
				TokenisedSource.Token T = source.GetExpressionToken(NameArg);
				if (!Label.IsValidLabelName(T.Data)) {
					throw new CompilerExpection(T, string.Format("Invalid variable name '{0}'.", T.Data));
				}

				TokenisedSource.Token VarName = T.Clone(compiler.Labels.ModuleGetFullPath(T.Data)) as TokenisedSource.Token;
				string VarNameLower = VarName.Data.ToLowerInvariant();

				if (AlreadyAllocated.Contains(VarNameLower)) throw new CompilerExpection(T, string.Format("Variable '{0}' already declared.", VarName.Data));

				ToAllocate.Add(new VariableToAllocate(T, VarType, ElementCount));
				AlreadyAllocated.Add(VarNameLower);
			}
			
		}


		public Var(Compiler c) {
			this.ToAllocate = new List<VariableToAllocate>();
			this.AlreadyAllocated = new List<string>();
			this.VariableLocations = new List<VariableAllocationRegion>();
			c.PassBegun += new EventHandler(c_PassBegun);
			c.PassEnded += new EventHandler(c_PassEnded);
		}

		void c_PassEnded(object sender, EventArgs e) {
			// Perform the allocation..!
			Compiler compiler = sender as Compiler;

			if (compiler.CurrentPass == AssemblyPass.Pass1 && this.ToAllocate.Count > 0) {

				compiler.Labels.CurrentModule = "";

				// Sort in order biggest->smallest.
				this.ToAllocate.Sort();


				// Check if there are any variable slots free:
				if (this.VariableLocations.Count == 0) throw new CompilerExpection((TokenisedSource)null, "No variable locations defined.");

				// Allocate each variable in turn:
				foreach (VariableToAllocate Var in this.ToAllocate) {

					// Sort by free space:
					this.VariableLocations.Sort();

					if (this.VariableLocations[0].FreeSpace < Var.Size) throw new CompilerExpection(Var.Name, string.Format("Not enough free space for variable '{0}'.", Var.Name.Data));
					Label StructLabel = compiler.Labels.Create(Var.Name);
					StructLabel.Value = this.VariableLocations[0].CurrentOffset;
					StructLabel.Type = Var.DataType;
					foreach (KeyValuePair<string, DataStructure.Field> Field in Var.DataType.GetAllFields()) {
						TokenisedSource.Token StructField = new TokenisedSource.Token(LabelCollection.ModuleCombine(Var.Name.Data, Field.Key));
						Label StructFieldLabel = compiler.Labels.Create(StructField);
						StructFieldLabel.Value = this.VariableLocations[0].CurrentOffset + Field.Value.Offset;
						StructFieldLabel.Type = Field.Value.DataType;						
					}

					
					this.VariableLocations[0].CurrentOffset += Var.Size;

				}
			}
		}

		void c_PassBegun(object sender, EventArgs e) {
			this.ToAllocate.Clear();
			this.AlreadyAllocated.Clear();
			this.VariableLocations.Clear();
		}
	}
}
