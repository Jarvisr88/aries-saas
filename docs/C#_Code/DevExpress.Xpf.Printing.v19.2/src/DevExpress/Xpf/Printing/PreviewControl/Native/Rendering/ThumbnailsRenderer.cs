namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ThumbnailsRenderer : DocumentViewerRenderer
    {
        private DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.INativeRendererImpl nativeRenderer;

        public ThumbnailsRenderer(ThumbnailsPresenterControl presenter) : base(presenter)
        {
            presenter.Loaded += new RoutedEventHandler(this.OnPresenterLoaded);
            this.UpdateInnerRenderer();
        }

        private void OnPresenterLoaded(object sender, RoutedEventArgs e)
        {
            this.Presenter.Loaded -= new RoutedEventHandler(this.OnPresenterLoaded);
            this.UpdateInnerRenderer();
        }

        public override bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize)
        {
            IDocument input = this.Presenter.Document;
            if (this.Presenter.BehaviorProvider == null)
            {
                return false;
            }
            Func<IDocument, bool> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<IDocument, bool> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => !x.Pages.Any<IPage>();
            }
            if (input.Return<IDocument, bool>(evaluator, <>c.<>9__6_1 ??= () => true))
            {
                base.SetRenderMask(new DrawingBrush(new GeometryDrawing()));
                return true;
            }
            RenderedContent content1 = new RenderedContent();
            content1.RenderedPages = this.Presenter.GetDrawingContent();
            RenderedContent renderedContent = content1;
            base.SetRenderMask(this.Presenter.GenerateRenderMask(renderedContent.RenderedPages));
            double scaleX = ScreenHelper.GetScaleX(this.Presenter);
            bool flag = this.nativeRenderer.RenderToGraphics(graphics, renderedContent, this.Presenter.BehaviorProvider.ZoomFactor, scaleX, this.Presenter.BehaviorProvider.RotateAngle);
            foreach (RenderItem item in renderedContent.RenderedPages)
            {
                item.Page.NeedsInvalidate = item.NeedsInvalidate;
                item.Page.ForceInvalidate = item.ForceInvalidate;
            }
            if (flag)
            {
                this.Presenter.ImmediateActionsManager.EnqueueAction(new InvalidateDocumentPreviewRenderingAction(this.Presenter));
            }
            return true;
        }

        public void UpdateInnerRenderer()
        {
            Func<ThumbnailsViewerControl, TextureCache> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<ThumbnailsViewerControl, TextureCache> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.TextureCache;
            }
            this.nativeRenderer = new TextureCacheRenderImplementation(this.Presenter.ThumbnailsViewer.Return<ThumbnailsViewerControl, TextureCache>(evaluator, <>c.<>9__5_1 ??= () => new TextureCache()), null);
        }

        protected ThumbnailsPresenterControl Presenter =>
            (ThumbnailsPresenterControl) base.Presenter;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsRenderer.<>c <>9 = new ThumbnailsRenderer.<>c();
            public static Func<ThumbnailsViewerControl, TextureCache> <>9__5_0;
            public static Func<TextureCache> <>9__5_1;
            public static Func<IDocument, bool> <>9__6_0;
            public static Func<bool> <>9__6_1;

            internal bool <RenderToGraphics>b__6_0(IDocument x) => 
                !x.Pages.Any<IPage>();

            internal bool <RenderToGraphics>b__6_1() => 
                true;

            internal TextureCache <UpdateInnerRenderer>b__5_0(ThumbnailsViewerControl x) => 
                x.TextureCache;

            internal TextureCache <UpdateInnerRenderer>b__5_1() => 
                new TextureCache();
        }
    }
}

