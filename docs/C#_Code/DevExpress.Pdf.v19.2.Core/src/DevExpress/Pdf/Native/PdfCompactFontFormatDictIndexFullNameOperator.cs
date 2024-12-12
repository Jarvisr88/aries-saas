namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexFullNameOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 2;
        private readonly string fullName;

        public PdfCompactFontFormatDictIndexFullNameOperator(string fullName)
        {
            this.fullName = fullName;
        }

        public PdfCompactFontFormatDictIndexFullNameOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.FullName = this.fullName;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.fullName));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.fullName));
            stream.WriteByte(2);
        }
    }
}

