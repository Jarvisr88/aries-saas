namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfStringData
    {
        private readonly PdfStringCommandData commandData;
        public byte[][] CharCodes =>
            this.commandData.CharCodes;
        public short[] Str =>
            this.commandData.Str;
        public double[] Offsets =>
            this.commandData.Offsets;
        public double[] Widths { get; }
        public double[] Advances { get; }
        public PdfStringData(PdfStringCommandData codePointData, double[] widths, double[] advances)
        {
            this.commandData = codePointData;
            this.<Widths>k__BackingField = widths;
            this.<Advances>k__BackingField = advances;
        }
    }
}

