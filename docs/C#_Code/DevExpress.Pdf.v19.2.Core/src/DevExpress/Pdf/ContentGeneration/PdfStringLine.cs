namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfStringLine
    {
        private readonly PdfPoint begin;
        private readonly PdfPoint end;
        public PdfPoint Begin =>
            this.begin;
        public PdfPoint End =>
            this.end;
        public PdfStringLine(PdfPoint begin, PdfPoint end)
        {
            this.begin = begin;
            this.end = end;
        }
    }
}

