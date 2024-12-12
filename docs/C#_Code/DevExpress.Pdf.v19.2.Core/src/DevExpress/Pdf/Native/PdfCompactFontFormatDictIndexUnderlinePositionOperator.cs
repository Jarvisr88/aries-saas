namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexUnderlinePositionOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 3;
        private readonly double underlinePosition;

        public PdfCompactFontFormatDictIndexUnderlinePositionOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatDictIndexUnderlinePositionOperator(double underlinePosition)
        {
            this.underlinePosition = underlinePosition;
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.UnderlinePosition = this.underlinePosition;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.UnderlinePosition = this.underlinePosition;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.underlinePosition);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.underlinePosition);
            stream.WriteByte(12);
            stream.WriteByte(3);
        }
    }
}

