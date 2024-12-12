namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexFontBBoxOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 5;
        private readonly PdfRectangle fontBBox;

        public PdfCompactFontFormatDictIndexFontBBoxOperator(PdfRectangle fontBBox)
        {
            this.fontBBox = fontBBox;
        }

        public PdfCompactFontFormatDictIndexFontBBoxOperator(IList<object> operands) : this(PdfRectangle.Parse(operands, null))
        {
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.FontBBox = this.fontBBox;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontBBox = this.fontBBox;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            (((base.GetSize(stringIndex) + CalcDoubleSize(this.fontBBox.Left)) + CalcDoubleSize(this.fontBBox.Bottom)) + CalcDoubleSize(this.fontBBox.Right)) + CalcDoubleSize(this.fontBBox.Top);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.fontBBox.Left);
            WriteDouble(stream, this.fontBBox.Bottom);
            WriteDouble(stream, this.fontBBox.Right);
            WriteDouble(stream, this.fontBBox.Top);
            stream.WriteByte(5);
        }
    }
}

