namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class JPXStartOfTilePartMarker : JPXMarker
    {
        public const byte Type = 0x90;

        public JPXStartOfTilePartMarker(PdfBigEndianStreamReader reader, JPXImage image) : base(reader)
        {
            if (base.DataLength != 8)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = reader.ReadInt16();
            int num2 = reader.ReadInt32();
            int num3 = reader.ReadByte();
            int num4 = reader.ReadByte();
            long position = reader.Position;
            while (true)
            {
                JPXMarker marker = Create(reader, image, new int?(num));
                if (marker is JPXStartOfDataMarker)
                {
                    image.Tiles[num].AppendPacket(new MemoryStream(reader.ReadBytes(((num2 - base.DataLength) - 6) - ((int) ((reader.Position - position) - 2L)))));
                    return;
                }
            }
        }
    }
}

