namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class BrushBuilder
    {
        protected BrushBuilder()
        {
        }

        public static BrushBuilder Create(Brush brush)
        {
            switch (brush)
            {
                case ((null) || (TileBrush _)):
                    return null;
                    break;
            }
            if (brush is SolidColorBrush)
            {
                return new SolidColorBrushBuilder((SolidColorBrush) brush);
            }
            if (brush is LinearGradientBrush)
            {
                return new LinearGradientBrushBuilder((LinearGradientBrush) brush);
            }
            if (brush is RadialGradientBrush)
            {
                return new RadialGradientBrushBuilder((RadialGradientBrush) brush);
            }
            if (!(brush is BitmapCacheBrush))
            {
                throw new ArgumentException("Unknow brush type");
            }
            return new BitmapCacheBrushBuilder((BitmapCacheBrush) brush);
        }

        public abstract DXBrush CreateDxBrush(Rect brushBounds);
    }
}

