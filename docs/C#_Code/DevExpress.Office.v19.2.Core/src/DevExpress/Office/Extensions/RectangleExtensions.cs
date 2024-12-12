namespace DevExpress.Office.Extensions
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class RectangleExtensions
    {
        public static Rect GetSaveRect(double x, double y, double width, double height) => 
            new Rect(x, y, Math.Max(0.0, width), Math.Max(0.0, height));

        public static Rect ToRect(this Rectangle rectangle) => 
            GetSaveRect((double) rectangle.X, (double) rectangle.Y, (double) rectangle.Width, (double) rectangle.Height);

        public static Rect ToRect(this RectangleF rectangle) => 
            GetSaveRect((double) rectangle.X, (double) rectangle.Y, (double) rectangle.Width, (double) rectangle.Height);

        public static Rectangle ToRectangle(this Rect rect) => 
            new Rectangle((int) Math.Round(rect.X), (int) Math.Round(rect.Y), (int) Math.Round(rect.Width), (int) Math.Round(rect.Height));

        public static System.Windows.Size ToSize(this Rectangle rectangle) => 
            new System.Windows.Size((double) Math.Max(0, rectangle.Width), (double) Math.Max(0, rectangle.Height));
    }
}

