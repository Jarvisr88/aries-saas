namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexBaseFontBlendOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x17;
        private readonly double[] baseFontBlend;

        public PdfCompactFontFormatDictIndexBaseFontBlendOperator(IList<object> operands) : this(GetDoubleArray(operands))
        {
        }

        public PdfCompactFontFormatDictIndexBaseFontBlendOperator(double[] baseFontBlend)
        {
            this.baseFontBlend = baseFontBlend;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.BaseFontBlend = this.baseFontBlend;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleArraySize(this.baseFontBlend);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDoubleArray(stream, this.baseFontBlend);
            stream.WriteByte(12);
            stream.WriteByte(0x17);
        }
    }
}

