namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXQuantizationComponentMarker : JPXMarker
    {
        public const byte Type = 0x5d;

        public JPXQuantizationComponentMarker(PdfBigEndianStreamReader reader, JPXImage image, int? tileNumber) : base(reader)
        {
            if (base.DataLength < 3)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            long position = reader.Position;
            int componentIndex = reader.ReadByte();
            int byteCount = (int) ((base.DataLength - reader.Position) + position);
            if (tileNumber != null)
            {
                image.Tiles[tileNumber.Value].AppendQuantizationParameters(componentIndex, JPXQuantizationComponentParameters.Create(reader, byteCount));
            }
            else
            {
                image.QuantizationParameters[componentIndex] = JPXQuantizationComponentParameters.Create(reader, byteCount);
            }
        }
    }
}

