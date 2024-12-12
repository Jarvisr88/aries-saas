namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    public class PdfXObjectMetafileCachedResource : PdfXObjectCachedResource
    {
        public const int MaxImageSampleCount = 0xc7ff38;
        private const float metafileContentDpi = 96f;
        private const float metafileDpiFactor = 1.333333f;
        private readonly PdfGraphicsDocument graphicsDocument;
        private readonly Metafile metafile;
        private readonly EmfMetafile emfMetafile;
        private PdfRectangle formBounds;
        private int objectNumber;

        private PdfXObjectMetafileCachedResource(Metafile metafile, EmfMetafile emfMetafile, float width, float height, PdfGraphicsDocument graphicsDocument) : base(width, height)
        {
            this.objectNumber = -1;
            this.metafile = metafile;
            this.graphicsDocument = graphicsDocument;
            this.emfMetafile = emfMetafile;
        }

        public static PdfXObjectMetafileCachedResource Create(EmfMetafile emfMetafile, PdfGraphicsDocument graphicsDocument)
        {
            EmfRectL bounds = emfMetafile.Bounds;
            return new PdfXObjectMetafileCachedResource(null, emfMetafile, ((float) ((bounds.Right - bounds.Left) + 1)) / 1.333333f, ((float) ((bounds.Bottom - bounds.Top) + 1)) / 1.333333f, graphicsDocument);
        }

        public static PdfXObjectMetafileCachedResource Create(Metafile metafile, PdfGraphicsDocument graphicsDocument) => 
            Create((Metafile) metafile.Clone(), graphicsDocument, () => PdfGDIPlusGraphicsConverter.ConvertMetafile(metafile));

        public static PdfXObjectMetafileCachedResource Create(Stream imageStream, PdfGraphicsDocument graphicsDocument) => 
            Create(new Metafile(imageStream), graphicsDocument, delegate {
                imageStream.Position = 0L;
                byte[] buffer = new byte[imageStream.Length];
                imageStream.Read(buffer, 0, buffer.Length);
                return EmfMetafile.Create(buffer);
            });

        private static PdfXObjectMetafileCachedResource Create(Metafile metafile, PdfGraphicsDocument graphicsDocument, Func<EmfMetafile> createEmfMetafile)
        {
            EmfMetafile emfMetafile = null;
            try
            {
                emfMetafile = createEmfMetafile();
                if ((emfMetafile != null) && !emfMetafile.IsVideoDisplay)
                {
                    SizeF physicalDimension = metafile.PhysicalDimension;
                    return new PdfXObjectMetafileCachedResource(metafile, emfMetafile, physicalDimension.Width * 0.02834646f, physicalDimension.Height * 0.02834646f, graphicsDocument);
                }
            }
            catch
            {
            }
            return new PdfXObjectMetafileCachedResource(metafile, emfMetafile, ((float) metafile.Width) / 1.333333f, ((float) metafile.Height) / 1.333333f, graphicsDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.metafile != null))
            {
                this.metafile.Dispose();
            }
        }

        public override void Draw(PdfCommandConstructor constructor, PdfRectangle bounds)
        {
            if (this.objectNumber != -1)
            {
                this.DrawMetafileForm(constructor, bounds);
            }
            else
            {
                try
                {
                    if (this.emfMetafile != null)
                    {
                        PdfDocumentCatalog documentCatalog = constructor.DocumentCatalog;
                        this.formBounds = new PdfRectangle(0.0, 0.0, bounds.Width, bounds.Height);
                        PdfForm form = new PdfForm(documentCatalog, this.formBounds);
                        using (PdfGraphicsCommandConstructor constructor2 = new PdfGraphicsCommandConstructor(form, this.graphicsDocument, 72f, 72f))
                        {
                            using (EmfMetafileExportVisitor visitor = new EmfMetafileExportVisitor(constructor2, this.graphicsDocument.FontCache, this.formBounds))
                            {
                                foreach (EmfRecord record in this.emfMetafile.Records)
                                {
                                    record.Accept(visitor);
                                }
                            }
                            form.ReplaceCommands(constructor2.Commands);
                            this.objectNumber = documentCatalog.Objects.AddResolvedObject(form);
                            this.DrawMetafileForm(constructor, bounds);
                            return;
                        }
                    }
                }
                catch
                {
                }
                if (this.metafile != null)
                {
                    double width = bounds.Width;
                    double height = bounds.Height;
                    if ((base.Width < width) || (base.Height < height))
                    {
                        this.DrawRasterImage(constructor, bounds, 4.1666665077209473, width, height);
                    }
                    else
                    {
                        int resolution = Math.Max(this.graphicsDocument.MetafileMinimalRasterizationResolution, Math.Max((int) this.metafile.HorizontalResolution, (int) this.metafile.VerticalResolution));
                        using (Bitmap bitmap = PdfMetafileToBitmapConverter.ConvertToBitmap(this.metafile, Color.Transparent, resolution))
                        {
                            this.graphicsDocument.ImageCache.AddXObject(bitmap).Draw(constructor, bounds);
                        }
                    }
                }
            }
        }

        private void DrawMetafileForm(PdfCommandConstructor constructor, PdfRectangle bounds)
        {
            constructor.DrawForm(this.objectNumber, new PdfTransformationMatrix(bounds.Width / this.formBounds.Width, 0.0, 0.0, bounds.Height / this.formBounds.Height, bounds.Left, bounds.Bottom));
        }

        private void DrawRasterImage(PdfCommandConstructor constructor, PdfRectangle bounds, double metafileImageSizeFactor, double width, double height)
        {
            if ((((width * height) * metafileImageSizeFactor) * metafileImageSizeFactor) > 13107000.0)
            {
                metafileImageSizeFactor = Math.Sqrt(13107000.0 / (width / height)) / height;
            }
            using (Bitmap bitmap = new Bitmap((int) (width * metafileImageSizeFactor), (int) (height * metafileImageSizeFactor)))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Transparent);
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(this.metafile, new Rectangle(Point.Empty, bitmap.Size));
                }
                this.graphicsDocument.ImageCache.AddXObject(bitmap).Draw(constructor, bounds);
            }
        }
    }
}

