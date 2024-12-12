namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexOtherBluesOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 7;
        private readonly PdfType1FontGlyphZone[] otherBlues;

        public PdfCompactFontFormatPrivateDictIndexOtherBluesOperator(IList<object> operands) : this(GetGlyphZones(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexOtherBluesOperator(PdfType1FontGlyphZone[] otherBlues)
        {
            this.otherBlues = otherBlues;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.OtherBlues = this.otherBlues;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcGlyphZonesSize(this.otherBlues);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteGlyphZones(stream, this.otherBlues);
            stream.WriteByte(7);
        }
    }
}

