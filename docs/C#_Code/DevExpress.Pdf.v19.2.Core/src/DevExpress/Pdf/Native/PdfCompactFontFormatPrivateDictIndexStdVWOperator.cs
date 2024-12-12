namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexStdVWOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 11;
        private readonly double stdVW;

        public PdfCompactFontFormatPrivateDictIndexStdVWOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexStdVWOperator(double stdVW)
        {
            this.stdVW = stdVW;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.StdVW = new double?(this.stdVW);
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.stdVW);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.stdVW);
            stream.WriteByte(11);
        }
    }
}

