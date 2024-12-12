namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexExpansionFactorOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x12;
        private readonly double expansionFactor;

        public PdfCompactFontFormatPrivateDictIndexExpansionFactorOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexExpansionFactorOperator(double expansionFactor)
        {
            this.expansionFactor = expansionFactor;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.ExpansionFactor = this.expansionFactor;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.expansionFactor);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.expansionFactor);
            stream.WriteByte(12);
            stream.WriteByte(0x12);
        }
    }
}

