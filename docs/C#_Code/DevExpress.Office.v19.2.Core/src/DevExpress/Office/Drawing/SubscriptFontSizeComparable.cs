namespace DevExpress.Office.Drawing
{
    using System;

    public class SubscriptFontSizeComparable : ScriptFontSizeComparableBase
    {
        public SubscriptFontSizeComparable(FontCache fontCache, string fontName, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, int scriptFontSize, int doublescriptFontSize, int textRotation) : base(fontCache, fontName, fontBold, fontItalic, fontStrikeout, fontUnderline, scriptFontSize, doublescriptFontSize, textRotation)
        {
        }

        protected internal override int CalculateScriptFontSize(FontCache fontCache, int baseFontIndex) => 
            fontCache.CalcSubscriptFontSize(baseFontIndex);
    }
}

