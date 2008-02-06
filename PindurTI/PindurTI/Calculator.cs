using System;
using System.Collections.Generic;
using System.Text;

namespace PindurTI {
    public partial class Calculator {

        /// <summary>
        /// Reference of emulated calculator
        /// </summary>
        private Emulator Pindur = null;

        private int? _Id = null;
        public int Id {
            get { return (int)_Id; }
            set {
                if (_Id == null) {
                    _Id = value;
                } else {
                    throw new Exception("Id can only be set once.");
                }
            }
        }

        public byte[] Memory {
            get {
                return (byte[])Pindur.SendCommand(this, Emulator.CommandType.DumpState, Emulator.DumpStateType.Memory);
            }
        }

        public string Model {
            get {
                Dictionary<string, string> ModelName = (Dictionary<string, string>)Pindur.SendCommand(this, Emulator.CommandType.DumpState, Emulator.DumpStateType.Model);
                return ModelName["Model"];
            }
        }
        
        public Calculator(Emulator Pindur, string RomFile, bool SelfTiming) {
            this.Pindur = Pindur;
            Pindur.BreakpointHit += new Emulator.BreakpointHitEventHandler(Pindur_BreakpointHit);
            if (!Pindur.AttachCalculator(this)) {
                throw new Exception("Not enough slots to create new calculator (please remove one then try again)");
            }
            Pindur.SendCommand(this, Emulator.CommandType.SendFile, RomFile);

            if (SelfTiming) InitTimer();
            Breakpoints = new BreakpointList(this);
            RecalculateColourRange();
        }



        public delegate void BreakpointHitEventHandler(object sender, Emulator.BreakpointArgs e);
        public event BreakpointHitEventHandler BreakpointHit;
        protected virtual void OnBreakpointHit(Emulator.BreakpointArgs e) {
            if (BreakpointHit != null) {
                RegistersDirty = true;
                UpdateRegisters();
                BreakpointHit(this, e);
            }
        }

        private bool RaisedBreakpointNotificaction = false;
        void Pindur_BreakpointHit(object sender, Emulator.BreakpointArgs e) {
            if (!RaisedBreakpointNotificaction && e.Owner == this) {
                RaisedBreakpointNotificaction = true;
                OnBreakpointHit(e);
            }
        }


        public void Close() {
            if (Pindur != null) Pindur.RemoveCalculator(this);
            if (_ScreenImage != null) _ScreenImage.Dispose();
        }
        ~Calculator() {
            Close();
        }
      

        public void Reset() {
            Pindur.SendCommand(this, Emulator.CommandType.Reset, null);
            UpdateRegisters();
        }

        public void SendFile(string Filename) {
            Pindur.SendCommand(this, Emulator.CommandType.SendFile, Filename);
        }

    }
}
