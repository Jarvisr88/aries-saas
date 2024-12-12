namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexCopyrightOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0;
        private readonly string copyright;

        public PdfCompactFontFormatDictIndexCopyrightOperator(string copyright)
        {
            this.copyright = copyright;
        }

        public PdfCompactFontFormatDictIndexCopyrightOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.Copyright = this.copyright;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.copyright));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.copyright));
            stream.WriteByte(12);
            stream.WriteByte(0);
        }
    }
}

