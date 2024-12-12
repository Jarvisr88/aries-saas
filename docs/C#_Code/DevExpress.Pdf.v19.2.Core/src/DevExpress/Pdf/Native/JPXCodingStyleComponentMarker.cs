namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXCodingStyleComponentMarker : JPXMarker
    {
        public const byte Type = 0x53;

        public JPXCodingStyleComponentMarker(PdfBigEndianStreamReader reader) : base(reader)
        {
            if (base.DataLength < 7)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            reader.Skip(base.DataLength);
        }
    }
}

