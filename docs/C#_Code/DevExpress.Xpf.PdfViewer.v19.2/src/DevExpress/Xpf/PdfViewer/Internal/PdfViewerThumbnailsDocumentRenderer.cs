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

    public class PdfViewerThumbnailsDocumentRenderer : DocumentViewerRenderer
    {
        private readonly TextureCache textureCache;
        private readonly PdfViewerBackend viewerBackend;

        public PdfViewerThumbnailsDocumentRenderer(DocumentPresenterControl presenter, TextureCache textureCache, PdfViewerBackend viewerBackend) : base(presenter)
        {
            this.textureCache = textureCache;
            this.viewerBackend = viewerBackend;
        }

        private TextureContent CreateRenderedPage(RenderItem item, double zoomFactor, double angle) => 
            new TextureContent(zoomFactor, angle, this.GetPdfPageAsBitmap(item));

        private Bitmap GetPdfPageAsBitmap(RenderItem renderItem)
        {
            Bitmap bitmap2;
            try
            {
                ISupportDefferedRendering page = renderItem.Page;
                double num = Math.Max(renderItem.Rect.Width, renderItem.Rect.Height) * ScreenHelper.GetScaleX(this.Presenter);
                bitmap2 = this.viewerBackend.CreateBitmap(page.PageIndex + 1, (int) num, PdfRenderMode.View);
            }
            catch
            {
                return null;
            }
            return bitmap2;
        }

        private TextureContent GetRenderedPage(RenderItem item, double zoomFactor, double angle)
        {
            TextureKey key = new TextureKey(item.Page.PageIndex);
            TextureContent content = this.textureCache.Get(key);
            if (item.ForceInvalidate)
            {
                content = this.CreateRenderedPage(item, zoomFactor, angle);
                this.textureCache.Add(key, content);
                item.ForceInvalidate = false;
            }
            return (content ?? new NotRenderedContent(angle, zoomFactor));
        }

        public override bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize)
        {
            ThumbnailsDocumentViewModel document = (ThumbnailsDocumentViewModel) this.Presenter.Document;
            if ((document == null) || (this.Presenter.BehaviorProvider == null))
            {
                base.SetRenderMask(new DrawingBrush(new GeometryDrawing()));
                return false;
            }
            Func<ThumbnailsDocumentViewModel, bool> evaluator = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<ThumbnailsDocumentViewModel, bool> local1 = <>c.<>9__8_0;
                evaluator = <>c.<>9__8_0 = x => x.IsLoaded;
            }
            if (!document.Return<ThumbnailsDocumentViewModel, bool>(evaluator, (<>c.<>9__8_1 ??= () => false)))
            {
                base.SetRenderMask(new DrawingBrush(new GeometryDrawing()));
                return true;
            }
            List<RenderItem> source = this.Presenter.GetDrawingContent().ToList<RenderItem>();
            RenderedContent content1 = new RenderedContent();
            content1.RenderedPages = source;
            content1.SelectionColor = this.Presenter.ThumbnailsViewer.ActualSettings.Owner.HighlightSelectionColor;
            RenderedContent renderedContent = content1;
            Func<RenderItem, TextureKey> selector = <>c.<>9__8_2;
            if (<>c.<>9__8_2 == null)
            {
                Func<RenderItem, TextureKey> local3 = <>c.<>9__8_2;
                selector = <>c.<>9__8_2 = x => new TextureKey(x.Page.PageIndex);
            }
            this.textureCache.UpdateCache(source.Select<RenderItem, TextureKey>(selector));
            base.SetRenderMask(this.Presenter.GenerateRenderMask(renderedContent.RenderedPages));
            bool flag = this.RenderToGraphics(graphics, renderedContent, this.Presenter.BehaviorProvider.ZoomFactor, ScreenHelper.GetScaleX(this.Presenter), this.Presenter.BehaviorProvider.RotateAngle);
            Action<RenderItem> action = <>c.<>9__8_3;
            if (<>c.<>9__8_3 == null)
            {
                Action<RenderItem> local4 = <>c.<>9__8_3;
                action = <>c.<>9__8_3 = delegate (RenderItem x) {
                    x.Page.NeedsInvalidate = x.NeedsInvalidate;
                };
            }
            renderedContent.RenderedPages.ForEach<RenderItem>(action);
            Action<RenderItem> action2 = <>c.<>9__8_4;
            if (<>c.<>9__8_4 == null)
            {
                Action<RenderItem> local5 = <>c.<>9__8_4;
                action2 = <>c.<>9__8_4 = delegate (RenderItem x) {
                    x.Page.ForceInvalidate = x.ForceInvalidate;
                };
            }
            renderedContent.RenderedPages.ForEach<RenderItem>(action2);
            if (flag)
            {
                this.Presenter.ImmediateActionsManager.EnqueueAction(new InvalidateThumbnailsViewerRenderingAction(this.Presenter));
            }
            return true;
        }

        private bool RenderToGraphics(Graphics graphics, RenderedContent renderedContent, double zoomFactor, double scaleX, double angle)
        {
            bool flag = false;
            foreach (RenderItem item in (renderedContent.RenderedPages as IList<RenderItem>) ?? renderedContent.RenderedPages.ToList<RenderItem>())
            {
                System.Drawing.Point point = item.Rect.Location.ToWinFormsPoint();
                Rect rect = item.Rect;
                System.Drawing.Size size = rect.Size.ToWinFormsSize();
                float num = (float) (zoomFactor * item.Page.DpiMultiplier);
                PointF tf = new PointF(((float) point.X) / num, ((float) point.Y) / num);
                float num2 = num * ((float) scaleX);
                TextureContent content = this.GetRenderedPage(item, (double) num2, angle);
                item.NeedsInvalidate = !content.Match((double) num2, angle);
                if (content.Texture == null)
                {
                    flag = true;
                    continue;
                }
                PointF tf2 = new PointF(point.X * ((float) scaleX), point.Y * ((float) scaleX));
                if (!item.NeedsInvalidate)
                {
                    graphics.DrawImageUnscaled(content.Texture, (int) tf2.X, (int) tf2.Y);
                    continue;
                }
                flag = true;
                graphics.DrawImage(content.Texture, tf2.X, tf2.Y, (float) size.Width, (float) size.Height);
            }
            return flag;
        }

        private ThumbnailsViewerPresenterControl Presenter =>
            base.Presenter as ThumbnailsViewerPresenterControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfViewerThumbnailsDocumentRenderer.<>c <>9 = new PdfViewerThumbnailsDocumentRenderer.<>c();
            public static Func<ThumbnailsDocumentViewModel, bool> <>9__8_0;
            public static Func<bool> <>9__8_1;
            public static Func<RenderItem, TextureKey> <>9__8_2;
            public static Action<RenderItem> <>9__8_3;
            public static Action<RenderItem> <>9__8_4;

            internal bool <RenderToGraphics>b__8_0(ThumbnailsDocumentViewModel x) => 
                x.IsLoaded;

            internal bool <RenderToGraphics>b__8_1() => 
                false;

            internal TextureKey <RenderToGraphics>b__8_2(RenderItem x) => 
                new TextureKey(x.Page.PageIndex);

            internal void <RenderToGraphics>b__8_3(RenderItem x)
            {
                x.Page.NeedsInvalidate = x.NeedsInvalidate;
            }

            internal void <RenderToGraphics>b__8_4(RenderItem x)
            {
                x.Page.ForceInvalidate = x.ForceInvalidate;
            }
        }
    }
}

