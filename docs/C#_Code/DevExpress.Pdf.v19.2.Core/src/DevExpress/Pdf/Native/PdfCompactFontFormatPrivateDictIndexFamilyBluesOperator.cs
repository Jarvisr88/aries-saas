namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexFamilyBluesOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 8;
        private readonly PdfType1FontGlyphZone[] familyBlues;

        public PdfCompactFontFormatPrivateDictIndexFamilyBluesOperator(IList<object> operands) : this(GetGlyphZones(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexFamilyBluesOperator(PdfType1FontGlyphZone[] familyBlues)
        {
            this.familyBlues = familyBlues;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.FamilyBlues = this.familyBlues;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcGlyphZonesSize(this.familyBlues);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteGlyphZones(stream, this.familyBlues);
            stream.WriteByte(8);
        }
    }
}

