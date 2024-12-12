namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Drawing;
    using System.Windows;

    public abstract class NativeRendererImpl : INativeRendererImpl, IDisposable
    {
        protected NativeRendererImpl()
        {
        }

        public void Dispose()
        {
            this.DisposeInternal();
        }

        protected virtual void DisposeInternal()
        {
        }

        public virtual void InvalidateCaches()
        {
        }

        public abstract bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize);
        public virtual void Reset()
        {
        }
    }
}

