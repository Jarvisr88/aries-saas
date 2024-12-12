namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexPaintTypeOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 5;
        private readonly PdfType1FontPaintType paintType;

        public PdfCompactFontFormatDictIndexPaintTypeOperator(PdfType1FontPaintType paintType)
        {
            this.paintType = paintType;
        }

        public PdfCompactFontFormatDictIndexPaintTypeOperator(IList<object> operands) : this((PdfType1FontPaintType) GetInteger(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.PaintType = this.paintType;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + 1;

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, (int) this.paintType);
            stream.WriteByte(12);
            stream.WriteByte(5);
        }
    }
}

