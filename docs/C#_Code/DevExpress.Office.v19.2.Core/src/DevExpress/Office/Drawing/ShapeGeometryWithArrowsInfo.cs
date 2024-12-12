namespace DevExpress.Office.Drawing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ShapeGeometryWithArrowsInfo : ConnectorShapeGeometryInfo
    {
        private readonly Geometry combinedGeometry;
        private readonly Geometry arrowsGeometry;

        public ShapeGeometryWithArrowsInfo(Geometry fillGeometry, Geometry arrowsGeometry, Geometry boundingGeometry) : base(fillGeometry, boundingGeometry)
        {
            this.combinedGeometry = Geometry.Combine(fillGeometry, arrowsGeometry, GeometryCombineMode.Union, Transform.Identity);
            this.arrowsGeometry = arrowsGeometry;
        }

        public override void Draw(DrawingContext dc, Pen pen, Brush brush)
        {
            dc.DrawGeometry(brush, null, base.Geometry);
            if (pen != null)
            {
                Brush brush2 = pen.Brush;
                if (brush2 != null)
                {
                    dc.DrawGeometry(brush2, null, this.arrowsGeometry);
                }
            }
        }

        public override void DrawGlow(DrawingContext dc, Pen parentOutlinePen, Brush parentFillBrush, Color glowColor, double blurRadius)
        {
            if (parentFillBrush != null)
            {
                Pen pen = base.CreateGlowPen(glowColor, parentFillBrush, blurRadius);
                dc.DrawGeometry(pen.Brush, pen, base.Geometry.GetOutlinedPathGeometry());
            }
            if (parentOutlinePen != null)
            {
                Pen pen = base.CreateGlowPen(glowColor, parentOutlinePen.Brush, blurRadius);
                dc.DrawGeometry(pen.Brush, pen, this.arrowsGeometry);
            }
        }

        public override Rect GetTransformedWidenedBounds(Pen pen, Matrix transform, double additionalSize) => 
            base.GetTransformedWidenedBounds(null, transform, additionalSize);

        public override Rect Bounds =>
            this.combinedGeometry.Bounds;
    }
}

