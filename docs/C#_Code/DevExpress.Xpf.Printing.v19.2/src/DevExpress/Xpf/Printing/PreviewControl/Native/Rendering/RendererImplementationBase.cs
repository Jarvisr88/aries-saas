namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public abstract class RendererImplementationBase : INativeRendererImpl, IDisposable
    {
        private Pen markPen;

        protected RendererImplementationBase()
        {
        }

        private static Pen CreateMarkPen()
        {
            Pen pen;
            using (Bitmap bitmap = ResourceImageHelper.CreateBitmapFromResources("Core.Images.MarkBrush.bmp", typeof(ResFinder)))
            {
                bitmap.MakeTransparent(DXColor.Magenta);
                using (Brush brush = new TextureBrush(bitmap))
                {
                    pen = new Pen(brush, (float) Math.Max(bitmap.Width, bitmap.Height));
                }
            }
            return pen;
        }

        public void Dispose()
        {
            this.MarkPen.Dispose();
        }

        private void DrawBookmark(Graphics gr, RectangleF rect)
        {
            float x = GraphicsUnitConverter.PixelToDoc((float) 3f);
            rect = RectangleF.Inflate(rect, x, x);
            gr.DrawRectangle(this.MarkPen, Rectangle.Round(rect));
        }

        protected void DrawBookmarks(Page page, Graphics gr, PointF position)
        {
            PrintingSystemBase printingSystem = page.Document.PrintingSystem;
            if (!page.PageSize.IsEmpty)
            {
                List<Brick> markedBricks = new List<Brick>(printingSystem.GetMarkedBricks(page));
                BrickNavigator navigator1 = new BrickNavigator(page, false, true);
                navigator1.BrickPosition = position;
                navigator1.IterateBricks(delegate (Brick brick, RectangleF brickRect, RectangleF brickClipRect) {
                    if (markedBricks.Remove(brick))
                    {
                        this.DrawBookmark(gr, brickRect);
                    }
                    return markedBricks.Count == 0;
                });
            }
        }

        public virtual void InvalidateCaches()
        {
        }

        public abstract bool RenderToGraphics(Graphics graphics, RenderedContent renderedContent, double zoomFactor, double scaleX, double angle);
        public virtual void Reset()
        {
        }

        private Pen MarkPen
        {
            get
            {
                this.markPen ??= CreateMarkPen();
                return this.markPen;
            }
        }
    }
}

