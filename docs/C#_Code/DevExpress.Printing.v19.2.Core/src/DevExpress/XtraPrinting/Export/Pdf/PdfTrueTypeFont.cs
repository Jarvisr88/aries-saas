namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Text;

    internal class PdfTrueTypeFont : PdfFontBase
    {
        public const int FirstChar = 0x20;
        public const int LastChar = 0xff;
        private PdfFontDescriptor fontDescriptor;
        protected PdfArray widths;

        public PdfTrueTypeFont(PdfFont owner, bool compressed) : base(owner, compressed)
        {
            this.fontDescriptor = this.CreateFontDescriptor();
            this.widths = new PdfArray(PdfObjectType.Indirect);
            this.FillWidths();
        }

        protected virtual PdfFontDescriptor CreateFontDescriptor() => 
            new PdfFontDescriptor(this, base.Compressed);

        public override void Dispose()
        {
            if (this.fontDescriptor != null)
            {
                this.fontDescriptor.Dispose();
                this.fontDescriptor = null;
            }
        }

        private static bool FamilyNamesAreDifferent(TTFName name) => 
            !string.IsNullOrEmpty(name.MacintoshFamilyName) && ((name.MacintoshFamilyName != name.FamilyName) && !string.IsNullOrEmpty(name.PostScriptName));

        public override void FillUp()
        {
            base.FillUp();
            base.Dictionary.Add("FirstChar", 0x20);
            base.Dictionary.Add("LastChar", 0xff);
            base.Dictionary.Add("Widths", this.widths);
            base.Dictionary.Add("Encoding", "WinAnsiEncoding");
            base.Dictionary.Add("FontDescriptor", this.fontDescriptor.Dictionary);
            this.fontDescriptor.FillUp();
        }

        protected virtual void FillWidths()
        {
            this.widths.MaxRowCount = 8;
            for (int i = 0x20; i <= 0xff; i++)
            {
                byte[] bytes = BitConverter.GetBytes(i);
                byte num2 = BitConverter.IsLittleEndian ? bytes[0] : bytes[bytes.Length - 1];
                byte[] buffer1 = new byte[] { num2 };
                byte[] buffer2 = Encoding.Convert(DXEncoding.GetEncoding(0x4e4), Encoding.Unicode, buffer1);
                char ch = Encoding.Unicode.GetChars(buffer2)[0];
                this.widths.Add((int) base.Owner.GetCharWidth(ch));
            }
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            xRef.RegisterObject(this.widths);
            this.fontDescriptor.Register(xRef);
        }

        protected override void WriteContent(StreamWriter writer)
        {
            base.WriteContent(writer);
            this.widths.WriteIndirect(writer);
            this.fontDescriptor.Write(writer);
        }

        public override string Subtype =>
            "TrueType";

        public override string BaseFont =>
            ((base.Owner.TTFFile == null) || !FamilyNamesAreDifferent(base.Owner.TTFFile.Name)) ? PdfFontUtils.GetTrueTypeFontName(base.Font, false) : PdfStringUtils.ClearSpaces(base.Owner.TTFFile.Name.PostScriptName);

        public PdfFontDescriptor FontDescriptor =>
            this.fontDescriptor;
    }
}

