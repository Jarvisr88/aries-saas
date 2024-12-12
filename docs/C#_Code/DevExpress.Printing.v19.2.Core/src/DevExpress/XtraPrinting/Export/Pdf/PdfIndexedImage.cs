namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;

    public abstract class PdfIndexedImage : PdfBitmap
    {
        private PdfStream colorTable;
        private int paletteLength;
        private int maskColorIndex;

        protected PdfIndexedImage(Image image, string name, bool compressed) : base(name, image.Size, compressed)
        {
            this.maskColorIndex = -1;
            this.CreateColorTable(image);
            this.FillStream(image);
        }

        private static int CalculateByteWidth(int imageWidth, int bitsPerComponent)
        {
            int num = 8 / bitsPerComponent;
            int num2 = imageWidth / num;
            if ((imageWidth % num) > 0)
            {
                num2++;
            }
            return num2;
        }

        public override void Close()
        {
            if (this.colorTable != null)
            {
                this.colorTable.Close();
                this.colorTable = null;
            }
            base.Close();
        }

        private void CreateColorTable(Image image)
        {
            if ((Image.GetPixelFormatSize(image.PixelFormat) <= 8) && (image.Palette.Entries.Length != 0))
            {
                this.paletteLength = image.Palette.Entries.Length;
                string str = "";
                for (int i = 0; i < this.paletteLength; i++)
                {
                    Color color = image.Palette.Entries[i];
                    if ((color.A == 0) && (this.maskColorIndex == -1))
                    {
                        this.maskColorIndex = i;
                    }
                    str = (str + color.R.ToString("X2", CultureInfo.CurrentCulture)) + color.G.ToString("X2", CultureInfo.CurrentCulture) + color.B.ToString("X2", CultureInfo.CurrentCulture);
                    if (i < (this.paletteLength - 1))
                    {
                        str = str + " ";
                    }
                }
                str = str + ">\n";
                this.colorTable = new PdfStream();
                this.colorTable.Attributes.Add("Filter", "ASCIIHexDecode");
                this.colorTable.SetString(str);
            }
        }

        private void FillStream(Image image)
        {
            byte[] bytes = BitmapUtils.ImageToByteArray(image);
            int length = bytes.Length;
            int byteWidth = CalculateByteWidth(image.Width, this.BitsPerComponent);
            int num3 = BitmapUtils.CalculateCorrectedByteWidth(byteWidth);
            for (int i = 0; i < image.Height; i++)
            {
                length -= num3;
                base.Stream.SetBytes(bytes, length, byteWidth);
            }
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Attributes.Add("BitsPerComponent", this.BitsPerComponent);
            PdfArray array = new PdfArray {
                "Indexed",
                "DeviceRGB",
                this.paletteLength - 1,
                this.colorTable
            };
            base.Attributes.Add("ColorSpace", array);
            if (this.maskColorIndex != -1)
            {
                PdfArray array2 = new PdfArray {
                    this.maskColorIndex,
                    this.maskColorIndex
                };
                base.Attributes.Add("Mask", array2);
            }
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            if (this.colorTable != null)
            {
                xRef.RegisterObject(this.colorTable);
            }
        }

        protected override void WriteContent(StreamWriter writer)
        {
            if (this.colorTable != null)
            {
                this.colorTable.WriteIndirect(writer);
            }
        }

        protected abstract int BitsPerComponent { get; }
    }
}

