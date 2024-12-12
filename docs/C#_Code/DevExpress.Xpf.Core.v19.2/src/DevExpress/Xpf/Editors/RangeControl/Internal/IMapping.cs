namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface IMapping
    {
        Point GetPoint(double x, double y);
        Point GetSnappedPoint(double x, double y, bool snapToPixels = true);

        System.Windows.Size Size { get; }

        Point Location { get; }
    }
}

