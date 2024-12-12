namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexSubrsOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 0x13;
        private PdfCompactFontFormatBinaryIndex subrs;
        private int offset;

        public PdfCompactFontFormatPrivateDictIndexSubrsOperator(IList<byte[]> subrs)
        {
            this.subrs = new PdfCompactFontFormatBinaryIndex(subrs);
        }

        public PdfCompactFontFormatPrivateDictIndexSubrsOperator(IList<object> operands)
        {
            this.offset = GetInteger(operands);
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            long position = stream.Position;
            stream.Position = position + this.offset;
            this.subrs = new PdfCompactFontFormatBinaryIndex(stream);
            privateData.Subrs = this.subrs.Data;
            stream.Position = position;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.offset);

        public bool UpdateOffset(int offset)
        {
            int size = this.GetSize(null);
            this.offset = offset;
            return (this.GetSize(null) != size);
        }

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.offset);
            stream.WriteByte(0x13);
        }

        public void WriteData(PdfBinaryStream stream)
        {
            this.subrs.Write(stream);
        }

        public int DataLength =>
            this.subrs.DataLength;
    }
}

