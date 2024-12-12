namespace DevExpress.Pdf.Native
{
    using System;

    public interface ICompactFontFormatDictIndexOffsetOperator
    {
        int GetSize(PdfCompactFontFormatStringIndex stringIndex);
        void WriteData(PdfBinaryStream stream);

        int Offset { get; set; }

        int Length { get; }
    }
}

