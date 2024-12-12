namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.DocumentView;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class TextureCacheRenderImplementation : RendererImplementationBase
    {
        private readonly Lazy<GdiPlusImageRenderHook> gdiPlusImageRenderHookLazy;
        private readonly TextureCache textureCache;

        public TextureCacheRenderImplementation(TextureCache cache, Func<GdiPlusImageRenderHook> gdiPlusImageRenderHookAccessor = null)
        {
            Guard.ArgumentNotNull(cache, "cache");
            this.textureCache = cache;
            if (gdiPlusImageRenderHookAccessor != null)
            {
                this.gdiPlusImageRenderHookLazy = new Lazy<GdiPlusImageRenderHook>(gdiPlusImageRenderHookAccessor, false);
            }
        }

        private static Bitmap CreateBitmap(RenderItem item, double scaleX)
        {
            Page page = item.Page.Page;
            System.Drawing.Size size = new System.Drawing.Size((int) (item.Rectangle.Width * scaleX), (int) (item.Rectangle.Height * scaleX));
            return new Bitmap(size.Width, size.Height);
        }

        private TextureContent CreateRenderedPage(RenderItem item, double zoomFactor, double scaleX) => 
            new TextureContent(zoomFactor, 0.0, item.Page.Page.IsRestored(), this.GetPageBitmap(item, zoomFactor, scaleX));

        private Bitmap GetPageBitmap(RenderItem renderItem, double zoomFactor, double scaleX)
        {
            try
            {
                Bitmap image = CreateBitmap(renderItem, scaleX);
                Graphics graphics = Graphics.FromImage(image);
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = (zoomFactor < 1.0) ? SmoothingMode.HighQuality : SmoothingMode.Default;
                graphics.ScaleTransform((float) zoomFactor, (float) zoomFactor);
                Lazy<GdiPlusImageRenderHook> gdiPlusImageRenderHookLazy = this.gdiPlusImageRenderHookLazy;
                if (<>c.<>9__5_0 == null)
                {
                    Lazy<GdiPlusImageRenderHook> local1 = this.gdiPlusImageRenderHookLazy;
                    gdiPlusImageRenderHookLazy = (Lazy<GdiPlusImageRenderHook>) (<>c.<>9__5_0 = x => x.Value);
                }
                ((Lazy<GdiPlusImageRenderHook>) <>c.<>9__5_0).With<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook>(((Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook>) gdiPlusImageRenderHookLazy)).Do<GdiPlusImageRenderHook>(delegate (GdiPlusImageRenderHook x) {
                    x.BeforeRender(graphics);
                });
                graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(new System.Drawing.Point(0, 0), image.Size));
                ((IPage) renderItem.Page.Page).Draw(graphics, new PointF(0f, 0f));
                return image;
            }
            catch
            {
                return null;
            }
        }

        private TextureContent GetRenderedPage(RenderItem item, double zoomFactor, double scaleX)
        {
            TextureKey key = new TextureKey(item.TextureType, item.Page.PageIndex);
            TextureContent content = this.textureCache.Get(key);
            if (item.ForceInvalidate)
            {
                content = this.CreateRenderedPage(item, zoomFactor, scaleX);
                this.textureCache.Add(key, content);
                item.ForceInvalidate = false;
            }
            return (content ?? new NotRenderedContent(0.0, zoomFactor));
        }

        public override bool RenderToGraphics(Graphics graphics, RenderedContent renderedContent, double zoomFactor, double scaleX, double angle)
        {
            bool flag = false;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            foreach (RenderItem renderItem in renderedContent.RenderedPages)
            {
                IDocumentPage page = renderItem.Page;
                double num = zoomFactor * page.ScaleMultiplier;
                Rect rectangle = renderItem.Rectangle;
                System.Windows.Point location = rectangle.Location;
                System.Windows.Point point = new System.Windows.Point(rectangle.Location.X * scaleX, location.Y * scaleX);
                PointF locationF = new PointF((float) (point.X / num), (float) (point.Y / num));
                graphics.SmoothingMode = SmoothingMode.Default;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                renderItem.ForceInvalidate = renderItem.ForceInvalidate || !page.Page.IsRestored();
                TextureContent content = this.GetRenderedPage(renderItem, num, scaleX);
                renderItem.NeedsInvalidate = page.Page.IsRestored() ? !content.Match(num, angle, page.Page.IsRestored()) : true;
                if (content.Texture == null)
                {
                    flag = true;
                    continue;
                }
                if (!renderItem.NeedsInvalidate)
                {
                    graphics.DrawImageUnscaled(content.Texture, (int) point.X, (int) point.Y);
                }
                else
                {
                    flag = true;
                    if (num == content.ZoomFactor)
                    {
                        graphics.DrawImageUnscaled(content.Texture, (int) point.X, (int) point.Y);
                    }
                    else
                    {
                        graphics.DrawImage(content.Texture, (float) point.X, (float) point.Y, (float) (rectangle.Width * scaleX), (float) (rectangle.Height * scaleX));
                    }
                }
                if (!renderItem.NeedsInvalidate && (renderItem.TextureType == TextureType.Content))
                {
                    GraphicsUnit pageUnit = graphics.PageUnit;
                    graphics.PageUnit = GraphicsUnit.Document;
                    graphics.ScaleTransform((float) zoomFactor, (float) zoomFactor);
                    renderedContent.SelectionService.Do<SelectionService>(delegate (SelectionService x) {
                        x.OnDrawPage(renderItem.Page.Page, graphics, GraphicsUnitConverter.PixelToDoc(locationF));
                    });
                    base.DrawBookmarks(page.Page, graphics, locationF);
                    graphics.ResetTransform();
                    graphics.PageUnit = pageUnit;
                }
            }
            return flag;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextureCacheRenderImplementation.<>c <>9 = new TextureCacheRenderImplementation.<>c();
            public static Func<Lazy<GdiPlusImageRenderHook>, GdiPlusImageRenderHook> <>9__5_0;

            internal GdiPlusImageRenderHook <GetPageBitmap>b__5_0(Lazy<GdiPlusImageRenderHook> x) => 
                x.Value;
        }
    }
}

