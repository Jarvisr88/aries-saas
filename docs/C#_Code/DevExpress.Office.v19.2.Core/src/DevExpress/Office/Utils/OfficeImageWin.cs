namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Office.PInvoke;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class OfficeImageWin : OfficeNativeImage
    {
        private static Dictionary<Guid, ImageCodecInfo> imageEncodersInfo = CreateImageEncodersInfo();
        private System.Drawing.Image nativeImage;
        private MemoryStream imageStream;

        public OfficeImageWin(System.Drawing.Image image, IUniqueImageId id) : this(image, null, id)
        {
        }

        public OfficeImageWin(System.Drawing.Image image, MemoryStream imageStream, IUniqueImageId id) : base(id)
        {
            Guard.ArgumentNotNull(image, "image");
            this.SetNativeImage(image, imageStream);
        }

        protected internal override Size CalculateImageSizeInModelUnits(DocumentModelUnitConverter unitConverter) => 
            this.EnsureNonZeroSize(unitConverter.PixelsToModelUnits(this.SizeInOriginalUnits, this.HorizontalResolution, this.VerticalResolution));

        public override bool CanGetImageBytes(OfficeImageFormat imageFormat) => 
            true;

        protected System.Drawing.Image CloneNativeImage()
        {
            if (!(this.NativeImage.RawFormat.Guid == ImageFormat.Gif.Guid))
            {
                return (System.Drawing.Image) this.NativeImage.Clone();
            }
            MemoryStream stream = new MemoryStream();
            this.NativeImage.Save(stream, GetImageCodecInfo(ImageFormat.Gif), null);
            return System.Drawing.Image.FromStream(stream);
        }

        protected override OfficeImage CreateClone(IDocumentModel target)
        {
            OfficeImage imageById = target.GetImageById(base.Id);
            return ((imageById == null) ? new OfficeImageWin(this.CloneNativeImage(), this.imageStream, base.Id) : imageById);
        }

        private static Dictionary<Guid, ImageCodecInfo> CreateImageEncodersInfo()
        {
            Dictionary<Guid, ImageCodecInfo> dictionary = new Dictionary<Guid, ImageCodecInfo>();
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            int length = imageEncoders.Length;
            for (int i = 0; i < length; i++)
            {
                ImageCodecInfo info = imageEncoders[i];
                Guid formatID = info.FormatID;
                dictionary.Add(formatID, info);
            }
            return dictionary;
        }

        public override void DiscardCachedData()
        {
            if (this.imageStream != null)
            {
                this.imageStream.Close();
                this.imageStream = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.NativeImage != null))
                {
                    this.NativeImage.Dispose();
                    this.SetNativeImageCore(null, null);
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected internal Size EnsureNonZeroSize(Size size) => 
            new Size(Math.Max(1, size.Width), Math.Max(1, size.Height));

        private static byte[] GetBitmapImageBytes(System.Drawing.Image nativeImage, OfficeImageFormat imageFormat)
        {
            byte[] buffer;
            ChunkedMemoryStream bitmapImageBytesStream = GetBitmapImageBytesStream(nativeImage, imageFormat);
            try
            {
                buffer = bitmapImageBytesStream.ToArray();
            }
            finally
            {
                bitmapImageBytesStream.Close();
                bitmapImageBytesStream.Dispose();
            }
            return buffer;
        }

        private static ChunkedMemoryStream GetBitmapImageBytesStream(System.Drawing.Image nativeImage, OfficeImageFormat imageFormat)
        {
            ChunkedMemoryStream stream = new ChunkedMemoryStream();
            nativeImage.Save(stream, GetImageCodecInfo(OfficeImageHelper.GetImageFormat(imageFormat)), null);
            stream.Flush();
            stream.Seek(0L, SeekOrigin.Begin);
            return stream;
        }

        public virtual byte[] GetEmfImageBytes()
        {
            byte[] enhMetafileBits;
            if (!OSHelper.IsWindows)
            {
                return (((this.ImageStream == null) || (this.RawFormat != OfficeImageFormat.Emf)) ? new byte[0] : this.ImageStream.ToArray());
            }
            using (Metafile metafile = (Metafile) this.NativeImage.Clone())
            {
                IntPtr zero = IntPtr.Zero;
                try
                {
                    enhMetafileBits = DevExpress.Office.PInvoke.Win32.GetEnhMetafileBits(metafile.GetHenhmetafile());
                }
                finally
                {
                    if (zero != IntPtr.Zero)
                    {
                        MetafileHelper.DeleteMetafileHandle(zero);
                    }
                }
            }
            return enhMetafileBits;
        }

        public virtual Stream GetEmfImageBytesStream()
        {
            byte[] emfImageBytes = this.GetEmfImageBytes();
            return new MemoryStream(emfImageBytes, 0, emfImageBytes.Length, false, true);
        }

        public override byte[] GetImageBytes(OfficeImageFormat imageFormat) => 
            !imageFormat.Equals(OfficeImageFormat.Wmf) ? (!imageFormat.Equals(OfficeImageFormat.Emf) ? GetBitmapImageBytes(this.NativeImage, imageFormat) : this.GetEmfImageBytes()) : this.GetWmfImageBytes();

        public override byte[] GetImageBytesSafe(OfficeImageFormat imageFormat)
        {
            byte[] imageBytesSafeCore;
            try
            {
                return this.GetImageBytesSafeCore(imageFormat);
            }
            catch
            {
            }
            if (!this.TryRepairImage())
            {
                goto TR_0000;
            }
            else
            {
                try
                {
                    imageBytesSafeCore = this.GetImageBytesSafeCore(imageFormat);
                }
                catch
                {
                    goto TR_0000;
                }
            }
            return imageBytesSafeCore;
        TR_0000:
            this.ReplaceInvalidImage();
            return this.GetImageBytesSafeCore(imageFormat);
        }

        protected byte[] GetImageBytesSafeCore(OfficeImageFormat imageFormat)
        {
            byte[] imageBytesSafe;
            try
            {
                imageBytesSafe = base.GetImageBytesSafe(imageFormat);
            }
            catch
            {
                return this.GetImageCopyPngBytes();
            }
            return imageBytesSafe;
        }

        protected internal override Stream GetImageBytesStream(OfficeImageFormat imageFormat) => 
            !imageFormat.Equals(OfficeImageFormat.Wmf) ? (!imageFormat.Equals(OfficeImageFormat.Emf) ? GetBitmapImageBytesStream(this.NativeImage, imageFormat) : this.GetEmfImageBytesStream()) : this.GetWmfImageBytesStream();

        public override Stream GetImageBytesStreamSafe(OfficeImageFormat imageFormat)
        {
            Stream imageBytesStreamSafeCore;
            try
            {
                return this.GetImageBytesStreamSafeCore(imageFormat);
            }
            catch
            {
            }
            if (!this.TryRepairImage())
            {
                goto TR_0000;
            }
            else
            {
                try
                {
                    imageBytesStreamSafeCore = this.GetImageBytesStreamSafeCore(imageFormat);
                }
                catch
                {
                    goto TR_0000;
                }
            }
            return imageBytesStreamSafeCore;
        TR_0000:
            this.ReplaceInvalidImage();
            return this.GetImageBytesStreamSafeCore(imageFormat);
        }

        protected Stream GetImageBytesStreamSafeCore(OfficeImageFormat imageFormat)
        {
            Stream imageBytesStreamSafe;
            try
            {
                imageBytesStreamSafe = base.GetImageBytesStreamSafe(imageFormat);
            }
            catch
            {
                return this.GetImageCopyPngBytesStream();
            }
            return imageBytesStreamSafe;
        }

        protected internal static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            ImageCodecInfo info;
            imageEncodersInfo.TryGetValue(format.Guid, out info);
            return info;
        }

        private byte[] GetImageCopyPngBytes()
        {
            using (Bitmap bitmap = new Bitmap(this.NativeImage))
            {
                return GetBitmapImageBytes(bitmap, OfficeImageFormat.Png);
            }
        }

        private Stream GetImageCopyPngBytesStream()
        {
            using (Bitmap bitmap = new Bitmap(this.NativeImage))
            {
                return GetBitmapImageBytesStream(bitmap, OfficeImageFormat.Png);
            }
        }

        private float GetResolution(float resolution) => 
            (OSHelper.IsWindows || (resolution != 0f)) ? resolution : 96f;

        public virtual byte[] GetWmfImageBytes()
        {
            byte[] buffer;
            if (!OSHelper.IsWindows)
            {
                return (((this.ImageStream == null) || (this.RawFormat != OfficeImageFormat.Wmf)) ? new byte[0] : this.ImageStream.ToArray());
            }
            IntPtr zero = IntPtr.Zero;
            using (MemoryStream stream = new MemoryStream())
            {
                Size sizeInPixels = base.SizeInPixels;
                using (Metafile metafile = MetafileCreator.CreateInstance(stream, base.SizeInPixels.Width + 1, sizeInPixels.Height + 1, MetafileFrameUnit.Pixel))
                {
                    try
                    {
                        using (Graphics graphics = Graphics.FromImage(metafile))
                        {
                            graphics.DrawImage(this.NativeImage, new Rectangle(0, 0, base.SizeInPixels.Width, base.SizeInPixels.Height));
                        }
                        buffer = DevExpress.Office.PInvoke.Win32.GdipEmfToWmfBits(metafile.GetHenhmetafile(), DevExpress.Office.PInvoke.Win32.MapMode.Anisotropic, DevExpress.Office.PInvoke.Win32.EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                    }
                    finally
                    {
                        MetafileHelper.DeleteMetafileHandle(zero);
                        stream.Close();
                    }
                }
            }
            return buffer;
        }

        public virtual Stream GetWmfImageBytesStream()
        {
            byte[] wmfImageBytes = this.GetWmfImageBytes();
            return new MemoryStream(wmfImageBytes, 0, wmfImageBytes.Length, false, true);
        }

        protected internal virtual void OnNativeImageChanged(Size desiredSize)
        {
            this.RaiseNativeImageChanged(desiredSize);
        }

        protected virtual void ReplaceInvalidImage()
        {
            System.Drawing.Image image = new Bitmap(this.nativeImage.Width, this.nativeImage.Height);
            this.SetNativeImage(image, null);
        }

        private void SetNativeImage(System.Drawing.Image value, MemoryStream imageStream)
        {
            if (!ReferenceEquals(this.NativeImage, value))
            {
                this.RaiseNativeImageChanging();
                this.SetNativeImageCore(value, imageStream);
                this.OnNativeImageChanged(value.Size);
            }
        }

        private void SetNativeImageCore(System.Drawing.Image value, MemoryStream imageStream)
        {
            this.nativeImage = value;
            this.imageStream = imageStream;
        }

        protected virtual bool TryRepairImage()
        {
            bool flag;
            try
            {
                if (this.imageStream == null)
                {
                    flag = false;
                }
                else
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(this.imageStream);
                    this.SetNativeImage(image, this.imageStream);
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        protected internal override Size UnitsToDocuments(Size sizeInUnits) => 
            this.EnsureNonZeroSize(Units.PixelsToDocuments(sizeInUnits, this.HorizontalResolution, this.VerticalResolution));

        protected internal override Size UnitsToHundredthsOfMillimeter(Size sizeInUnits) => 
            this.EnsureNonZeroSize(Units.PixelsToHundredthsOfMillimeter(sizeInUnits, this.HorizontalResolution, this.VerticalResolution));

        protected internal override Size UnitsToPixels(Size sizeInUnits) => 
            sizeInUnits;

        protected internal override Size UnitsToTwips(Size sizeInUnits) => 
            this.EnsureNonZeroSize(Units.PixelsToTwips(sizeInUnits, this.HorizontalResolution, this.VerticalResolution));

        public override System.Drawing.Image NativeImage =>
            this.nativeImage;

        public override OfficeImageFormat RawFormat =>
            OfficeImageHelper.GetRtfImageFormat(this.NativeImage.RawFormat);

        public override int PaletteLength =>
            this.NativeImage.Palette.Entries.Length;

        public override Size SizeInOriginalUnits =>
            this.NativeImage.Size;

        public override OfficePixelFormat PixelFormat =>
            (OfficePixelFormat) this.NativeImage.PixelFormat;

        public override float HorizontalResolution =>
            this.GetResolution(this.NativeImage.HorizontalResolution);

        public override float VerticalResolution =>
            this.GetResolution(this.NativeImage.VerticalResolution);

        public MemoryStream ImageStream =>
            this.imageStream;
    }
}

