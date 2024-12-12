namespace DevExpress.Xpf.Core.Core.Native
{
    using System;
    using System.Drawing;
    using System.Windows;

    public class XpfTypeConverter
    {
        public static System.Drawing.Point FromPlatformPoint(System.Windows.Point point) => 
            new System.Drawing.Point((int) point.X, (int) point.Y);

        public static System.Windows.Point ToPlatformPoint(System.Drawing.Point point) => 
            new System.Windows.Point((double) point.X, (double) point.Y);

        public static System.Windows.Point ToPlatformPoint(System.Windows.Point point) => 
            point;
    }
}

