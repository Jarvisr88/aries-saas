namespace DevExpress.Office.Drawing
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.PInvoke;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class ShortFontInfo
    {
        public static ShortFontInfo FromParameters(string fontName, double fontSize, bool italic, XlScriptType scriptMode, bool bold, XlUnderlineType underlineMode, bool strikeThrough, bool shadow = false, bool outline = false)
        {
            ShortFontInfo info = new ShortFontInfo {
                FontName = fontName,
                FontSizeTwips = (int) (fontSize * 20.0),
                Italic = italic,
                StrikeThrough = strikeThrough,
                FontWeight = SpreadsheetFontWeight.Normal
            };
            if (bold)
            {
                info.FontWeight = SpreadsheetFontWeight.Bold;
            }
            info.ScriptMode = scriptMode;
            info.UnderlineMode = underlineMode;
            info.FontFamily = FontsInfoHelper.GetFontFamily(fontName);
            info.CharSet = FontsInfoHelper.GetFontCharSet(fontName);
            return info;
        }

        public string FontName { get; set; }

        public int FontSizeTwips { get; set; }

        public SpreadsheetFontWeight FontWeight { get; set; }

        public XlScriptType ScriptMode { get; set; }

        public XlUnderlineType UnderlineMode { get; set; }

        public FontFamilyIndex FontFamily { get; set; }

        public PInvokeSafeNativeMethods.LogFontCharSet CharSet { get; set; }

        public bool Italic { get; set; }

        public bool StrikeThrough { get; set; }
    }
}

