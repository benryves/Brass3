using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace PindurTI {
    public partial class Calculator {

        /// <summary>
        /// Set up the timer to keep the calculator ticking over.
        /// </summary>
        private void InitTimer() {
            RunTimer = new Timer(40.0d);
            RunTimer.Elapsed += new ElapsedEventHandler(Run);
            RunTimer.Start();
        }

        /// <summary>
        /// Timer that 'ticks' the calculator every so often.
        /// </summary>
        private Timer RunTimer = null;

        /// <summary>
        /// Let the calculator run or not.
        /// </summary>
        private bool running = true;
        public bool Running {
            get {
                if (RunTimer != null) {
                    return RunTimer.Enabled;
                } else {
                    return running;
                }
            }
            set {
                if (RunTimer != null) {
                    RunTimer.Enabled = value;
                } else {
                    running = value;
                }
            }
        }

        /// <summary>
        /// Clock speed of the emulated calculator in megahertz.
        /// </summary>
        public double ClockSpeed = 6.0d;
        
        /// <summary>
        /// The time of the last 'tick'.
        /// </summary>
        private DateTime? LastRun = null;

        /// <summary>
        /// Timer handler that runs the calculator for the elapsed time
        /// </summary>
        private void Run(object sender, ElapsedEventArgs e) {
            Run(e.SignalTime);
        }

        public void Step() {
            Pindur.SendCommand(this, Emulator.CommandType.Step, 1);
            RaisedBreakpointNotificaction = false;
            UpdateRegisters();
            ScreenDirty = true;
            LastRun = DateTime.Now;
        }

        public void Run() {
            Run(DateTime.Now);
        }

        public void Run(DateTime SignalTime) {
            
            if (LastRun == null) {
                LastRun = SignalTime;
                return;
            }
            int TicksToRun = (int)Math.Round(((TimeSpan)(SignalTime - LastRun)).TotalMilliseconds * this.ClockSpeed * 1000.0d, 0);
            if (TicksToRun == 0) {
                return;
            } else {
                Pindur.SendCommand(this, Emulator.CommandType.Run, TicksToRun);
                ScreenDirty = true;
                RegistersDirty = true;
                UpdateRegisters();
            }
            LastRun = SignalTime;
        }

        public void RunAbsolute(int Ticks) {
            ClearRunning();
            Pindur.SendCommand(this, Emulator.CommandType.Run, Ticks);
        }

        public void ClearRunning() {
            LastRun = DateTime.Now;
        }
    }
}
