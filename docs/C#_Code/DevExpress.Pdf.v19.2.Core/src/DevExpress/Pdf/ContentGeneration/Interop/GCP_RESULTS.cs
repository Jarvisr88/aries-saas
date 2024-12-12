namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct GCP_RESULTS
    {
        private int structSize;
        [MarshalAs(UnmanagedType.LPTStr)]
        private string outString;
        private IntPtr order;
        private IntPtr dx;
        private IntPtr caretPos;
        private IntPtr cls;
        private IntPtr glyphs;
        private uint glyphCount;
        private int maxFit;
        public int StructSize
        {
            get => 
                this.structSize;
            set => 
                this.structSize = value;
        }
        public string OutString
        {
            get => 
                this.outString;
            set => 
                this.outString = value;
        }
        public IntPtr Order
        {
            get => 
                this.order;
            set => 
                this.order = value;
        }
        public IntPtr Dx
        {
            get => 
                this.dx;
            set => 
                this.dx = value;
        }
        public IntPtr CaretPos
        {
            get => 
                this.caretPos;
            set => 
                this.caretPos = value;
        }
        public IntPtr Cls
        {
            get => 
                this.cls;
            set => 
                this.cls = value;
        }
        public IntPtr Glyphs
        {
            get => 
                this.glyphs;
            set => 
                this.glyphs = value;
        }
        public uint GlyphCount
        {
            get => 
                this.glyphCount;
            set => 
                this.glyphCount = value;
        }
        public int MaxFit
        {
            get => 
                this.maxFit;
            set => 
                this.maxFit = value;
        }
    }
}

