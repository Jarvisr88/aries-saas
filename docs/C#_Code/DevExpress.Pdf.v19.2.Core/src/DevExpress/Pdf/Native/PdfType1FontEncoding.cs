namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfType1FontEncoding
    {
        protected PdfType1FontEncoding()
        {
        }

        public abstract short[] GetCodeToGIDMapping(PdfType1FontCharset charset, PdfCompactFontFormatStringIndex stringIndex);

        public virtual bool IsDefault =>
            false;

        public virtual int Offset =>
            0;

        public abstract int DataLength { get; }
    }
}

