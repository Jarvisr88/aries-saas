namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexForceBoldOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 14;
        private readonly bool forceBold;

        public PdfCompactFontFormatPrivateDictIndexForceBoldOperator(bool forceBold)
        {
            this.forceBold = forceBold;
        }

        public PdfCompactFontFormatPrivateDictIndexForceBoldOperator(IList<object> operands) : this(GetBoolean(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.ForceBold = this.forceBold;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + 1;

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteBoolean(stream, this.forceBold);
            stream.WriteByte(12);
            stream.WriteByte(14);
        }
    }
}

