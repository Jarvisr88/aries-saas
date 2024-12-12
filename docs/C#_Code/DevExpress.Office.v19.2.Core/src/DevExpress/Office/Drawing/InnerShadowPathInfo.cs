namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class InnerShadowPathInfo : BitmapBasedPathInfo
    {
        private bool drawOutline;

        public InnerShadowPathInfo(GraphicsPath graphicsPath, Func<Bitmap> bitmapRenderer, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, boundsInLayoutUnits)
        {
        }

        public override void Draw(Graphics graphics, PenInfo penInfo, Matrix shapeTransform)
        {
            this.drawOutline = false;
            base.Draw(graphics, penInfo, shapeTransform);
            if (penInfo != null)
            {
                this.drawOutline = true;
                base.DrawCore(graphics, penInfo, shapeTransform);
                this.drawOutline = false;
            }
        }

        internal override void DrawCore(Graphics graphics, PenInfo penInfo)
        {
            if (!this.drawOutline)
            {
                base.DrawCore(graphics, penInfo);
            }
            else
            {
                Pen pen = penInfo?.Pen;
                if (pen != null)
                {
                    graphics.DrawPath(pen, base.GraphicsPath);
                }
            }
        }
    }
}

