namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXSizeMarker : JPXMarker
    {
        public const byte Type = 0x51;

        public JPXSizeMarker(PdfBigEndianStreamReader reader, JPXImage image) : base(reader)
        {
            reader.Skip(2);
            JPXSize size = new JPXSize(reader);
            image.Size = size;
            if (base.DataLength != (0x24 + (size.Components.Length * 3)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

