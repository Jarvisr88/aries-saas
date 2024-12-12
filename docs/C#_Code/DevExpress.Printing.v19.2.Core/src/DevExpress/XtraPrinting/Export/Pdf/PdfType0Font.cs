namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;
    using System.Text;

    internal class PdfType0Font : PdfFontBase
    {
        private PdfCIDFont cidFont;
        private PdfToUnicodeCMap toUnicode;

        public PdfType0Font(PdfFont owner, bool compressed) : base(owner, compressed)
        {
            this.cidFont = new PdfCIDFont(this, base.Compressed);
            this.toUnicode = new PdfToUnicodeCMap(this, base.Compressed);
        }

        public override void Dispose()
        {
            if (this.cidFont != null)
            {
                this.cidFont.Dispose();
                this.cidFont = null;
            }
            if (this.toUnicode != null)
            {
                this.toUnicode.Close();
                this.toUnicode = null;
            }
        }

        public override void FillUp()
        {
            base.FillUp();
            PdfArray array = new PdfArray {
                this.cidFont.Dictionary
            };
            base.Dictionary.Add("DescendantFonts", array);
            base.Dictionary.Add("Encoding", "Identity-H");
            this.cidFont.FillUp();
            base.Dictionary.Add("ToUnicode", this.toUnicode.Stream);
            this.toUnicode.FillUp();
        }

        public override string ProcessText(TextRun text)
        {
            StringBuilder builder = new StringBuilder(2 + (4 * (text.HasGlyphs ? text.Glyphs.Length : text.Text.Length)));
            builder.Append('<');
            if (text.HasGlyphs)
            {
                foreach (short num2 in text.Glyphs)
                {
                    builder.Append(((ushort) num2).ToString("X4"));
                }
            }
            else
            {
                foreach (char ch in text.Text)
                {
                    if (ch != '\0')
                    {
                        builder.Append(base.TTFFile.GetGlyphIndex(ch).ToString("X4"));
                    }
                }
            }
            builder.Append('>');
            return builder.ToString();
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            this.cidFont.Register(xRef);
            this.toUnicode.Register(xRef);
        }

        protected override void WriteContent(StreamWriter writer)
        {
            base.WriteContent(writer);
            this.cidFont.Write(writer);
            this.toUnicode.Write(writer);
        }

        public override string Subtype =>
            "Type0";

        public override string BaseFont =>
            PdfFontUtils.GetTrueTypeFontName(base.Font, true);

        public PdfCIDFont CIDFont =>
            this.cidFont;
    }
}

