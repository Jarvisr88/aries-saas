namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexUnderlineThicknessOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 4;
        private readonly double underlineThickness;

        public PdfCompactFontFormatDictIndexUnderlineThicknessOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatDictIndexUnderlineThicknessOperator(double underlineThickness)
        {
            this.underlineThickness = underlineThickness;
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.UnderlineThickness = this.underlineThickness;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.UnderlineThickness = this.underlineThickness;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.underlineThickness);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.underlineThickness);
            stream.WriteByte(12);
            stream.WriteByte(4);
        }
    }
}

