namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class PartialTrustPdfFontDescriptor : PdfFontDescriptor
    {
        public PartialTrustPdfFontDescriptor(PdfFontBase ownerFont, bool compressed) : base(ownerFont, compressed)
        {
        }

        public override void FillUp()
        {
            if (base.ownerFont != null)
            {
                base.Dictionary.Add("Type", "FontDescriptor");
                base.Dictionary.Add("FontName", base.OwnerFont.BaseFont);
                base.Dictionary.Add("Ascent", 0);
                base.Dictionary.Add("CapHeight", 500);
                base.Dictionary.Add("Descent", 0);
                base.Dictionary.Add("Flags", this.GetFlags());
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                base.Dictionary.Add("FontBBox", new PdfRectangle((float) num, (float) num2, (float) num3, (float) num4));
                base.Dictionary.Add("ItalicAngle", 0);
                base.Dictionary.Add("StemV", 0);
            }
        }

        protected override int GetFlags()
        {
            byte[] buffer = new byte[4];
            return BitConverter.ToInt32(base.SetBit(buffer, 6), 0);
        }
    }
}

