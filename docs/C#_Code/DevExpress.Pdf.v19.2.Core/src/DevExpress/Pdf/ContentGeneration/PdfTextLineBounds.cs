namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfTextLineBounds
    {
        private readonly int firstLineIndex;
        private readonly int lastLineIndex;
        public int FirstLineIndex =>
            this.firstLineIndex;
        public int LastLineIndex =>
            this.lastLineIndex;
        public PdfTextLineBounds(int firstLineIndex, int lastLineIndex)
        {
            this.firstLineIndex = firstLineIndex;
            this.lastLineIndex = lastLineIndex;
        }
    }
}

