namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class ImageSinglePageByPageDocumentBuilder : ImageDocumentBuilderBase
    {
        private int[] pageIndices;

        public ImageSinglePageByPageDocumentBuilder(PrintingSystemBase ps, ImageExportOptions options) : base(ps, options)
        {
            this.pageIndices = ExportOptionsHelper.GetPageIndices(options, ps.Document.Pages.Count);
        }

        public override void CreateDocument(Stream stream)
        {
            base.DocInfo.Update(base.Ps.Document.Pages, base.Options);
            base.CreatePicture(stream);
        }

        protected internal override void CreateImage(Stream stream, ImageFormat format)
        {
            if (this.IsMultiPageFormat(format))
            {
                this.DrawMultiPageImage(stream, base.Ps.Document);
            }
            else
            {
                base.CreateImage(stream, format);
            }
        }

        private void DrawBorder(IGraphics gr, float pageVerticalPosition, SizeF pageSize, int pageIndex)
        {
            Brush brush = gr.GetBrush(base.GetValidBackColor(base.Options.PageBorderColor));
            SizeF size = new SizeF(pageSize.Width + (base.DocInfo.BorderWidth * 2f), base.DocInfo.BorderWidth);
            SizeF ef2 = new SizeF(base.DocInfo.BorderWidth, pageSize.Height + (base.DocInfo.BorderWidth * 2f));
            if (pageIndex == 0)
            {
                gr.FillRectangle(brush, new RectangleF(new PointF(0f, pageVerticalPosition), size));
            }
            gr.FillRectangle(brush, new RectangleF(new PointF(0f, pageVerticalPosition), ef2));
            gr.FillRectangle(brush, new RectangleF(new PointF(0f, (pageVerticalPosition + pageSize.Height) + base.DocInfo.BorderWidth), size));
            gr.FillRectangle(brush, new RectangleF(new PointF(pageSize.Width + base.DocInfo.BorderWidth, pageVerticalPosition), ef2));
        }

        protected internal override void DrawDocument(IGraphics gr)
        {
            float pageVerticalPosition = 0f;
            foreach (int num3 in this.pageIndices)
            {
                SizeF pageSize = base.Ps.Document.Pages[num3].PageData.PageSize;
                this.DrawBorder(gr, pageVerticalPosition, pageSize, num3);
                pageVerticalPosition += base.DocInfo.BorderWidth;
                Page brick = base.Ps.Document.Pages[num3];
                PageExporter exporter = base.Ps.ExportersFactory.GetExporter(brick) as PageExporter;
                exporter.RetainBackgroundTransparency = base.AllowBackgroundTransparency;
                exporter.DrawPage(gr, new PointF(base.DocInfo.BorderWidth, pageVerticalPosition));
                pageVerticalPosition += (int) pageSize.Height;
                ProgressReflector progressReflector = base.Ps.ProgressReflector;
                float rangeValue = progressReflector.RangeValue;
                progressReflector.RangeValue = rangeValue + 1f;
            }
        }

        private void DrawMultiPageImage(Stream stream, Document document)
        {
            ImageCodecInfo encoderInfo = this.GetEncoderInfo("image/tiff");
            if (encoderInfo != null)
            {
                Encoder saveFlag = Encoder.SaveFlag;
                EncoderParameters encoderParams = new EncoderParameters(1);
                try
                {
                    System.Drawing.Image objB = null;
                    int[] pageIndices = this.pageIndices;
                    int index = 0;
                    while (true)
                    {
                        if (index >= pageIndices.Length)
                        {
                            encoderParams.Param[0] = new EncoderParameter(saveFlag, (long) 20);
                            objB.SaveAdd(encoderParams);
                            objB.Dispose();
                            break;
                        }
                        int num2 = pageIndices[index];
                        Page brick = document.Pages[num2];
                        Bitmap img = base.CreateBitmap(brick.PageSize, 300f);
                        IGraphics gr = base.CreateGraphics(img);
                        gr.ClipBounds = new RectangleF(new PointF(0f, 0f), brick.PageSize);
                        try
                        {
                            PageExporter exporter = gr.PrintingSystem.ExportersFactory.GetExporter(brick) as PageExporter;
                            exporter.RetainBackgroundTransparency = base.AllowBackgroundTransparency;
                            exporter.DrawPage(gr, PointF.Empty);
                            ImageGraphics graphics2 = gr as ImageGraphics;
                            if (graphics2 != null)
                            {
                                GraphicsModifier modifier = ((IServiceProvider) base.Ps).GetService(typeof(GraphicsModifier)) as GraphicsModifier;
                                if (modifier != null)
                                {
                                    modifier.OnGraphicsDispose();
                                }
                                BufferedImageGraphicsService service = ((IServiceProvider) base.Ps).GetService(typeof(BufferedImageGraphicsService)) as BufferedImageGraphicsService;
                                if (service != null)
                                {
                                    service.Render(graphics2.Img);
                                }
                            }
                            if (num2 != this.pageIndices[0])
                            {
                                encoderParams.Param[0] = new EncoderParameter(saveFlag, (long) 0x17);
                                objB.SaveAdd(img, encoderParams);
                            }
                            else
                            {
                                encoderParams.Param[0] = new EncoderParameter(saveFlag, (long) 0x12);
                                img.Save(stream, encoderInfo, encoderParams);
                                objB = img;
                            }
                        }
                        finally
                        {
                            if (!ReferenceEquals(img, objB))
                            {
                                img.Dispose();
                            }
                            gr.Dispose();
                        }
                        ProgressReflector progressReflector = base.Ps.ProgressReflector;
                        progressReflector.RangeValue++;
                        index++;
                    }
                }
                finally
                {
                    base.Ps.ProgressReflector.MaximizeRange();
                }
            }
        }

        protected internal override void FlushDocument()
        {
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.MimeType == mimeType)
                {
                    return info;
                }
            }
            return null;
        }

        private bool IsMultiPageFormat(ImageFormat format) => 
            ReferenceEquals(format, ImageFormat.Tiff);

        protected internal override DevExpress.XtraPrinting.Export.Imaging.ImageGraphicsFactory ImageGraphicsFactory =>
            DevExpress.XtraPrinting.Export.Imaging.ImageGraphicsFactory.MultiplePageImageGraphicsFactory;

        protected override float DocumentWidth =>
            base.DocInfo.PageWidth;

        protected override float DocumentHeight =>
            base.DocInfo.PagesHeight;
    }
}

