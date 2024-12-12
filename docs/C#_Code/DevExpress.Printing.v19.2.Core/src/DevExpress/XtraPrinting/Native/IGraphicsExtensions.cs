namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class IGraphicsExtensions
    {
        public static void ExecUsingClipBounds(this IGraphics gr, Action action, RectangleF clipBounds, bool snapToDpi = false);
    }
}

