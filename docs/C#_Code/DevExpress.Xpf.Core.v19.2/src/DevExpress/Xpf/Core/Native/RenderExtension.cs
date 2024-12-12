namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal static class RenderExtension
    {
        public static void UpdateBounds(this IRenderInfo obj, ref Rect bounds);
    }
}

