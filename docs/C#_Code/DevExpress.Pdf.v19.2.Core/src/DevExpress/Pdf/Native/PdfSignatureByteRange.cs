namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfSignatureByteRange
    {
        private readonly int start;
        private readonly int length;
        public int Start =>
            this.start;
        public int Length =>
            this.length;
        public PdfSignatureByteRange(int start, int length)
        {
            this.start = start;
            this.length = length;
        }
    }
}

