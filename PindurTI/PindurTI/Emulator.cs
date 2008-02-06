using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PindurTI {
    public class Emulator {

        private Process Pindur = null;
        private BinaryReader PindurBinaryOut = null;

        public Emulator() {
            Pindur = new Process();
            Pindur.StartInfo.FileName = Path.Combine(Application.StartupPath, "pindurti.exe");
            Pindur.StartInfo.Arguments = "-p";
            Pindur.StartInfo.UseShellExecute = false;
            Pindur.StartInfo.RedirectStandardInput = true;
            Pindur.StartInfo.RedirectStandardOutput = true;
            Pindur.Start();
            PindurBinaryOut = new BinaryReader(Pindur.StandardOutput.BaseStream);
        }

        ~Emulator() {
            Close();
        }

        public void Close() {
			if (Pindur != null) {
				Pindur.Kill();
				Pindur.Dispose();
				Pindur = null;
			}
        }

        private Calculator[] Calculators = new Calculator[16];

        public bool AttachCalculator(Calculator ToAttach) {
            for (int i = 0; i < 16; ++i) {
                if (Calculators[i] == null) {
                    Calculators[i] = ToAttach;
                    ToAttach.Id = i;
                    return true;
                }
            }
            return false;
        }

        public void RemoveCalculator(Calculator ToRemove) {
            Calculators[ToRemove.Id] = null;
        }

        public enum CommandType {
            SendFile,
            Reset,
            Run,
            Step,
            GetScreen,
            GetLcd,
            KeyDown,
            KeyUp,
            DumpState,
            SetBreakpoint,
            RemoveBreakpoint
        }

        public enum DumpStateType {
            LcdPhysics,
            LcdSoftware,
            Model,
            Time,
            Interrupt,
            Pager,
            Keyboard,
            Link,
            Cpu,
            Memory
        }

        public class Command {
            public readonly Calculator Originator = null;
            public readonly CommandType Type;
            public readonly object Data = null;

            public Command(Calculator Originator, CommandType Type, object Data) {
                this.Originator = Originator;
                this.Type = Type;
                this.Data = Data;
            }
        }

        private int LastActivatedSlot = -1;

        private bool AmCurrentlySending = false;

        private Queue<Command> CommandBacklog = new Queue<Command>();


        private string GetResponse() {
            string Return = "";
            bool WaitingLf = false;

            for (; ; ) {
                byte B = PindurBinaryOut.ReadByte();
                if (WaitingLf) {
                    return Return;
                } else {
                    if (B == '\r') {
                        WaitingLf = true;
                    } else {
                        Return += (char)B;
                    }
                }
            }
        }

        public object SendCommand(Calculator Originator, CommandType Type, object Data) {

            Command ToSend = new Command(Originator, Type, Data);

            
            if (AmCurrentlySending) {
                Console.WriteLine(Originator.Id);                
            }

            AmCurrentlySending = true;

            object ToReturn;
            try {
                ToReturn = SendCommand(ToSend);
            } catch (Exception) {
                AmCurrentlySending = false;
                throw;
            }
            AmCurrentlySending = false;
            return ToReturn;
        }


        public class BreakpointArgs : EventArgs {
            private readonly Calculator owner;

            public Calculator Owner {
                get { return owner; }
            }
            public BreakpointArgs(Calculator Owner) {
                this.owner = Owner;
            }
        }

        public delegate void BreakpointHitEventHandler(object sender, BreakpointArgs e);
        public event BreakpointHitEventHandler BreakpointHit;
        protected virtual void OnBreakpointHit(BreakpointArgs e) {
            if (BreakpointHit != null) {
                BreakpointHit(this, e);
            }
        }


        private object SendCommand(Command ToSend) {

            // Use to check responses
            string Response;

            // Switch to the correct calculator
            if (ToSend.Type != CommandType.SendFile && ToSend.Originator.Id != LastActivatedSlot) {
                Pindur.StandardInput.WriteLine("activate-slot " + ToSend.Originator.Id);
                Response = GetResponse();
                if (Response != "OK") throw new Exception(Response);
                LastActivatedSlot = ToSend.Originator.Id;
            }

            // Send the command!
            switch (ToSend.Type) {

                case CommandType.GetLcd:
                case CommandType.GetScreen:
                    Pindur.StandardInput.WriteLine("draw-screen-" + ((ToSend.Type == CommandType.GetLcd) ? "bw" : "gs"));
                    PindurBinaryOut.ReadBytes(4);
                    byte[] ScreenImage = PindurBinaryOut.ReadBytes(0x1804);
                    return ScreenImage;

                case CommandType.KeyDown:
                case CommandType.KeyUp:
                    Pindur.StandardInput.WriteLine("key-" + ((ToSend.Type == CommandType.KeyDown) ? "down" : "up") + " " + ToSend.Data);
                    Response = GetResponse();
                    if (Response != "OK") throw new Exception(Response);
                    return null;

                case CommandType.SendFile:
                    if (!File.Exists((string)ToSend.Data)) throw new Exception("Error: File " + ToSend.Data.ToString() + " not found.");
                    Pindur.StandardInput.WriteLine("send-file " + ToSend.Originator.Id + " " + ToSend.Data);
                    Response = GetResponse();
                    if (Response != "OK") throw new Exception(Response + " (" + ToSend.Data.ToString() + ")");
                    return null;

                case CommandType.Run:
                    int TicksToRun = Convert.ToInt32(ToSend.Data);
                    if (TicksToRun < 1) return null;
                    while (TicksToRun > 0) {
                        bool HitBreakpoint = false;
                        int RunThisLoop = Math.Min(2000000000, TicksToRun);
                        Pindur.StandardInput.WriteLine("run " + RunThisLoop);
                        TicksToRun -= RunThisLoop;

                        Response = GetResponse();
                        if (Response.StartsWith("Info:")) {
                            HitBreakpoint = true;
                            Response = GetResponse();
                            TicksToRun = 0;
                        }
                        if (Response != "OK") throw new Exception(Response);

                        if (HitBreakpoint) {
                            OnBreakpointHit(new BreakpointArgs(ToSend.Originator));
                        }

                    }
                    return null;

                case CommandType.Reset:
                    Pindur.StandardInput.WriteLine("reset-calc");
                    Response = GetResponse();
                    if (Response != "OK") throw new Exception(Response);
                    return null;

                case CommandType.DumpState:
                    DumpStateType T = (DumpStateType)ToSend.Data;
                    string TypeString = "";
                    switch (T) {
                        case DumpStateType.LcdPhysics:
                            TypeString = "lcd physics";
                            break;
                        case DumpStateType.LcdSoftware:
                            TypeString = "lcd software";
                            break;
                        case DumpStateType.Model:
                            TypeString = "model";
                            break;
                        case DumpStateType.Time:
                            TypeString = "time";
                            break;
                        case DumpStateType.Interrupt:
                            TypeString = "interrupt";
                            break;
                        case DumpStateType.Pager:
                            TypeString = "pager";
                            break;
                        case DumpStateType.Keyboard:
                            TypeString = "keyboard";
                            break;
                        case DumpStateType.Link:
                            TypeString = "link";
                            break;
                        case DumpStateType.Cpu:
                            TypeString = "cpu";
                            break;
                        case DumpStateType.Memory:
                            TypeString = "memory";
                            break;
                    }
                    Pindur.StandardInput.WriteLine("dump-state " + TypeString);
                    byte[] ResponseBytes = PindurBinaryOut.ReadBytes(4);
                    if (ResponseBytes[0] != (byte)'O' || ResponseBytes[1] != (byte)'K') throw new Exception("Error dumping state");

                    switch (T) {
                        case DumpStateType.Cpu:
                            int[] Registers = new int[16];
                            for (int i = 0; i < 16; ++i) {
                                Response = GetResponse();
                                string[] Values = Response.Split('=');
                                Registers[i] = Convert.ToInt32(Values[1], 16);
                            }
                            return Registers;
                        case DumpStateType.Memory:
                            return PindurBinaryOut.ReadBytes(65536);
                        default:
                            string ItemCount = GetResponse();
                            int NumberOfItems = Convert.ToInt32(ItemCount);
                            Dictionary<string, string> Return = new Dictionary<string, string>();
                            for (int i = 0; i < NumberOfItems; ++i) {
                                string[] Items = GetResponse().Split(':');
                                Return.Add(Items[0].Trim(), Items[1].Trim());
                            }
                            return Return;
                    }

                case CommandType.Step:
                    Pindur.StandardInput.WriteLine("step " + ToSend.Data);
                    Response = GetResponse();
                    if (Response != "OK") throw new Exception(Response);
                    return null;

                case CommandType.SetBreakpoint:
                    Pindur.StandardInput.WriteLine("set-breakpoint code " + ToSend.Data);
                    Response = GetResponse();
                    if (Response != "OK") throw new Exception(Response);
                    return null;

                case CommandType.RemoveBreakpoint:
                    Pindur.StandardInput.WriteLine("remove-breakpoint code " + ToSend.Data);
                    Response = GetResponse();
                    if (Response != "OK") throw new Exception(Response);
                    return null;
            }
            return null;
        }

        public void ClearRunning() {
            foreach (Calculator C in this.Calculators) {
                if (C != null) C.ClearRunning();
            }
        }



    }
}
