using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PindurTI {
    public partial class Calculator {
        private bool RegistersDirty = true;

        private void UpdateRegisters() {
            if (RegistersDirty == false) return;
            Registers.RegisterValues = (int[])Pindur.SendCommand(this, Emulator.CommandType.DumpState, Emulator.DumpStateType.Cpu);
            RegistersDirty = false;
        }

        public class RegisterList {
            public int[] RegisterValues = new int[16];

            [Category("Registers")]
            [DisplayName("AF")]
            public string AF { get { return RegisterValues[0].ToString("X4"); } }
            [Category("Registers")]
            [DisplayName("BC")]
            public string BC { get { return RegisterValues[1].ToString("X4"); } }
            [Category("Registers")]
            [DisplayName("DE")]
            public string DE { get { return RegisterValues[2].ToString("X4"); } }
            [Category("Registers")]
            [DisplayName("HL")]
            public string HL { get { return RegisterValues[3].ToString("X4"); } }
            [Category("Registers")]
            [DisplayName("IX")]
            public string IX { get { return RegisterValues[4].ToString("X4"); } }
            [Category("Registers")]
            [DisplayName("IY")]
            public string IY { get { return RegisterValues[5].ToString("X4"); } }

            [Category("Program Control")]
            [DisplayName("PC")]
            [Description("Program counter.")]
            public string PC { get { return RegisterValues[6].ToString("X4"); } }
            [Category("Program Control")]
            [DisplayName("SP")]
            [Description("Stack pointer.")]
            public string SP { get { return RegisterValues[7].ToString("X4"); } }

            [Category("Shadow Registers")]
            [DisplayName("AF'")]
            public string AFs { get { return RegisterValues[8].ToString("X4"); } }
            [Category("Shadow Registers")]
            [DisplayName("BC'")]
            public string BCs { get { return RegisterValues[9].ToString("X4"); } }
            [Category("Shadow Registers")]
            [DisplayName("DE'")]
            public string DEs { get { return RegisterValues[10].ToString("X4"); } }
            [Category("Shadow Registers")]
            [DisplayName("HL'")]
            public string HLs { get { return RegisterValues[11].ToString("X4"); } }

            [Description("Memory refresh register.")]
            public string R { get { return RegisterValues[12].ToString("X2"); } }
            public string I { get { return RegisterValues[13].ToString("X2"); } }

            [Description("Interrupt mode.")]
            public string IM { get { return RegisterValues[14].ToString("X2"); } }

            public string Halt { get { return RegisterValues[15].ToString("X2"); } }

            [Category("Flags")]
            [DisplayName("C")]
            [Description("Carry flag.")]
            public int C { get { return (RegisterValues[0] & (1 << 0)) == 0 ? 0 : 1; } }
            [Category("Flags")]
            [DisplayName("N")]
            [Description("Subtraction flag.")]
            public int N { get { return (RegisterValues[0] & (1 << 1)) == 0 ? 0 : 1; } }
            [Category("Flags")]
            [DisplayName("P/V")]
            [Description("Parity/Overflow flag.")]
            public int PV { get { return (RegisterValues[0] & (1 << 2)) == 0 ? 0 : 1; } }
            [Category("Flags")]
            [DisplayName("H")]
            [Description("BCD half-carry flag.")]
            public int H { get { return (RegisterValues[0] & (1 << 4)) == 0 ? 0 : 1; } }
            [Category("Flags")]
            [DisplayName("Z")]
            [Description("Zero flag.")]
            public int Z { get { return (RegisterValues[0] & (1 << 6)) == 0 ? 0 : 1; } }
            [Category("Flags")]
            [DisplayName("S")]
            [Description("Sign flag.")]
            public int S { get { return (RegisterValues[0] & (1 << 7)) == 0 ? 0 : 1; } }


        }

        public RegisterList Registers = new RegisterList();

    }
}
