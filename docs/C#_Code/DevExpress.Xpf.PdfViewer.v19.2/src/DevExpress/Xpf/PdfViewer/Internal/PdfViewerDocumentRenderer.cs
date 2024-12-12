namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PdfViewerDocumentRenderer : DocumentViewerRenderer
    {
        private readonly PdfViewerBackend viewerBackend;

        public PdfViewerDocumentRenderer(DocumentPresenterControl presenter, PdfViewerBackend viewerBackend) : base(presenter)
        {
            this.viewerBackend = viewerBackend;
        }

        public override bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize)
        {
            PdfDocumentViewModel document = (PdfDocumentViewModel) this.Presenter.Document;
            if ((document == null) || ((this.Presenter.PdfBehaviorProvider == null) || (document.DocumentState == null)))
            {
                base.SetRenderMask(new DrawingBrush(new GeometryDrawing()));
                return false;
            }
            Func<PdfDocumentViewModel, bool> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<PdfDocumentViewModel, bool> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.IsLoaded;
            }
            if (!document.Return<PdfDocumentViewModel, bool>(evaluator, (<>c.<>9__4_1 ??= () => false)))
            {
                base.SetRenderMask(new DrawingBrush(new GeometryDrawing()));
                return true;
            }
            List<RenderItem> drawingContent = this.Presenter.GetDrawingContent().ToList<RenderItem>();
            base.SetRenderMask(this.Presenter.GenerateRenderMask(drawingContent));
            double scaleX = ScreenHelper.GetScaleX(this.Presenter);
            double zoomFactor = this.Presenter.PdfBehaviorProvider.ZoomFactor;
            graphics.SetClip(invalidateRect.ToWinFormsRectangle());
            this.viewerBackend.BeginRenderPages();
            foreach (RenderItem item in drawingContent)
            {
                Rect rect = item.Rect;
                System.Drawing.Point point = rect.Location.ToWinFormsPoint();
                float num3 = (float) ((zoomFactor * item.Page.DpiMultiplier) / ((PdfPageViewModel) item.Page).UserUnit);
                PointF location = new PointF(point.X * ((float) scaleX), point.Y * ((float) scaleX));
                float scale = num3 * ((float) scaleX);
                this.viewerBackend.RenderPage(item.Page.PageIndex, graphics, location, scale);
            }
            this.viewerBackend.EndRenderPages();
            Action<RenderItem> action = <>c.<>9__4_2;
            if (<>c.<>9__4_2 == null)
            {
                Action<RenderItem> local3 = <>c.<>9__4_2;
                action = <>c.<>9__4_2 = delegate (RenderItem x) {
                    x.Page.NeedsInvalidate = false;
                };
            }
            drawingContent.ForEach(action);
            return true;
        }

        private PdfPresenterControl Presenter =>
            base.Presenter as PdfPresenterControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfViewerDocumentRenderer.<>c <>9 = new PdfViewerDocumentRenderer.<>c();
            public static Func<PdfDocumentViewModel, bool> <>9__4_0;
            public static Func<bool> <>9__4_1;
            public static Action<RenderItem> <>9__4_2;

            internal bool <RenderToGraphics>b__4_0(PdfDocumentViewModel x) => 
                x.IsLoaded;

            internal bool <RenderToGraphics>b__4_1() => 
                false;

            internal void <RenderToGraphics>b__4_2(RenderItem x)
            {
                x.Page.NeedsInvalidate = false;
            }
        }
    }
}

