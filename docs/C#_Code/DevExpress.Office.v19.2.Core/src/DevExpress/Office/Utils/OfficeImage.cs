namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public abstract class OfficeImage : IDisposable
    {
        private static readonly Dictionary<OfficeImageFormat, string> contentTypeTable = CreateContentTypeTable();
        private static readonly Dictionary<OfficeImageFormat, string> extenstionTable = CreateExtenstionTable();
        private bool suppressStoreToFile;
        private string uri = string.Empty;
        private EventHandler onNativeImageChanging;
        private NativeImageChangedEventHandler onNativeImageChanged;

        [Description("Intended for internal use.")]
        public event NativeImageChangedEventHandler NativeImageChanged
        {
            add
            {
                this.onNativeImageChanged += value;
            }
            remove
            {
                this.onNativeImageChanged -= value;
            }
        }

        [Description("Intended for internal use.")]
        public event EventHandler NativeImageChanging
        {
            add
            {
                this.onNativeImageChanging += value;
            }
            remove
            {
                this.onNativeImageChanging -= value;
            }
        }

        protected OfficeImage()
        {
        }

        protected internal abstract Size CalculateImageSizeInModelUnits(DocumentModelUnitConverter unitConverter);
        public abstract bool CanGetImageBytes(OfficeImageFormat imageFormat);
        public OfficeImage Clone(IDocumentModel target)
        {
            OfficeImage image = this.CreateClone(target);
            image.CopyFrom(this);
            return image;
        }

        protected virtual void CopyFrom(OfficeImage officeImage)
        {
            this.SuppressStoreInternal = officeImage.SuppressStoreInternal;
            this.Uri = officeImage.Uri;
        }

        protected abstract OfficeImage CreateClone(IDocumentModel target);
        private static Dictionary<OfficeImageFormat, string> CreateContentTypeTable() => 
            new Dictionary<OfficeImageFormat, string> { 
                { 
                    OfficeImageFormat.Jpeg,
                    "image/jpeg"
                },
                { 
                    OfficeImageFormat.Png,
                    "image/png"
                },
                { 
                    OfficeImageFormat.Bmp,
                    "image/bitmap"
                },
                { 
                    OfficeImageFormat.Tiff,
                    "image/tiff"
                },
                { 
                    OfficeImageFormat.Gif,
                    "image/gif"
                },
                { 
                    OfficeImageFormat.Icon,
                    "image/x-icon"
                },
                { 
                    OfficeImageFormat.Wmf,
                    "application/x-msmetafile"
                },
                { 
                    OfficeImageFormat.Emf,
                    "application/x-msmetafile"
                }
            };

        private static Dictionary<OfficeImageFormat, string> CreateExtenstionTable() => 
            new Dictionary<OfficeImageFormat, string> { 
                { 
                    OfficeImageFormat.Bmp,
                    "bmp"
                },
                { 
                    OfficeImageFormat.Emf,
                    "emf"
                },
                { 
                    OfficeImageFormat.Gif,
                    "gif"
                },
                { 
                    OfficeImageFormat.Icon,
                    "ico"
                },
                { 
                    OfficeImageFormat.Jpeg,
                    "jpg"
                },
                { 
                    OfficeImageFormat.Png,
                    "png"
                },
                { 
                    OfficeImageFormat.Tiff,
                    "tif"
                },
                { 
                    OfficeImageFormat.Wmf,
                    "wmf"
                }
            };

        public static OfficeNativeImage CreateImage(MemoryStreamBasedImage streamBasedImage)
        {
            Image image = streamBasedImage.Image;
            if (image.RawFormat.Equals(ImageFormat.Wmf))
            {
                if ((!DevExpress.Utils.AzureCompatibility.Enable && OSHelper.IsWindows) || (streamBasedImage.ImageStream == null))
                {
                    return new OfficeWmfImageWin((Metafile) image, streamBasedImage.Id);
                }
                return new OfficeWmfImageWinAzure((Metafile) image, ImageLoaderHelper.GetMemoryStream(streamBasedImage.ImageStream, -1, true), streamBasedImage.Id);
            }
            if (!image.RawFormat.Equals(ImageFormat.Emf))
            {
                return new OfficeImageWin(streamBasedImage.Image, streamBasedImage.ImageStream, streamBasedImage.Id);
            }
            if ((!DevExpress.Utils.AzureCompatibility.Enable && OSHelper.IsWindows) || (streamBasedImage.ImageStream == null))
            {
                return new OfficeEmfImageWin((Metafile) image, streamBasedImage.Id);
            }
            return new OfficeEmfImageWinAzure((Metafile) image, ImageLoaderHelper.GetMemoryStream(streamBasedImage.ImageStream, -1, true), streamBasedImage.Id);
        }

        [ComVisible(false)]
        public static OfficeNativeImage CreateImage(Image nativeImage) => 
            CreateImage(new MemoryStreamBasedImage(nativeImage, null));

        [ComVisible(false)]
        public static OfficeNativeImage CreateImage(Stream stream) => 
            CreateImage(stream, -1, null);

        [ComVisible(false)]
        public static OfficeNativeImage CreateImage(Stream stream, IUniqueImageId id) => 
            CreateImage(stream, -1, id);

        private static OfficeNativeImage CreateImage(Stream stream, int length, IUniqueImageId id)
        {
            MemoryStreamBasedImage image;
            MemoryStream memoryStream = ImageLoaderHelper.GetMemoryStream(stream, length);
            try
            {
                image = ImageLoaderHelper.ImageFromStream(memoryStream);
            }
            catch (ArgumentException)
            {
                memoryStream.Position = 0L;
                if ((memoryStream.Length < 2L) || ((memoryStream.ReadByte() != 0x1f) || (memoryStream.ReadByte() != 0x8b)))
                {
                    throw;
                }
                memoryStream.Position = 0L;
                MemoryStream stream3 = new MemoryStream();
                using (GZipStream stream4 = new GZipStream(memoryStream, CompressionMode.Decompress, false))
                {
                    byte[] buffer = new byte[0x1000];
                    while (true)
                    {
                        int count = stream4.Read(buffer, 0, buffer.Length);
                        if (count <= 0)
                        {
                            break;
                        }
                        stream3.Write(buffer, 0, count);
                    }
                }
                stream3.Position = 0L;
                image = ImageLoaderHelper.ImageFromStream(stream3);
            }
            IUniqueImageId id1 = id;
            if (id == null)
            {
                IUniqueImageId local2 = id;
                id1 = new NativeImageId(image.Image);
            }
            return CreateImage(new MemoryStreamBasedImage(image.Image, image.ImageStream, id1));
        }

        public virtual void DiscardCachedData()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            bool flag1 = disposing;
        }

        protected internal virtual void EnsureLoadComplete()
        {
            this.EnsureLoadComplete(TimeSpan.FromSeconds(5.0));
        }

        protected internal virtual void EnsureLoadComplete(TimeSpan timeout)
        {
        }

        [ComVisible(false)]
        public static string GetContentType(OfficeImageFormat imageFormat)
        {
            string str;
            return (!contentTypeTable.TryGetValue(imageFormat, out str) ? string.Empty : str);
        }

        [ComVisible(false)]
        public static string GetExtension(OfficeImageFormat imageFormat)
        {
            string str;
            return (!extenstionTable.TryGetValue(imageFormat, out str) ? string.Empty : str);
        }

        public abstract byte[] GetImageBytes(OfficeImageFormat imageFormat);
        public virtual byte[] GetImageBytesSafe(OfficeImageFormat imageFormat)
        {
            byte[] imageBytes;
            if (imageFormat == OfficeImageFormat.MemoryBmp)
            {
                return this.GetImageBytes(OfficeImageFormat.Png);
            }
            try
            {
                imageBytes = this.GetImageBytes(imageFormat);
            }
            catch
            {
                return this.GetImageBytes(OfficeImageFormat.Png);
            }
            return imageBytes;
        }

        protected internal abstract Stream GetImageBytesStream(OfficeImageFormat imageFormat);
        public virtual Stream GetImageBytesStreamSafe(OfficeImageFormat imageFormat)
        {
            Stream imageBytesStream;
            try
            {
                imageBytesStream = this.GetImageBytesStream(imageFormat);
            }
            catch
            {
                return this.GetImageBytesStream(OfficeImageFormat.Png);
            }
            return imageBytesStream;
        }

        public float GetResolution() => 
            Math.Min(Math.Max(this.VerticalResolution, 96f), 300f);

        public virtual bool IsExportSupported(OfficeImageFormat rawFormat) => 
            true;

        protected internal virtual void RaiseNativeImageChanged(Size desiredImageSize)
        {
            if (this.onNativeImageChanged != null)
            {
                NativeImageChangedEventArgs e = new NativeImageChangedEventArgs(desiredImageSize);
                this.onNativeImageChanged(this, e);
            }
        }

        protected internal virtual void RaiseNativeImageChanging()
        {
            if (this.onNativeImageChanging != null)
            {
                this.onNativeImageChanging(this, EventArgs.Empty);
            }
        }

        protected internal abstract Size UnitsToDocuments(Size sizeInUnits);
        protected internal abstract Size UnitsToHundredthsOfMillimeter(Size sizeInUnits);
        protected internal abstract Size UnitsToPixels(Size sizeInUnits);
        protected internal abstract Size UnitsToTwips(Size sizeInUnits);

        [Description("Provides access to the native Image object.")]
        public abstract Image NativeImage { get; }

        [Description("This property is overridden in OfficeImage descendants to get the size of a native image measured in original units.")]
        public abstract Size SizeInOriginalUnits { get; }

        [Description("Gets the file format of this image.")]
        public abstract OfficeImageFormat RawFormat { get; }

        [Description("Gets the pixel format of the image.")]
        public abstract OfficePixelFormat PixelFormat { get; }

        [Description("Gets the number of colors in the image palette.")]
        public abstract int PaletteLength { get; }

        [Description("Gets the horizontal resolution of the image in pixels per inch.")]
        public abstract float HorizontalResolution { get; }

        [Description("Gets the horizontal resolution of the image in pixels per inch.")]
        public abstract float VerticalResolution { get; }

        [Description("This property is overridden in OfficeImage class descendants to get the URI of the image.")]
        public virtual string Uri
        {
            get => 
                this.uri;
            set => 
                this.uri = value;
        }

        protected internal virtual bool SuppressStore
        {
            get => 
                this.suppressStoreToFile;
            set => 
                this.suppressStoreToFile = value;
        }

        protected internal bool SuppressStoreInternal
        {
            get => 
                this.suppressStoreToFile;
            set => 
                this.suppressStoreToFile = value;
        }

        [Description("Gets the size of an image in pixels.")]
        public Size SizeInPixels =>
            this.UnitsToPixels(this.SizeInOriginalUnits);

        [Description("Gets the size of an image in hundredths of a millimeter.")]
        public Size SizeInHundredthsOfMillimeter =>
            this.UnitsToHundredthsOfMillimeter(this.SizeInOriginalUnits);

        [Description("Gets the size of an image in DocumentUnit.Document units.")]
        public Size SizeInDocuments =>
            this.UnitsToDocuments(this.SizeInOriginalUnits);

        [Description("Gets the size of an image in twips (1,440 twips equals one inch, and 567 twips equals one centimeter).")]
        public Size SizeInTwips =>
            this.UnitsToTwips(this.SizeInOriginalUnits);

        [Description("This property is overridden in OfficeImage descendants to point to a single instance of an image that can be incorporated in different objects.")]
        public virtual OfficeImage RootImage =>
            this;

        public abstract OfficeNativeImage EncapsulatedOfficeNativeImage { get; }

        [Description("For internal use.")]
        public virtual bool IsLoaded =>
            true;

        [Description("For internal use.")]
        public Size DesiredSizeAfterLoad { get; set; }

        [Description("For internal use.")]
        public bool ShouldSetDesiredSizeAfterLoad { get; set; }

        public abstract int ImageCacheKey { get; }
    }
}

