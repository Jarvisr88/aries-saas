namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexStemSnapVOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 13;
        private readonly double[] stemSnapV;

        public PdfCompactFontFormatPrivateDictIndexStemSnapVOperator(IList<object> operands) : this(GetDoubleArray(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexStemSnapVOperator(double[] stemSnapV)
        {
            this.stemSnapV = stemSnapV;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.StemSnapV = this.stemSnapV;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleArraySize(this.stemSnapV);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDoubleArray(stream, this.stemSnapV);
            stream.WriteByte(12);
            stream.WriteByte(13);
        }
    }
}

