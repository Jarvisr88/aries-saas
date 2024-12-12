namespace DevExpress.Office.Drawing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface IShapeGeometryInfo
    {
        void Draw(DrawingContext dc, Pen pen, Brush brush);
        void DrawGlow(DrawingContext dc, Pen pen, Brush brush, Color glowColor, double blurRadius);
        Rect GetTransformedBounds(Matrix transform);
        Rect GetTransformedWidenedBounds(Pen pen, Matrix transform, double additionalSize);
        System.Windows.Media.Geometry GetWidenedByPenGeometry(Pen pen);

        System.Windows.Media.Geometry Geometry { get; }

        Rect Bounds { get; }
    }
}

