namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexStdHWOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 10;
        private readonly double stdHW;

        public PdfCompactFontFormatPrivateDictIndexStdHWOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexStdHWOperator(double stdHW)
        {
            this.stdHW = stdHW;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.StdHW = new double?(this.stdHW);
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.stdHW);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.stdHW);
            stream.WriteByte(10);
        }
    }
}

