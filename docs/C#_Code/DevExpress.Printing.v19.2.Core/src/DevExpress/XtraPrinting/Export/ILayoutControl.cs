namespace DevExpress.XtraPrinting.Export
{
    using System;
    using System.Drawing;

    public interface ILayoutControl
    {
        int Left { get; }

        int Top { get; }

        Rectangle Bounds { get; set; }

        RectangleF BoundsF { get; }
    }
}

