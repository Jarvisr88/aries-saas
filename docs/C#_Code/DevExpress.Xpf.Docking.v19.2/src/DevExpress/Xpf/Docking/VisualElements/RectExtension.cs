namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class RectExtension
    {
        public static Size GetSize(this Rect rect) => 
            rect.Size;
    }
}

