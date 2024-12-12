namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Emf;
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class PdfGraphics : GraphicsBase, IPdfGraphics, IGraphics, IGraphicsBase, IPrintingSystemContext, System.IServiceProvider, IDisposable, IPdfDocumentOwner
    {
        private PdfGraphicsImpl pdfGraphicsImpl;
        private PdfDocument document;
        private PdfPageInfo pageInfo;
        private PdfStreamWriter writer;
        private bool flashed;
        private GraphicsUnit pageUnit;
        private System.Drawing.Drawing2D.SmoothingMode smoothingMode;
        private Color imageBackColor;
        private PdfImageCache imageCache;
        private PdfNeverEmbeddedFonts neverEmbeddedFonts;
        private PdfHashtable pdfHashtable;
        private bool scaleStrings;
        private Measurer metafileMeasurer;
        private PageRangeIndexMapper pageIndexMapper;

        public PdfGraphics(Stream stream, PrintingSystemBase ps) : this(stream, ps.ExportOptions.Pdf, ps)
        {
        }

        public PdfGraphics(Stream stream, PdfExportOptions pdfOptions, PrintingSystemBase ps) : this(stream, pdfOptions, ps, PageRangeIndexMapper.CreateIdentityMapper())
        {
        }

        public PdfGraphics(Stream stream, PdfExportOptions options, PrintingSystemBase ps, PageRangeIndexMapper pageIndexMapper) : base(ps)
        {
            this.pageUnit = GraphicsUnit.Document;
            this.smoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.imageBackColor = DXColor.Empty;
            this.imageCache = new PdfImageCache();
            this.neverEmbeddedFonts = new PdfNeverEmbeddedFonts();
            this.pdfHashtable = new PdfHashtable();
            this.document = new PdfDocument(options.Compressed, options.ShowPrintDialogOnOpen);
            this.ApplyExportOptions(options);
            this.document.Calculate();
            this.writer = new PdfStreamWriter(stream, this.document);
            this.document.WriteHeader(this.writer);
            this.pageIndexMapper = pageIndexMapper;
        }

        public void AddOutlineEntries(BookmarkNode bmNode, int[] pageIndices)
        {
            this.AddOutlineEntriesCore(null, bmNode, pageIndices);
        }

        private void AddOutlineEntriesCore(PdfOutlineEntry parent, BookmarkNode bmNode, int[] pageIndices)
        {
            RectangleF brickBounds = bmNode.Pair.GetBrickBounds(base.PrintingSystem.Pages);
            float destTop = (brickBounds != RectangleF.Empty) ? brickBounds.Top : 0f;
            int pageRangeIndex = bmNode.GetPageRangeIndex(pageIndices);
            if (bmNode.IsValid(base.PrintingSystem.Document) && (pageRangeIndex >= 0))
            {
                PdfOutlineEntry entry = this.SetOutlineEntry(parent, bmNode.Text, pageRangeIndex, destTop);
                foreach (BookmarkNode node in bmNode.Nodes)
                {
                    this.AddOutlineEntriesCore(entry, node, pageIndices);
                }
            }
        }

        public void AddPage(SizeF pageSize, int pageIndex = 0x7fffffff)
        {
            if (pageSize.Width <= 0f)
            {
                throw new PdfGraphicsException("Invalid page width");
            }
            if (pageSize.Height <= 0f)
            {
                throw new PdfGraphicsException("Invalid page height");
            }
            this.ClosePage();
            this.OpenPage(pageSize);
            this.pdfGraphicsImpl = new PdfGraphicsImpl(this, this.pageInfo.Context, this.pageInfo.SizeInPoints, this.document, this.pageInfo);
            this.pdfGraphicsImpl.PageUnit = this.pageUnit;
            this.ClipBounds = new RectangleF(PointF.Empty, pageSize);
        }

        public void AddPage(float width, float height)
        {
            this.AddPage(new SizeF(width, height), 0x7fffffff);
        }

        private void ApplyExportOptions(PdfExportOptions pdfOptions)
        {
            this.document.ConvertImagesToJpeg = pdfOptions.ConvertImagesToJpeg;
            this.document.JpegImageQuality = pdfOptions.ImageQuality;
            this.document.Info.Producer = pdfOptions.DocumentOptions.ActualProducer;
            this.document.Info.Author = pdfOptions.DocumentOptions.Author;
            this.document.Info.Application = pdfOptions.DocumentOptions.Application;
            this.document.Info.Title = pdfOptions.DocumentOptions.Title;
            this.document.Info.Subject = pdfOptions.DocumentOptions.Subject;
            this.document.Info.Keywords = pdfOptions.DocumentOptions.Keywords;
            this.document.Catalog.Metadata.Producer = pdfOptions.DocumentOptions.ActualProducer;
            this.document.Catalog.Metadata.Author = pdfOptions.DocumentOptions.Author;
            this.document.Catalog.Metadata.Application = pdfOptions.DocumentOptions.Application;
            this.document.Catalog.Metadata.Title = pdfOptions.DocumentOptions.Title;
            this.document.Catalog.Metadata.Subject = pdfOptions.DocumentOptions.Subject;
            this.document.Catalog.Metadata.Keywords = pdfOptions.DocumentOptions.Keywords;
            this.document.Catalog.Metadata.AdditionalMetadata = pdfOptions.AdditionalMetadata;
            this.document.Catalog.Attachments = pdfOptions.Attachments;
            this.document.Info.CreationDate = this.document.Catalog.Metadata.CreationDate = DateTimeHelper.Now;
            char[] separator = new char[] { ';' };
            foreach (string str in pdfOptions.NeverEmbeddedFonts.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                this.SetNeverEmbeddedFontFamily(str);
            }
            PdfPasswordSecurityOptions passwordSecurityOptions = pdfOptions.PasswordSecurityOptions;
            this.document.Encryption.OpenPassword = passwordSecurityOptions.OpenPassword;
            this.document.Encryption.PermissionsPassword = passwordSecurityOptions.PermissionsPassword;
            if (!string.IsNullOrEmpty(passwordSecurityOptions.PermissionsPassword))
            {
                this.document.Encryption.PrintingPermissions = passwordSecurityOptions.PermissionsOptions.PrintingPermissions;
                this.document.Encryption.ChangingPermissions = passwordSecurityOptions.PermissionsOptions.ChangingPermissions;
                this.document.Encryption.EnableCoping = passwordSecurityOptions.PermissionsOptions.EnableCopying;
                this.document.Encryption.EnableScreenReaders = passwordSecurityOptions.PermissionsOptions.EnableScreenReaders;
            }
            this.document.Signature.Certificate = pdfOptions.SignatureOptions.Certificate;
            this.document.Signature.Reason = pdfOptions.SignatureOptions.Reason;
            this.document.Signature.Location = pdfOptions.SignatureOptions.Location;
            this.document.Signature.ContactInfo = pdfOptions.SignatureOptions.ContactInfo;
            this.document.PdfACompatible = (pdfOptions.PdfACompatibility != PdfACompatibility.None) && !this.document.Encryption.IsEncryptionOn;
        }

        public void ApplyTransformState(MatrixOrder order, bool removeState)
        {
            this.pdfGraphicsImpl.ApplyTransformState(order, removeState);
        }

        private void ClosePage()
        {
            if (this.pageInfo != null)
            {
                if (this.pdfGraphicsImpl != null)
                {
                    this.pdfGraphicsImpl.Finish();
                }
                this.pageInfo.WriteAndClose(this.writer);
                this.pageInfo = null;
                ProgressReflector progressReflector = base.ProgressReflector;
                progressReflector.RangeValue++;
            }
        }

        void IGraphics.AddDrawingAction(DeferredAction action)
        {
            action.Execute(base.PrintingSystem, this);
        }

        void IGraphicsBase.IntersectClip(GraphicsPath path)
        {
        }

        void IGraphicsBase.Restore(IGraphicsState gstate)
        {
        }

        IGraphicsState IGraphicsBase.Save() => 
            null;

        void IPdfGraphics.AddCheckFormField(CheckEditingField checkEditingField, RectangleF rect)
        {
        }

        void IPdfGraphics.AddCombTextFormField(TextEditingField textEditingField, RectangleF rect, string text, int length)
        {
        }

        void IPdfGraphics.AddDeferredDestination(string destinationName, int pageIndex, float destinationTop)
        {
        }

        void IPdfGraphics.AddSignatureForm(RectangleF rect)
        {
        }

        void IPdfGraphics.AddTextFormField(TextEditingField textEditingField, RectangleF rect)
        {
        }

        void IPdfGraphics.DrawImage(EmfMetafile image, System.Drawing.Image bitmap, RectangleF rect, Color underlyingColor)
        {
        }

        void IPdfGraphics.FlushPageContent()
        {
        }

        void IPdfGraphics.SetDeferredGoToArea(RectangleF bounds, out string destinationName)
        {
            destinationName = null;
        }

        public void DrawCheckBox(RectangleF rect, CheckState state)
        {
            this.DrawImage(CheckBoxImageHelper.GetCheckBoxImage(state), rect);
        }

        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            this.TestPage();
            this.pdfGraphicsImpl.DrawEllipse(pen, rect);
        }

        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            this.DrawEllipse(pen, new RectangleF(x, y, width, height));
        }

        public void DrawImage(System.Drawing.Image image, Point point)
        {
            this.DrawImage(image, new RectangleF((PointF) point, (SizeF) image.Size));
        }

        public void DrawImage(System.Drawing.Image image, RectangleF bounds)
        {
            this.DrawImage(image, bounds, this.imageBackColor);
        }

        public void DrawImage(System.Drawing.Image image, RectangleF bounds, Color underlyingColor)
        {
            this.TestPage();
            this.pdfGraphicsImpl.DrawImage(image, bounds, underlyingColor);
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            this.TestPage();
            this.pdfGraphicsImpl.DrawLine(pen, pt1, pt2);
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            this.DrawLine(pen, new PointF(x1, y1), new PointF(x2, y2));
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
            this.TestPage();
            this.pdfGraphicsImpl.DrawLines(pen, points);
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            this.TestPage();
            this.pdfGraphicsImpl.DrawPath(pen, path);
        }

        public void DrawRectangle(Pen pen, RectangleF bounds)
        {
            this.TestPage();
            this.pdfGraphicsImpl.DrawRectangle(pen, bounds);
        }

        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            this.DrawString(s, font, brush, point, null);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF bounds)
        {
            this.DrawString(s, font, brush, bounds, null);
        }

        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            this.pdfGraphicsImpl.DrawString(s, font, brush, point, format);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF bounds, StringFormat format)
        {
            this.pdfGraphicsImpl.DrawString(s, font, brush, bounds, format);
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            this.TestPage();
            this.pdfGraphicsImpl.FillEllipse(brush, rect);
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            this.FillEllipse(brush, new RectangleF(x, y, width, height));
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            this.TestPage();
            this.pdfGraphicsImpl.FillPath(brush, path);
        }

        public void FillRectangle(Brush brush, RectangleF bounds)
        {
            this.TestPage();
            this.pdfGraphicsImpl.FillRectangle(brush, bounds);
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            this.FillRectangle(brush, new RectangleF(x, y, width, height));
        }

        public void Flush()
        {
            if (!this.flashed)
            {
                this.WriteDocument();
                if (this.document != null)
                {
                    this.document.Dispose();
                    this.document = null;
                }
                this.pdfHashtable.Clear();
                this.flashed = true;
                if (this.metafileMeasurer != null)
                {
                    this.metafileMeasurer.Dispose();
                    this.metafileMeasurer = null;
                }
            }
        }

        private static SizeF GetValidPageSize(SizeF pageSizeInPoints) => 
            new SizeF(Math.Min(pageSizeInPoints.Width, 14400f), Math.Min(pageSizeInPoints.Height, 14400f));

        public SizeF MeasureString(string text, Font font, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, graphicsUnit);

        public SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, location, stringFormat, graphicsUnit);

        public SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, size, stringFormat, graphicsUnit);

        public SizeF MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, width, stringFormat, graphicsUnit);

        public void MultiplyTransform(Matrix matrix)
        {
        }

        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
        }

        private void OpenDefaultPageIfNeeded()
        {
            if (this.pageInfo == null)
            {
                this.OpenPage(new SizeF(0f, 0f));
            }
        }

        private void OpenPage(SizeF pageSize)
        {
            this.pageInfo = new PdfPageInfo(GetValidPageSize(PdfCoordinate.TransformValue(this.pageUnit, pageSize)), this.document, this.pdfHashtable);
        }

        public void ResetTransform()
        {
            this.pdfGraphicsImpl.ResetTransform();
        }

        public void RotateTransform(float angle)
        {
            this.RotateTransform(angle, MatrixOrder.Prepend);
        }

        public void RotateTransform(float angle, MatrixOrder order)
        {
            this.pdfGraphicsImpl.RotateTransform(angle, order);
        }

        public void SaveTransformState()
        {
            this.pdfGraphicsImpl.SaveTransformState();
        }

        public void ScaleTransform(float sx, float sy)
        {
            this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
        }

        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            this.pdfGraphicsImpl.ScaleTransform(sx, sy, order);
        }

        public void SetGoToArea(int destPageIndex, float destTop, RectangleF bounds)
        {
            this.document.AddDestinationInfo(new DestinationInfo(this.pageIndexMapper.GetPageRangeIndex(destPageIndex), destTop, this.pageInfo.Page, bounds));
        }

        public void SetNeverEmbeddedFont(Font font)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            this.neverEmbeddedFonts.RegisterFont(font);
        }

        public void SetNeverEmbeddedFontFamily(string fontFamilyName)
        {
            this.neverEmbeddedFonts.RegisterFontFamily(fontFamilyName);
        }

        public PdfOutlineEntry SetOutlineEntry(PdfOutlineItem parent, string title, int destPageIndex, float destTop)
        {
            DestinationInfo info = new DestinationInfo(destPageIndex, destTop);
            this.document.AddDestinationInfo(info);
            PdfOutlineItem item = (parent != null) ? parent : this.document.Catalog.Outlines;
            PdfOutlineEntry entry = new PdfOutlineEntry(item, title, info, this.document.Compressed);
            item.Entries.Add(entry);
            return entry;
        }

        public void SetUriArea(string uri, RectangleF bounds)
        {
            bounds = PdfCoordinate.CorrectRectangle(this.pageUnit, bounds, this.pageInfo.SizeInPoints);
            PdfLinkAnnotation annotation = this.document.CreateLinkAnnotation(new PdfURIAction(uri, this.document.Compressed), Utils.ToPdfRectangle(bounds));
            this.pageInfo.Page.AddAnnotation(annotation);
        }

        void IDisposable.Dispose()
        {
            this.Flush();
        }

        private void TestPage()
        {
            if (this.pageInfo == null)
            {
                throw new PdfGraphicsException("The current page undefined");
            }
        }

        public void TranslateTransform(float dx, float dy)
        {
            this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
        }

        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            this.pdfGraphicsImpl.TranslateTransform(dx, dy, order);
        }

        private void ValidateDestinations()
        {
            foreach (DestinationInfo info in this.document.DestinationInfos)
            {
                PdfPage page = this.document.GetPage(info.DestPageIndex);
                if (page != null)
                {
                    info.DestTop = PdfCoordinate.CorrectPoint(this.pageUnit, new PointF(0f, info.DestTop), page.MediaBox.Size).Y;
                }
                if (info.LinkPage != null)
                {
                    info.LinkArea = PdfCoordinate.CorrectRectangle(this.pageUnit, info.LinkArea, info.LinkPage.MediaBox.Size);
                }
            }
        }

        private void WriteDocument()
        {
            this.OpenDefaultPageIfNeeded();
            this.ClosePage();
            this.ValidateDestinations();
            this.document.BeforeWrite();
            this.document.Write(this.writer, base.ProgressReflector);
            this.writer.Flush();
            this.document.AfterWrite();
        }

        [Obsolete("This property has become obsolete. Use the 'DevExpress.Utils.AzureCompatibility.Enable' property instead.")]
        public static bool EnableAzureCompatibility
        {
            get => 
                AzureCompatibility.Enable;
            set => 
                AzureCompatibility.Enable = value;
        }

        public static bool RenderMetafileAsBitmap { get; set; }

        public bool ScaleStrings
        {
            get => 
                this.scaleStrings;
            set => 
                this.scaleStrings = value;
        }

        PdfImageCache IPdfDocumentOwner.ImageCache =>
            this.imageCache;

        PdfNeverEmbeddedFonts IPdfDocumentOwner.NeverEmbeddedFonts =>
            this.neverEmbeddedFonts;

        Measurer IPdfDocumentOwner.MetafileMeasurer
        {
            get
            {
                this.metafileMeasurer ??= new GdiPlusMeasurer();
                return this.metafileMeasurer;
            }
        }

        public Color ImageBackColor
        {
            get => 
                this.imageBackColor;
            set => 
                this.imageBackColor = value;
        }

        public bool AcroFormSupported =>
            false;

        public float Dpi =>
            72f;

        public RectangleF ClipBounds
        {
            get => 
                this.pdfGraphicsImpl.ClipBounds;
            set => 
                this.pdfGraphicsImpl.ClipBounds = value;
        }

        public Region Clip
        {
            get => 
                null;
            set
            {
            }
        }

        public GraphicsUnit PageUnit
        {
            get => 
                this.pageUnit;
            set
            {
                this.pageUnit = value;
                if (this.pdfGraphicsImpl != null)
                {
                    this.pdfGraphicsImpl.PageUnit = this.pageUnit;
                }
            }
        }

        public Matrix Transform
        {
            get => 
                null;
            set
            {
            }
        }

        public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            get => 
                this.smoothingMode;
            set => 
                this.smoothingMode = value;
        }
    }
}

