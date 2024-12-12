namespace DevExpress.Office.Drawing
{
    using System;

    public class GdiStringViewInfo
    {
        private IntPtr glyphs;
        private IntPtr characterWidths;
        private int glyphCount;

        public IntPtr Glyphs
        {
            get => 
                this.glyphs;
            set => 
                this.glyphs = value;
        }

        public IntPtr CharacterWidths
        {
            get => 
                this.characterWidths;
            set => 
                this.characterWidths = value;
        }

        public int GlyphCount
        {
            get => 
                this.glyphCount;
            set => 
                this.glyphCount = value;
        }
    }
}

