namespace DevExpress.Office.Drawing
{
    using System;

    public class SuperscriptFontSizeComparable : ScriptFontSizeComparableBase
    {
        public SuperscriptFontSizeComparable(FontCache fontCache, string fontName, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, int scriptFontSize, int doublescriptFontSize, int textRotation) : base(fontCache, fontName, fontBold, fontItalic, fontStrikeout, fontUnderline, scriptFontSize, doublescriptFontSize, textRotation)
        {
        }

        protected internal override int CalculateScriptFontSize(FontCache fontCache, int baseFontIndex) => 
            fontCache.CalcSuperscriptFontSize(baseFontIndex);
    }
}

