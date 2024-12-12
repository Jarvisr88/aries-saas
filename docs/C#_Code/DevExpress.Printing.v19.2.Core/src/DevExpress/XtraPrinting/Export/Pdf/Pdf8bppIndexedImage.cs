namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class Pdf8bppIndexedImage : PdfIndexedImage
    {
        public Pdf8bppIndexedImage(Image image, string name, bool compressed) : base(image, name, compressed)
        {
        }

        protected override int BitsPerComponent =>
            8;
    }
}

