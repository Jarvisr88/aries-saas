namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class PageExtension
    {
        public static RectangleF DeflateMinMargins(this Page page, RectangleF rect);
        public static RectangleF GetRect(this Page page, PointF offset);
    }
}

