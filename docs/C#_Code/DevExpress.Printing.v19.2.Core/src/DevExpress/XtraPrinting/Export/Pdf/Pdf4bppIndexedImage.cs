namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class Pdf4bppIndexedImage : PdfIndexedImage
    {
        public Pdf4bppIndexedImage(Image image, string name, bool compressed) : base(image, name, compressed)
        {
        }

        protected override int BitsPerComponent =>
            4;
    }
}

