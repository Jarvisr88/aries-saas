namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class PdfJpegImage : PdfBitmap
    {
        private Pdf8bppGrayscaleAlpaImage maskImage;

        public PdfJpegImage(Image image, string name, PdfJpegImageQuality quality, bool compressed) : base(name, image.Size, false)
        {
            if (image.RawFormat.Equals(ImageFormat.Jpeg) && ((quality == PdfJpegImageQuality.Highest) && (!IsCmykColorSpace(image) && !IsGrayColorSpace(image))))
            {
                SaveImage(image, base.Stream.Data, ImageFormat.Jpeg);
            }
            else
            {
                ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
                if (encoderInfo == null)
                {
                    SaveImage(image, base.Stream.Data, ImageFormat.Jpeg);
                }
                else
                {
                    if (image.PixelFormat == PixelFormat.Format32bppArgb)
                    {
                        Pdf8bppGrayscaleAlpaImage image2 = new Pdf8bppGrayscaleAlpaImage(image, name + "Mask", compressed, false);
                        if (image2.HasTransparentPixels)
                        {
                            this.maskImage = image2;
                        }
                    }
                    using (EncoderParameters parameters = new EncoderParameters(1))
                    {
                        EncoderParameter parameter = new EncoderParameter(Encoder.Quality, (long) quality);
                        parameters.Param[0] = parameter;
                        SaveImage(image, base.Stream.Data, encoderInfo, parameters);
                    }
                }
            }
        }

        private void AddImageAttributes()
        {
            base.Attributes.Add("BitsPerComponent", 8);
            base.Attributes.Add("ColorSpace", "DeviceRGB");
            PdfArray array = new PdfArray { "DCTDecode" };
            base.Attributes.Add("Filter", array);
            if (this.maskImage != null)
            {
                base.Attributes.Add("SMask", this.maskImage.InnerObject);
            }
        }

        public override void FillUp()
        {
            base.FillUp();
            this.AddImageAttributes();
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].MimeType == mimeType)
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }

        private static bool IsCmykColorSpace(Image image) => 
            ((image.Flags & 0x20) != 0) || ((image.Flags & 0x100) != 0);

        private static bool IsGrayColorSpace(Image image) => 
            (image.Flags & 0x40) != 0;

        private static void SaveImage(Image image, Stream stream, ImageFormat format)
        {
            long position = stream.Position;
            try
            {
                image.Save(stream, format);
            }
            catch (ExternalException exception)
            {
                Tracer.TraceInformation("DXperience.Reporting", "image.Save");
                Tracer.TraceData("DXperience.Reporting", TraceEventType.Verbose, exception);
                if (stream.CanSeek)
                {
                    stream.Position = position;
                }
                using (Bitmap bitmap = new Bitmap(image))
                {
                    bitmap.Save(stream, format);
                }
            }
        }

        private static void SaveImage(Image image, Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            long position = stream.Position;
            try
            {
                image.Save(stream, encoder, encoderParams);
            }
            catch (ExternalException exception)
            {
                Tracer.TraceInformation("DXperience.Reporting", "image.Save");
                Tracer.TraceData("DXperience.Reporting", TraceEventType.Verbose, exception);
                if (stream.CanSeek)
                {
                    stream.Position = position;
                }
                using (Bitmap bitmap = new Bitmap(image))
                {
                    bitmap.Save(stream, encoder, encoderParams);
                }
            }
        }

        protected override bool UseFlateEncoding =>
            false;

        public override PdfImageBase MaskImage =>
            this.maskImage;
    }
}

