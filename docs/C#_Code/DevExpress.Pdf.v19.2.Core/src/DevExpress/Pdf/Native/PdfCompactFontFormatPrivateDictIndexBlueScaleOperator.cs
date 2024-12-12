namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexBlueScaleOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 9;
        private readonly double blueScale;

        public PdfCompactFontFormatPrivateDictIndexBlueScaleOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexBlueScaleOperator(double blueScale)
        {
            this.blueScale = blueScale;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.BlueScale = this.blueScale;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.blueScale);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.blueScale);
            stream.WriteByte(12);
            stream.WriteByte(9);
        }
    }
}

