namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfEncoding : PdfObject
    {
        protected PdfEncoding()
        {
        }

        protected internal abstract PdfStringCommandData GetStringData(byte[] bytes, double[] glyphOffsets);
        protected internal abstract object Write(PdfObjectCollection objects);

        protected internal abstract bool ShouldUseEmbeddedFontEncoding { get; }
    }
}

