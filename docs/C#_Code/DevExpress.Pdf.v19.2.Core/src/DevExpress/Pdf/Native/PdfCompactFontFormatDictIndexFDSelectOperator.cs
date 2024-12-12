namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexFDSelectOperator : PdfCompactFontFormatDictIndexTwoByteOperator, ICompactFontFormatDictIndexOffsetOperator
    {
        public const byte Code = 0x25;
        private PdfType1FontCIDGlyphGroupSelector selector;
        private int offset;

        public PdfCompactFontFormatDictIndexFDSelectOperator(PdfType1FontCIDGlyphGroupSelector selector)
        {
            this.selector = selector;
        }

        public PdfCompactFontFormatDictIndexFDSelectOperator(IList<object> operands)
        {
            this.offset = GetInteger(operands);
        }

        void ICompactFontFormatDictIndexOffsetOperator.WriteData(PdfBinaryStream stream)
        {
            this.selector.Write(stream);
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            stream.Position = this.offset;
            this.selector = PdfType1FontCIDGlyphGroupSelector.Parse(stream, fontProgram.CharStrings.Count);
            ToCIDFontProgram(fontProgram).GlyphGroupSelector = this.selector;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.offset);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.offset);
            stream.WriteByte(12);
            stream.WriteByte(0x25);
        }

        int ICompactFontFormatDictIndexOffsetOperator.Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }

        int ICompactFontFormatDictIndexOffsetOperator.Length =>
            this.selector.DataLength;
    }
}

