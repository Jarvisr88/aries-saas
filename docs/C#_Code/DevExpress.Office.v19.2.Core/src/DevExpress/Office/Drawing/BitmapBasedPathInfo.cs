namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    public abstract class BitmapBasedPathInfo : PathInfoBase
    {
        private Lazy<Image> print;

        protected BitmapBasedPathInfo(GraphicsPath graphicsPath, Func<Image> bitmapRenderer, Rectangle boundsInLayoutUnits) : base(graphicsPath, null, false, false)
        {
            this.BoundsInLayoutUnits = boundsInLayoutUnits;
            this.print = new Lazy<Image>(bitmapRenderer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.print != null)
            {
                if (this.print.IsValueCreated && (this.print.Value != null))
                {
                    this.print.Value.Dispose();
                }
                this.print = null;
            }
        }

        public override void Draw(Graphics graphics, PenInfo penInfo, Matrix shapeTransform)
        {
            this.DrawCore(graphics, penInfo, this.RotateWithShape ? shapeTransform : null);
        }

        internal override void DrawCore(Graphics graphics, PenInfo penInfo)
        {
            if (this.Print != null)
            {
                if (this.Print is Metafile)
                {
                    if ((graphics.PageUnit == GraphicsUnit.Pixel) && OSHelper.IsWindows)
                    {
                        graphics.DrawImage(this.Print, this.BoundsInLayoutUnits, 0, 0, this.Print.Width, this.Print.Height, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        graphics.DrawImage(this.Print, this.BoundsInLayoutUnits);
                    }
                }
                else
                {
                    graphics.DrawImage(this.Print, this.Location);
                }
            }
        }

        public override RectangleF GetRealBounds(Matrix transform, PenInfo penInfo)
        {
            RectangleF boundsInLayoutUnits = this.BoundsInLayoutUnits;
            if (this.RotateWithShape && (transform != null))
            {
                boundsInLayoutUnits = RectangleUtils.BoundingRectangle(boundsInLayoutUnits, transform);
            }
            return boundsInLayoutUnits;
        }

        protected Rectangle BoundsInLayoutUnits { get; set; }

        public bool RotateWithShape { get; set; }

        public Image Print =>
            this.print.Value;

        public Point Location =>
            this.BoundsInLayoutUnits.Location;
    }
}

