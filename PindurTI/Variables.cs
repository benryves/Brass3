using System;
using System.Collections.Generic;
using System.Text;

namespace PindurTI {
    public partial class CalcWindow {
        
        public interface IVariable {
            string FormattedValue { get; }
            string HexValue { get; }
            int Address { set; }
            CalcWindow Owner { set; }            
        }

        public class TiFloat : IVariable {

         private CalcWindow owner;
            public CalcWindow Owner { set { owner = value; } }

            private byte[] oldMemory;

            private bool HasChanged {
                get {
                    if (formattedValue == null || hexValue == null) return true;
                    bool hasChanged = false;
                    for (int i = StartAddress; i <= EndAddress; ++i) {
                        if (owner.Calc.Memory[i] != oldMemory[i - StartAddress]) {
                            hasChanged = true;
                            oldMemory[i - StartAddress] = owner.Calc.Memory[i];
                        }
                    }
                    return hasChanged;
                }
            }

            string formattedValue;
            public string FormattedValue {
                get {
                    if (HasChanged) {
                        string HexVal = "";
                        for (int i = StartAddress + 2; i <= EndAddress; ++i) HexVal += owner.Calc.Memory[i].ToString("X2");
                        double Mantissa;
                        if (!double.TryParse(HexVal, out Mantissa)) {
                            formattedValue = "(Invalid)";
                        }
                        int Exponent = owner.Calc.Memory[(StartAddress + 1) & 0xFFFF] - 0x80;
                        Mantissa = Math.Pow(10, Exponent) / 10000000000000 * Mantissa;
                        if ((owner.Calc.Memory[StartAddress] & 0x80) == 0) {
                            formattedValue = Mantissa.ToString();
                        } else {
                            formattedValue = "-" + Mantissa.ToString();
                        }
                    }
                    return formattedValue;
                }
            }

            string hexValue;
            public string HexValue {
                get {
                    if (HasChanged) {
                        hexValue = "";
                        for (int i = StartAddress; i <= EndAddress; ++i) hexValue += owner.Calc.Memory[i].ToString("X2");
                    }
                    return hexValue;
                }
            }

            private int StartAddress;
            private int EndAddress;
            private int address;
            public int Address {
                set {
                    this.address = Math.Min(65535, Math.Max(0, value));
                    this.StartAddress = this.address;
                    this.EndAddress = Math.Min(65535, Math.Max(0, StartAddress + 8));
                    this.oldMemory = new byte[9];
                }
            }

        }

        public class AsciiType : IVariable {
            public string FormattedValue {
                get { return "'" + (char)owner.Calc.Memory[address] + "'"; }
            }

            public string HexValue {
                get { return (owner.Calc.Memory[address]).ToString("X2"); }
            }

            private int address;
            public int Address {
                set { this.address = value; }
            }

            private CalcWindow owner;
            public CalcWindow Owner {
                set { this.owner = value; }
            }
        }


        public class FixedPointType : IVariable {
                
            private CalcWindow owner;
            public CalcWindow Owner { set { owner = value; } }

            int StartAddress = 0;
            int EndAddress = 0;

            double SizeVal;
            double MaxPos;

            double DivFactor = 1;

            private byte[] oldMemory;

            private bool HasChanged {
                get {
                    if (formattedValue == null || hexValue == null) return true;
                    bool hasChanged = false;
                    for (int i = StartAddress; i <= EndAddress; ++i) {
                        if (owner.Calc.Memory[i] != oldMemory[i - StartAddress]) {
                            hasChanged = true;
                            oldMemory[i - StartAddress] = owner.Calc.Memory[i];
                        }
                    }
                    return hasChanged;
                }
            }

            string formattedValue;
            public string FormattedValue {
                get {
                    if (HasChanged) {
                        double RetVal = 0;
                        for (int i = EndAddress; i >= StartAddress; --i) {
                            RetVal *= 256.0d;
                            RetVal += (double)owner.Calc.Memory[i];
                        }
                        // Now we have the 'full' value, remove the bit after the decimal point
                        RetVal /= DivFactor;
                        if (Unsigned || RetVal < MaxPos) {
                            formattedValue = RetVal.ToString();
                        } else {
                            formattedValue = ((double)(RetVal - SizeVal)).ToString();
                        }
                    }
                    return formattedValue;
                }
            }

            string hexValue;
            public string HexValue {
                get {
                    if (HasChanged) {
                        hexValue = "";
                        for (int i = StartAddress; i <= EndAddress; ++i) hexValue += owner.Calc.Memory[i].ToString("X2");
                    }
                    return hexValue;                    
                }
            }

            private int Size {
                get {
                    return (A + B - 1) / 8 + 1;
                }
            }

            private int A;
            private int B;
            private bool Unsigned;
            public FixedPointType(int A, int B, bool Unsigned) {
                this.A = A;
                this.B = B;
                this.Unsigned = Unsigned;
                this.DivFactor = Math.Pow(2.0d, B);
                this.SizeVal = Math.Pow(2.0d, A);
                this.MaxPos = SizeVal / 2.0d;  
            }

            private int address;
            public int Address {
                set {
                    this.address = Math.Min(65535, Math.Max(0, value));
                    this.StartAddress = this.address;
                    this.EndAddress = Math.Min(65535, Math.Max(0, StartAddress + Size - 1));
                    this.oldMemory = new byte[Size];
                    formattedValue = null;
                    hexValue = null;
                }
            }

        }

        public class IntegerType : IVariable {
            
            private CalcWindow owner;
            public CalcWindow Owner { set { owner = value; } }

            double SizeVal;
            double MaxPos;

            private string formattedValue;
            private byte[] oldMemory;

            private bool HasChanged {
                get {
                    if (formattedValue == null || hexValue == null) return true;
                    bool hasChanged = false;
                    for (int i = StartAddress; i <= EndAddress; ++i) {
                        if (owner.Calc.Memory[i] != oldMemory[i - StartAddress]) {
                            hasChanged = true;
                            oldMemory[i - StartAddress] = owner.Calc.Memory[i];
                        }
                    }
                    return hasChanged;
                }
            }

            public string FormattedValue {
                get {
                    if (HasChanged) {
                        double RetVal = 0;
                        for (int i = EndAddress; i >= StartAddress; --i) {
                            RetVal *= 256.0d;
                            RetVal += (double)owner.Calc.Memory[i];
                        }
                        if (Unsigned || RetVal < MaxPos) {
                            formattedValue = RetVal.ToString();
                        } else {
                            formattedValue = ((double)(RetVal - SizeVal)).ToString();
                        }
                    }
                    return formattedValue;
                }
            }

            private string hexValue;
            public string HexValue {
                get {
                    if (HasChanged) {
                        hexValue = "";
                        for (int i = StartAddress; i <= EndAddress; ++i) hexValue += owner.Calc.Memory[i].ToString("X2");
                    }
                    return hexValue;
                }
            }

            private int Size;
            private bool Unsigned;
            public IntegerType(int Size, bool Unsigned) {
                this.Size = Size;
                this.Unsigned = Unsigned;
                this.SizeVal = Math.Pow(2.0d, Size * 8);
                this.MaxPos = SizeVal / 2.0d;                
            }

            int StartAddress = 0;
            int EndAddress = 0;

            private int address;
            public int Address {
                set {
                    this.address = Math.Min(65535, Math.Max(0, value));
                    this.StartAddress = this.address;
                    this.EndAddress = Math.Min(65535, Math.Max(0, StartAddress + (Size - 1)));
                    oldMemory = new byte[this.Size];
                    formattedValue = null;
                    hexValue = null;
                }
            }

            
        }

    }
}
