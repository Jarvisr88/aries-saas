namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexCIDFontVersionOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x1f;
        private readonly double cidFontVersion;

        public PdfCompactFontFormatDictIndexCIDFontVersionOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatDictIndexCIDFontVersionOperator(double cidFontVersion)
        {
            this.cidFontVersion = cidFontVersion;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            ToCIDFontProgram(fontProgram).CIDFontVersion = this.cidFontVersion;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.cidFontVersion);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.cidFontVersion);
            stream.WriteByte(12);
            stream.WriteByte(0x1f);
        }
    }
}

