namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class Pdf1bppIndexedImage : PdfIndexedImage
    {
        public Pdf1bppIndexedImage(Image image, string name, bool compressed) : base(image, name, compressed)
        {
        }

        protected override int BitsPerComponent =>
            1;
    }
}

