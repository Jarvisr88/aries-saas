namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class PartialTrustPdfTrueTypeFont : PdfTrueTypeFont
    {
        public PartialTrustPdfTrueTypeFont(PdfFont owner, bool compressed) : base(owner, compressed)
        {
        }

        protected override PdfFontDescriptor CreateFontDescriptor() => 
            new PartialTrustPdfFontDescriptor(this, base.Compressed);

        protected override void FillWidths()
        {
            base.widths.MaxRowCount = 8;
            for (int i = 0x20; i <= 0xff; i++)
            {
                float num2 = MeasuringHelper.MeasureCharWidth(Convert.ToChar(i), base.Font);
                base.widths.Add((int) ((num2 * 1000f) / base.Font.Size));
            }
        }
    }
}

