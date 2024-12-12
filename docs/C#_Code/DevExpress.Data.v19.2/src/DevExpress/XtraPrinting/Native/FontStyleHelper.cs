namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class FontStyleHelper
    {
        public static bool IsBold(this FontStyle style) => 
            (style & FontStyle.Bold) != FontStyle.Regular;

        public static bool IsItalic(this FontStyle style) => 
            (style & FontStyle.Italic) != FontStyle.Regular;

        public static bool IsStrikeout(this FontStyle style) => 
            (style & FontStyle.Strikeout) != FontStyle.Regular;

        public static bool IsUnderline(this FontStyle style) => 
            (style & FontStyle.Underline) != FontStyle.Regular;
    }
}

