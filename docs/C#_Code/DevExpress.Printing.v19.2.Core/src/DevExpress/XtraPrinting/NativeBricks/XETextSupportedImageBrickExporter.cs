namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    internal class XETextSupportedImageBrickExporter : XETextBrickExporter
    {
        private bool CanSnapImage(float dpi) => 
            ((this.SizeMode == ImageSizeMode.Normal) || ((this.SizeMode == ImageSizeMode.CenterImage) || (this.SizeMode == ImageSizeMode.AutoSize))) ? ((this.ImageSource.Image != null) && ((((float) Math.Round((double) this.ImageSource.Image.HorizontalResolution)) == dpi) && (((float) Math.Round((double) this.ImageSource.Image.VerticalResolution)) == dpi))) : false;

        protected override System.Drawing.Image CreateTableCellImage(ITableExportProvider exportProvider, RectangleF rect)
        {
            System.Drawing.Image image;
            System.Drawing.Image image;
            ExportContext exportContext = exportProvider.ExportContext;
            BrickImageProviderBase base2 = base.CreateImageProvider();
            DevExpress.XtraPrinting.Drawing.ImageSource imageSource = this.ImageSource;
            if (imageSource != null)
            {
                image = imageSource.Image;
            }
            else
            {
                DevExpress.XtraPrinting.Drawing.ImageSource local1 = imageSource;
                image = null;
            }
            if (image is Metafile)
            {
                return ((!VisualBrickExporter.IsMetafileExportAvailable || exportContext.ExportOptions.RasterizeImages) ? base2.CreateContentImage(exportContext.PrintingSystem, rect, false, (float) exportContext.ExportOptions.RasterizationResolution, ImageFormat.Png, null) : base2.CreateContentMetafileImage(exportContext.PrintingSystem, rect, false));
            }
            DevExpress.XtraPrinting.Drawing.ImageSource source2 = this.ImageSource;
            if (source2 != null)
            {
                image = source2.Image;
            }
            else
            {
                DevExpress.XtraPrinting.Drawing.ImageSource local2 = source2;
                image = null;
            }
            bool patchTransparentBackground = (image != null) && !System.Drawing.Image.IsAlphaPixelFormat(this.ImageSource.Image.PixelFormat);
            return base2.CreateContentImage(exportContext.PrintingSystem, rect, patchTransparentBackground, this.GetImageResolution(), this.GetImageFormat(), null);
        }

        protected override unsafe void DrawClientContent(IGraphics gr, RectangleF clientRect)
        {
            if (!clientRect.IsEmpty && !DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(this.ImageSource))
            {
                RectangleF imageRect = ImageTool.CalculateImageRect(clientRect, this.GetResolutionImageSize(gr), this.SizeMode, this.CombinedBrick.ImageAlignment);
                if (this.CanSnapImage(gr.Dpi))
                {
                    imageRect.Location = SnapPointF(gr, imageRect.Location);
                }
                if (this.ImageSource.HasSvgImage)
                {
                    this.DrawSvgImage(gr, imageRect);
                }
                else
                {
                    this.DrawImage(gr, imageRect);
                }
                float num = 1.25f;
                if ((this.ImageAlignment == DevExpress.XtraPrinting.ImageAlignment.MiddleLeft) && ((base.Style.StringFormat.Value.Alignment == StringAlignment.Near) && (clientRect.X == imageRect.X)))
                {
                    RectangleF* efPtr1 = &clientRect;
                    efPtr1.X += imageRect.Width * num;
                }
                base.DrawClientContent(gr, clientRect);
            }
        }

        private void DrawImage(IGraphics gr, RectangleF imageRect)
        {
            System.Drawing.Image image = (this.SizeMode != ImageSizeMode.Tile) ? this.ImageSource.Image : this.GetTileImage(this.ImageSource.Image, Size.Round(GraphicsUnitConverter.Convert(imageRect.Size, (float) 300f, (float) 96f)));
            gr.DrawImage(image, imageRect);
        }

        private void DrawSvgImage(IGraphics gr, RectangleF imageRect)
        {
            if (this.SizeMode == ImageSizeMode.Tile)
            {
                ImagePainter.DrawTileSvgImage(this.ImageSource.SvgImage, gr, imageRect);
            }
            else
            {
                ImagePainter.DrawSvgImage(this.ImageSource.SvgImage, gr, imageRect, null);
            }
        }

        protected internal override void FillDocxTableCellInternal(IDocxExportProvider exportProvider)
        {
            this.FillTableCellWithImage(exportProvider);
        }

        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider)
        {
            this.FillHtmlTableCellImage(exportProvider);
            exportProvider.SetCellText(base.Text);
        }

        private void FillHtmlTableCellImage(IHtmlExportProvider exportProvider)
        {
            if ((this.ImageSource != null) && (this.ImageSource.HasSvgImage || (this.ImageSource.Image is Metafile)))
            {
                base.FillHtmlTableCellWithImage(exportProvider);
            }
            else
            {
                System.Drawing.Image tileImage;
                Rectangle boundsWithoutBorders = base.GetBoundsWithoutBorders(exportProvider.CurrentData.OriginalBounds);
                Size imageSize = this.GetResolutionImageSize(exportProvider.ExportContext).ToSize();
                Size clientSize = new Size((boundsWithoutBorders.Width - this.CombinedBrick.Padding.Right) - this.CombinedBrick.Padding.Left, (boundsWithoutBorders.Height - this.CombinedBrick.Padding.Top) - this.CombinedBrick.Padding.Bottom);
                if (this.SizeMode != ImageSizeMode.Tile)
                {
                    DevExpress.XtraPrinting.Drawing.ImageSource imageSource = this.ImageSource;
                    if (imageSource != null)
                    {
                        tileImage = imageSource.Image;
                    }
                    else
                    {
                        DevExpress.XtraPrinting.Drawing.ImageSource local2 = imageSource;
                        tileImage = null;
                    }
                }
                else
                {
                    System.Drawing.Image image1;
                    DevExpress.XtraPrinting.Drawing.ImageSource imageSource = this.ImageSource;
                    if (imageSource != null)
                    {
                        image1 = imageSource.Image;
                    }
                    else
                    {
                        DevExpress.XtraPrinting.Drawing.ImageSource local1 = imageSource;
                        image1 = null;
                    }
                    tileImage = this.GetTileImage(image1, clientSize);
                }
                System.Drawing.Image image = tileImage;
                TableCellImageInfo imageInfo = new TableCellImageInfo(imageSize, this.SizeMode, this.ImageAlignment);
                exportProvider.SetCellImage(image, imageInfo, boundsWithoutBorders, this.CombinedBrick.Padding);
            }
        }

        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider)
        {
            this.FillTableCellWithImage(exportProvider);
        }

        private void FillTableCellWithImage(ITableExportProvider exportProvider)
        {
            base.FillTableCellWithImage(exportProvider, this.SizeMode, this.ImageAlignment, exportProvider.CurrentData.Bounds);
        }

        protected internal override void FillTextTableCellInternal(ITableExportProvider exportProvider, bool shouldSplitText)
        {
            TextLayoutBuilder builder = exportProvider as TextLayoutBuilder;
            string textValue = (builder == null) ? base.Text : builder.GetText(base.Text, this.TextValue);
            if (!shouldSplitText)
            {
                exportProvider.SetCellText(textValue);
            }
            else
            {
                char[] separator = new char[] { '\r' };
                string[] strArray = textValue.Split(separator);
                for (int i = 0; i < strArray.Length; i++)
                {
                    char[] trimChars = new char[] { '\n' };
                    strArray[i] = strArray[i].TrimStart(trimChars);
                }
                if (strArray.Length == 1)
                {
                    exportProvider.SetCellText(strArray[0]);
                }
                else
                {
                    exportProvider.SetCellText(strArray);
                }
            }
        }

        protected internal override void FillXlTableCellInternal(IXlExportProvider exportProvider)
        {
            TextExportMode realTextExportMode = this.GetRealTextExportMode(exportProvider);
            exportProvider.SetCellText(this.GetValue(realTextExportMode));
        }

        protected override RectangleF GetClipRect(RectangleF rect, IGraphics gr) => 
            this.CombinedBrick.Padding.Deflate(base.BrickPaint.GetClientRect(rect, this.CombinedBrick.RightToLeftLayout), 300f);

        private float GetEffectiveResolution()
        {
            System.Drawing.Image image1;
            DevExpress.XtraPrinting.Drawing.ImageSource imageSource = this.ImageSource;
            if (imageSource != null)
            {
                image1 = imageSource.Image;
            }
            else
            {
                DevExpress.XtraPrinting.Drawing.ImageSource local1 = imageSource;
                image1 = null;
            }
            if (image1 == null)
            {
                return 96f;
            }
            System.Drawing.Image image = this.ImageSource.Image;
            float verticalResolution = image.VerticalResolution;
            float num2 = Math.Min(Math.Max(verticalResolution, 96f), 300f);
            if ((this.SizeMode == ImageSizeMode.ZoomImage) || (this.SizeMode == ImageSizeMode.Squeeze))
            {
                SizeF size = this.CombinedBrick.Size;
                SizeF ef2 = (SizeF) GraphicsUnitConverter.Convert(image.Size, verticalResolution, (float) 300f);
                float num3 = Math.Min((float) (size.Width / ef2.Width), (float) (size.Height / ef2.Height));
                if (num3 < 1f)
                {
                    num2 = Math.Min((float) 600f, (float) (num2 / num3));
                }
            }
            return num2;
        }

        protected override ImageFormat GetImageFormat()
        {
            System.Drawing.Image image;
            DevExpress.XtraPrinting.Drawing.ImageSource imageSource = this.ImageSource;
            if (imageSource != null)
            {
                image = imageSource.Image;
            }
            else
            {
                DevExpress.XtraPrinting.Drawing.ImageSource local1 = imageSource;
                image = null;
            }
            return ((image != null) ? this.ImageSource.Image.RawFormat : base.GetImageFormat());
        }

        protected override float GetImageResolution() => 
            this.GetEffectiveResolution();

        private Color GetPageBackColor(PrintingSystemBase ps) => 
            PSNativeMethods.ValidateBackgrColor(ps.Graph.PageBackColor);

        private TextExportMode GetRealTextExportMode(IXlExportProvider exportProvider) => 
            ToTextExportMode(base.TextBrick.XlsExportNativeFormat, exportProvider.XlExportContext.XlExportOptions.TextExportMode);

        protected SizeF GetResolutionImageSize(IPrintingSystemContext context) => 
            !DevExpress.XtraPrinting.Drawing.ImageSource.IsNullOrEmpty(this.ImageSource) ? MathMethods.Scale(this.ImageSource.GetImageSize(this.CombinedBrick.UseImageResolution), base.VisualBrick.GetScaleFactor(context)) : ((SizeF) Size.Empty);

        protected override object[] GetSpecificKeyPart() => 
            new object[] { this.SizeMode, this.ImageAlignment, this.ImageSource };

        private System.Drawing.Image GetTileImage(System.Drawing.Image baseImage, Size clientSize)
        {
            System.Drawing.Image image2;
            if (baseImage == null)
            {
                return baseImage;
            }
            try
            {
                VisualBrick brick = base.Brick as VisualBrick;
                object[] keyParts = new object[] { clientSize, HtmlImageHelper.GetImageHashCode(baseImage) };
                object key = new MultiKey(keyParts);
                System.Drawing.Image imageByKey = brick.PrintingSystem.GarbageImages.GetImageByKey(key);
                if (imageByKey != null)
                {
                    image2 = imageByKey;
                }
                else
                {
                    imageByKey = ImageHelper.CreateTileImage(baseImage, clientSize);
                    brick.PrintingSystem.GarbageImages.Add(key, imageByKey);
                    image2 = imageByKey;
                }
            }
            catch
            {
                return baseImage;
            }
            return image2;
        }

        private static object GetValidValue(object value, string text) => 
            ((value is DateTime) || (value is TimeSpan)) ? value : ((!(value is string) || !string.IsNullOrEmpty((string) value)) ? (!(value is IConvertible) ? (((value == DBNull.Value) || value.GetType().IsPrimitive) ? value : text) : ConvertHelper.ToCodeType((IConvertible) value, text)) : text);

        private object GetValue(TextExportMode textExportMode) => 
            (textExportMode != TextExportMode.Text) ? ((this.TextValue != null) ? GetValidValue(this.TextValue, base.Text) : base.TextBrick.ConvertHelper.GetNativeValue(base.Text)) : base.Text;

        private static PointF SnapPointF(IGraphics gr, PointF point) => 
            GraphicsUnitConverter.Convert((PointF) GraphicsUnitConverter.Round(GraphicsUnitConverter.Convert(point, (float) 300f, gr.Dpi)), gr.Dpi, (float) 300f);

        private XETextSupportedImageBrick CombinedBrick =>
            base.Brick as XETextSupportedImageBrick;

        public ImageSizeMode SizeMode
        {
            get => 
                this.CombinedBrick.SizeMode;
            set => 
                this.CombinedBrick.SizeMode = value;
        }

        public DevExpress.XtraPrinting.ImageAlignment ImageAlignment =>
            this.CombinedBrick.ImageAlignment;

        public System.Drawing.Image Image
        {
            get => 
                this.CombinedBrick.Image;
            set => 
                this.CombinedBrick.Image = value;
        }

        public DevExpress.XtraPrinting.Drawing.ImageSource ImageSource =>
            this.CombinedBrick.ImageSource;

        private object TextValue =>
            this.CombinedBrick.TextValue;
    }
}

