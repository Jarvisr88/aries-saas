namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class PdfGraphics : GraphicsBase, IPdfGraphics, IGraphics, IGraphicsBase, IPrintingSystemContext, System.IServiceProvider, IDisposable
    {
        private readonly PdfPrintingGraphicsImplementation graphics;
        private readonly PageRangeIndexMapper pageIndexMapper;

        public PdfGraphics(Stream stream, PdfExportOptions exportOptions, PrintingSystemBase ps) : this(stream, exportOptions, ps, PageRangeIndexMapper.CreateIdentityMapper())
        {
        }

        public PdfGraphics(Stream stream, PdfExportOptions exportOptions, PrintingSystemBase ps, PageRangeIndexMapper pageIndexMapper) : base(ps)
        {
            this.graphics = new PdfPrintingGraphicsImplementation(stream, exportOptions, base.ProgressReflector, ps.PrintingDocument.RightToLeftLayout);
            this.pageIndexMapper = pageIndexMapper;
        }

        public virtual void AddDrawingAction(DeferredAction action)
        {
            action.Execute(base.PrintingSystem, this);
        }

        private void AddOutlineEntries(IList<PdfBookmark> parent, BookmarkNode bmNode, int[] pageIndices, bool isRootNode)
        {
            RectangleF brickBounds = bmNode.Pair.GetBrickBounds(base.PrintingSystem.Pages);
            float destinationTop = (brickBounds != RectangleF.Empty) ? brickBounds.Top : 0f;
            int pageRangeIndex = bmNode.GetPageRangeIndex(pageIndices);
            if (bmNode.IsValid(base.PrintingSystem.Document) && (pageRangeIndex >= 0))
            {
                PdfBookmark item = this.graphics.ExportDocument.CreateBookmark(bmNode.Text, pageRangeIndex, destinationTop);
                item.IsInitiallyClosed = !isRootNode;
                item.Parent = (PdfBookmarkList) parent;
                parent.Add(item);
                foreach (BookmarkNode node in bmNode.Nodes)
                {
                    this.AddOutlineEntries(item.Children, node, pageIndices, false);
                }
            }
        }

        void IGraphicsBase.ApplyTransformState(MatrixOrder order, bool removeState)
        {
            this.graphics.ApplyTransformState(order, removeState);
        }

        void IGraphicsBase.DrawCheckBox(RectangleF rect, CheckState state)
        {
            this.graphics.DrawImage(CheckBoxImageHelper.GetCheckBoxImage(state), rect);
        }

        void IGraphicsBase.DrawEllipse(Pen pen, RectangleF rect)
        {
            this.graphics.DrawEllipse(pen, rect);
        }

        void IGraphicsBase.DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            this.graphics.DrawEllipse(pen, x, y, width, height);
        }

        void IGraphicsBase.DrawImage(System.Drawing.Image image, Point point)
        {
            this.graphics.DrawImage(image, point);
        }

        void IGraphicsBase.DrawImage(System.Drawing.Image image, RectangleF rect)
        {
            this.graphics.DrawImage(image, rect);
        }

        void IGraphicsBase.DrawImage(System.Drawing.Image image, RectangleF rect, Color underlyingColor)
        {
            this.graphics.DrawImage(image, rect, underlyingColor);
        }

        void IGraphicsBase.DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            this.graphics.DrawLine(pen, pt1, pt2);
        }

        void IGraphicsBase.DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            this.graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        void IGraphicsBase.DrawLines(Pen pen, PointF[] points)
        {
            this.graphics.DrawLines(pen, points);
        }

        void IGraphicsBase.DrawPath(Pen pen, GraphicsPath path)
        {
            this.graphics.DrawPath(pen, path);
        }

        void IGraphicsBase.DrawRectangle(Pen pen, RectangleF bounds)
        {
            this.graphics.DrawRectangle(pen, bounds);
        }

        void IGraphicsBase.DrawString(string s, Font font, Brush brush, PointF point)
        {
            this.DrawString(s, font, brush, point, null);
        }

        void IGraphicsBase.DrawString(string s, Font font, Brush brush, RectangleF bounds)
        {
            this.DrawString(s, font, brush, bounds, null);
        }

        void IGraphicsBase.FillEllipse(Brush brush, RectangleF rect)
        {
            this.graphics.FillEllipse(brush, rect);
        }

        void IGraphicsBase.FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            this.graphics.FillEllipse(brush, new RectangleF(x, y, width, height));
        }

        void IGraphicsBase.FillPath(Brush brush, GraphicsPath path)
        {
            this.graphics.FillPath(brush, path);
        }

        void IGraphicsBase.FillRectangle(Brush brush, RectangleF bounds)
        {
            this.graphics.FillRectangle(brush, bounds);
        }

        void IGraphicsBase.FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            this.graphics.FillRectangle(brush, new RectangleF(x, y, width, height));
        }

        void IGraphicsBase.IntersectClip(GraphicsPath path)
        {
            this.graphics.IntersectClip(path);
        }

        SizeF IGraphicsBase.MeasureString(string text, Font font, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, graphicsUnit);

        SizeF IGraphicsBase.MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, location, stringFormat, graphicsUnit);

        SizeF IGraphicsBase.MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, size, stringFormat, graphicsUnit);

        SizeF IGraphicsBase.MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            base.Measurer.MeasureString(text, font, width, stringFormat, graphicsUnit);

        void IGraphicsBase.ResetTransform()
        {
            this.graphics.ResetTransform();
        }

        void IGraphicsBase.Restore(IGraphicsState gstate)
        {
            this.graphics.Restore(gstate);
        }

        void IGraphicsBase.RotateTransform(float angle)
        {
            this.graphics.RotateTransform(angle, MatrixOrder.Prepend);
        }

        void IGraphicsBase.RotateTransform(float angle, MatrixOrder order)
        {
            this.graphics.RotateTransform(angle, order);
        }

        IGraphicsState IGraphicsBase.Save() => 
            this.graphics.Save();

        void IGraphicsBase.SaveTransformState()
        {
            this.graphics.SaveTransformState();
        }

        void IGraphicsBase.ScaleTransform(float sx, float sy)
        {
            this.graphics.ScaleTransform(sx, sy, MatrixOrder.Prepend);
        }

        void IGraphicsBase.ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            this.graphics.ScaleTransform(sx, sy, order);
        }

        void IGraphicsBase.TranslateTransform(float dx, float dy)
        {
            this.graphics.TranslateTransform(dx, dy, MatrixOrder.Prepend);
        }

        void IGraphicsBase.TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            this.graphics.TranslateTransform(dx, dy, order);
        }

        void IPdfGraphics.AddCheckFormField(CheckEditingField checkEditingField, RectangleF rect)
        {
            PdfRectangle pdfRect = this.GetTransformationMatrix().TransformRectangle(rect);
            this.graphics.AddCheckEditingField(checkEditingField, this.GetPageNumber(checkEditingField.PageID), pdfRect);
        }

        void IPdfGraphics.AddCombTextFormField(TextEditingField textEditingField, RectangleF rect, string text, int length)
        {
            PdfRectangle pdfRect = this.GetTransformationMatrix().TransformRectangle(rect);
            this.graphics.AddCombTextEditingField(textEditingField, this.GetPageNumber(textEditingField.PageID), pdfRect, text, length);
        }

        void IPdfGraphics.AddDeferredDestination(string destinationName, int pageIndex, float destinationTop)
        {
            this.graphics.ExportDocument.AddDestination(destinationName, this.pageIndexMapper.GetPageRangeIndex(pageIndex), (double) destinationTop);
        }

        void IPdfGraphics.AddOutlineEntries(BookmarkNode bmNode, int[] pageIndices)
        {
            this.AddOutlineEntries(this.graphics.ExportDocument.Bookmarks, bmNode, pageIndices, true);
        }

        void IPdfGraphics.AddPage(SizeF pageSize, int pageIndex)
        {
            this.graphics.AddPage(pageSize, pageIndex);
        }

        void IPdfGraphics.AddSignatureForm(RectangleF rect)
        {
            this.graphics.CreateSignatureForm(rect);
        }

        void IPdfGraphics.AddTextFormField(TextEditingField textEditingField, RectangleF rect)
        {
            PdfRectangle pdfRect = this.GetTransformationMatrix().TransformRectangle(rect);
            this.graphics.AddTextEditingField(textEditingField, this.GetPageNumber(textEditingField.PageID), pdfRect, textEditingField.Brick.Text);
        }

        void IPdfGraphics.DrawImage(EmfMetafile image, System.Drawing.Image bitmap, RectangleF rect, Color underlyingColor)
        {
            this.graphics.DrawImage(image, bitmap, rect, underlyingColor);
        }

        void IPdfGraphics.Flush()
        {
            this.Dispose();
        }

        void IPdfGraphics.FlushPageContent()
        {
            this.graphics.FlushPageContent = true;
        }

        void IPdfGraphics.SetDeferredGoToArea(RectangleF bounds, out string destinationName)
        {
            PdfTransformationMatrix transformationMatrix = this.GetTransformationMatrix();
            destinationName = this.graphics.ExportDocument.GetNewDestinationName();
            this.graphics.ExportDocument.AddLinkToPage(transformationMatrix.TransformRectangle(bounds), destinationName);
        }

        void IPdfGraphics.SetGoToArea(int destPageIndex, float destTop, RectangleF bounds)
        {
            PdfTransformationMatrix transformationMatrix = this.GetTransformationMatrix();
            this.graphics.ExportDocument.AddLinkToPage(transformationMatrix.TransformRectangle(bounds), this.pageIndexMapper.GetPageRangeIndex(destPageIndex), (double) destTop);
        }

        void IPdfGraphics.SetUriArea(string uri, RectangleF bounds)
        {
            this.graphics.SetUriArea(uri, bounds);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.graphics.Dispose();
            }
        }

        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            RectangleF ef;
            if ((format != null) && ((format.Alignment != StringAlignment.Near) || (format.LineAlignment != StringAlignment.Near)))
            {
                SizeF ef2 = base.Measurer.MeasureString(s, font, float.MaxValue, format, this.graphics.PageUnit);
                ef = new RectangleF(point.X - GetOffset(format.Alignment, ef2.Width), point.Y - GetOffset(format.LineAlignment, ef2.Height), ef2.Width, ef2.Height);
            }
            else
            {
                SizeF pageSize = this.graphics.PageSize;
                ef = new RectangleF(point.X, point.Y, pageSize.Width - point.X, pageSize.Height - point.Y);
            }
            this.DrawString(s, font, brush, ef, format);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF bounds, StringFormat format)
        {
            this.graphics.DrawString(s, font, brush, bounds, format, base.Measurer);
        }

        ~PdfGraphics()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        private static float GetOffset(StringAlignment alignment, float size) => 
            (alignment == StringAlignment.Near) ? 0f : ((alignment == StringAlignment.Center) ? (size / 2f) : size);

        private int GetPageNumber(long pageID)
        {
            int pageIndexByID = base.PrintingSystem.Pages.GetPageIndexByID(pageID);
            return (this.pageIndexMapper.GetPageRangeIndex(pageIndexByID) + 1);
        }

        private PdfTransformationMatrix GetTransformationMatrix()
        {
            float num = GraphicsDpi.UnitToDpi(((IGraphicsBase) this).PageUnit);
            float num2 = 72f;
            return new PdfTransformationMatrix((double) (num2 / num), 0.0, 0.0, (double) (-num2 / num), 0.0, (double) ((base.DrawingPage.PageSize.Height * num2) / num));
        }

        protected PdfPrintingGraphicsImplementation Graphics =>
            this.graphics;

        SmoothingMode IGraphicsBase.SmoothingMode
        {
            get => 
                SmoothingMode.None;
            set
            {
            }
        }

        RectangleF IGraphicsBase.ClipBounds
        {
            get => 
                this.graphics.ClipBounds;
            set => 
                this.graphics.ClipBounds = value;
        }

        GraphicsUnit IGraphicsBase.PageUnit
        {
            get => 
                this.graphics.PageUnit;
            set => 
                this.graphics.PageUnit = value;
        }

        float IGraphics.Dpi =>
            72f;

        bool IPdfGraphics.AcroFormSupported =>
            this.graphics.ExportDocument.ExportEditingFieldsToAcroForms;

        Matrix IGraphicsBase.Transform
        {
            get => 
                this.graphics.Transform.Clone();
            set
            {
                this.graphics.Transform.Dispose();
                this.graphics.Transform = value.Clone();
            }
        }
    }
}

