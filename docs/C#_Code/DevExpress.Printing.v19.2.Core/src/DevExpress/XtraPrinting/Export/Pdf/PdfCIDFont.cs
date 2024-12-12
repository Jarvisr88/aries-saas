namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.IO;

    internal class PdfCIDFont : PdfDocumentDictionaryObject, IDisposable
    {
        private PdfFontBase ownerFont;
        private PdfFontDescriptor fontDescriptor;
        private PdfArray widths;

        public PdfCIDFont(PdfFontBase ownerFont, bool compressed) : base(compressed)
        {
            this.ownerFont = ownerFont;
            this.fontDescriptor = new PdfFontDescriptor(ownerFont, base.Compressed);
            this.widths = new PdfArray();
            this.widths.MaxRowCount = 2;
        }

        public void Dispose()
        {
            if (this.fontDescriptor != null)
            {
                this.fontDescriptor.Dispose();
                this.fontDescriptor = null;
            }
        }

        private void FillConsecutiveWidths(ArrayList list)
        {
            if (list.Count != 0)
            {
                PdfArray array = new PdfArray();
                for (int i = 0; i < list.Count; i++)
                {
                    array.Add((int) this.OwnerFont.Owner.GetCharWidth((ushort) list[i]));
                }
                this.widths.Add((int) ((ushort) list[0]));
                this.widths.Add(array);
                list.Clear();
            }
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Type", "Font");
            base.Dictionary.Add("Subtype", "CIDFontType2");
            base.Dictionary.Add("BaseFont", this.OwnerFont.BaseFont);
            PdfDictionary dictionary = new PdfDictionary();
            dictionary.Add("Registry", new PdfLiteralString("Adobe"));
            dictionary.Add("Ordering", new PdfLiteralString("Identity"));
            dictionary.Add("Supplement", 0);
            base.Dictionary.Add("CIDSystemInfo", dictionary);
            base.Dictionary.Add("FontDescriptor", this.fontDescriptor.Dictionary);
            this.fontDescriptor.FillUp();
            base.Dictionary.Add("CIDToGIDMap", "Identity");
            base.Dictionary.Add("W", this.widths);
            this.FillWidths();
        }

        private void FillWidths()
        {
            ArrayList list = new ArrayList();
            foreach (ushort num2 in this.OwnerFont.Owner.CharCache.GlyphIndices)
            {
                if ((list.Count > 0) && ((((ushort) list[list.Count - 1]) + 1) != num2))
                {
                    this.FillConsecutiveWidths(list);
                }
                list.Add(num2);
            }
            this.FillConsecutiveWidths(list);
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            this.fontDescriptor.Register(xRef);
        }

        protected override void WriteContent(StreamWriter writer)
        {
            base.WriteContent(writer);
            this.fontDescriptor.Write(writer);
        }

        public PdfFontBase OwnerFont =>
            this.ownerFont;

        public PdfFontDescriptor FontDescriptor =>
            this.fontDescriptor;
    }
}

