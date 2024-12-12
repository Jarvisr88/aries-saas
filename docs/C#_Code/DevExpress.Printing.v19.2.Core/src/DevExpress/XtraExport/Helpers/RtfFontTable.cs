namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;

    internal class RtfFontTable : RtfControl
    {
        private readonly Dictionary<string, RtfFont> fonts;

        public RtfFontTable()
        {
            Dictionary<string, RtfFont> dictionary1 = new Dictionary<string, RtfFont>();
            dictionary1.Add(RtfDocument.DefaultFont, new RtfFont(RtfDocument.DefaultFont, 0));
            this.fonts = dictionary1;
        }

        public void CheckFont(string fontName)
        {
            this.GetOrCreateFont(fontName);
        }

        public override string Compile()
        {
            base.WriteOpenBrace();
            int? nullable = null;
            base.WriteKeyword(Keyword.FontTable, nullable, false, false);
            this.WriteFonts();
            base.WriteCloseBrace();
            return base.Compile();
        }

        public int GetFontIndex(string fontName) => 
            this.GetOrCreateFont(fontName).Index;

        private RtfFont GetOrCreateFont(string fontName)
        {
            RtfFont font;
            if (!this.fonts.TryGetValue(fontName, out font))
            {
                font = new RtfFont(fontName, this.fonts.Count);
                this.fonts.Add(fontName, font);
            }
            return font;
        }

        private void WriteFonts()
        {
            foreach (RtfFont font in this.fonts.Values)
            {
                base.WriteChild(font, false);
            }
        }
    }
}

