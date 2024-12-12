namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class PdfMeasuringContext
    {
        private float currentCharSpacing;
        private float currentWordSpacing;
        private PdfFont currentFont;
        private Font actualFont;
        private int currentHorizontalScaling = 100;

        internal PdfMeasuringContext()
        {
        }

        protected virtual float GetCharWidth(char ch) => 
            this.GetCharWidth(this.currentFont.GetGlyphByChar(ch));

        private float GetCharWidth(ushort glyph) => 
            (this.currentFont.GetCharWidth(glyph) / 1000f) * this.actualFont.Size;

        public float GetTextWidth(TextRun run) => 
            run.HasGlyphs ? this.GetTextWidth(run.Glyphs) : this.GetTextWidth(run.Text);

        public float GetTextWidth(string text)
        {
            if (this.currentFont == null)
            {
                return 0f;
            }
            float num = 0f;
            for (int i = 0; i < text.Length; i++)
            {
                float charWidth = this.GetCharWidth(text[i]);
                if (this.currentHorizontalScaling != 100)
                {
                    charWidth *= ((float) this.currentHorizontalScaling) / 100f;
                }
                charWidth = (charWidth <= 0f) ? 0f : (charWidth + this.currentCharSpacing);
                if ((text[i] == ' ') && (i < (text.Length - 1)))
                {
                    charWidth += this.currentWordSpacing;
                }
                num += charWidth;
            }
            return num;
        }

        internal float GetTextWidth(ushort[] glyphs)
        {
            if (this.currentFont == null)
            {
                return 0f;
            }
            ushort glyphByChar = this.currentFont.GetGlyphByChar(' ');
            float num2 = 0f;
            for (int i = 0; i < glyphs.Length; i++)
            {
                float charWidth = this.GetCharWidth(glyphs[i]);
                if (this.currentHorizontalScaling != 100)
                {
                    charWidth *= ((float) this.currentHorizontalScaling) / 100f;
                }
                charWidth = (charWidth <= 0f) ? 0f : (charWidth + this.currentCharSpacing);
                if ((glyphs[i] == glyphByChar) && (i < (glyphs.Length - 1)))
                {
                    charWidth += this.currentWordSpacing;
                }
                num2 += charWidth;
            }
            return num2;
        }

        public virtual void SetCharSpacing(float charSpace)
        {
            this.currentCharSpacing = charSpace;
        }

        protected void SetFont(PdfFont currentFont, Font font)
        {
            this.currentFont = currentFont;
            this.actualFont = font;
        }

        internal void SetFont(TTFFile ttfFile, Font font)
        {
            this.SetFont((ttfFile == null) ? null : new PdfFont(font, ttfFile, true), font);
        }

        public virtual void SetHorizontalScaling(int scale)
        {
            if (scale >= 0)
            {
                this.currentHorizontalScaling = scale;
            }
        }

        public virtual void SetWordSpacing(float wordSpace)
        {
            this.currentWordSpacing = wordSpace;
        }

        protected Font ActualFont =>
            this.actualFont;

        protected PdfFont CurrentFont =>
            this.currentFont;

        protected float CurrentCharSpacing
        {
            get => 
                this.currentCharSpacing;
            set => 
                this.currentCharSpacing = value;
        }

        protected float CurrentWordSpacing
        {
            get => 
                this.currentWordSpacing;
            set => 
                this.currentWordSpacing = value;
        }
    }
}

