using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PindurTI {
    public partial class Calculator {

        public void KeyDown(string Name) {
            this.Pindur.SendCommand(this, Emulator.CommandType.KeyDown, Name);
        }
        public void KeyUp(string Name) {
            this.Pindur.SendCommand(this, Emulator.CommandType.KeyUp, Name);
        }        

    }
}
