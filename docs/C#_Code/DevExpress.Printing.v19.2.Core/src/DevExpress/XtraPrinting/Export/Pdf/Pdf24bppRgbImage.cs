namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class Pdf24bppRgbImage : PdfTrueColorImage
    {
        public Pdf24bppRgbImage(Image image, string name, bool compressed) : base(image, name, compressed)
        {
            PixelConverter converter = new ColorChannels24PixelConverter();
            ImageStreamBuilder.Create(base.Compressed, converter).Build(image, base.Stream);
        }
    }
}

