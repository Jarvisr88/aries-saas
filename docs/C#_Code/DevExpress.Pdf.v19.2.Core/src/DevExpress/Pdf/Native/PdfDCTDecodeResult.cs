namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfDCTDecodeResult
    {
        private readonly byte[] data;
        private readonly int stride;
        public byte[] Data =>
            this.data;
        public int Stride =>
            this.stride;
        public PdfDCTDecodeResult(byte[] data, int stride)
        {
            this.data = data;
            this.stride = stride;
        }
    }
}

