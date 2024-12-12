namespace DevExpress.Pdf
{
    using System;

    public class PdfCharacter
    {
        private readonly string unicodeData;
        private readonly PdfFont font;
        private readonly double fontSize;
        private readonly PdfOrientedRectangle rectangle;

        public PdfCharacter(string unicodeData, PdfFont font, double fontSize, PdfOrientedRectangle rectangle)
        {
            this.unicodeData = unicodeData;
            this.font = font;
            this.fontSize = fontSize;
            this.rectangle = rectangle;
        }

        public string UnicodeData =>
            this.unicodeData;

        public PdfFont Font =>
            this.font;

        public double FontSize =>
            this.fontSize;

        public PdfOrientedRectangle Rectangle =>
            this.rectangle;
    }
}

