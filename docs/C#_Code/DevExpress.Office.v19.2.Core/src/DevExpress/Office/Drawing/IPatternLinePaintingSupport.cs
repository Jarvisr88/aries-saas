namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;

    public interface IPatternLinePaintingSupport
    {
        void DrawLine(Pen pen, float x1, float y1, float x2, float y2);
        void DrawLines(Pen pen, PointF[] points);
        Brush GetBrush(Color color);
        Pen GetPen(Color color);
        Pen GetPen(Color color, float thickness);
        void ReleaseBrush(Brush brush);
        void ReleasePen(Pen pen);
    }
}

