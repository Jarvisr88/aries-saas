namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class FrameworkElementExtensions
    {
        public static Bitmap CreateBitmap(this FrameworkElement element) => 
            (Bitmap) DrawingConverter.CreateGdiImage(element);
    }
}

