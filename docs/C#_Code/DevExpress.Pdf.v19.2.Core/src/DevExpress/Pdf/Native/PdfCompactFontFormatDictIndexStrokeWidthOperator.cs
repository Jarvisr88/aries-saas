namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexStrokeWidthOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 8;
        private readonly double strokeWidth;

        public PdfCompactFontFormatDictIndexStrokeWidthOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatDictIndexStrokeWidthOperator(double strokeWidth)
        {
            this.strokeWidth = strokeWidth;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.StrokeWidth = this.strokeWidth;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.strokeWidth);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.strokeWidth);
            stream.WriteByte(12);
            stream.WriteByte(8);
        }
    }
}

