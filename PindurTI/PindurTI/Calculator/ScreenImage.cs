using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PindurTI {
    public partial class Calculator {

        /// <summary>
        /// Flag stating whether the screen image is dirty or not
        /// </summary>
        private bool ScreenDirty = true;

        private bool _LcdOn = false;
        public bool LcdOn {
            get {
                if (ScreenDirty) {
                    UpdateScreen();
                }
                return _LcdOn;
            }
        }

        private bool _UseLcdMemory = false;
        public bool UseLcdMemory {
            get {
                return _UseLcdMemory;
            }
            set {
                _UseLcdMemory = value;
                ScreenDirty = true;
                UpdateScreen();
            }
        }



        /// <summary>
        /// Internal storage of the screen image
        /// </summary>
        private Bitmap _ScreenImage = null;

        public Bitmap ScreenImage {
            get {
                if (ScreenDirty) {
                    UpdateScreen();
                }
                return _ScreenImage;
            }
        }

        public enum VideoModes {
            Unscaled,
            Zoom2x,
            Zoom3x,
            Zoom4x,
            Smooth2x,
            Smooth3x
        }

        private VideoModes videoMode = (VideoModes)Properties.Settings.Default.VideoMode;
        public VideoModes VideoMode {
            get { return videoMode; }
            set {
                _ScreenImage = null;
                videoMode = value;
                Properties.Settings.Default.VideoMode = (int)value;
            }
        }
        private int Height;
        private int Width;
        int[] ExpandedScreenData;
        private void UpdateScreen() {
            if (_ScreenImage == null) {
                switch (videoMode) {
                    case VideoModes.Zoom2x:
                    case VideoModes.Smooth2x:
                        Width = 96 * 2;
                        Height = 64 * 2;
                        break;
                    case VideoModes.Zoom3x:
                    case VideoModes.Smooth3x:
                        Width = 96 * 3;
                        Height = 64 * 3;
                        break;
                    case VideoModes.Zoom4x:
                        Width = 96 * 4;
                        Height = 64 * 4;
                        break;
                    default:
                        Width = 96;
                        Height = 64;
                        break;
                }
                ExpandedScreenData = new int[Width * Height];
                _ScreenImage = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            }
            byte[] ScreenData = (byte[])Pindur.SendCommand(this, _UseLcdMemory ? Emulator.CommandType.GetLcd : Emulator.CommandType.GetScreen, null);
            this._LcdOn = ScreenData[1] == (byte)'1';


            BitmapData BmpData = _ScreenImage.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            switch (videoMode) {
                case VideoModes.Unscaled:
                    for (int i = 0; i < 0x1800; ++i) {
                        ExpandedScreenData[i] = LcdColourRange[ScreenData[i + 4]];
                    }
                    Marshal.Copy(ExpandedScreenData, 0, BmpData.Scan0, 0x1800);
                    break;
                case VideoModes.Zoom2x: {
                        int WritePointer = 0;
                        for (int y = 0; y < 64; ++y) {
                            int ScanPointer = y * 96 + 4;
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 16)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                            }
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 16)];
                            }
                        }
                        Marshal.Copy(ExpandedScreenData, 0, BmpData.Scan0, 24576);
                        break;
                    }
                case VideoModes.Zoom3x: {
                        int WritePointer = 0;
                        for (int y = 0; y < 64; ++y) {
                            int ScanPointer = y * 96 + 4;
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 24)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 8)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                            }
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 8)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 8)];
                            }
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 8)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 24)];
                            }
                        }
                        Marshal.Copy(ExpandedScreenData, 0, BmpData.Scan0, 55296);
                        break;
                    }
                case VideoModes.Zoom4x: {
                        int WritePointer = 0;
                        for (int y = 0; y < 64; ++y) {
                            int ScanPointer = y * 96 + 4;
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 24)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 16)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 8)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                            }
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 16)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 8)];
                            }
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Min(255, ScreenData[ScanPointer + x] + 8)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 16)];
                            }
                            for (int x = 0; x < 96; ++x) {
                                ExpandedScreenData[WritePointer++] = LcdColourRange[ScreenData[ScanPointer + x]];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 8)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 16)];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[Math.Max(0, ScreenData[ScanPointer + x] - 24)];
                            }
                        }
                        Marshal.Copy(ExpandedScreenData, 0, BmpData.Scan0, 98304);
                        break;
                    }
                case VideoModes.Smooth2x: {
                        int WritePointer = 0;
                        for (int y = 0; y < 64; ++y) {
                            for (int x = 0; x < 96; ++x) {

                                int PixelC = ScreenData[x + y * 96 + 4];
                                int PixelA = y > 00 ? ScreenData[x + (y - 1) * 96 + 4] : PixelC; // Above
                                int PixelB = y < 63 ? ScreenData[x + (y + 1) * 96 + 4] : PixelC; // Below
                                int PixelL = x > 00 ? ScreenData[(x - 1) + y * 96 + 4] : PixelC; // Left
                                int PixelR = x < 95 ? ScreenData[(x + 1) + y * 96 + 4] : PixelC; // Right
                                

                                // Limit to 8 shades;
                                PixelA &= 0xE0;
                                PixelB &= 0xE0;
                                PixelL &= 0xE0;
                                PixelR &= 0xE0;
                                PixelC &= 0xE0;

                                int NewTL;
                                int NewTR;
                                int NewBL;
                                int NewBR;

                                if (PixelA != PixelB && PixelL != PixelR) {
                                    NewTL = PixelL == PixelA ? PixelL : PixelC;
                                    NewTR = PixelA == PixelR ? PixelR : PixelC;
                                    NewBL = PixelL == PixelB ? PixelL : PixelC;
                                    NewBR = PixelB == PixelR ? PixelR : PixelC;
                                } else {
                                    NewTL = PixelC;
                                    NewTR = PixelC;
                                    NewBL = PixelC;
                                    NewBR = PixelC;
                                }

                                ExpandedScreenData[WritePointer + 192] = LcdColourRange[NewBL];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[NewTL];

                                ExpandedScreenData[WritePointer + 192] = LcdColourRange[NewBR];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[NewTR];

                            }
                            WritePointer += 192;
                        }
                        Marshal.Copy(ExpandedScreenData, 0, BmpData.Scan0, 24576);
                        break;
                    }
                case VideoModes.Smooth3x: {
                        int WritePointer = 0;
                        for (int y = 0; y < 64; ++y) {
                            for (int x = 0; x < 96; ++x) {

                                int E = ScreenData[x + y * 96 + 4];

                                int B = y > 00 ? ScreenData[x + (y - 1) * 96 + 4] : E;
                                int H = y < 63 ? ScreenData[x + (y + 1) * 96 + 4] : E;
                                int D = x > 00 ? ScreenData[(x - 1) + y * 96 + 4] : E;
                                int F = x < 95 ? ScreenData[(x + 1) + y * 96 + 4] : E;

                                int A = x > 00
                                    ? (y > 00 ? ScreenData[(x - 1) + (y - 1) * 96 + 4] : ScreenData[(x - 1) + y * 96 + 4])
                                    : (y > 00 ? ScreenData[x + (y - 1) * 96 + 4] : ScreenData[x + y * 96 + 4]);

                                int G = x > 00
                                    ? (y < 63 ? ScreenData[(x - 1) + (y + 1) * 96 + 4] : ScreenData[(x - 1) + y * 96 + 4])
                                    : (y < 63 ? ScreenData[x + (y + 1) * 96 + 4] : ScreenData[x + y * 96 + 4]);

                                int C = x < 95
                                    ? (y > 00 ? ScreenData[(x + 1) + (y - 1) * 96 + 4] : ScreenData[(x + 1) + y * 96 + 4])
                                    : (y > 00 ? ScreenData[x + (y - 1) * 96 + 4] : ScreenData[x + y * 96 + 4]);

                                int I = x < 95
                                    ? (y < 63 ? ScreenData[(x + 1) + (y + 1) * 96 + 4] : ScreenData[(x + 1) + y * 96 + 4])
                                    : (y < 63 ? ScreenData[x + (y + 1) * 96 + 4] : ScreenData[x + y * 96 + 4]);

                                

                                // Limit to 8 shades;
                                B &= 0xE0; H &= 0xE0; D &= 0xE0; F &= 0xE0; E &= 0xE0;
                                A &= 0xE0; C &= 0xE0; G &= 0xE0; I &= 0xE0;

                                int E0, E1, E2, E3, E4, E5, E6, E7, E8;

                                if (B != H && D != F) {
                                    E0 = D == B ? D : E;
                                    E1 = (D == B && E != C) || (B == F && E != A) ? B : E;
                                    E2 = B == F ? F : E;
                                    E3 = (D == B && E != G) || (D == H && E != A) ? D : E;
                                    E4 = E;
                                    E5 = (B == F && E != I) || (H == F && E != C) ? F : E;
                                    E6 = D == H ? D : E;
                                    E7 = (D == H && E != I) || (H == F && E != G) ? H : E;
                                    E8 = H == F ? F : E;
                                } else {
                                    E0 = E; E1 = E; E2 = E; E3 = E; E4 = E; E5 = E; E6 = E; E7 = E; E8 = E;
                                }


                                ExpandedScreenData[WritePointer + 288] = LcdColourRange[E3];
                                ExpandedScreenData[WritePointer + 576] = LcdColourRange[E6];
                                ExpandedScreenData[WritePointer ++] = LcdColourRange[E0];

                                ExpandedScreenData[WritePointer + 288] = LcdColourRange[E4];
                                ExpandedScreenData[WritePointer + 576] = LcdColourRange[E7];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[E1];

                                ExpandedScreenData[WritePointer + 288] = LcdColourRange[E5];
                                ExpandedScreenData[WritePointer + 576] = LcdColourRange[E8];
                                ExpandedScreenData[WritePointer++] = LcdColourRange[E2];

                            }
                            WritePointer += 576;
                        }
                        Marshal.Copy(ExpandedScreenData, 0, BmpData.Scan0, 55296);
                        break;
                    }
                    
            }
            _ScreenImage.UnlockBits(BmpData);
        }

        private Color[] lcdColours = new Color[] { Properties.Settings.Default.BrightColour, Properties.Settings.Default.DarkColour };
        public Color BrightColour {
            get { return lcdColours[0]; }
            set { lcdColours[0] = value; RecalculateColourRange(); Properties.Settings.Default.BrightColour = value; }
        }
        public Color DarkColour {
            get { return lcdColours[1]; }
            set { lcdColours[1] = value; RecalculateColourRange(); Properties.Settings.Default.DarkColour = value; }
        }
        private int[] LcdColourRange = new int[256];

        private void RecalculateColourRange() {

            int[] Start = new int[3];
            Start[0] = lcdColours[0].R;
            Start[1] = lcdColours[0].G;
            Start[2] = lcdColours[0].B;

            int[] End = new int[3];
            End[0] = lcdColours[1].R;
            End[1] = lcdColours[1].G;
            End[2] = lcdColours[1].B;

            int[] Result = new int[3];

            for (int i = 0; i < 256; ++i) {
                decimal Fraction = (decimal)i / 256.0m;
                for (int j = 0; j < 3; ++j) {
                    Result[j] = (int)((decimal)(End[j] - Start[j]) * Fraction) + Start[j];
                }
                LcdColourRange[i] = (Result[0] << 16) | (Result[1] << 8) | Result[2];
            }
        }
    }
}
