namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public static class DragServiceHelper
    {
        public static bool IsOutOfArea(Point pt, Point startPoint, int threshold)
        {
            double num2 = pt.Y - startPoint.Y;
            return (!MathHelper.IsZero(pt.X - startPoint.X, (double) threshold) || !MathHelper.IsZero(num2, (double) threshold));
        }
    }
}

