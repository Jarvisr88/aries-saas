namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXCodingStyleDefaultMarker : JPXMarker
    {
        public const byte Type = 0x52;

        public JPXCodingStyleDefaultMarker(PdfBigEndianStreamReader reader, JPXImage image) : base(reader)
        {
            if (base.DataLength < 10)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            JPXCodingStyleParameter parameter = (JPXCodingStyleParameter) reader.ReadByte();
            image.CodingStyleDefault = new JPXCodingStyleDefault(reader);
            JPXCodingStyleComponent component = new JPXCodingStyleComponent(reader, parameter.HasFlag(JPXCodingStyleParameter.UsePrecincts));
            int index = 0;
            while (true)
            {
                JPXSize size = image.Size;
                if (index >= size.Components.Length)
                {
                    return;
                }
                image.CodingStyleComponents[index] = component;
                index++;
            }
        }
    }
}

