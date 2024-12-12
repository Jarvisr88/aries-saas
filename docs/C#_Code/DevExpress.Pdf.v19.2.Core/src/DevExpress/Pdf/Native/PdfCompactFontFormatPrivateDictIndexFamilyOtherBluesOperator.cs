namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexFamilyOtherBluesOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 9;
        private readonly PdfType1FontGlyphZone[] familyOtherBlues;

        public PdfCompactFontFormatPrivateDictIndexFamilyOtherBluesOperator(IList<object> operands) : this(GetGlyphZones(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexFamilyOtherBluesOperator(PdfType1FontGlyphZone[] familyOtherBlues)
        {
            this.familyOtherBlues = familyOtherBlues;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.FamilyOtherBlues = this.familyOtherBlues;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcGlyphZonesSize(this.familyOtherBlues);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteGlyphZones(stream, this.familyOtherBlues);
            stream.WriteByte(9);
        }
    }
}

