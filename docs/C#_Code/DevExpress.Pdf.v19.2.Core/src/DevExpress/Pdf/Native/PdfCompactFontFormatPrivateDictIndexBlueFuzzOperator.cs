namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexBlueFuzzOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 11;
        private readonly int blueFuzz;

        public PdfCompactFontFormatPrivateDictIndexBlueFuzzOperator(IList<object> operands) : this(GetInteger(operands))
        {
            this.blueFuzz = GetInteger(operands);
        }

        public PdfCompactFontFormatPrivateDictIndexBlueFuzzOperator(int blueFuzz)
        {
            this.blueFuzz = blueFuzz;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.BlueFuzz = this.blueFuzz;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.blueFuzz);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.blueFuzz);
            stream.WriteByte(12);
            stream.WriteByte(11);
        }
    }
}

