namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXContiguousCodeStreamBox : JPXBox
    {
        public const int Type = 0x6a703263;

        public JPXContiguousCodeStreamBox(PdfBigEndianStreamReader reader, int length, JPXImage image)
        {
            long num = reader.Position + length;
            JPXMarker.Read(reader, image, 0x4f);
            JPXMarker.Read(reader, image, 0x51);
            JPXCodingStyleDefaultMarker marker = null;
            JPXQuantizationDefaultMarker marker2 = null;
            while (true)
            {
                if (reader.Position < num)
                {
                    JPXMarker marker3 = JPXMarker.Create(reader, image);
                    marker = marker ?? (marker3 as JPXCodingStyleDefaultMarker);
                    JPXQuantizationDefaultMarker marker1 = marker2;
                    if (marker2 == null)
                    {
                        JPXQuantizationDefaultMarker local2 = marker2;
                        marker1 = marker3 as JPXQuantizationDefaultMarker;
                    }
                    marker2 = marker1;
                    if (!(marker3 is JPXCodeStreamEndMarker))
                    {
                        continue;
                    }
                }
                if ((marker == null) || ((marker2 == null) || (reader.Position > num)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return;
            }
        }
    }
}

