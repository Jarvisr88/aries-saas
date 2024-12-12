namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXPrecinctSize
    {
        private readonly int width;
        private readonly int height;
        public int WidthExponent =>
            this.width;
        public int HeightExponent =>
            this.height;
        public JPXPrecinctSize(int value)
        {
            this.width = value >> 4;
            this.height = value & 15;
        }
    }
}

