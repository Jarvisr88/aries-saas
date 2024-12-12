namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Media;

    public static class WindowsFormsHelper
    {
        public static System.Windows.Media.Brush ConvertBrush(System.Drawing.Brush brush) => 
            new SolidColorBrush(ConvertColor(((SolidBrush) brush).Color));

        public static System.Windows.Media.Color ConvertBrushToColor(System.Drawing.Brush brush)
        {
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);
            if ((brush == null) || !(brush is SolidBrush))
            {
                return color;
            }
            System.Drawing.Color color2 = (brush as SolidBrush).Color;
            return System.Windows.Media.Color.FromArgb(color2.A, color2.R, color2.G, color2.B);
        }

        public static System.Drawing.Color ConvertBrushToColor(System.Windows.Media.Brush brush)
        {
            if ((brush == null) || !(brush is SolidColorBrush))
            {
                return System.Drawing.Color.FromArgb(0);
            }
            System.Windows.Media.Color color = (brush as SolidColorBrush).Color;
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static System.Windows.Media.Color ConvertColor(System.Drawing.Color color) => 
            System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);

        public static System.Windows.Media.Brush ConvertColorToBrush(System.Drawing.Color color) => 
            new SolidColorBrush(ConvertColor(color));

        public static TextAlignment ConvertStringToTextAlignment(StringAlignment stringAlignment)
        {
            switch (stringAlignment)
            {
                case StringAlignment.Near:
                    return TextAlignment.Left;

                case StringAlignment.Center:
                    return TextAlignment.Center;

                case StringAlignment.Far:
                    return TextAlignment.Right;
            }
            throw new ArgumentException();
        }
    }
}

