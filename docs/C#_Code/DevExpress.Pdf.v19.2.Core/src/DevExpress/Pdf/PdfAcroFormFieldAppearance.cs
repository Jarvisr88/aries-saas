namespace DevExpress.Pdf
{
    using System;
    using System.Runtime.CompilerServices;

    public class PdfAcroFormFieldAppearance
    {
        private PdfAcroFormBorderAppearance borderAppearance;
        private string fontFamily = string.Empty;
        private double fontSize = 12.0;

        public PdfRGBColor BackgroundColor { get; set; }

        public PdfRGBColor ForeColor { get; set; }

        public PdfFontStyle FontStyle { get; set; }

        public PdfAcroFormBorderAppearance BorderAppearance
        {
            get => 
                this.borderAppearance;
            set => 
                this.borderAppearance = value;
        }

        public string FontFamily
        {
            get => 
                this.fontFamily;
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.fontFamily = text1;
            }
        }

        public double FontSize
        {
            get => 
                this.fontSize;
            set => 
                this.fontSize = Math.Max(0.0, value);
        }
    }
}

