namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(XETextSupportedImageBrickExporter))]
    public class XETextSupportedImageBrick : XETextBrick
    {
        private bool useImageResolution;
        private bool disposeImage;
        private const ImageSizeMode DefaultSizeMode = ImageSizeMode.StretchImage;
        private DevExpress.XtraPrinting.Native.ImageEntry imageEntry;
        protected ImageSizeMode fSizeMode;

        public XETextSupportedImageBrick()
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
        }

        public XETextSupportedImageBrick(BrickStyle style) : base(style)
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
        }

        internal XETextSupportedImageBrick(XETextSupportedImageBrick brick) : base(brick)
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.fSizeMode = ImageSizeMode.StretchImage;
            this.fSizeMode = brick.SizeMode;
            this.useImageResolution = brick.UseImageResolution;
            this.disposeImage = brick.DisposeImage;
            this.imageEntry = brick.ImageEntry;
            base.ImageAlignmentCore = brick.ImageAlignment;
        }

        public override void Dispose()
        {
            if (this.DisposeImage && (this.imageEntry.Image != null))
            {
                this.imageEntry.Image.Dispose();
                this.imageEntry.Image = null;
            }
            base.Dispose();
        }

        private static System.Drawing.Image HandleImage(System.Drawing.Image value) => 
            value;

        protected override bool ShouldSerializeCore(string propertyName) => 
            (propertyName != "ImageEntry") ? base.ShouldSerializeCore(propertyName) : this.ShouldSerializeImageEntryInternal();

        internal bool ShouldSerializeImageEntryInternal() => 
            this.Image != null;

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF brickRect, IPrintingSystemContext context) => 
            pageBottom;

        [Description("")]
        public bool DisposeImage
        {
            get => 
                this.disposeImage;
            set => 
                this.disposeImage = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.Cached)]
        public DevExpress.XtraPrinting.Native.ImageEntry ImageEntry
        {
            get => 
                this.imageEntry;
            set => 
                this.imageEntry = value;
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

        [EditorBrowsable(EditorBrowsableState.Never), Description(""), DefaultValue((string) null)]
        public DevExpress.XtraPrinting.Drawing.ImageSource ImageSource
        {
            get => 
                this.imageEntry.ImageSource;
            set => 
                this.imageEntry.ImageSource = value;
        }

        [Description(""), XtraSerializableProperty, DefaultValue(1)]
        public ImageSizeMode SizeMode
        {
            get => 
                this.fSizeMode;
            set => 
                this.fSizeMode = value;
        }

        [Description(""), XtraSerializableProperty, DefaultValue(false)]
        public bool UseImageResolution
        {
            get => 
                this.useImageResolution;
            set => 
                this.useImageResolution = value;
        }

        [Description("")]
        public override string BrickType =>
            "Image";

        [Description(""), DefaultValue(0), XtraSerializableProperty]
        public virtual DevExpress.XtraPrinting.ImageAlignment ImageAlignment
        {
            get => 
                base.ImageAlignmentCore;
            set => 
                base.ImageAlignmentCore = value;
        }
    }
}

