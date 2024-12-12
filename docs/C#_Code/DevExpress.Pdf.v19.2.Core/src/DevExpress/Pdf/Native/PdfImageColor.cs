namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfImageColor
    {
        private readonly byte red;
        private readonly byte green;
        private readonly byte blue;
        private readonly byte alpha;

        public PdfImageColor(byte red, byte green, byte blue) : this(red, green, blue, 0xff)
        {
        }

        public PdfImageColor(byte red, byte green, byte blue, byte alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public static PdfImageColor FromArgb(byte a, byte r, byte g, byte b) => 
            new PdfImageColor(r, g, b, a);

        public byte Red =>
            this.red;

        public byte Green =>
            this.green;

        public byte Blue =>
            this.blue;

        public byte Alpha =>
            this.alpha;
    }
}

