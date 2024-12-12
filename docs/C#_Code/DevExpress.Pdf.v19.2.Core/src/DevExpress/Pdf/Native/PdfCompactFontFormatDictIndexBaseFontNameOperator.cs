namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexBaseFontNameOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x16;
        private readonly string baseFontName;

        public PdfCompactFontFormatDictIndexBaseFontNameOperator(string baseFontName)
        {
            this.baseFontName = baseFontName;
        }

        public PdfCompactFontFormatDictIndexBaseFontNameOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.BaseFontName = this.baseFontName;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.baseFontName));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.baseFontName));
            stream.WriteByte(12);
            stream.WriteByte(0x16);
        }
    }
}

