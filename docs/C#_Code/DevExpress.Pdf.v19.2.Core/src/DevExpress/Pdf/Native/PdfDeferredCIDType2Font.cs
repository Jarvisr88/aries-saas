namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfDeferredCIDType2Font : PdfCIDType2Font
    {
        public PdfDeferredCIDType2Font(string baseFont, PdfCompositeFontDescriptor fontDescriptor) : base(baseFont, fontDescriptor)
        {
        }

        protected internal override PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isCloning)
        {
            if (!isCloning)
            {
                return this;
            }
            PdfDeferredCIDType2Font font = new PdfDeferredCIDType2Font(base.BaseFont, (PdfCompositeFontDescriptor) this.FontDescriptor);
            int num = objects.LastObjectNumber + 1;
            objects.LastObjectNumber = num;
            font.ObjectNumber = num;
            this.UpdateFont(font);
            return font;
        }

        protected internal override bool IsDeferredObject(bool isCloning) => 
            true;

        private void UpdateFont(PdfDeferredCIDType2Font font)
        {
            if (font != null)
            {
                font.ToUnicode = base.ToUnicode;
                byte[] fontFileData = base.FontFileData;
                byte[] buffer2 = font.FontFileData;
                if ((buffer2 == null) || (buffer2.Length != fontFileData.Length))
                {
                    font.FontFileData = fontFileData;
                }
                font.Widths = base.Widths;
            }
        }

        protected internal override void UpdateObject(PdfObject value)
        {
            base.UpdateObject(value);
            this.UpdateFont(value as PdfDeferredCIDType2Font);
        }
    }
}

