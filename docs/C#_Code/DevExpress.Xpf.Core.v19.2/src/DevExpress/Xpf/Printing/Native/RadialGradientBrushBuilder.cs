namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class RadialGradientBrushBuilder : BrushBuilder<RadialGradientBrush>
    {
        public RadialGradientBrushBuilder(RadialGradientBrush brush) : base(brush)
        {
        }

        public override DXBrush CreateDxBrush(Rect brushBounds) => 
            null;
    }
}

