namespace DevExpress.Pdf
{
    using System;

    public class PdfAcroFormBorderAppearance
    {
        private PdfAcroFormBorderStyle style;
        private double width = 1.0;
        private PdfRGBColor color = new PdfRGBColor(0.0, 0.0, 0.0);

        public PdfAcroFormBorderStyle Style
        {
            get => 
                this.style;
            set => 
                this.style = value;
        }

        public double Width
        {
            get => 
                this.width;
            set => 
                this.width = Math.Max(0.0, value);
        }

        public PdfRGBColor Color
        {
            get => 
                this.color;
            set => 
                this.color = value;
        }

        internal bool IsVisible =>
            (this.color != null) && (this.width > 0.0);
    }
}

