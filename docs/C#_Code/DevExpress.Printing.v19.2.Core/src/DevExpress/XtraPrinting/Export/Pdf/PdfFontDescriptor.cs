namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    internal class PdfFontDescriptor : PdfDocumentDictionaryObject, IDisposable
    {
        protected PdfFontBase ownerFont;
        private PdfStream fontFile;

        public PdfFontDescriptor(PdfFontBase ownerFont, bool compressed) : base(compressed)
        {
            this.ownerFont = ownerFont;
            if (ownerFont is PdfType0Font)
            {
                this.fontFile = base.Compressed ? new PdfFlateStream(true) : new PdfStream(true);
            }
        }

        public void Dispose()
        {
            if (this.fontFile != null)
            {
                this.fontFile.Close();
                this.fontFile = null;
            }
        }

        public override void FillUp()
        {
            if (this.ownerFont != null)
            {
                base.Dictionary.Add("Type", "FontDescriptor");
                base.Dictionary.Add("FontName", this.OwnerFont.BaseFont);
                base.Dictionary.Add("Ascent", (int) Math.Round((double) (this.TTFFile.HHea.Ascender * this.Owner.ScaleFactor)));
                base.Dictionary.Add("CapHeight", 500);
                base.Dictionary.Add("Descent", (int) Math.Round((double) (this.TTFFile.HHea.Descender * this.Owner.ScaleFactor)));
                base.Dictionary.Add("Flags", this.GetFlags());
                int num = (int) Math.Round((double) (this.TTFFile.Head.XMin * this.Owner.ScaleFactor));
                int num2 = (int) Math.Round((double) (this.TTFFile.Head.YMin * this.Owner.ScaleFactor));
                int num3 = (int) Math.Round((double) (this.TTFFile.Head.XMax * this.Owner.ScaleFactor));
                int num4 = (int) Math.Round((double) (this.TTFFile.Head.YMax * this.Owner.ScaleFactor));
                base.Dictionary.Add("FontBBox", new PdfRectangle((float) num, (float) num2, (float) num3, (float) num4));
                base.Dictionary.Add("ItalicAngle", (int) this.TTFFile.Post.ItalicAngle);
                base.Dictionary.Add("StemV", 0);
                if (this.fontFile != null)
                {
                    if (!this.TTFFile.IsEmbeddableFont)
                    {
                        throw new PdfException($"The {this.Owner.Font.Name} font cannot be embedded");
                    }
                    base.Dictionary.Add("FontFile2", this.fontFile);
                    this.Owner.WriteFontSubset(this.fontFile.Data);
                }
            }
        }

        protected virtual int GetFlags()
        {
            byte[] buffer = new byte[4];
            buffer = this.SetBit(buffer, 6);
            if (this.TTFFile.OS2.Panose.bProportion == 9)
            {
                buffer = this.SetBit(buffer, 1);
            }
            if ((this.TTFFile.OS2.Panose.bSerifType != 11) && ((this.TTFFile.OS2.Panose.bSerifType != 12) && (this.TTFFile.OS2.Panose.bSerifType != 13)))
            {
                buffer = this.SetBit(buffer, 2);
            }
            if (this.TTFFile.OS2.Panose.bFamilyType == 3)
            {
                buffer = this.SetBit(buffer, 4);
            }
            return BitConverter.ToInt32(buffer, 0);
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            if (this.fontFile != null)
            {
                xRef.RegisterObject(this.fontFile);
            }
        }

        protected unsafe byte[] SetBit(byte[] value, int bitNumber)
        {
            bitNumber--;
            if ((bitNumber >= 0) && (bitNumber < (value.Length * 8)))
            {
                int index = bitNumber / 8;
                byte num3 = (byte) (1 << ((bitNumber % 8) & 0x1f));
                byte* numPtr1 = &(value[index]);
                numPtr1[0] = (byte) (numPtr1[0] | num3);
            }
            return value;
        }

        protected override void WriteContent(StreamWriter writer)
        {
            if (this.fontFile != null)
            {
                this.fontFile.WriteIndirect(writer);
            }
        }

        internal DevExpress.XtraPrinting.Export.Pdf.TTFFile TTFFile =>
            this.OwnerFont.TTFFile;

        internal PdfFont Owner =>
            this.OwnerFont.Owner;

        public PdfFontBase OwnerFont =>
            this.ownerFont;
    }
}

