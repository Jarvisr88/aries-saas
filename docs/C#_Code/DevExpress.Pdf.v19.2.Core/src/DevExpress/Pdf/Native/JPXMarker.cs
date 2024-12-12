namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JPXMarker
    {
        private readonly int dataLength;

        protected JPXMarker()
        {
            this.dataLength = 0;
        }

        protected JPXMarker(PdfBigEndianStreamReader reader)
        {
            this.dataLength = reader.ReadInt16() - 2;
        }

        public static JPXMarker Create(PdfBigEndianStreamReader reader, JPXImage image)
        {
            int? tileNumber = null;
            return Create(reader, image, tileNumber);
        }

        public static JPXMarker Create(PdfBigEndianStreamReader reader, JPXImage image, int? tileNumber)
        {
            if (reader.ReadByte() != 0xff)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return Create(reader, image, reader.ReadByte(), tileNumber);
        }

        private static JPXMarker Create(PdfBigEndianStreamReader reader, JPXImage image, byte type, int? tileNumber)
        {
            if (type > 0x5d)
            {
                if (type == 0x90)
                {
                    return new JPXStartOfTilePartMarker(reader, image);
                }
                if (type == 0x93)
                {
                    return new JPXStartOfDataMarker();
                }
                if (type == 0xd9)
                {
                    return new JPXCodeStreamEndMarker();
                }
            }
            else
            {
                switch (type)
                {
                    case 0x4f:
                        return new JPXStartOfCodeStreamMarker();

                    case 80:
                        break;

                    case 0x51:
                        return new JPXSizeMarker(reader, image);

                    case 0x52:
                        return new JPXCodingStyleDefaultMarker(reader, image);

                    case 0x53:
                        return new JPXCodingStyleComponentMarker(reader);

                    default:
                        if (type == 0x5c)
                        {
                            return new JPXQuantizationDefaultMarker(reader, image);
                        }
                        if (type != 0x5d)
                        {
                            break;
                        }
                        return new JPXQuantizationComponentMarker(reader, image, tileNumber);
                }
            }
            return new JPXUnknownMarker(reader, image);
        }

        public static void Read(PdfBigEndianStreamReader reader, JPXImage image, byte type)
        {
            if ((reader.ReadByte() != 0xff) || (reader.ReadByte() != type))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int? tileNumber = null;
            Create(reader, image, type, tileNumber);
        }

        protected int DataLength =>
            this.dataLength;
    }
}

