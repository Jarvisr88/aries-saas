namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfCompactFontFormatDictIndexTwoByteOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Marker = 12;

        protected PdfCompactFontFormatDictIndexTwoByteOperator()
        {
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            2;
    }
}

