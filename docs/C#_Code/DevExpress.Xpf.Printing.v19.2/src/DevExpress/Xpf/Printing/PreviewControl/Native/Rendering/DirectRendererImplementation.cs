namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.DocumentView;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DirectRendererImplementation : RendererImplementationBase
    {
        private readonly Lazy<GdiPlusImageRenderHook> gdiPlusImageRenderHookLazy;

        public DirectRendererImplementation() : this(null)
        {
        }

        public DirectRendererImplementation(Func<GdiPlusImageRenderHook> gdiPlusImageRenderHookAccessor)
        {
            if (gdiPlusImageRenderHookAccessor != null)
            {
                this.gdiPlusImageRenderHookLazy = new Lazy<GdiPlusImageRenderHook>(gdiPlusImageRenderHookAccessor, false);
            }
        }

        private static Bitmap CreateBitmap(RenderItem item)
        {
            Page page = item.Page.Page;
            System.Drawing.Size size = new System.Drawing.Size((int) item.Rectangle.Width, (int) item.Rectangle.Height);
            return new Bitmap(size.Width, size.Height);
        }

        private Bitmap GetPageBitmap(RenderItem renderItem, SelectionService selection, double zoomFactor, bool drawAdditionalBricks)
        {
            try
            {
                Bitmap image = CreateBitmap(renderItem);
                Graphics graphics = Graphics.FromImage(image);
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = (zoomFactor < 1.0) ? SmoothingMode.HighQuality : SmoothingMode.Default;
                graphics.ScaleTransform((float) zoomFactor, (float) zoomFactor);
                Lazy<GdiPlusImageRenderHook> gdiPlusImageRenderHookLazy = this.gdiPlusImageRenderHookLazy;
                if (<>c.<>9__3_0 == null)
                {
                    Lazy<GdiPlusImageRenderHook> local1 = this.gdiPlusImageRenderHookLazy;
                    gdiPlusImageRenderHookLazy = (Lazy<GdiPlusImageRenderHook>) (<>c.<>9__3_0 = x => x.Value);
                }
                ((Lazy<GdiPlusImageRenderHook>) <>c.<>9__3_0).With<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook>(((Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook>) gdiPlusImageRenderHookLazy)).Do<GdiPlusImageRenderHook>(delegate (GdiPlusImageRenderHook x) {
                    x.BeforeRender(graphics);
                });
                ((IPage) renderItem.Page.Page).Draw(graphics, new PointF(0f, 0f));
                if (drawAdditionalBricks)
                {
                    selection.Do<SelectionService>(delegate (SelectionService x) {
                        x.OnDrawPage(renderItem.Page.Page, graphics, new PointF(0f, 0f));
                    });
                    base.DrawBookmarks(renderItem.Page.Page, graphics, new PointF(0f, 0f));
                }
                return image;
            }
            catch
            {
                return null;
            }
        }

        public override bool RenderToGraphics(Graphics graphics, RenderedContent renderedContent, double zoomFactor, double scaleX, double angle)
        {
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            foreach (RenderItem renderItem in renderedContent.RenderedPages)
            {
                Action<GdiPlusImageRenderHook> <>9__3;
                Action<GdiPlusImageRenderHook> <>9__1;
                IDocumentPage page = renderItem.Page;
                double num = zoomFactor * page.ScaleMultiplier;
                double num2 = scaleX / ScreenHelper.ScaleX;
                Rect rectangle = renderItem.Rectangle;
                System.Windows.Point location = rectangle.Location;
                System.Windows.Point point = new System.Windows.Point(rectangle.Location.X, location.Y);
                PointF val = new PointF((float) (point.X / num), (float) (point.Y / num));
                graphics.ResetTransform();
                graphics.SmoothingMode = (num < 1.0) ? SmoothingMode.HighQuality : SmoothingMode.Default;
                Lazy<GdiPlusImageRenderHook> gdiPlusImageRenderHookLazy = this.gdiPlusImageRenderHookLazy;
                if (<>c.<>9__5_0 == null)
                {
                    Lazy<GdiPlusImageRenderHook> local1 = this.gdiPlusImageRenderHookLazy;
                    gdiPlusImageRenderHookLazy = (Lazy<GdiPlusImageRenderHook>) (<>c.<>9__5_0 = x => x.Value);
                }
                Action<GdiPlusImageRenderHook> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<GdiPlusImageRenderHook> local2 = <>9__1;
                    action = <>9__1 = delegate (GdiPlusImageRenderHook x) {
                        x.BeforeRender(graphics);
                    };
                }
                ((Lazy<GdiPlusImageRenderHook>) <>c.<>9__5_0).With<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook>(((Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook>) gdiPlusImageRenderHookLazy)).Do<GdiPlusImageRenderHook>(action);
                graphics.ScaleTransform((float) (num * num2), (float) (num * num2));
                Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook> evaluator = <>c.<>9__5_2;
                if (<>c.<>9__5_2 == null)
                {
                    Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook> local3 = <>c.<>9__5_2;
                    evaluator = <>c.<>9__5_2 = x => x.Value;
                }
                Action<GdiPlusImageRenderHook> action4 = <>9__3;
                if (<>9__3 == null)
                {
                    Action<GdiPlusImageRenderHook> local4 = <>9__3;
                    action4 = <>9__3 = delegate (GdiPlusImageRenderHook x) {
                        x.BeforeRender(graphics);
                    };
                }
                this.gdiPlusImageRenderHookLazy.With<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook>(evaluator).Do<GdiPlusImageRenderHook>(action4);
                using (GdiGraphics graphics2 = new GdiGraphics(graphics, page.Page.Document.PrintingSystem))
                {
                    (page.Page.Document.PrintingSystem.ExportersFactory.GetExporter(page.Page) as PageExporter).DrawPage(graphics2, GraphicsUnitConverter.Convert(val, (float) 96f, graphics2.PageUnit.ToDpiI()));
                }
                SizeF size = new SizeF();
                RectangleF rectF = GraphicsUnitConverter.DipToDoc(new RectangleF(val, size));
                renderedContent.SelectionService.Do<SelectionService>(delegate (SelectionService x) {
                    x.OnDrawPage(renderItem.Page.Page, graphics, rectF.Location);
                });
                base.DrawBookmarks(renderItem.Page.Page, graphics, rectF.Location);
            }
            return false;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DirectRendererImplementation.<>c <>9 = new DirectRendererImplementation.<>c();
            public static Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook> <>9__3_0;
            public static Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook> <>9__5_0;
            public static Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook> <>9__5_2;

            internal GdiPlusImageRenderHook <GetPageBitmap>b__3_0(Lazy<GdiPlusImageRenderHook> x) => 
                x.Value;

            internal GdiPlusImageRenderHook <RenderToGraphics>b__5_0(Lazy<GdiPlusImageRenderHook> x) => 
                x.Value;

            internal GdiPlusImageRenderHook <RenderToGraphics>b__5_2(Lazy<GdiPlusImageRenderHook> x) => 
                x.Value;
        }
    }
}

