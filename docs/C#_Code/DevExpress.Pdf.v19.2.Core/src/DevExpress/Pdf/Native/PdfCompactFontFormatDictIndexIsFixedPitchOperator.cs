namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexIsFixedPitchOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 1;
        private readonly bool isFixedPitch;

        public PdfCompactFontFormatDictIndexIsFixedPitchOperator(bool isFixedPitch)
        {
            this.isFixedPitch = isFixedPitch;
        }

        public PdfCompactFontFormatDictIndexIsFixedPitchOperator(IList<object> operands) : this(GetBoolean(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.IsFixedPitch = this.isFixedPitch;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + 1;

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteBoolean(stream, this.isFixedPitch);
            stream.WriteByte(12);
            stream.WriteByte(1);
        }
    }
}

