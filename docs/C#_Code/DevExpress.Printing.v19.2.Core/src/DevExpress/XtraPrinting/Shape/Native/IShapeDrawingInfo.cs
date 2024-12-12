namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public interface IShapeDrawingInfo
    {
        float LineWidth { get; }

        DashStyle LineStyle { get; }

        int Angle { get; }

        bool Stretch { get; }

        Color FillColor { get; }

        Color ForeColor { get; }
    }
}

