namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexCharStringsOperator : PdfCompactFontFormatDictIndexOperator, ICompactFontFormatDictIndexOffsetOperator
    {
        public const byte Code = 0x11;
        private PdfCompactFontFormatBinaryIndex charStrings;
        private int offset;

        public PdfCompactFontFormatDictIndexCharStringsOperator(IList<byte[]> charStrings)
        {
            this.charStrings = new PdfCompactFontFormatBinaryIndex(charStrings);
        }

        public PdfCompactFontFormatDictIndexCharStringsOperator(IList<object> operands)
        {
            this.offset = GetInteger(operands);
        }

        void ICompactFontFormatDictIndexOffsetOperator.WriteData(PdfBinaryStream stream)
        {
            this.charStrings.Write(stream);
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            stream.Position = this.offset;
            this.charStrings = new PdfCompactFontFormatBinaryIndex(stream);
            fontProgram.CharStrings = this.charStrings.Data;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.offset);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.offset);
            stream.WriteByte(0x11);
        }

        int ICompactFontFormatDictIndexOffsetOperator.Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }

        int ICompactFontFormatDictIndexOffsetOperator.Length =>
            this.charStrings.DataLength;
    }
}

