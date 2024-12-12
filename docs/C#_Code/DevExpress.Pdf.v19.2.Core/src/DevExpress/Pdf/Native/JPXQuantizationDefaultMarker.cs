namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXQuantizationDefaultMarker : JPXMarker
    {
        public const byte Type = 0x5c;

        public JPXQuantizationDefaultMarker(PdfBigEndianStreamReader reader, JPXImage image) : base(reader)
        {
            if (base.DataLength < 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            long position = reader.Position;
            int byteCount = (int) ((base.DataLength - reader.Position) + position);
            JPXQuantizationComponentParameters parameters = JPXQuantizationComponentParameters.Create(reader, byteCount);
            JPXQuantizationComponentParameters[] quantizationParameters = image.QuantizationParameters;
            for (int i = 0; i < quantizationParameters.Length; i++)
            {
                quantizationParameters[i] = parameters;
            }
            if ((reader.Position - position) > base.DataLength)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

