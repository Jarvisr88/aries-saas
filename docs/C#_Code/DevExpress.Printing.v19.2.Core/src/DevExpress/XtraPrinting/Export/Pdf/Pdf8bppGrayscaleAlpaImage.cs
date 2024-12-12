namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class Pdf8bppGrayscaleAlpaImage : PdfBitmap
    {
        private bool useMatte;

        public Pdf8bppGrayscaleAlpaImage(Image image, string name, bool compressed, bool useMatte) : base(name, image.Size, compressed)
        {
            this.useMatte = useMatte;
            AlphaChannelPixelConverter converter = new AlphaChannelPixelConverter();
            ImageStreamBuilder.Create(base.Compressed, converter).Build(image, base.Stream);
            this.HasTransparentPixels = converter.HasTransparentPixels;
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Attributes.Add("BitsPerComponent", 8);
            base.Attributes.Add("ColorSpace", "DeviceGray");
            if (this.useMatte)
            {
                PdfArray array1 = new PdfArray();
                array1.Add(0);
                array1.Add(0);
                array1.Add(0);
                base.Attributes.Add("Matte", array1);
            }
            if (base.Compressed)
            {
                PdfDictionary dictionary = new PdfDictionary();
                dictionary.Add("Predictor", 15);
                dictionary.Add("BitsPerComponent", 8);
                dictionary.Add("Colors", 1);
                dictionary.Add("Columns", base.Size.Width);
                base.Attributes.Add("DecodeParms", dictionary);
            }
        }

        public bool HasTransparentPixels { get; private set; }
    }
}

