namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexItalicAngleOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 2;
        private readonly double italicAngle;

        public PdfCompactFontFormatDictIndexItalicAngleOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatDictIndexItalicAngleOperator(double italicAngle)
        {
            this.italicAngle = italicAngle;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.ItalicAngle = this.italicAngle;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.italicAngle);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.italicAngle);
            stream.WriteByte(12);
            stream.WriteByte(2);
        }
    }
}

