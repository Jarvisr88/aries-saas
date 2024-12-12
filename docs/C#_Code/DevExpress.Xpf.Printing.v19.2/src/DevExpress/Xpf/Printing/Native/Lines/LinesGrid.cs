namespace DevExpress.Xpf.Printing.Native.Lines
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class LinesGrid : Grid
    {
        protected override Size MeasureOverride(Size constraint)
        {
            Size size = base.MeasureOverride(constraint);
            return new Size(Math.Min(size.Width, constraint.Width), Math.Min(size.Height, constraint.Height));
        }
    }
}

