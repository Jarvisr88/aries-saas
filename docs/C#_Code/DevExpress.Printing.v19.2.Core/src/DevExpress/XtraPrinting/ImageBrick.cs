namespace DevExpress.XtraPrinting
{
    using DevExpress.Emf;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(ImageBrickExporter))]
    public class ImageBrick : VisualBrick, IImageBrick, IVisualBrick, IBaseBrick, IBrick, IXtraSupportAfterDeserialize
    {
        private const ImageSizeMode DefaultSizeMode = ImageSizeMode.StretchImage;
        private DevExpress.XtraPrinting.Native.ImageEntry imageEntry;
        protected ImageSizeMode fSizeMode;
        private string htmlImageUrl;
        private bool useImageResolution;
        private bool disposeImage;

        public ImageBrick()
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
        }

        public ImageBrick(BrickStyle style) : base(style)
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
        }

        public ImageBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
        }

        internal ImageBrick(ImageBrick imageBrick) : base(imageBrick)
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
            this.fSizeMode = imageBrick.SizeMode;
            this.htmlImageUrl = imageBrick.HtmlImageUrl;
            this.useImageResolution = imageBrick.UseImageResolution;
            this.disposeImage = imageBrick.DisposeImage;
            this.imageEntry = imageBrick.ImageEntry;
            base.ImageAlignmentCore = imageBrick.ImageAlignment;
            this.EmfMetafile = imageBrick.EmfMetafile;
        }

        public ImageBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor) : base(sides, borderWidth, borderColor, backColor)
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
        }

        public override object Clone() => 
            new ImageBrick(this);

        void IXtraSupportAfterDeserialize.AfterDeserialize(XtraItemEventArgs e)
        {
            if (e.Item.Name == "ImageEntry")
            {
                DocumentSerializationOptions.AddImageEntryToCache(e);
            }
        }

        public override void Dispose()
        {
            if (this.DisposeImage && (this.imageEntry != null))
            {
                this.imageEntry.Dispose();
                this.imageEntry = null;
            }
            base.Dispose();
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "ImageEntry") ? ((propertyName != "EmfMetafile") ? base.ShouldSerializeCore(propertyName) : this.ShouldSerializeEmfMetafile()) : this.ShouldSerializeImageEntryInternal();

        private bool ShouldSerializeEmfMetafile() => 
            this.EmfMetafile != null;

        internal bool ShouldSerializeImageEntryInternal() => 
            !DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(this.ImageSource);

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF brickRect, IPrintingSystemContext context) => 
            pageBottom;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get => 
                base.Text;
            set => 
                base.Text = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override object TextValue
        {
            get => 
                base.TextValue;
            set => 
                base.TextValue = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string TextValueFormatString
        {
            get => 
                base.TextValueFormatString;
            set => 
                base.TextValueFormatString = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string XlsxFormatString
        {
            get => 
                base.XlsxFormatString;
            set => 
                base.XlsxFormatString = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual System.Drawing.Image Image
        {
            get
            {
                DevExpress.XtraPrinting.Drawing.ImageSource imageSource = this.ImageSource;
                if (imageSource != null)
                {
                    return imageSource.Image;
                }
                DevExpress.XtraPrinting.Drawing.ImageSource local1 = imageSource;
                return null;
            }
            set => 
                this.ImageSource = (value != null) ? new DevExpress.XtraPrinting.Drawing.ImageSource(value) : null;
        }

        [Description("Defines the image displayed within the brick."), DefaultValue((string) null)]
        public virtual DevExpress.XtraPrinting.Drawing.ImageSource ImageSource
        {
            get => 
                this.imageEntry.ImageSource;
            set => 
                this.imageEntry.ImageSource = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.Cached)]
        public DevExpress.XtraPrinting.Native.ImageEntry ImageEntry
        {
            get => 
                this.imageEntry;
            set => 
                this.imageEntry = value;
        }

        [Description("Specifies the size mode for the ImageBrick."), XtraSerializableProperty, DefaultValue(1)]
        public ImageSizeMode SizeMode
        {
            get => 
                this.fSizeMode;
            set => 
                this.fSizeMode = value;
        }

        [Description("Gets or sets the path to the image to display in the ImageBrick."), XtraSerializableProperty, DefaultValue((string) null)]
        public string HtmlImageUrl
        {
            get => 
                this.htmlImageUrl;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.htmlImageUrl = value;
                }
            }
        }

        [Description("Gets or sets whether to use the original image DPI (dots per inch) value when the image is rendered in a document."), XtraSerializableProperty, DefaultValue(false)]
        public bool UseImageResolution
        {
            get => 
                this.useImageResolution;
            set => 
                this.useImageResolution = value;
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Image";

        [Description("Gets or sets the alignment of the image displayed within the current brick."), DefaultValue(0), XtraSerializableProperty]
        public virtual DevExpress.XtraPrinting.ImageAlignment ImageAlignment
        {
            get => 
                base.ImageAlignmentCore;
            set => 
                base.ImageAlignmentCore = value;
        }

        [Browsable(false), XtraSerializableProperty, EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.Emf.EmfMetafile EmfMetafile
        {
            get => 
                base.GetValue<DevExpress.Emf.EmfMetafile>(BrickAttachedProperties.EmfMetafile, null);
            set => 
                base.SetAttachedValue<DevExpress.Emf.EmfMetafile>(BrickAttachedProperties.EmfMetafile, value, null);
        }

        [Description("Gets or sets a value indicating whether it is necessary to dispose of an image assigned to the ImageBrick.ImageSource property, when disposing the ImageBrick object.")]
        public bool DisposeImage
        {
            get => 
                this.disposeImage;
            set => 
                this.disposeImage = value;
        }
    }
}

