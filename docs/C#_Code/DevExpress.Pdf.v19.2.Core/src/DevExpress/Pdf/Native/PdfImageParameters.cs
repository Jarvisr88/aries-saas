namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfImageParameters
    {
        private readonly int width;
        private readonly int height;
        private readonly bool shouldInterpolate;
        public int Width =>
            this.width;
        public int Height =>
            this.height;
        public bool ShouldInterpolate =>
            this.shouldInterpolate;
        public PdfImageParameters(int width, int height, bool shouldInterpolate)
        {
            this.width = width;
            this.height = height;
            this.shouldInterpolate = shouldInterpolate;
        }
    }
}

