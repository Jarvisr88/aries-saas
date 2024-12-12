namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.ComponentModel;
    using System.Windows.Media;

    [Obsolete("Use the System.Windows.Media.Brushes instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class FreezedBrushes
    {
        public static readonly Brush Transparent;

        static FreezedBrushes()
        {
            Brush brush = new SolidColorBrush(Colors.Transparent);
            brush.Freeze();
            Transparent = brush;
        }
    }
}

