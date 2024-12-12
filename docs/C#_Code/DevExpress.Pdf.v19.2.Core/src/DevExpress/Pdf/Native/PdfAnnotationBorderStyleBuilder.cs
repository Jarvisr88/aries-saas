namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfAnnotationBorderStyleBuilder : IPdfAnnotationBorderStyleBuilder
    {
        private const string solidStyleName = "S";
        private const string dashedStyleName = "D";
        private const string beveledStyleName = "B";
        private const string insetStyleName = "I";

        public PdfAnnotationBorderStyle CreateBorderStyle() => 
            new PdfAnnotationBorderStyle(this);

        public PdfAnnotationBorderStyleBuilder SetBorderAppearance(PdfAcroFormBorderAppearance borderAppearance)
        {
            if (borderAppearance != null)
            {
                this.Width = borderAppearance.Width;
                switch (borderAppearance.Style)
                {
                    case PdfAcroFormBorderStyle.Inset:
                        this.StyleName = "I";
                        break;

                    case PdfAcroFormBorderStyle.Beveled:
                        this.StyleName = "B";
                        break;

                    case PdfAcroFormBorderStyle.Dashed:
                        this.StyleName = "D";
                        break;

                    default:
                        this.StyleName = "S";
                        break;
                }
            }
            return this;
        }

        public string StyleName { get; set; }

        public double Width { get; set; }
    }
}

