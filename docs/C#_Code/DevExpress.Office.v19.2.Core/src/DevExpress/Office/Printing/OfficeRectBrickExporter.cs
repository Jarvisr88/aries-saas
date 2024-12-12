namespace DevExpress.Office.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class OfficeRectBrickExporter : VisualBrickExporter
    {
        internal RectangleF CorrectBounds(RectangleF bounds, GdiGraphics graphics)
        {
            Matrix transform = graphics.Transform;
            graphics.ResetTransform();
            PointF[] pts = new PointF[] { new PointF(0f, 0f), new PointF(bounds.Width, bounds.Height) };
            graphics.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World, pts);
            float x = Math.Max((float) (pts[1].X - pts[0].X), (float) 1f);
            pts = new PointF[] { new PointF(0f, 0f), new PointF(x, Math.Max((float) (pts[1].Y - pts[0].Y), (float) 1f)) };
            graphics.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);
            bounds.Width = pts[1].X - pts[0].X;
            bounds.Height = pts[1].Y - pts[0].Y;
            graphics.Transform = transform;
            return bounds;
        }

        protected override void DrawBackground(IGraphics gr, RectangleF rect)
        {
            if (gr is PdfGraphics)
            {
                base.DrawBackground(gr, rect);
            }
            else
            {
                RectangleF bounds = (base.Brick as OfficeRectBrick).unitConverter.DocumentsToLayoutUnits(rect);
                GdiGraphics graphics = gr as GdiGraphics;
                if (graphics != null)
                {
                    bounds = this.CorrectBounds(bounds, graphics);
                }
                base.DrawBackground(gr, bounds);
            }
        }
    }
}

