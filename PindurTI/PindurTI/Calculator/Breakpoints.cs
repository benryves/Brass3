using System;
using System.Collections.Generic;
using System.Text;

namespace PindurTI {
    public partial class Calculator {

        public class BreakpointList : IList<Breakpoint> {
            
            private bool breakOnBreakpoints;
            public bool BreakOnBreakpoints {
                get { return breakOnBreakpoints; }
                set {
                    if (breakOnBreakpoints != value) {
                        if (value) {
                            foreach (Breakpoint B in this.breakpoints) {
                                this.Owner.Pindur.SendCommand(this.Owner, Emulator.CommandType.SetBreakpoint, B.Address);               
                            }
                        } else {
                            this.Owner.Pindur.SendCommand(this.Owner, Emulator.CommandType.RemoveBreakpoint, -1);
                        }
                        breakOnBreakpoints = value;
                    }
                }
            }
            private List<Breakpoint> breakpoints = new List<Breakpoint>();

            protected readonly Calculator Owner;

            public BreakpointList(Calculator Owner) {
                this.Owner = Owner;
            }

            public void Add(Breakpoint item) {
                breakpoints.Add(item);
                if (breakOnBreakpoints) this.Owner.Pindur.SendCommand(this.Owner, Emulator.CommandType.SetBreakpoint, item.Address);
            }
            public void Add(int Address, int Page, string Filename, int Line, string Description) {
                Add(new Breakpoint(this.Owner, Address, Page, Filename, Line, Description));
            }

            public bool Remove(Breakpoint item) {
                if (breakOnBreakpoints) this.Owner.Pindur.SendCommand(this.Owner, Emulator.CommandType.RemoveBreakpoint, item.Address);
                return breakpoints.Remove(item);
            }

            public int IndexOf(Breakpoint item) {
                return breakpoints.IndexOf(item);
            }

            public void Insert(int index, Breakpoint item) {
            }

            public void RemoveAt(int index) {
                breakpoints.RemoveAt(index);
            }

            public Breakpoint this[int i] {
                get { return breakpoints[i]; }
                set { breakpoints[i] = value; }
            }

            public void Clear() {
                this.Owner.Pindur.SendCommand(this.Owner, Emulator.CommandType.RemoveBreakpoint, -1);
                breakpoints.Clear();
            }

            public bool Contains(Breakpoint item) {
                return breakpoints.Contains(item);
            }

            public void CopyTo(Breakpoint[] array, int arrayIndex) {
                breakpoints.CopyTo(array, arrayIndex);
            }

            public int Count {
                get { return breakpoints.Count; }
            }



            public bool IsReadOnly {
                get { return false; }
            }


            public IEnumerator<Breakpoint> GetEnumerator() {
                return breakpoints.GetEnumerator();
            }


            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
                return breakpoints.GetEnumerator();
            }
        }

        public class Breakpoint {
            
            private readonly Calculator Owner;

            private int? address = null;

            public int Page = 0;
            public string Filename = "?.asm";
            public string Description = "";
            public int Line = 0;
            
            public int Address {
                get { return (int)address; }
                set { address = value; }
            }
            public Breakpoint(Calculator Owner, int Address, int Page, string Filename, int Line, string Description) {
                this.Owner = Owner;
                this.Address = Address;
                this.Page = Page;
                this.Filename = Filename;
                this.Line = Line;
                this.Description = Description;
                
            }
        }


        public BreakpointList Breakpoints;
    }
}
