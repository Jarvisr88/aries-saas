namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public abstract class ScriptFontSizeComparableBase : IComparable<int>
    {
        private readonly string fontName;
        private readonly bool fontBold;
        private readonly bool fontItalic;
        private readonly bool fontStrikeout;
        private readonly bool fontUnderline;
        private readonly FontCache fontCache;
        private readonly int formattingFontSize;
        private readonly int doubleformattingFontSize;
        private readonly int textRotation;

        protected ScriptFontSizeComparableBase(FontCache fontCache, string fontName, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, int scriptFontSize, int doublescriptFontSize, int textRotation)
        {
            Guard.ArgumentNotNull(fontCache, "fontCache");
            this.fontCache = fontCache;
            this.fontName = fontName;
            this.fontBold = fontBold;
            this.fontItalic = fontItalic;
            this.fontStrikeout = fontStrikeout;
            this.fontUnderline = fontUnderline;
            this.formattingFontSize = fontCache.UnitConverter.PointsToFontUnits(scriptFontSize);
            this.doubleformattingFontSize = fontCache.UnitConverter.PointsToFontUnits(doublescriptFontSize);
            this.textRotation = textRotation;
        }

        public int CalculateDistance(int doublebaseFontSize)
        {
            int baseFontIndex = this.fontCache.CalcFontIndex(this.fontName, doublebaseFontSize, this.fontBold, this.fontItalic, CharacterFormattingScript.Normal, this.fontStrikeout, this.fontUnderline, this.textRotation);
            return (this.CalculateScriptFontSize(this.fontCache, baseFontIndex) - this.formattingFontSize);
        }

        protected internal abstract int CalculateScriptFontSize(FontCache fontCache, int baseFontIndex);
        public int CompareTo(int doublebaseFontSize) => 
            Math.Sign(this.CalculateDistance(doublebaseFontSize));
    }
}

