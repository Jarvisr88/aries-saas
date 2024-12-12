namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexDefaultWidthXOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 20;
        private readonly double defaultWidthX;

        public PdfCompactFontFormatPrivateDictIndexDefaultWidthXOperator(IList<object> operands) : this(GetDouble(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexDefaultWidthXOperator(double defaultWidthX)
        {
            this.defaultWidthX = defaultWidthX;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.DefaultWidthX = this.defaultWidthX;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcDoubleSize(this.defaultWidthX);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.defaultWidthX);
            stream.WriteByte(20);
        }
    }
}

