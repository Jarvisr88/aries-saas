namespace DevExpress.Xpf.Core.HandleDecorator
{
    using DevExpress.Utils.Controls;
    using System;
    using System.Drawing;

    public class BitmapManager : DisposableObject
    {
        private Bitmap cachedBitmapCore = null;
        private bool cachedBitmapActive = true;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.CachedBitmap = null;
            }
            base.Dispose(disposing);
        }

        private void DrawBorderBitmap(Graphics g, Rectangle imgRect, HandleDecoratorWindowTypes type, ThemeElementPainter painter, bool isActive)
        {
            try
            {
                int imgIndex = isActive ? 0 : 1;
                ThemeElementInfo info = new ThemeElementInfo(type, imgRect, isActive, imgIndex);
                painter.DrawObject(info, g);
            }
            catch (Exception)
            {
            }
        }

        public Bitmap GetCompositeBitmap(CompositeBitmapAttributes attr)
        {
            if ((attr.CompositeBmpSize.Width == 0) || (attr.CompositeBmpSize.Height == 0))
            {
                return null;
            }
            if ((this.CachedBitmap != null) && ((this.CachedBitmap.Size == attr.CompositeBmpSize) && (this.cachedBitmapActive == attr.IsActive)))
            {
                return this.CachedBitmap;
            }
            this.cachedBitmapActive = attr.IsActive;
            int num = attr.TopBorderSize.Height + attr.BottomBorderSize.Height;
            int num2 = attr.LeftBorderSize.Width + attr.RightBorderSize.Width;
            if ((attr.CompositeBmpSize.Width < num2) || (attr.CompositeBmpSize.Height < num))
            {
                this.CachedBitmap = null;
                return null;
            }
            Rectangle imgRect = new Rectangle(Point.Empty, attr.LeftBorderSize);
            Rectangle rectangle2 = new Rectangle(new Point(attr.LeftBorderSize.Width, 0), attr.TopBorderSize);
            Rectangle rectangle3 = new Rectangle(new Point(attr.LeftBorderSize.Width + attr.TopBorderSize.Width, 0), attr.RightBorderSize);
            Size bottomBorderSize = attr.BottomBorderSize;
            Rectangle rectangle4 = new Rectangle(new Point(attr.LeftBorderSize.Width, attr.CompositeBmpSize.Height - bottomBorderSize.Height), attr.BottomBorderSize);
            try
            {
                Bitmap image = new Bitmap(attr.CompositeBmpSize.Width, attr.CompositeBmpSize.Height);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    this.DrawBorderBitmap(graphics, imgRect, HandleDecoratorWindowTypes.Left, attr.SkinPainter, attr.IsActive);
                    this.DrawBorderBitmap(graphics, rectangle2, HandleDecoratorWindowTypes.Top, attr.SkinPainter, attr.IsActive);
                    this.DrawBorderBitmap(graphics, rectangle3, HandleDecoratorWindowTypes.Right, attr.SkinPainter, attr.IsActive);
                    this.DrawBorderBitmap(graphics, rectangle4, HandleDecoratorWindowTypes.Bottom, attr.SkinPainter, attr.IsActive);
                }
                this.CachedBitmap = image;
                return image;
            }
            catch (Exception)
            {
                this.CachedBitmap = null;
                return null;
            }
        }

        public void ReleaseCachedBitmap()
        {
            if (this.CachedBitmap != null)
            {
                this.CachedBitmap.Dispose();
            }
            this.CachedBitmap = null;
        }

        public Bitmap CachedBitmap
        {
            get => 
                this.cachedBitmapCore;
            set
            {
                if (!ReferenceEquals(this.cachedBitmapCore, value) && (this.cachedBitmapCore != null))
                {
                    this.cachedBitmapCore.Dispose();
                }
                this.cachedBitmapCore = value;
            }
        }

        public Size CachedBmpSize =>
            (this.CachedBitmap != null) ? this.CachedBitmap.Size : Size.Empty;
    }
}

