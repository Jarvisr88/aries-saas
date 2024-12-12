namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexForceBoldThresholdOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 15;
        private readonly double forceBoldThreshold;

        public PdfCompactFontFormatPrivateDictIndexForceBoldThresholdOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexForceBoldThresholdOperator(double forceBoldThreshold)
        {
            this.forceBoldThreshold = forceBoldThreshold;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.ForceBoldThreshold = new double?(this.forceBoldThreshold);
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.forceBoldThreshold);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.forceBoldThreshold);
            stream.WriteByte(12);
            stream.WriteByte(15);
        }
    }
}

