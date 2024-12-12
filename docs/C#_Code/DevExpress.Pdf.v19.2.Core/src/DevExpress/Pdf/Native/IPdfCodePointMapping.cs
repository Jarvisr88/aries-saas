namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfCodePointMapping
    {
        bool UpdateCodePoints(short[] codePoints, bool useEmbeddedFontEncoding);
    }
}

