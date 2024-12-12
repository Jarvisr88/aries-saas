namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfDeferredTrueTypeFont : PdfTrueTypeFont
    {
        public PdfDeferredTrueTypeFont(string baseFont, PdfSimpleFontEncoding fontEncoding, PdfFontDescriptor fontDescriptor) : base(baseFont, fontEncoding, fontDescriptor, 0, new double[0x100])
        {
        }

        protected internal override PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isCloning)
        {
            if (!isCloning)
            {
                return this;
            }
            PdfDeferredTrueTypeFont font = new PdfDeferredTrueTypeFont(base.BaseFont, base.Encoding, this.FontDescriptor);
            int num = objects.LastObjectNumber + 1;
            objects.LastObjectNumber = num;
            font.ObjectNumber = num;
            this.UpdateFont(font);
            return font;
        }

        protected internal override bool IsDeferredObject(bool isCloning) => 
            true;

        private void UpdateFont(PdfDeferredTrueTypeFont font)
        {
            font.ToUnicode = base.ToUnicode;
            double[] widths = base.Widths;
            Array.Copy(widths, font.Widths, widths.Length);
        }

        protected internal override void UpdateObject(PdfObject value)
        {
            base.UpdateObject(value);
            PdfDeferredTrueTypeFont font = value as PdfDeferredTrueTypeFont;
            if (font != null)
            {
                this.UpdateFont(font);
            }
        }
    }
}

