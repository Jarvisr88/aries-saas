namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public abstract class PdfTrueColorImage : PdfBitmap
    {
        private Color maskColor;

        public PdfTrueColorImage(Image image, string name, bool compressed) : base(name, image.Size, compressed)
        {
            this.maskColor = Color.Empty;
            if (this.UseMaskColor)
            {
                this.InitilizeMaskColor(image as Bitmap);
            }
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Attributes.Add("BitsPerComponent", 8);
            base.Attributes.Add("ColorSpace", "DeviceRGB");
            if (base.Compressed)
            {
                PdfDictionary dictionary = new PdfDictionary();
                dictionary.Add("Predictor", 15);
                dictionary.Add("BitsPerComponent", 8);
                dictionary.Add("Colors", 3);
                dictionary.Add("Columns", base.Size.Width);
                base.Attributes.Add("DecodeParms", dictionary);
            }
            if (this.maskColor != DXColor.Empty)
            {
                PdfArray array = new PdfArray {
                    (int) this.maskColor.R,
                    (int) this.maskColor.R,
                    (int) this.maskColor.G,
                    (int) this.maskColor.G,
                    (int) this.maskColor.B,
                    (int) this.maskColor.B
                };
                base.Attributes.Add("Mask", array);
            }
        }

        private void InitilizeMaskColor(Bitmap bitmap)
        {
            if ((bitmap != null) && ((bitmap.Width != 0) && (bitmap.Height != 0)))
            {
                Color pixel = bitmap.GetPixel(0, 0);
                if (pixel.A == 0)
                {
                    this.maskColor = pixel;
                }
                else
                {
                    Color color2 = bitmap.GetPixel(bitmap.Width - 1, 0);
                    if (color2.A == 0)
                    {
                        this.maskColor = color2;
                    }
                    else
                    {
                        Color color3 = bitmap.GetPixel(0, bitmap.Height - 1);
                        if (color3.A == 0)
                        {
                            this.maskColor = color3;
                        }
                        else
                        {
                            Color color4 = bitmap.GetPixel(bitmap.Width - 1, bitmap.Height - 1);
                            if (color4.A == 0)
                            {
                                this.maskColor = color4;
                            }
                        }
                    }
                }
            }
        }

        protected virtual bool UseMaskColor =>
            true;
    }
}

