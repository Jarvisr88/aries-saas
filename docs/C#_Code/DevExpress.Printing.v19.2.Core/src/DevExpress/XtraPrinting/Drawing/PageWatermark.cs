namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.Printing;
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Design;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class PageWatermark : StorableObjectBase, IDisposable
    {
        private string text = "";
        private int transparency = 50;
        private int imageTransparency;
        private bool imageTiling;
        private Color foreColor = DXColor.Red;
        private DevExpress.XtraPrinting.Drawing.ImageViewMode imageViewMode;
        private DirectionMode textDirection = DirectionMode.ForwardDiagonal;
        private ContentAlignment imageAlign = ContentAlignment.MiddleCenter;
        private System.Drawing.Font font;
        private DevExpress.XtraPrinting.Drawing.ImageSource imageSource;
        private bool showBehind = true;
        private DevExpress.XtraPrinting.Drawing.ImageSource actualImageSource;
        private System.Drawing.StringFormat sf = new System.Drawing.StringFormat();

        public PageWatermark()
        {
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;
            this.font = CreateDefaultFont();
        }

        internal void CopyFromInternal(PageWatermark watermark)
        {
            if (watermark != null)
            {
                DevExpress.XtraPrinting.Drawing.ImageSource source1;
                this.DisposeGdiResources();
                this.text = watermark.text;
                this.showBehind = watermark.showBehind;
                this.transparency = watermark.transparency;
                this.imageTransparency = watermark.imageTransparency;
                this.imageTiling = watermark.imageTiling;
                this.foreColor = watermark.foreColor;
                this.imageViewMode = watermark.imageViewMode;
                this.textDirection = watermark.textDirection;
                this.imageAlign = watermark.imageAlign;
                this.font = (System.Drawing.Font) watermark.font.Clone();
                if (watermark.imageSource != null)
                {
                    source1 = watermark.imageSource.Clone();
                }
                else
                {
                    DevExpress.XtraPrinting.Drawing.ImageSource imageSource = watermark.imageSource;
                    source1 = null;
                }
                this.imageSource = source1;
            }
        }

        protected static System.Drawing.Font CreateDefaultFont()
        {
            try
            {
                return new System.Drawing.Font("Verdana", 36f);
            }
            catch
            {
                return new System.Drawing.Font("Tahoma", 36f);
            }
        }

        private static Bitmap CreateTransparentBitmap(System.Drawing.Image originalImage, int imageTransparency)
        {
            Bitmap bitmap;
            try
            {
                bitmap = BitmapCreator.CreateClearBitmap(originalImage, 600f);
            }
            catch
            {
                bitmap = BitmapCreator.CreateClearBitmap(originalImage, 96f);
            }
            ImageAttributes attributes = BitmapCreator.CreateTransparencyAttributes(imageTransparency);
            try
            {
                BitmapCreator.TransformBitmap(originalImage, bitmap, attributes);
            }
            catch
            {
                bitmap = BitmapCreator.CreateClearBitmap(originalImage, 96f);
                BitmapCreator.TransformBitmap(originalImage, bitmap, attributes);
            }
            return bitmap;
        }

        public void Dispose()
        {
            this.sf.Dispose();
            this.DisposeGdiResources();
        }

        private void DisposeActualImageSource()
        {
            if (this.actualImageSource != null)
            {
                if (!this.actualImageSource.Equals(this.imageSource))
                {
                    this.actualImageSource.Dispose();
                }
                this.actualImageSource = null;
            }
        }

        private void DisposeGdiResources()
        {
            if (this.font != null)
            {
                this.font.Dispose();
                this.font = null;
            }
            this.DisposeActualImageSource();
            if (this.imageSource != null)
            {
                this.imageSource.Dispose();
                this.imageSource = null;
            }
        }

        protected internal virtual void Draw(IGraphics gr, RectangleF rect, int pageIndex, int pageCount)
        {
            try
            {
                this.DrawPictureWatermark(gr, rect);
            }
            catch (OutOfMemoryException)
            {
            }
            catch (ArgumentException)
            {
            }
            this.DrawTextWatermark(gr, rect);
        }

        protected void DrawPictureWatermark(IGraphics gr, RectangleF rect)
        {
            if (!DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(this.ActualImageSource))
            {
                SizeF ef = GraphicsUnitConverter.DipToDoc(this.ActualImageSource.GetImageSize(true));
                RectangleF ef2 = new RectangleF((PointF) Point.Round(rect.Location), ef);
                switch (this.imageViewMode)
                {
                    case DevExpress.XtraPrinting.Drawing.ImageViewMode.Clip:
                    {
                        RectangleF ef4 = rect;
                        ef4.Intersect(gr.ClipBounds);
                        gr.ClipBounds = ef4;
                        try
                        {
                            if (!this.imageTiling)
                            {
                                ImagePainter.Draw(this.ActualImageSource, gr, RectHelper.AlignRectangleF(ef2, rect, this.imageAlign));
                            }
                            else
                            {
                                WatermarkHelper.DrawTileImage(gr, this.ActualImageSource, 1f, rect);
                            }
                        }
                        finally
                        {
                            RectangleF ef3;
                            gr.ClipBounds = ef3;
                        }
                        break;
                    }
                    case DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch:
                        ImagePainter.Draw(this.ActualImageSource, gr, rect);
                        return;

                    case DevExpress.XtraPrinting.Drawing.ImageViewMode.Zoom:
                    {
                        if (this.imageTiling)
                        {
                            float adjustedScale = WatermarkHelper.GetAdjustedScale(rect.Size, ef2.Size);
                            WatermarkHelper.DrawTileImage(gr, this.ActualImageSource, adjustedScale, rect);
                            return;
                        }
                        Size adjustedSize = WatermarkHelper.GetAdjustedSize(rect.Size, ef2.Size);
                        ContentAlignment alignment = WatermarkHelper.GetAdjustedAlignment(rect.Size, ef2.Size, this.imageAlign);
                        ImagePainter.Draw(this.ActualImageSource, gr, RectHelper.AlignRectangleF(new Rectangle(Point.Empty, adjustedSize), rect, alignment));
                        break;
                    }
                    default:
                        return;
                }
            }
        }

        protected void DrawTextWatermark(IGraphics gr, RectangleF rect)
        {
            if (!string.IsNullOrEmpty(this.text))
            {
                RectangleF clipBounds = gr.ClipBounds;
                using (Brush brush = new SolidBrush(Color.FromArgb(0xff - Math.Max(0, Math.Min(0xff, this.transparency)), this.foreColor)))
                {
                    RectangleF ef2 = rect;
                    ef2.Intersect(clipBounds);
                    gr.ClipBounds = ef2;
                    try
                    {
                        WatermarkHelper.DrawWatermark(this.textDirection, gr, this.text, this.font, brush, Rectangle.Round(rect), this.sf);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        gr.ClipBounds = clipBounds;
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            PageWatermark watermark = obj as PageWatermark;
            return ((watermark == null) ? base.Equals(obj) : (Equals(this.text, watermark.text) && (Equals(this.showBehind, watermark.showBehind) && (Equals(this.transparency, watermark.transparency) && (Equals(this.imageTransparency, watermark.ImageTransparency) && (Equals(this.imageTiling, watermark.imageTiling) && (Equals(this.foreColor, watermark.foreColor) && (Equals(this.imageViewMode, watermark.imageViewMode) && (Equals(this.imageAlign, watermark.imageAlign) && (Equals(this.textDirection, watermark.textDirection) && (Equals(this.font, watermark.font) && ImageComparer.Equals(this.imageSource, watermark.imageSource))))))))))));
        }

        private static DevExpress.XtraPrinting.Drawing.ImageSource GetActualImageSource(DevExpress.XtraPrinting.Drawing.ImageSource original, int imageTransparency) => 
            !DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(original) ? ((original.HasSvgImage || (imageTransparency == 0)) ? original : new DevExpress.XtraPrinting.Drawing.ImageSource(CreateTransparentBitmap(original.Image, imageTransparency))) : null;

        public override int GetHashCode() => 
            base.GetHashCode();

        private void ResetImageSource()
        {
            this.ImageSource = null;
        }

        private bool ShouldSerializeFont()
        {
            using (System.Drawing.Font font = CreateDefaultFont())
            {
                return !ReferenceEquals(this.font, font);
            }
        }

        private bool ShouldSerializeForeColor() => 
            this.ForeColor != DXColor.Red;

        private bool ShouldSerializeImage() => 
            false;

        private bool ShouldSerializeImageSource() => 
            !DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(this.ImageSource);

        internal System.Drawing.StringFormat StringFormat =>
            this.sf;

        [Description("Gets or sets a value indicating whether a watermark should be printed behind or in front of the contents of a page."), XtraSerializableProperty, DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.ShowBehind")]
        public bool ShowBehind
        {
            get => 
                this.showBehind;
            set => 
                this.showBehind = value;
        }

        [XtraSerializableProperty(XtraSerializationFlags.LoadOnly), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue((string) null), TypeConverter(typeof(ImageTypeConverter)), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Description(""), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.ImageSource"), DefaultValue((string) null), XtraSerializableProperty]
        public virtual DevExpress.XtraPrinting.Drawing.ImageSource ImageSource
        {
            get => 
                this.imageSource;
            set
            {
                this.imageSource = value;
                this.DisposeActualImageSource();
            }
        }

        [Description("Gets or sets the position of the PageWatermark's picture."), XtraSerializableProperty, DefaultValue(0x20), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.ImageAlign"), TypeConverter(typeof(ContentAlignmentTypeConverter))]
        public ContentAlignment ImageAlign
        {
            get => 
                this.imageAlign;
            set => 
                this.imageAlign = value;
        }

        [Description("Gets or sets the mode in which a picture PageWatermark is displayed."), XtraSerializableProperty, DefaultValue(0), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.ImageViewMode")]
        public DevExpress.XtraPrinting.Drawing.ImageViewMode ImageViewMode
        {
            get => 
                this.imageViewMode;
            set => 
                this.imageViewMode = value;
        }

        [Description("Gets or sets a value indicating if a PageWatermark's picture should be tiled."), XtraSerializableProperty, DefaultValue(false), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.ImageTiling"), TypeConverter(typeof(BooleanTypeConverter))]
        public bool ImageTiling
        {
            get => 
                this.imageTiling;
            set => 
                this.imageTiling = value;
        }

        [Description("Gets or sets the incline of the PageWatermark's text."), XtraSerializableProperty, DefaultValue(1), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.TextDirection")]
        public DirectionMode TextDirection
        {
            get => 
                this.textDirection;
            set => 
                this.textDirection = value;
        }

        [Description("Gets or sets a PageWatermark's text."), XtraSerializableProperty, DefaultValue(""), TypeConverter(typeof(WMTextConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.Text"), Localizable(true), RefreshProperties(RefreshProperties.All)]
        public string Text
        {
            get => 
                this.text;
            set => 
                this.text = value;
        }

        [Description("Gets or sets the font of the PageWatermark."), XtraSerializableProperty, DefaultValue(typeof(System.Drawing.Font), "Verdana, 36pt"), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.Font"), TypeConverter(typeof(FontTypeConverter))]
        public System.Drawing.Font Font
        {
            get => 
                this.font;
            set
            {
                if (value != null)
                {
                    if (this.font != null)
                    {
                        this.font.Dispose();
                    }
                    this.font = (System.Drawing.Font) value.Clone();
                }
            }
        }

        [Description("Gets or sets the foreground color of the PageWatermark's text."), XtraSerializableProperty, DefaultValue(typeof(Color), "Red"), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.ForeColor")]
        public Color ForeColor
        {
            get => 
                this.foreColor;
            set => 
                this.foreColor = value;
        }

        [Description("Gets or sets the transparency of the PageWatermark's text."), XtraSerializableProperty, DefaultValue(50), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.TextTransparency")]
        public int TextTransparency
        {
            get => 
                this.transparency;
            set => 
                this.transparency = Math.Max(0, Math.Min(value, 0xff));
        }

        [Description("Gets or sets the transparency of the watermark's image."), XtraSerializableProperty, DefaultValue(0), TypeConverter(typeof(WatermarkImageTransparencyConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.PageWatermark.ImageTransparency")]
        public int ImageTransparency
        {
            get => 
                this.imageTransparency;
            set
            {
                value = Math.Max(0, Math.Min(value, 0xff));
                if (this.imageTransparency != value)
                {
                    this.imageTransparency = value;
                    this.DisposeActualImageSource();
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.XtraPrinting.Drawing.ImageSource ActualImageSource
        {
            get
            {
                this.actualImageSource ??= GetActualImageSource(this.imageSource, this.ImageTransparency);
                return this.actualImageSource;
            }
        }

        [Browsable(false)]
        public bool IsEmpty =>
            string.IsNullOrEmpty(this.text) && DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(this.imageSource);
    }
}

