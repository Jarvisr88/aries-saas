namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexBlueValuesOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 6;
        private readonly PdfType1FontGlyphZone[] blueValues;

        public PdfCompactFontFormatPrivateDictIndexBlueValuesOperator(IList<object> operands) : this(GetGlyphZones(operands))
        {
        }

        public PdfCompactFontFormatPrivateDictIndexBlueValuesOperator(PdfType1FontGlyphZone[] blueValues)
        {
            this.blueValues = blueValues;
        }

        public override void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            privateData.BlueValues = this.blueValues;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcGlyphZonesSize(this.blueValues);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteGlyphZones(stream, this.blueValues);
            stream.WriteByte(6);
        }
    }
}

