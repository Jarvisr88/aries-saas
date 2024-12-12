namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public static class WinFormsCompatibilityExtensions
    {
        public static Point FromWinForms(this Point point);
        public static Rect FromWinForms(this Rectangle rect);
        public static Size FromWinForms(this Size size);
        public static Color ToWinFormsColor(this Color color);
        public static Point ToWinFormsPoint(this Point point);
        public static Rectangle ToWinFormsRectangle(this Rect rect);
        public static Size ToWinFormsSize(this Size size);
    }
}

