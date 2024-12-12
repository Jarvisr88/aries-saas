namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public static class MeasurePixelSnapperHelper
    {
        public static Size MeasureOverride(Size result, SnapperType snapper) => 
            (snapper != SnapperType.Ceil) ? ((snapper != SnapperType.Floor) ? new Size(Math.Round(result.Width), Math.Round(result.Height)) : new Size(Math.Floor(result.Width), Math.Floor(result.Height))) : new Size(Math.Ceiling(result.Width), Math.Ceiling(result.Height));
    }
}

