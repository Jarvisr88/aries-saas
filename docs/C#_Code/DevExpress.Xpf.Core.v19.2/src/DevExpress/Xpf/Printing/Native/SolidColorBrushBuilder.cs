namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class SolidColorBrushBuilder : BrushBuilder<SolidColorBrush>
    {
        public SolidColorBrushBuilder(SolidColorBrush brush) : base(brush)
        {
        }

        public override DXBrush CreateDxBrush(Rect brushBounds) => 
            new DXSolidBrush(base.Brush.Color.ToColor());
    }
}

