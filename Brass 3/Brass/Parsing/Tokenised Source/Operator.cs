using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Brass3.Attributes;

namespace Brass3 {
	public partial class TokenisedSource {

		private class Operator : IComparable {

			#region Types

			/// <summary>
			/// Defines the function of the operator.
			/// </summary>
			public enum OperatorType {
				LabelAccess = 16 * 0,
				// Unary
				UnaryAddition = 16 * 1,
				UnarySubtraction,
				UnaryLogicalNot,
				UnaryBitwiseNot,
				UnaryIncrement,
				UnaryDecrement,
				// Power
				Power = 16 * 2,
				// Arithmetic: multiplicative
				Modulo = 16 * 3,
				Division,
				Multiplication,
				// Arithmetic: additive
				Addition = 16 * 4,
				Subtraction,
				// Shift
				ShiftLeft = 16 * 5,
				ShiftRight,
				// Relational and type testing
				GreaterOrEqualTo = 16 * 6,
				LessOrEqualTo,
				GreaterThan,
				LessThan,
				// Equality
				NotEqual = 16 * 7,
				Equal,
				// Bitwise
				BitwiseAnd = 16 * 8,
				BitwiseXor = 16 * 9,
				BitwiseOr = 16 * 10,
				// Conditional    
				LogicalAnd = 16 * 11,
				LogicalOr = 16 * 12,
				// Ternary conditional
				ConditionalQuery = 16 * 13,
				ConditionalResultSplitter,
				// Assignment
				AssignmentEqual = 16 * 14,
				AssignmentAddition,
				AssignmentSubtraction,
				AssignmentMultiplication,
				AssignmentDivision,
				AssignmentModulo,
				AssignmentBitwiseAnd,
				AssignmentBitwiseOr,
				AssignmentBitwiseXor,
				AssignmentShiftLeft,
				AssignmentShiftRight,
				// Indexing
				IndexingOpen = 16 * 15,
			}
			
			#endregion

			#region Properties

			/// <summary>
			/// Returns the name of the operator.
			/// </summary>
			/// <returns></returns>
			public override string ToString() {
				return this.Type.ToString();
			}

			public bool IsRightAssociative {
				get {
					if (this.IsAssignment) return true;
					return this.OperandCount != 2;
					
				}
			}

			/// <summary>
			/// Returns the number of operands for the operator.
			/// </summary>
			public int OperandCount {
				get {
					switch (this.Type) {
						case OperatorType.ConditionalQuery:
						case OperatorType.ConditionalResultSplitter:
							return 3;
						case OperatorType.UnaryAddition:
						case OperatorType.UnaryBitwiseNot:
						case OperatorType.UnaryDecrement:
						case OperatorType.UnaryIncrement:
						case OperatorType.UnaryLogicalNot:
						case OperatorType.UnarySubtraction:
						case OperatorType.LabelAccess:
							return 1;
						default:
							return 2;
					}
				}
			}

			/// <summary>
			/// Returns true if the operator performs an assignment of some description.
			/// </summary>
			public bool IsAssignment {
				get {
					switch (this.Type) {
						case OperatorType.AssignmentAddition:
						case OperatorType.AssignmentBitwiseAnd:
						case OperatorType.AssignmentBitwiseOr:
						case OperatorType.AssignmentBitwiseXor:
						case OperatorType.AssignmentDivision:
						case OperatorType.AssignmentEqual:
						case OperatorType.AssignmentModulo:
						case OperatorType.AssignmentMultiplication:
						case OperatorType.AssignmentShiftLeft:
						case OperatorType.AssignmentShiftRight:
						case OperatorType.AssignmentSubtraction:
						case OperatorType.UnaryIncrement:
						case OperatorType.UnaryDecrement:
							return true;
						default:
							return false;
					}
				}
			}

			#endregion

			#region Public Fields

			/// <summary>
			/// Represents the operator's function.
			/// </summary>
			public OperatorType Type;

			/// <summary>
			/// Represents how deeply nested this operator is in the expression.
			/// </summary>
			public int BraceDepth;

			/// <summary>
			/// Represents the token that this operator is constructed from.
			/// </summary>
			public TokenisedSource.Token Token;

			/// <summary>
			/// Represents the position of the operator within an expression.
			/// </summary>
			public LinkedListNode<LabelAccessor> ExpressionPosition;

			#endregion


			#region Public Methods

			/// <summary>
			/// Compares the order of precedence of two operators.
			/// </summary>
			/// <param name="obj">The other operator to compare with.</param>
			public int CompareTo(object obj) {
				Operator that = (Operator)obj;
				if (this.BraceDepth != that.BraceDepth) {
					return that.BraceDepth.CompareTo(this.BraceDepth);
				} else if (((int)this.Type >> 4) != ((int)that.Type >> 4)) {
					return this.Type.CompareTo(that.Type);
				} else {
					return this.Token.SourcePosition.CompareTo(that.Token.SourcePosition) * (this.IsRightAssociative ? -1 : +1);
				}
			}

			#endregion

			#region Constructor

			/// <summary>
			/// Creates an instance of an Operator from a token.
			/// </summary>
			/// <param name="token">The token to create the operator for.</param>
			/// <param name="position">The position in the expression of the operator.</param>
			/// <param name="braceIndex">How deeply nested the operator is in the expression.</param>
			public Operator(Token token, LinkedListNode<LabelAccessor> expressionPosition, int braceDepth) {
				this.Token = token;
				this.ExpressionPosition = expressionPosition;
				this.BraceDepth = braceDepth;
				switch (token.Data) {
					case "+": this.Type = OperatorType.Addition; break;
					case "-": this.Type = OperatorType.Subtraction; break;
					case "*": this.Type = OperatorType.Multiplication; break;
					case "/": this.Type = OperatorType.Division; break;
					case "!": this.Type = OperatorType.UnaryLogicalNot; break;
					case "++": this.Type = OperatorType.UnaryIncrement; break;
					case "--": this.Type = OperatorType.UnaryDecrement; break;
					case "**": this.Type = OperatorType.Power; break;
					case "%": this.Type = OperatorType.Modulo; break;
					case "<<": this.Type = OperatorType.ShiftLeft; break;
					case ">>": this.Type = OperatorType.ShiftRight; break;
					case ">=": this.Type = OperatorType.GreaterOrEqualTo; break;
					case "<=": this.Type = OperatorType.LessOrEqualTo; break;
					case ">": this.Type = OperatorType.GreaterThan; break;
					case "<": this.Type = OperatorType.LessThan; break;
					case "!=": this.Type = OperatorType.NotEqual; break;
					case "==": this.Type = OperatorType.Equal; break;
					case "&": this.Type = OperatorType.BitwiseAnd; break;
					case "^": this.Type = OperatorType.BitwiseXor; break;
					case "|": this.Type = OperatorType.BitwiseOr; break;
					case "=": this.Type = OperatorType.AssignmentEqual; break;
					case "+=": this.Type = OperatorType.AssignmentAddition; break;
					case "-=": this.Type = OperatorType.AssignmentSubtraction; break;
					case "*=": this.Type = OperatorType.AssignmentMultiplication; break;
					case "/=": this.Type = OperatorType.AssignmentDivision; break;
					case "%=": this.Type = OperatorType.AssignmentModulo; break;
					case "&=": this.Type = OperatorType.AssignmentBitwiseAnd; break;
					case "|=": this.Type = OperatorType.AssignmentBitwiseOr; break;
					case "&&": this.Type = OperatorType.LogicalAnd; break;
					case "||": this.Type = OperatorType.LogicalOr; break;
					case "<<=": this.Type = OperatorType.AssignmentShiftLeft; break;
					case ">>=": this.Type = OperatorType.AssignmentShiftRight; break;
					case ":": this.Type = OperatorType.LabelAccess; break;
					case "?": this.Type = OperatorType.ConditionalQuery; break;
					case "~": this.Type = OperatorType.UnaryBitwiseNot; break;
					case "[": this.Type = OperatorType.IndexingOpen; break;
					//case "]": this.Type = OperatorType.IndexingClose; break;
					default: throw new ArgumentException();
				}
			}

			#endregion
		}
	}
}
