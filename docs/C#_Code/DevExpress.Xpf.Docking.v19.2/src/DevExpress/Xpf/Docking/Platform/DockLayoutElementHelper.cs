namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class DockLayoutElementHelper
    {
        public static Point GetElementPoint(LayoutElementHitInfo hi) => 
            new Point(hi.HitPoint.X - hi.Element.Location.X, hi.HitPoint.Y - hi.Element.Location.Y);
    }
}

