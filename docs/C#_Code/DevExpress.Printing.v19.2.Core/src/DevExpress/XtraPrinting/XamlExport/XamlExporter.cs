namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class XamlExporter
    {
        public const string EmbeddedImageConverterName = "embeddedImageConverter";
        public const string RepositoryImageConverterName = "repositoryImageConverter";
        public const string BindingFormatString = "{{Binding Source={{StaticResource {0}}}, Converter={{StaticResource {1}}}}}";
        public const string BindingWithConverterFormatString = "{{Binding RelativeSource={{RelativeSource Self}}, Converter={{StaticResource {0}}}, ConverterParameter={1}}}";
        private static readonly PageWatermark DefaultWatermark = new Watermark();
        private PrintingSystemBase currentPrintingSystem;

        public event EventHandler<DeclareNamespacesEventArgs> DeclareNamespaces;

        public event EventHandler<ImageResolveEventArgs> ImageResolve;

        public event EventHandler<WriteCustomPropertiesEventArgs> WriteCustomProperties;

        public XamlExporter()
        {
            this.EmbedImagesToXaml = true;
        }

        private void AddToResourceMap(string imageResourceKey, XamlExportContext context, BrickBase brick)
        {
            string str = !this.EmbedImagesToXaml ? imageResourceKey : $"{{Binding Source={{StaticResource {imageResourceKey}}}, Converter={{StaticResource {"embeddedImageConverter"}}}}}";
            context.ResourceMap.ImageResourcesDictionary[brick] = str;
        }

        private static bool BrickIsBarCodeOrZip(BrickBase brick) => 
            (brick is BarCodeBrick) || (brick is ZipCodeBrick);

        private void CollectStyles(BrickBase brick, XamlExportContext exportContext)
        {
            RegisterBorderStyle(brick, exportContext);
            RegisterTextBlockStyle(brick, exportContext);
            RegisterLineStyle(brick, exportContext);
            this.RegisterImage(brick, exportContext);
            exportContext.ResourceMap.ShouldAddCheckBoxTemplates = exportContext.ResourceMap.ShouldAddCheckBoxTemplates || (brick is CheckBoxBrick);
            foreach (BrickBase base2 in GetInnerBricks(brick))
            {
                this.CollectStyles(base2, exportContext);
            }
        }

        private ImageResource CreateImageResource(BrickBase brick, float scaleFactor)
        {
            if (brick is ImageBrick)
            {
                System.Drawing.Image image = ((ImageBrick) brick).Image;
                return ((image != null) ? new ImageResource(image) : null);
            }
            if (!XamlResourceHelper.ExporterRequiresImageResource(brick))
            {
                return null;
            }
            VisualBrick brick2 = brick as VisualBrick;
            double ratio = 1.0 / ((double) scaleFactor);
            SizeF size = MathMethods.Scale(brick2.Size, ratio);
            RectangleF rect = new RectangleF(PointF.Empty, size).DocToPixel();
            return new ImageResource((this.currentPrintingSystem.ExportersFactory.GetExporter(brick2) as VisualBrickExporter).DrawContentToImage(this.currentPrintingSystem, this.currentPrintingSystem.GarbageImages, rect, false, XamlResourceHelper.GetExporterGraphicsDpi(brick)));
        }

        private static ImageResource CreateWatermarkImageResource(Page page)
        {
            if ((page != null) && ((page.ActualWatermark != null) && !Equals(page.ActualWatermark, DefaultWatermark)))
            {
                System.Drawing.Image image = ImageSource.GetImage(page.ActualWatermark.ActualImageSource);
                if (image != null)
                {
                    return new ImageResource(image);
                }
            }
            return null;
        }

        internal static XamlBorderStyle CreateXamlBorderStyle(BrickStyle brickStyle)
        {
            Color baseColor = brickStyle.BorderDashStyle.IsSolidLineStyle() ? brickStyle.BorderColor : brickStyle.BackColor;
            if ((brickStyle.BorderWidth == 0f) || (brickStyle.Sides == BorderSide.None))
            {
                baseColor = Color.FromArgb(0, baseColor);
            }
            return new XamlBorderStyle(brickStyle.BorderWidth, baseColor, brickStyle.Sides, brickStyle.BackColor, brickStyle.Padding);
        }

        public void Export(Stream stream, Page page, int pageNumber, int pageCount, XamlCompatibility compatibility, TextMeasurementSystem textMeasurementSystem)
        {
            using (XamlWriter writer = new XamlWriter(stream))
            {
                XamlExportContext exportContext = new XamlExportContext(page, pageNumber, pageCount, new ResourceCache(), new ResourceMap(), compatibility, textMeasurementSystem, this.IsPartialTrustMode, this.EmbedImagesToXaml);
                if (page.Document != null)
                {
                    this.currentPrintingSystem = page.Document.PrintingSystem;
                }
                this.CollectStyles(page, exportContext);
                RectangleF clipRect = new RectangleF(0f, 0f, page.Rect.X + page.Rect.Width, page.PageData.PageSize.Height);
                this.WriteBricks(writer, page, exportContext, page.Location, clipRect, RectangleF.Empty);
                writer.Flush();
            }
            if (this.EmbedImagesToXaml && (this.currentPrintingSystem != null))
            {
                this.currentPrintingSystem.GarbageImages.Clear();
            }
            this.currentPrintingSystem = null;
        }

        private static IList GetInnerBricks(BrickBase brickBase)
        {
            IRichTextBrick brick = brickBase as IRichTextBrick;
            if (brick != null)
            {
                return brick.GetChildBricks();
            }
            if (!(brickBase is BrickContainerBase))
            {
                Brick brick2 = brickBase as Brick;
                if ((brick2 != null) && !ReferenceEquals(brick2.Bricks, EmptyBrickCollection.Instance))
                {
                    return brick2.Bricks;
                }
            }
            return brickBase.InnerBrickList;
        }

        private void RaiseDeclareNamespaces(XamlWriter writer)
        {
            if (this.DeclareNamespaces != null)
            {
                this.DeclareNamespaces(this, new DeclareNamespacesEventArgs(writer));
            }
        }

        private void RaiseWriteCustomProperties(XamlWriter writer, object obj)
        {
            if (this.WriteCustomProperties != null)
            {
                this.WriteCustomProperties(this, new WriteCustomPropertiesEventArgs(writer, obj));
            }
        }

        private static void RegisterBorderStyle(BrickBase brick, XamlExportContext exportContext)
        {
            if (XamlResourceHelper.ExporterRequiresBorderStyle(brick))
            {
                VisualBrick brick2 = brick as VisualBrick;
                if (brick2 != null)
                {
                    BrickStyle brickStyle = brick2.Style.Clone() as BrickStyle;
                    if (BrickIsBarCodeOrZip(brick))
                    {
                        brickStyle.Padding = PaddingInfo.Empty;
                    }
                    XamlBorderStyle style = CreateXamlBorderStyle(brickStyle);
                    exportContext.ResourceMap.BorderStylesDictionary[brick2] = exportContext.ResourceCache.RegisterBorderStyle(style);
                    if (brick2.Style.BorderDashStyle.IsDashedOrDottedLineStyle())
                    {
                        XamlLineStyle style3 = new XamlLineStyle(brick2.Style.BorderColor, brick2.BorderWidth, VisualBrick.GetDashPattern(brick2.BorderDashStyle));
                        exportContext.ResourceMap.BorderDashStylesDictionary[brick2] = exportContext.ResourceCache.RegisterBorderDashStyle(style3);
                    }
                }
            }
        }

        private void RegisterImage(BrickBase brick, XamlExportContext exportContext)
        {
            if ((brick is ImageBrick) && ReferenceEquals(((ImageBrick) brick).Image, EmptyImage.Instance))
            {
                ImageBrick brick2 = (ImageBrick) brick;
                string str = $"{{Binding RelativeSource={{RelativeSource Self}}, Converter={{StaticResource {"repositoryImageConverter"}}}, ConverterParameter={$"{RepositoryImageHelper.GetDocumentId(this.currentPrintingSystem)}_{string.Empty}"}}}";
                exportContext.ResourceMap.ImageResourcesDictionary[brick] = str;
            }
            else
            {
                ImageResource imageResource = CreateWatermarkImageResource(brick as Page);
                this.RegisterImageResource(imageResource, new Func<ImageResource, string>(exportContext.ResourceCache.RegisterWatermarkImageResource), exportContext, brick);
                if (exportContext.Page.Document != null)
                {
                    float scaleFactor = exportContext.Page.Document.ScaleFactor;
                    ImageResource resource2 = this.CreateImageResource(brick, scaleFactor);
                    this.RegisterImageResource(resource2, new Func<ImageResource, string>(exportContext.ResourceCache.RegisterImageResource), exportContext, brick);
                }
            }
        }

        private void RegisterImageResource(ImageResource imageResource, Func<ImageResource, string> addToResourceCache, XamlExportContext exportContext, BrickBase brick)
        {
            if (imageResource != null)
            {
                string imageResourceKey = this.EmbedImagesToXaml ? addToResourceCache(imageResource) : this.ResolveImage(imageResource.Image);
                this.AddToResourceMap(imageResourceKey, exportContext, brick);
            }
        }

        private static void RegisterLineStyle(BrickBase brick, XamlExportContext exportContext)
        {
            LineBrick brick2 = brick as LineBrick;
            if (brick2 != null)
            {
                XamlLineStyle style = new XamlLineStyle(brick2.Style.ForeColor, brick2.LineWidth, VisualBrick.GetDashPattern(brick2.LineStyle));
                exportContext.ResourceMap.LineStylesDictionary[brick2] = exportContext.ResourceCache.RegisterLineStyle(style);
            }
        }

        private static void RegisterTextBlockStyle(BrickBase brick, XamlExportContext exportContext)
        {
            TextBrick brick2 = brick as TextBrick;
            if (brick2 != null)
            {
                XamlTextBlockStyle style = new XamlTextBlockStyle(brick2.Font, brick2.Style.TextAlignment, (brick2.Style.StringFormat.FormatFlags & StringFormatFlags.NoWrap) == 0, brick2.Style.ForeColor, brick2.StringFormat.Trimming);
                exportContext.ResourceMap.TextBlockStylesDictionary[brick2] = exportContext.ResourceCache.RegisterTextBlockStyle(style);
            }
        }

        private string ResolveImage(System.Drawing.Image image)
        {
            if ((this.ImageResolve == null) || (image == null))
            {
                return null;
            }
            ImageResolveEventArgs e = new ImageResolveEventArgs(image);
            this.ImageResolve(this, e);
            return e.Uri;
        }

        private void WriteBricks(XamlWriter writer, BrickBase brickBase, XamlExportContext exportContext, PointF offset, RectangleF clipRect, RectangleF exportBrickClipRect)
        {
            Brick brick = brickBase as Brick;
            if ((brick == null) || brick.IsVisible)
            {
                BrickXamlExporterBase base2 = BrickXamlExporterFactory.CreateExporter(brickBase);
                if (base2 != null)
                {
                    base2.WriteBrickToXaml(writer, brickBase, exportContext, exportBrickClipRect, new Action<XamlWriter>(this.RaiseDeclareNamespaces), new Action<XamlWriter, object>(this.RaiseWriteCustomProperties));
                }
                BrickIterator iterator = new BrickIterator(brickBase, GetInnerBricks(brickBase), offset, clipRect);
                while (iterator.MoveNext())
                {
                    PointF innerBrickListOffset = iterator.CurrentBrick.InnerBrickListOffset;
                    PointF tf = new PointF((iterator.Offset.X + iterator.CurrentBrick.X) + iterator.CurrentBrick.InnerBrickListOffset.X, (iterator.Offset.Y + iterator.CurrentBrick.Y) + innerBrickListOffset.Y);
                    RectangleF currentBrickRectangle = iterator.CurrentBrickRectangle;
                    RectangleF currentClipRectangle = iterator.CurrentClipRectangle;
                    if (!currentClipRectangle.IsEmpty)
                    {
                        clipRect.Intersect(iterator.CurrentClipRectangle);
                    }
                    RectangleF ef2 = currentBrickRectangle;
                    ef2.Offset(-tf.X, -tf.Y);
                    this.WriteBricks(writer, iterator.CurrentBrick, exportContext, tf, currentBrickRectangle, ef2);
                }
                if (base2 != null)
                {
                    base2.WriteEndTags(writer);
                }
            }
        }

        public bool IsPartialTrustMode { get; set; }

        public bool EmbedImagesToXaml { get; set; }

        public static bool InheritParentFlowDirection { get; set; }
    }
}

