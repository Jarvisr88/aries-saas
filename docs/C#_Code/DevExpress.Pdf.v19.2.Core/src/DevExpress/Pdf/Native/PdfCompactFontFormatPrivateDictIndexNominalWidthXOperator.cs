namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexNominalWidthXOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 0x15;
        private readonly double nominalWidthX;

        public PdfCompactFontFormatPrivateDictIndexNominalWidthXOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexNominalWidthXOperator(double nominalWidthX)
        {
            this.nominalWidthX = nominalWidthX;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.NominalWidthX = this.nominalWidthX;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.nominalWidthX);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.nominalWidthX);
            stream.WriteByte(0x15);
        }
    }
}

