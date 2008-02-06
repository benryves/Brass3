using System;
using System.Collections.Generic;
using System.Text;

namespace PindurTI {
    class LateniteInteraction {
        public void Update() {
            Console.WriteLine("GET");
            int CommandCount;
            if (int.TryParse(Console.ReadLine(), out CommandCount)) {
                for (int i = 0; i < CommandCount; ++i) {
                    string Command = Console.ReadLine();
                    switch (Command.ToLower()) {
                        case "quit":
                            foreach (CalcWindow c in Program.RunningCalcs) {
                                c.Visible = false;
                            }
                            break;
                        case "play":
                            Program.InitialWindow.Calc.Running = true;
                            break;
                        case "pause":
                            Program.InitialWindow.Calc.Running = false;
                            break;
                        case "enumerate":
                            foreach (string s in new string[] { "QUIT", "PLAY", "PAUSE" }) {
                                Console.WriteLine("ICAN " + s);
                            }
                            break;
                        default: {
                                string GetCommand = Command.ToLower();
                                if (GetCommand.StartsWith("tooltip")) {
                                    string[] Details = Command.Split(' ');
                                    if (Details.Length < 3) {
                                        Console.WriteLine("TOOLTIP");
                                    } else {
                                        Console.WriteLine("TOOLTIP Tooltip support not yet implemented");
                                    }
                                }
                            }
                            break;
                    }

                }
            }
        }
    }
}
