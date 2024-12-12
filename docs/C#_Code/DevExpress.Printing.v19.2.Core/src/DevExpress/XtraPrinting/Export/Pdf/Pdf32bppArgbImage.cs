namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class Pdf32bppArgbImage : PdfTrueColorImage
    {
        private Pdf8bppGrayscaleAlpaImage maskImage;

        public Pdf32bppArgbImage(Image image, string name, bool compressed) : base(image, name, compressed)
        {
            ColorChannels32PixelConverter converter = new ColorChannels32PixelConverter();
            ImageStreamBuilder.Create(base.Compressed, converter).Build(image, base.Stream);
            if (converter.HasTransparentPixels)
            {
                this.maskImage = new Pdf8bppGrayscaleAlpaImage(image, name + "Mask", compressed, true);
            }
        }

        public override void FillUp()
        {
            base.FillUp();
            if (this.maskImage != null)
            {
                base.Attributes.Add("SMask", this.maskImage.InnerObject);
            }
        }

        protected override bool UseMaskColor =>
            false;

        public override PdfImageBase MaskImage =>
            this.maskImage;
    }
}

