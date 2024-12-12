namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexFontNameOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x26;
        private readonly string fontName;

        public PdfCompactFontFormatDictIndexFontNameOperator(string fontName)
        {
            this.fontName = fontName;
        }

        public PdfCompactFontFormatDictIndexFontNameOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.FontName = this.fontName;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.CIDFontName = this.fontName;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.fontName));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.fontName));
            stream.WriteByte(12);
            stream.WriteByte(0x26);
        }
    }
}

