namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;

    public abstract class ImageDocumentBuilderBase
    {
        private PrintingSystemBase ps;
        private DocumentInfo docInfo;
        private ImageExportOptions options;

        protected ImageDocumentBuilderBase(PrintingSystemBase ps, ImageExportOptions options)
        {
            this.ps = ps;
            this.options = options;
            this.docInfo = new DocumentInfo();
        }

        protected internal Bitmap CreateBitmap(SizeF size, float dpi)
        {
            Size size2 = Size.Ceiling(GraphicsUnitConverter.Convert(size, dpi, (float) this.options.Resolution));
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(size2.Width, size2.Height);
            }
            catch (ArgumentException exception)
            {
                throw new Exception(PreviewLocalizer.GetString(PreviewStringId.Msg_BigBitmapToCreate), exception);
            }
            bitmap.SetResolution((float) this.options.Resolution, (float) this.options.Resolution);
            return bitmap;
        }

        public abstract void CreateDocument(Stream stream);
        protected internal IGraphics CreateGraphics(System.Drawing.Image img) => 
            this.ImageGraphicsFactory.CreateGraphics(img, this.ps);

        protected internal virtual void CreateImage(Stream stream, ImageFormat format)
        {
            try
            {
                System.Drawing.Image img = this.CreateBitmap(this.DocumentSize, 300f);
                IGraphics gr = this.CreateGraphics(img);
                PointF location = new PointF();
                gr.ClipBounds = new RectangleF(location, this.DocumentSize);
                try
                {
                    this.DrawImageContent(gr, this.DocumentSize);
                    PSConvert.SaveImage(img, stream, format);
                }
                catch (ArgumentException exception)
                {
                    throw new Exception(PreviewLocalizer.GetString(PreviewStringId.Msg_BigBitmapToCreate), exception);
                }
                finally
                {
                    img.Dispose();
                    gr.Dispose();
                }
            }
            finally
            {
                this.FlushDocument();
            }
        }

        private void CreateMetafile(Stream stream)
        {
            try
            {
                int width = (int) Math.Round((double) this.DocumentWidth, MidpointRounding.AwayFromZero);
                int height = (int) Math.Round((double) this.DocumentHeight, MidpointRounding.AwayFromZero);
                System.Drawing.Image img = MetafileCreator.CreateInstance(stream, width, height, MetafileFrameUnit.Document, EmfType.EmfPlusDual);
                IGraphics gr = this.CreateGraphics(img);
                try
                {
                    if (gr is GdiGraphics)
                    {
                        GdiGraphics graphics2 = (GdiGraphics) gr;
                        MetafileHeader metafileHeader = ((Metafile) img).GetMetafileHeader();
                        gr.ScaleTransform(metafileHeader.DpiX / graphics2.Dpi, metafileHeader.DpiY / graphics2.Dpi);
                    }
                    gr.ClipBounds = new RectangleF(0f, 0f, (float) width, (float) height);
                    this.DrawImageContent(gr, this.DocumentSize);
                }
                finally
                {
                    gr.Dispose();
                    img.Dispose();
                }
            }
            finally
            {
                this.FlushDocument();
            }
        }

        protected internal void CreatePicture(Stream stream)
        {
            if (this.IsMetafile(this.options.Format))
            {
                this.CreateMetafile(stream);
            }
            else
            {
                this.CreateImage(stream, this.GetValidFormat(this.options.Format));
            }
        }

        protected internal abstract void DrawDocument(IGraphics gr);
        protected internal void DrawImageContent(IGraphics gr, SizeF size)
        {
            if (gr is GdiGraphics)
            {
                ((GdiGraphics) gr).TextRenderingHint = (TextRenderingHint) this.options.TextRenderingMode;
            }
            this.DrawDocument(gr);
            if (gr is ImageGraphics)
            {
                GraphicsModifier modifier = ((IServiceProvider) this.ps).GetService(typeof(GraphicsModifier)) as GraphicsModifier;
                if (modifier != null)
                {
                    modifier.Dispose();
                }
                BufferedImageGraphicsService service = ((IServiceProvider) this.ps).GetService(typeof(BufferedImageGraphicsService)) as BufferedImageGraphicsService;
                if (service != null)
                {
                    service.Render(((ImageGraphics) gr).Img);
                }
            }
        }

        protected internal abstract void FlushDocument();
        protected internal Color GetValidBackColor(Color c) => 
            (!DXColor.IsTransparentColor(c) || this.AllowBackgroundTransparency) ? c : Color.White;

        protected internal ImageFormat GetValidFormat(ImageFormat format)
        {
            Guid guid = format.Guid;
            return (((guid == ImageFormat.Exif.Guid) || ((guid == ImageFormat.Icon.Guid) || (guid == ImageFormat.MemoryBmp.Guid))) ? ImageFormat.Png : format);
        }

        protected internal bool IsMetafile(ImageFormat format)
        {
            Guid guid = format.Guid;
            return ((guid == ImageFormat.Emf.Guid) || (guid == ImageFormat.Wmf.Guid));
        }

        private bool IsSupportTransparency(ImageFormat format)
        {
            ImageFormat validFormat = this.GetValidFormat(format);
            Guid guid = validFormat.Guid;
            return ((guid == ImageFormat.Png.Guid) || ((guid == ImageFormat.Tiff.Guid) || this.IsMetafile(validFormat)));
        }

        internal DocumentInfo DocInfo =>
            this.docInfo;

        protected internal ImageExportOptions Options =>
            this.options;

        protected internal PrintingSystemBase Ps =>
            this.ps;

        protected internal abstract DevExpress.XtraPrinting.Export.Imaging.ImageGraphicsFactory ImageGraphicsFactory { get; }

        protected abstract float DocumentWidth { get; }

        protected abstract float DocumentHeight { get; }

        protected SizeF DocumentSize =>
            new SizeF(Math.Max(1f, this.DocumentWidth), Math.Max(1f, this.DocumentHeight));

        protected bool AllowBackgroundTransparency =>
            this.options.RetainBackgroundTransparency && this.IsSupportTransparency(this.options.Format);
    }
}

