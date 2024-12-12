namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexWeightOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 4;
        private readonly string weight;

        public PdfCompactFontFormatDictIndexWeightOperator(string weight)
        {
            this.weight = weight;
        }

        public PdfCompactFontFormatDictIndexWeightOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.Weight = this.weight;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.weight));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.weight));
            stream.WriteByte(4);
        }
    }
}

