namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexBlueShiftOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 10;
        private readonly double blueShift;

        public PdfCompactFontFormatPrivateDictIndexBlueShiftOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexBlueShiftOperator(double blueShift)
        {
            this.blueShift = blueShift;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.BlueShift = this.blueShift;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.blueShift);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.blueShift);
            stream.WriteByte(12);
            stream.WriteByte(10);
        }
    }
}

