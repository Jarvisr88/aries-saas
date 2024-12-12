namespace DevExpress.Office.Extensions
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public static class ColorExtensions
    {
        public static bool IsEmpty(this System.Windows.Media.Color color) => 
            (((color.A + color.R) + color.G) + color.B) == 0;

        public static System.Windows.Media.Color ToWpfColor(this System.Drawing.Color color) => 
            System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}

