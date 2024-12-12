namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfStringCommandData
    {
        private readonly byte[][] charCodes;
        private readonly short[] str;
        private readonly double[] offsets;
        public byte[][] CharCodes =>
            this.charCodes;
        public short[] Str =>
            this.str;
        public double[] Offsets =>
            this.offsets;
        public PdfStringCommandData(byte[][] charCodes, short[] str, double[] offsets)
        {
            this.charCodes = charCodes;
            this.str = str;
            this.offsets = offsets;
        }
    }
}

