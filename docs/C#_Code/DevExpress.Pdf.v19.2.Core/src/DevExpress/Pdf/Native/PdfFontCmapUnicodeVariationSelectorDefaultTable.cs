namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapUnicodeVariationSelectorDefaultTable
    {
        private readonly int startUnicodeValue;
        private readonly byte additionalCount;

        public PdfFontCmapUnicodeVariationSelectorDefaultTable(int startUnicodeValue, byte additionalCount)
        {
            this.startUnicodeValue = startUnicodeValue;
            this.additionalCount = additionalCount;
        }

        public void Write(PdfBinaryStream tableStream)
        {
            tableStream.Write24BitInt(this.startUnicodeValue);
            tableStream.WriteByte(this.additionalCount);
        }

        public int StartUnicodeValue =>
            this.startUnicodeValue;

        public byte AdditionalCount =>
            this.additionalCount;
    }
}

