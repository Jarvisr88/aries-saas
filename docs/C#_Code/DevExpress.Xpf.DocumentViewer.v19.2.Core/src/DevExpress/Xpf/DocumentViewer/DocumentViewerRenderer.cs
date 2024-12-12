namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class DocumentViewerRenderer : INativeImageRenderer, INativeRendererImpl, IDisposable
    {
        private INativeImageRendererCallback callback;

        public DocumentViewerRenderer(DocumentPresenterControl presenter)
        {
            this.Presenter = presenter;
        }

        public virtual void Dispose()
        {
        }

        public void Invalidate()
        {
            Action<INativeImageRendererCallback> action = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Action<INativeImageRendererCallback> local1 = <>c.<>9__12_0;
                action = <>c.<>9__12_0 = x => x.Invalidate();
            }
            this.callback.Do<INativeImageRendererCallback>(action);
        }

        public void RegisterCallback(INativeImageRendererCallback callback)
        {
            this.callback = callback;
        }

        public void ReleaseCallback()
        {
            this.callback = null;
        }

        public void Render(Graphics graphics, Rect invalidateRect, System.Windows.Size renderSize)
        {
            this.RenderToGraphics(graphics, this, invalidateRect, renderSize);
        }

        public virtual bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize) => 
            false;

        public virtual void Reset()
        {
        }

        public void SetRenderMask(DrawingBrush drawing)
        {
            this.callback.Do<INativeImageRendererCallback>(x => x.SetRenderMask(drawing));
        }

        protected DocumentPresenterControl Presenter { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentViewerRenderer.<>c <>9 = new DocumentViewerRenderer.<>c();
            public static Action<INativeImageRendererCallback> <>9__12_0;

            internal void <Invalidate>b__12_0(INativeImageRendererCallback x)
            {
                x.Invalidate();
            }
        }
    }
}

