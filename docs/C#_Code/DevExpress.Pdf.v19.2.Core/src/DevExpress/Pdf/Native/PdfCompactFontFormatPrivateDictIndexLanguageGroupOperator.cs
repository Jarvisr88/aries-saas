namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexLanguageGroupOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x11;
        private readonly int languageGroup;

        public PdfCompactFontFormatPrivateDictIndexLanguageGroupOperator(IList<object> operands) : this(GetInteger(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexLanguageGroupOperator(int languageGroup)
        {
            this.languageGroup = languageGroup;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.LanguageGroup = this.languageGroup;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.languageGroup);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.languageGroup);
            stream.WriteByte(12);
            stream.WriteByte(0x11);
        }
    }
}

