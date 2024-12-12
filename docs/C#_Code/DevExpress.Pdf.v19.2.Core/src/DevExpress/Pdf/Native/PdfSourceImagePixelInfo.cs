namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfSourceImagePixelInfo
    {
        private readonly int index;
        private readonly PdfFixedPointNumber factor;
        public int Index =>
            this.index;
        public PdfFixedPointNumber Factor =>
            this.factor;
        public PdfSourceImagePixelInfo(int index, PdfFixedPointNumber factor)
        {
            this.index = index;
            this.factor = factor;
        }
    }
}

