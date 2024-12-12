namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class TileBrushBuilder
    {
        protected TileBrushBuilder()
        {
        }

        public static TileBrushBuilder Create(Brush brush)
        {
            switch (brush)
            {
                case ((null) || !(brush is TileBrush)):
                    return null;
                    break;
            }
            if (brush is DrawingBrush)
            {
                return new DrawingBrushBuilder((DrawingBrush) brush);
            }
            if (brush is ImageBrush)
            {
                return new ImageBrushBuilder((ImageBrush) brush);
            }
            if (!(brush is VisualBrush))
            {
                throw new ArgumentException("Unknown brush type");
            }
            return new VisualBrushBuilder((VisualBrush) brush);
        }

        public abstract void SetDXBrush(Rect brushBounds, PdfGraphicsCommandConstructor constructor);
    }
}

