namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfHatchPatternRect
    {
        private int x;
        private int y;
        private int width;
        private int height;
        public int X =>
            this.x;
        public int Y =>
            this.y;
        public int Right =>
            this.x + this.width;
        public int Top =>
            this.y + this.height;
        public PdfHatchPatternRect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public PdfHatchPatternRect(int x, int y) : this(x, y, 1, 1)
        {
        }
    }
}

