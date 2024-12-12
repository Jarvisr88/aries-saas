namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexStemSnapHOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 12;
        private readonly double[] stemSnapH;

        public PdfCompactFontFormatPrivateDictIndexStemSnapHOperator(IList<object> operands) : this(GetDoubleArray(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexStemSnapHOperator(double[] stemSnapH)
        {
            this.stemSnapH = stemSnapH;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.StemSnapH = this.stemSnapH;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleArraySize(this.stemSnapH);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDoubleArray(stream, this.stemSnapH);
            stream.WriteByte(12);
            stream.WriteByte(12);
        }
    }
}

