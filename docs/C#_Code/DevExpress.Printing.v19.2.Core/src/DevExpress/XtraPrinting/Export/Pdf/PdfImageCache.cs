namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    public class PdfImageCache
    {
        private Hashtable hash = new Hashtable();

        public void AddPdfImage(PdfImageBase pdfImage, Params imageParams)
        {
            this.hash.Add(imageParams, pdfImage);
        }

        public PdfImageBase this[Params imageParams] =>
            this.hash[imageParams] as PdfImageBase;

        public class Params
        {
            private Image image;
            private Color backgroundColor;

            public Params(Image image, Color backgroundColor)
            {
                if (image == null)
                {
                    throw new ArgumentNullException("image");
                }
                this.image = image;
                this.backgroundColor = backgroundColor;
            }

            public override bool Equals(object obj)
            {
                PdfImageCache.Params @params = obj as PdfImageCache.Params;
                return ((@params != null) && (ReferenceEquals(this.image, @params.image) && (this.backgroundColor == @params.backgroundColor)));
            }

            public override int GetHashCode() => 
                HashCodeHelper.CalculateGeneric<Image, Color>(this.image, this.backgroundColor);
        }
    }
}

