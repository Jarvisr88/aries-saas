namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.Data.Helpers;
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.ImageSource"), TypeConverter(typeof(ImageSourceTypeConverter)), Editor("DevExpress.XtraReports.Design.ImageSourceEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
    public class ImageSource : IDisposable, ICloneable
    {
        private const string Img = "img";
        private const string Svg = "svg";
        private System.Drawing.Image image;
        private DevExpress.Utils.Svg.SvgImage svgImage;

        public ImageSource(DevExpress.Utils.Svg.SvgImage svgImage) : this(svgImage, false)
        {
        }

        public ImageSource(System.Drawing.Image image) : this(image, false)
        {
        }

        public ImageSource(DevExpress.Utils.Svg.SvgImage svgImage, bool isSharedResource)
        {
            this.SvgImage = svgImage;
            this.IsSharedResource = isSharedResource;
        }

        public ImageSource(System.Drawing.Image image, bool isSharedResource)
        {
            this.Image = image;
            this.IsSharedResource = isSharedResource;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageSource(string type, string data)
        {
            byte[] buffer = Convert.FromBase64String(data);
            if (type != "svg")
            {
                this.Image = PSConvert.ImageFromArray(buffer);
                this.IsSharedResource = false;
            }
            else if (!SecurityHelper.IsPartialTrust)
            {
                this.LoadSvgFromBytes(buffer);
            }
        }

        public ImageSource Clone()
        {
            DevExpress.Utils.Svg.SvgImage image3;
            if (!this.HasSvgImage)
            {
                object obj1;
                System.Drawing.Image image = this.Image;
                if (image != null)
                {
                    obj1 = image.Clone();
                }
                else
                {
                    System.Drawing.Image local1 = image;
                    obj1 = null;
                }
                return new ImageSource((System.Drawing.Image) obj1);
            }
            DevExpress.Utils.Svg.SvgImage svgImage = this.SvgImage;
            if (svgImage != null)
            {
                image3 = svgImage.Clone();
            }
            else
            {
                DevExpress.Utils.Svg.SvgImage local2 = svgImage;
                image3 = null;
            }
            return new ImageSource(image3);
        }

        private static System.Drawing.Image CreateRasterizedSvgImage(ImageSource imageSource) => 
            ImagePainter.CreateRasterizedSvgImage(imageSource.SvgImage);

        public void Dispose()
        {
            if (this.image != null)
            {
                if (!this.IsSharedResource)
                {
                    this.image.Dispose();
                }
                this.image = null;
            }
            this.ResetSvgImage();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ImageSource))
            {
                return false;
            }
            ImageSource source = (ImageSource) obj;
            return (this.HasSvgImage ? (source.HasSvgImage && this.GetSvgImageBytes().SequenceEqual<byte>(source.GetSvgImageBytes())) : Equals(this.image, source.image));
        }

        public static ImageSource FromFile(string filename)
        {
            ImageSource source;
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
            try
            {
                System.Drawing.Image image = PSConvert.ImageFromArray(File.ReadAllBytes(Path.GetFullPath(filename)));
                if (image != null)
                {
                    return new ImageSource(image);
                }
            }
            catch
            {
            }
            try
            {
                DevExpress.Utils.Svg.SvgImage svgImage = DevExpress.Utils.Svg.SvgImage.FromFile(filename);
                if (svgImage == null)
                {
                    goto TR_0001;
                }
                else
                {
                    source = new ImageSource(svgImage);
                }
            }
            catch
            {
                goto TR_0001;
            }
            return source;
        TR_0001:
            return null;
        }

        internal static byte[] GetBytes(ImageSource imageSource) => 
            !IsNullOrEmpty(imageSource) ? (imageSource.HasSvgImage ? imageSource.GetSvgImageBytes() : PSConvert.ImageToArray(imageSource.Image)) : new byte[0];

        public override int GetHashCode() => 
            this.HasSvgImage ? HashCodeHelper.CalculateGeneric<object>(this.SvgObject) : HashCodeHelper.CalculateGeneric<System.Drawing.Image>(this.Image);

        internal static System.Drawing.Image GetImage(ImageSource imageSource) => 
            !IsNullOrEmpty(imageSource) ? (imageSource.HasSvgImage ? CreateRasterizedSvgImage(imageSource) : imageSource.Image) : null;

        public SizeF GetImageSize(bool useImageResolution) => 
            !this.IsEmpty ? (!this.HasSvgImage ? (useImageResolution ? PSNativeMethods.GetResolutionImageSize(this.Image) : ((SizeF) this.Image.Size)) : this.GetSvgImageSize()) : SizeF.Empty;

        internal string[] GetMetadata()
        {
            string str = this.HasSvgImage ? "svg" : "img";
            byte[] bytes = GetBytes(this);
            return new string[] { str, Convert.ToBase64String(bytes) };
        }

        private byte[] GetSvgImageBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                SvgSerializer.SaveSvgImageToXML(stream, this.SvgImage);
                return stream.ToArray();
            }
        }

        private SizeF GetSvgImageSize() => 
            new SizeF((float) this.SvgImage.Width, (float) this.SvgImage.Height);

        public static bool IsNullOrEmpty(ImageSource imageSource) => 
            (imageSource == null) || imageSource.IsEmpty;

        private void LoadSvgFromBytes(byte[] bytes)
        {
            if ((bytes != null) && (bytes.Length != 0))
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        this.SvgImage = SvgLoader.LoadFromStream(stream);
                        this.IsSharedResource = false;
                    }
                }
                catch
                {
                }
            }
        }

        private void ResetSvgImage()
        {
            this.svgImage = null;
        }

        object ICloneable.Clone() => 
            this.Clone();

        private void TraceSvgImageUnknownTags(DevExpress.Utils.Svg.SvgImage svg)
        {
            if ((svg != null) && svg.UnknownTags.Any<string>())
            {
                Tracer.TraceWarning("DXperience.Reporting", "The following SVG elements will be ignored: " + string.Join(", ", svg.UnknownTags.Distinct<string>()));
            }
        }

        internal bool IsSharedResource { get; private set; }

        [Browsable(false)]
        public System.Drawing.Image Image
        {
            get => 
                this.image;
            private set
            {
                this.image = value;
                if (this.HasSvgImage)
                {
                    this.ResetSvgImage();
                }
            }
        }

        [Browsable(false)]
        public DevExpress.Utils.Svg.SvgImage SvgImage
        {
            get => 
                this.svgImage;
            private set
            {
                this.svgImage = value;
                this.image = null;
                this.TraceSvgImageUnknownTags(this.svgImage);
            }
        }

        [Browsable(false)]
        public bool IsEmpty =>
            !this.HasSvgImage && ReferenceEquals(this.Image, null);

        internal object ImageInstance =>
            this.HasSvgImage ? this.SvgObject : this.Image;

        internal bool HasSvgImage =>
            !SecurityHelper.IsPartialTrust && (this.SvgObject != null);

        private object SvgObject =>
            this.SvgImage;
    }
}

