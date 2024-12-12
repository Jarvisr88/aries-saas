namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions")]
    public class ImageExportOptions : PageByPageExportOptionsBase, IXtraSupportShouldSerialize
    {
        private static readonly Color DefaultPageBorderColor = Color.Black;
        private static readonly ImageFormat DefaultImageFormat = ImageFormat.Png;
        private const int DefaultResolution = 0x60;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Dictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>();
        private const ImageExportMode DefaultExportMode = ImageExportMode.SingleFile;
        private const int DefaultPageBorderWidth = 1;
        private const bool DefaultRetainBackgroundTransparency = false;
        private const DevExpress.XtraPrinting.TextRenderingMode DefaultTextRenderingMode = DevExpress.XtraPrinting.TextRenderingMode.SystemDefault;
        private ImageExportMode exportMode;
        private ImageFormat format;
        private int resolution;
        private Color pageBorderColor;
        private int pageBorderWidth;
        private bool retainBackgroundTransparency;
        private DevExpress.XtraPrinting.TextRenderingMode textRenderingMode;

        static ImageExportOptions()
        {
            ImageFormats[".bmp"] = ImageFormat.Bmp;
            ImageFormats[".gif"] = ImageFormat.Gif;
            ImageFormats[".jpg"] = ImageFormat.Jpeg;
            ImageFormats[".png"] = ImageFormat.Png;
            ImageFormats[".emf"] = ImageFormat.Emf;
            ImageFormats[".wmf"] = ImageFormat.Wmf;
            ImageFormats[".tiff"] = ImageFormat.Tiff;
        }

        public ImageExportOptions()
        {
            this.format = DefaultImageFormat;
            this.resolution = 0x60;
            this.pageBorderColor = DefaultPageBorderColor;
            this.pageBorderWidth = 1;
        }

        private ImageExportOptions(ImageExportOptions source) : base(source)
        {
            this.format = DefaultImageFormat;
            this.resolution = 0x60;
            this.pageBorderColor = DefaultPageBorderColor;
            this.pageBorderWidth = 1;
        }

        public ImageExportOptions(ImageFormat format)
        {
            this.format = DefaultImageFormat;
            this.resolution = 0x60;
            this.pageBorderColor = DefaultPageBorderColor;
            this.pageBorderWidth = 1;
            this.format = format;
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            ImageExportOptions options = (ImageExportOptions) source;
            this.exportMode = options.ExportMode;
            this.format = options.Format;
            this.resolution = options.Resolution;
            this.pageBorderColor = options.PageBorderColor;
            this.pageBorderWidth = options.PageBorderWidth;
            this.retainBackgroundTransparency = options.RetainBackgroundTransparency;
            this.textRenderingMode = options.TextRenderingMode;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new ImageExportOptions(this);

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName == "Format") ? this.ShouldSerializeFormat() : ((propertyName == "PageBorderColor") ? this.ShouldSerializePageBorderColor() : true);

        internal static bool GetImageFormatSupported(ImageFormat format) => 
            ImageFormats.ContainsValue(format);

        protected internal override bool ShouldSerialize() => 
            this.ShouldSerializeFormat() || (this.ShouldSerializePageBorderColor() || ((this.pageBorderWidth != 1) || ((this.exportMode != ImageExportMode.SingleFile) || ((this.resolution != 0x60) || ((this.textRenderingMode != DevExpress.XtraPrinting.TextRenderingMode.SystemDefault) || (this.retainBackgroundTransparency || base.ShouldSerialize()))))));

        private bool ShouldSerializeFormat() => 
            !ReferenceEquals(this.format, DefaultImageFormat);

        private bool ShouldSerializePageBorderColor() => 
            this.PageBorderColor != DefaultPageBorderColor;

        [Description("Specifies the page border color when a document is exported to a single image page-by-page."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.PageBorderColor"), TypeConverter(typeof(ImagePageBorderColorConverter)), XtraSerializableProperty]
        public Color PageBorderColor
        {
            get => 
                this.pageBorderColor;
            set => 
                this.pageBorderColor = value;
        }

        [Description("Specifies the page border width (in pixels) when a document is exported to a single image page-by-page."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.PageBorderWidth"), DefaultValue(1), TypeConverter(typeof(ImagePageBorderWidthConverter)), XtraSerializableProperty]
        public int PageBorderWidth
        {
            get => 
                this.pageBorderWidth;
            set => 
                this.pageBorderWidth = value;
        }

        [Description("Specifies whether document pages are exported to a single or multiple images."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.ExportMode"), DefaultValue(0), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public ImageExportMode ExportMode
        {
            get => 
                this.exportMode;
            set => 
                this.exportMode = value;
        }

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase
        {
            get
            {
                switch (this.ExportMode)
                {
                    case ImageExportMode.SingleFile:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFile;

                    case ImageExportMode.SingleFilePageByPage:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;

                    case ImageExportMode.DifferentFiles:
                        return DevExpress.XtraPrinting.ExportModeBase.DifferentFiles;
                }
                throw new NotSupportedException();
            }
        }

        [Description("Specifies the range of pages to be exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.PageRange"), TypeConverter(typeof(ImagePageRangeConverter)), XtraSerializableProperty]
        public override string PageRange
        {
            get => 
                base.PageRange;
            set => 
                base.PageRange = value;
        }

        [Description("Specifies the image format for exporting a document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.Format"), TypeConverter(typeof(PSImageFormatConverter)), XtraSerializableProperty]
        public ImageFormat Format
        {
            get => 
                this.format;
            set => 
                this.format = value;
        }

        [Description("Specifies the image resolution (in DPI)."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.Resolution"), DefaultValue(0x60), XtraSerializableProperty]
        public int Resolution
        {
            get => 
                this.resolution;
            set => 
                this.resolution = value;
        }

        [Description("Specifies whether a report is printed with a white or transparent background when the XtraReport.PageColor (BrickGraphics.PageBackColor) property is set to transparent."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.RetainBackgroundTransparency"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool RetainBackgroundTransparency
        {
            get => 
                this.retainBackgroundTransparency;
            set => 
                this.retainBackgroundTransparency = value;
        }

        [Description("Specifies the quality of text rendering in images (especially in images having small DPI values and a transparent background)."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageExportOptions.TextRenderingMode"), DefaultValue(0), XtraSerializableProperty]
        public DevExpress.XtraPrinting.TextRenderingMode TextRenderingMode
        {
            get => 
                this.textRenderingMode;
            set => 
                this.textRenderingMode = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DXHelpExclude(true)]
        public override bool RasterizeImages
        {
            get => 
                true;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DXHelpExclude(true)]
        public override int RasterizationResolution
        {
            get => 
                0x60;
            set
            {
            }
        }
    }
}

