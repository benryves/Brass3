using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PindurTI {
    public partial class Keypad : Form {

        private readonly CalcWindow OwnerCalc;

        public Keypad(CalcWindow OwnerCalc) {
            this.OwnerCalc = OwnerCalc;
            this.MdiParent = OwnerCalc;
            InitializeComponent();
        }


        private bool CanUpdatePosition = false;
        private void Keypad_Load(object sender, EventArgs e) {

            try { this.Location = Properties.Settings.Default.KeypadPos; } catch { }

            string[] ButtonText = new string[] { "Y=", "Win", "Zoom", "Trace", "Graph", "2nd", "Mode", "Del", "Alpha", "XTΘn", "Stat", "Math", "Apps", "Prgm", "Vars", "Clear", "x¹־", "Sin", "Cos", "Tan", "^", "x²", ",", "(", ")", "÷", "Log", "7", "8", "9", "×", "Ln", "4", "5", "6", "−", "Sto »", "1", "2", "3", "+", "On", "0", "·", "(−)", "Enter", "←", "↑", "→", "↓" };
            string[] AltText = new string[] { "Stat", "TblSet", "Format", "Calc", "Table", "", "Quit", "Ins", "A-Lock", "Link", "List", "Test", "Angle", "Draw", "Distr", "", "Matrx", "Sin¹־", "Cos¹־", "Tan¹־", "π", "√¯", "EE", "{", "}", "e", "10^x", "u", "v", "w", "[", "e^x", "L4", "L5", "L6", "]", "Rcl", "L1", "L2", "L3", "Mem", "Off", "Catalog", "i", "Ans", "Entry", "", "", "", "" };
            string[] LetText = new string[] { "F1", "F2", "F3", "F4", "F5", "", "", "", "", "", "", "A", "B", "C", "", "", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "O", "\"", "", "_", ":", "?", "", "", "", "", "" };
            string[] ButtonName = new string[] { "y=", "window", "zoom", "trace", "graph", "2nd", "mode", "del", "alpha", "x", "stat", "math", "matrx", "prgm", "vars", "clear", "x^-1", "sin", "cos", "tan", "^", "x^2", ",", "(", ")", "/", "log", "7", "8", "9", "*", "ln", "4", "5", "6", "-", "sto", "1", "2", "3", "+", "on", "0", ".", "(-)", "enter", "left", "up", "right", "down" };

            int BN = 0;
            for (int y = 0; y < 10; ++y) {
                for (int x = 0; x < 5; x++) {
                    if (y < 1 || y > 2 || x < 3) {
                        Button B = new Button();
                        B.Width = 48;
                        B.Height = 24;
                        B.Left = x * 48;
                        B.Top = y * 36 + 12;
                        B.Text = ButtonText[BN];
                        B.Tag = ButtonName[BN];
                        B.Margin = new Padding(0);
                        B.Padding = new Padding(0);
                        B.Font = new Font("Tahoma", 6.8f, FontStyle.Bold);
                        B.MouseDown += new MouseEventHandler(CalcKeyDown);
                        B.MouseUp += new MouseEventHandler(CalcKeyUp);
                        this.Controls.Add(B);

                        Label T = new Label();
                        T.Text = LetText[BN];
                        T.Width = B.Width;
                        T.Left = B.Left;
                        T.Top = B.Top - 12;
                        T.Height = 12;
						T.Font = new Font("Tahoma", 6.2f);
                        T.TextAlign = ContentAlignment.MiddleRight;
                        T.ForeColor = Color.DarkGreen;
                        T.BackColor = this.BackColor;
                        this.Controls.Add(T);
                        T.SendToBack();


                        Label L = new Label();
                        L.Text = AltText[BN];
                        L.Left = B.Left;
                        L.Top = B.Top - 11;
                        L.Height = 12;
                        L.TextAlign = ContentAlignment.MiddleLeft;
						L.Font = new Font("Tahoma", 6.2f);
                        L.ForeColor = Color.DarkOrange;
                        L.BackColor = this.BackColor;
                        L.AutoSize = true;
                        this.Controls.Add(L);
                        L.BringToFront();
                        ++BN;
                    }
                }

            }
            for (int i = 0; i < 4; ++i) {
                Button B = new Button();
                B.Width = 31;
                B.Height = 28;
                B.Left = 3 * 48;
                B.Top = 2 * 24;
                switch (i) {
                    case 0:
                        B.Height = 60;
                        break;
                    case 1:
                        B.Left += 32;
                        break;
                    case 2:
                        B.Left += 64;
                        B.Height = 60;
                        break;
                    case 3:
                        B.Left += 32;
                        B.Top += 32;
                        break;
                }
                B.Tag = ButtonName[BN];
                B.Text = ButtonText[BN++];
                B.Font = new Font(FontFamily.GenericSansSerif, 7.0f, FontStyle.Bold);
                this.Controls.Add(B);
                B.BringToFront();
                B.MouseDown += new MouseEventHandler(CalcKeyDown);
                B.MouseUp += new MouseEventHandler(CalcKeyUp);
            }
            this.Show();
            this.CanUpdatePosition = true;
        }

        void CalcKeyDown(object sender, MouseEventArgs e) {
            OwnerCalc.Calc.KeyDown((string)((Button)sender).Tag);
        }
        void CalcKeyUp(object sender, MouseEventArgs e) {
            OwnerCalc.Calc.KeyUp((string)((Button)sender).Tag);
        }

        private void Keypad_FormClosing(object sender, FormClosingEventArgs e) {
            if (!this.MdiParent.Disposing) {
                e.Cancel = true;
                this.Hide();
                if (e.CloseReason != CloseReason.MdiFormClosing) Properties.Settings.Default.KeypadVisible = false;
            }
        }

        private void Keypad_LocationChanged(object sender, EventArgs e) {
            if (this.Visible && CanUpdatePosition) {
                Properties.Settings.Default.KeypadPos = this.Location;
            }
        }
    }
}