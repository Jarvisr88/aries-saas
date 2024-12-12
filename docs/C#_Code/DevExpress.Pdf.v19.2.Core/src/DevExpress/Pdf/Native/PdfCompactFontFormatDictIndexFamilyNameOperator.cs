namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexFamilyNameOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 3;
        private readonly string familyName;

        public PdfCompactFontFormatDictIndexFamilyNameOperator(string familyName)
        {
            this.familyName = familyName;
        }

        public PdfCompactFontFormatDictIndexFamilyNameOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.FamilyName = this.familyName;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.familyName));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.familyName));
            stream.WriteByte(3);
        }
    }
}

