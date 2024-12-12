namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexCIDCountOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x22;
        private readonly int cidCount;

        public PdfCompactFontFormatDictIndexCIDCountOperator(IList<object> operands) : this(GetInteger(operands))
        {
        }

        public PdfCompactFontFormatDictIndexCIDCountOperator(int cidCount)
        {
            this.cidCount = cidCount;
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.CIDCount = this.cidCount;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            ToCIDFontProgram(fontProgram).CIDCount = this.cidCount;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.cidCount);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.cidCount);
            stream.WriteByte(12);
            stream.WriteByte(0x22);
        }
    }
}

