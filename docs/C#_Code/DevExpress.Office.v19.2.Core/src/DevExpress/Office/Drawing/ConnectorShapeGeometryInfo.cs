namespace DevExpress.Office.Drawing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ConnectorShapeGeometryInfo : ShapeGeometryInfo
    {
        private readonly Geometry boundingGeometry;

        public ConnectorShapeGeometryInfo(Geometry fillGeometry, Geometry boundingGeometry) : base(fillGeometry)
        {
            this.boundingGeometry = boundingGeometry;
        }

        public override Rect GetTransformedWidenedBounds(Pen pen, Matrix transform, double additionalSize)
        {
            Geometry geometry = Geometry.Combine(base.Geometry, this.boundingGeometry, GeometryCombineMode.Union, Transform.Identity);
            Geometry geometry2 = base.GetWidenedByPenGeometry(geometry, pen, additionalSize).Clone();
            return base.GetTransformedGeometryBounds(geometry2, transform);
        }
    }
}

