using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PindurTI {
    public partial class MemoryEditor : UserControl {

        private ComponentResourceManager Resources = new ComponentResourceManager(typeof(MemoryEditor));
        private Image AsciiMap;
        private Image InverseAsciiMap;

        private Graphics ValuesG = null;
        private Bitmap BackBuffer = null;

        ~MemoryEditor() {
            this.BackBuffer.Dispose();
            this.AsciiMap.Dispose();
            this.InverseAsciiMap.Dispose();
        }


        public event EventHandler SelectedAddressChanged;
        protected virtual void OnSelectedAddressChanged(EventArgs e) {
            if (SelectedAddressChanged != null) SelectedAddressChanged(this, e);
        }

        private int selectedAddress = 0;
        public int SelectedAddress {
            get {
                return selectedAddress;
            }
            set {
                selectedAddress = value;
                if (selectedAddress < FirstVisibleMemory || selectedAddress > LastVisibleMemory) {
                    this.MemoryStart = selectedAddress & 0xFFF0;
                }
                FullRedraw();
                OnSelectedAddressChanged(new EventArgs());
            }
        }

        private byte[] memory = new byte[65536];
        public byte[] Memory {
            get {
                return this.memory;
            }
            set {
                if (IsVisible) {
                    bool MemoryDirty = false;
                    for (int i = FirstVisibleMemory; i <= LastVisibleMemory; ++i) {
                        if (this.memory[i] != value[i]) {
                            MemoryDirty = true;
                            break;
                        }
                    }
                    if (MemoryDirty) {
                        this.memory = value;
                        FullRedraw();
                    }
                } else {
                    this.memory = value;
                }
            }
        }
        private int FirstVisibleMemory = 0;
        private int LastVisibleMemory = 0;

        public MemoryEditor() {
            InitializeComponent();
            this.MemoryOffset.Scroll += new ScrollEventHandler(MemoryOffset_Scroll);
            this.MemoryOffset.ValueChanged += new EventHandler(MemoryOffset_ValueChanged);
            OnResize(null);
            base.ResizeRedraw = true;
            FullRedraw();
            this.MouseWheel += new MouseEventHandler(MemoryEditor_MouseWheel);
            this.CellsImage.MouseDown += new MouseEventHandler(CellsImage_MouseDown);
            
            Image GetAsciiMap = (Image)this.Resources.GetObject("AsciiMap");

            AsciiMap = new Bitmap(GetAsciiMap.Width, GetAsciiMap.Height, PixelFormat.Format32bppArgb);
            InverseAsciiMap = new Bitmap(GetAsciiMap.Width, GetAsciiMap.Height, PixelFormat.Format32bppArgb);

            BitmapData Rd;
            BitmapData Wr;

            Rd = ((Bitmap)GetAsciiMap).LockBits(new Rectangle(0, 0, GetAsciiMap.Width, GetAsciiMap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int Size = AsciiMap.Width * AsciiMap.Height;
            int[] Edit = new int[Size];
            Marshal.Copy(Rd.Scan0, Edit, 0, Size);

            
            Wr = ((Bitmap)AsciiMap).LockBits(new Rectangle(0, 0, AsciiMap.Width, AsciiMap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int i = 0; i < Size; ++i) {
                if ((Edit[i] & 0xFF000000) != 0) {
                    Edit[i] = this.ForeColor.ToArgb();
                }
            }
            Marshal.Copy(Edit, 0, Wr.Scan0, Size); ((Bitmap)AsciiMap).UnlockBits(Wr);

            Wr = ((Bitmap)InverseAsciiMap).LockBits(new Rectangle(0, 0, AsciiMap.Width, AsciiMap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int i = 0; i < Size; ++i) {
                if ((Edit[i] & 0xFF000000) != 0) {
                    Edit[i] = Color.FromKnownColor(KnownColor.HighlightText).ToArgb();
                }
            }
            Marshal.Copy(Edit, 0, Wr.Scan0, Size); ((Bitmap)InverseAsciiMap).UnlockBits(Wr);

            ((Bitmap)GetAsciiMap).UnlockBits(Rd);
            GetAsciiMap.Dispose();
        }

        void CellsImage_MouseDown(object sender, MouseEventArgs e) {
            int ClickedX = 0;
            for (int i = 0; i < 16; i++) {
                if (e.X < this.ColBounds[i]) {
                    ClickedX = i;
                    break;
                }
            }
            SelectedAddress = Math.Min(0xFFFF, ClickedX + (e.Y / 17) * 16 + FirstVisibleMemory);
        }

        void MemoryEditor_MouseWheel(object sender, MouseEventArgs e) {
            this.MemoryStart -= e.Delta;
            FullRedraw();
        }


        void MemoryOffset_ValueChanged(object sender, EventArgs e) {
            FullRedraw();
        }

        void MemoryOffset_Scroll(object sender, ScrollEventArgs e) {
            FullRedraw();
        }


        private int MemoryStart {
            get {
                return this.MemoryOffset.Value * 16;
            }
            set {
                this.MemoryOffset.Value = Math.Min(MemoryOffset.Maximum, Math.Max(0, value / 16));
            }
        }

        private bool IsVisible {
            get {
                Form F = this.FindForm();
                return !(F == null || !F.Visible);
                
            }
        }

        private int[] ColBounds = new int[16];

        private bool displayASCII = false;
        public bool DisplayASCII {
            get { return displayASCII; }
            set {
                if (displayASCII != value) {
                    displayASCII = value;
                    FullRedraw();                    
                }
            }
        }

        public void FullRedraw() {
            if (!IsVisible) return;
            int NumRows = (this.CellsImage.Height / this.Font.Height) + 1;

            Font BoldText = new Font(this.Font, FontStyle.Bold);
            int RowHeight = 17;

            ValuesG.Clear(this.BackColor);
            ValuesG.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)), new Rectangle(0, 0, 33, this.CellsImage.Height));
            

            this.FirstVisibleMemory = this.MemoryStart;

            int StartOffset = this.FirstVisibleMemory;
            float OffsetPerItem = (float)(this.CellsImage.Width - 34) / 16.0f;
            float CentreAlignment = displayASCII ? (OffsetPerItem - 8.5f) / 2.0f : (OffsetPerItem - 16.5f) / 2.0f;

            Brush B = new SolidBrush(this.ForeColor);

            

            Pen LightLine = new Pen(Color.FromKnownColor(KnownColor.ControlLightLight));
            Pen DarkLine = new Pen(Color.FromKnownColor(KnownColor.ControlDark));


            int Address = StartOffset;
            for (int y = 0; y < NumRows; y++) {

                if (StartOffset > 0xFFF0) break;

                float YPos = y * RowHeight;

                ValuesG.DrawLine(LightLine, 0, YPos, this.CellsImage.Width, YPos);
                ValuesG.DrawLine(DarkLine, 0, YPos + RowHeight - 1, this.CellsImage.Width, YPos + RowHeight - 1);
                ValuesG.DrawString(StartOffset.ToString("X4"), BoldText, B, 0.0f, YPos + 2.0f);
                if (this.memory != null) {
                    float XPos = 34.0f + CentreAlignment;
                    for (int x = 0; x < 16; ++x) {
                        Brush TextColour = B;
                        if (StartOffset + x == selectedAddress) {
                            ValuesG.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)),
                                (XPos - CentreAlignment), YPos,
                                OffsetPerItem, RowHeight - 1);
                            TextColour = new SolidBrush(Color.FromKnownColor(KnownColor.HighlightText));
                        }
                        int Value = memory[StartOffset + x];

                        if (displayASCII) {
                            ValuesG.DrawImage((StartOffset + x == selectedAddress) ? InverseAsciiMap : AsciiMap, new Rectangle((int)XPos, (int)YPos + 1, 10, 14), 5.0f * (Value / 32) - 0.5f, 8.0f * (Value & 0x1F) - 0.5f, 5, 7, GraphicsUnit.Pixel);
                        } else {
                            ValuesG.DrawString(Value.ToString("X2"), this.Font, TextColour, XPos, YPos + 2.0f);                            
                        }
                        XPos += OffsetPerItem;
                        ColBounds[x] = (int)XPos;
                    }
                    StartOffset += 16;
                }
            }

            ValuesG.DrawLine(DarkLine, 33, 0, 33, this.CellsImage.Height);

            this.CellsImage.BackgroundImage = BackBuffer;
            this.LastVisibleMemory = StartOffset - 1;
            
            this.Refresh();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            if (BackBuffer != null) {
                BackBuffer.Dispose();
            }
            BackBuffer = new Bitmap(Math.Max(1, this.Width), Math.Max(1, this.Height));
            ValuesG = Graphics.FromImage(BackBuffer);
            ValuesG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            ValuesG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            FullRedraw();
            
        }
        
    }
}
