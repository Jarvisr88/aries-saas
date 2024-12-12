namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public interface ILineBrick
    {
        void CalculateDirection(PointF pt1, PointF pt2);

        float LineWidth { get; set; }

        DashStyle LineStyle { get; set; }

        Color ForeColor { get; set; }
    }
}

