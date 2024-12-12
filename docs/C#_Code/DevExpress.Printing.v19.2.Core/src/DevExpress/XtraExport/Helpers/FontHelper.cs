namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;

    internal static class FontHelper
    {
        public static FontStyle GetFontStyle(XlFont font)
        {
            FontStyle regular = FontStyle.Regular;
            if (font.Bold)
            {
                regular |= FontStyle.Bold;
            }
            if (font.Italic)
            {
                regular |= FontStyle.Italic;
            }
            if (font.Underline != XlUnderlineType.None)
            {
                regular |= FontStyle.Underline;
            }
            if (font.StrikeThrough)
            {
                regular |= FontStyle.Strikeout;
            }
            return regular;
        }
    }
}

