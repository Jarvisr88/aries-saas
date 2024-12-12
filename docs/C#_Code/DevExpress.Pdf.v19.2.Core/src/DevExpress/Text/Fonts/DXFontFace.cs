namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;

    public abstract class DXFontFace : IDisposable
    {
        protected DXFontFace()
        {
        }

        public abstract DXCTLShaper CreateShaper();
        public abstract void Dispose();
        public abstract IList<DXCharRange> GetFontCharacterRanges();
        public abstract DXFontFileInfo GetFontFile();
        public abstract DXShapedGlyphInfo GetShapedGlyphsInfo(string text);
        public abstract float MeasureCharacterWidth(char character, float fontSize);
        public abstract DXSizeF MeasureString(string text, float fontSize);

        public abstract string FamilyName { get; }

        public abstract DXFontWeight Weight { get; }

        public abstract DXFontStretch Stretch { get; }

        public abstract DXFontStyle Style { get; }

        public abstract DXFontSimulations Simulations { get; }

        public abstract byte[] Panose { get; }

        public abstract DXFontMetrics Metrics { get; }
    }
}

