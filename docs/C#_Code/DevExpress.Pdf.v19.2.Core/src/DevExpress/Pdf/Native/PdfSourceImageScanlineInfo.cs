namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfSourceImageScanlineInfo
    {
        private readonly PdfSourceImagePixelInfo[] pixelInfo;
        private readonly int startIndex;
        private readonly int endIndex;
        public PdfSourceImagePixelInfo[] PixelInfo =>
            this.pixelInfo;
        public int StartIndex =>
            this.startIndex;
        public int EndIndex =>
            this.endIndex;
        public PdfSourceImageScanlineInfo(PdfSourceImagePixelInfo[] pixelInfo, int startIndex, int endIndex)
        {
            this.pixelInfo = pixelInfo;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }
    }
}

