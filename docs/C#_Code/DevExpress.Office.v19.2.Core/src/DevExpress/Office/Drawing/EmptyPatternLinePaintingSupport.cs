namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;

    public class EmptyPatternLinePaintingSupport : IPatternLinePaintingSupport
    {
        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
        }

        public Brush GetBrush(Color color) => 
            new SolidBrush(color);

        public Pen GetPen(Color color) => 
            new Pen(color);

        public Pen GetPen(Color color, float thickness) => 
            new Pen(color, thickness);

        public void ReleaseBrush(Brush brush)
        {
            brush.Dispose();
        }

        public void ReleasePen(Pen pen)
        {
            pen.Dispose();
        }
    }
}

