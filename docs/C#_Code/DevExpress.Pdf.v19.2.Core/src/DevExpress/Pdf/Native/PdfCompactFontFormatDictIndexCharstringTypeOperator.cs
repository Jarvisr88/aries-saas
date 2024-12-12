namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexCharstringTypeOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 6;
        private readonly PdfType1FontType fontType;

        public PdfCompactFontFormatDictIndexCharstringTypeOperator(PdfType1FontType fontType)
        {
            this.fontType = fontType;
        }

        public PdfCompactFontFormatDictIndexCharstringTypeOperator(IList<object> operands) : this((PdfType1FontType) GetInteger(operands))
        {
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.FontType = this.fontType;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.SetFontType(this.fontType);
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + 1;

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, (int) this.fontType);
            stream.WriteByte(12);
            stream.WriteByte(6);
        }
    }
}

