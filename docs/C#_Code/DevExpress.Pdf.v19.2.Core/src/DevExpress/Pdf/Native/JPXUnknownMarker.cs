namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXUnknownMarker : JPXMarker
    {
        public JPXUnknownMarker(PdfBigEndianStreamReader reader, JPXImage image) : base(reader)
        {
            reader.ReadBytes(base.DataLength);
        }
    }
}

