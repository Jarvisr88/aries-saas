namespace DevExpress.Office.Printing
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class OfficeBufferedImageGraphicsService : BufferedImageGraphicsService
    {
        private Dictionary<System.Drawing.Image, BufferedGraphicsItem> imageToGraphics = new Dictionary<System.Drawing.Image, BufferedGraphicsItem>();

        public override Graphics CreateGraphics(System.Drawing.Image img)
        {
            Graphics targetGraphics = Graphics.FromImage(img);
            BufferedGraphics bufferedGraphics = BufferedGraphicsManager.Current.Allocate(targetGraphics, new Rectangle(0, 0, img.Width, img.Height));
            Dictionary<System.Drawing.Image, BufferedGraphicsItem> imageToGraphics = this.imageToGraphics;
            lock (imageToGraphics)
            {
                this.imageToGraphics.Add(img, new BufferedGraphicsItem(bufferedGraphics, targetGraphics));
            }
            Graphics graphics = bufferedGraphics.Graphics;
            graphics.ScaleTransform(targetGraphics.DpiX / graphics.DpiX, targetGraphics.DpiY / graphics.DpiY);
            return graphics;
        }

        public override void OnGraphicsDispose(System.Drawing.Image img)
        {
            BufferedGraphicsItem item;
            Dictionary<System.Drawing.Image, BufferedGraphicsItem> imageToGraphics = this.imageToGraphics;
            lock (imageToGraphics)
            {
                if (this.imageToGraphics.TryGetValue(img, out item))
                {
                    this.imageToGraphics.Remove(img);
                }
                else
                {
                    return;
                }
            }
            BufferedGraphics bufferedGraphics = item.BufferedGraphics;
            Graphics imageGraphics = item.ImageGraphics;
            bufferedGraphics.Render();
            bufferedGraphics.Dispose();
            imageGraphics.Dispose();
        }

        public override void Render(System.Drawing.Image img)
        {
            BufferedGraphicsItem item;
            Dictionary<System.Drawing.Image, BufferedGraphicsItem> imageToGraphics = this.imageToGraphics;
            lock (imageToGraphics)
            {
                if (!this.imageToGraphics.TryGetValue(img, out item))
                {
                    return;
                }
            }
            item.BufferedGraphics.Render();
        }

        private class BufferedGraphicsItem
        {
            private readonly System.Drawing.BufferedGraphics bufferedGraphics;
            private readonly Graphics imageGraphics;

            public BufferedGraphicsItem(System.Drawing.BufferedGraphics bufferedGraphics, Graphics imageGraphics)
            {
                this.bufferedGraphics = bufferedGraphics;
                this.imageGraphics = imageGraphics;
            }

            public System.Drawing.BufferedGraphics BufferedGraphics =>
                this.bufferedGraphics;

            public Graphics ImageGraphics =>
                this.imageGraphics;
        }
    }
}

