namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexUIDBaseOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x23;
        private readonly int uidBase;

        public PdfCompactFontFormatDictIndexUIDBaseOperator(IList<object> operands) : this(GetInteger(operands))
        {
        }

        public PdfCompactFontFormatDictIndexUIDBaseOperator(int uidBase)
        {
            this.uidBase = uidBase;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            ToCIDFontProgram(fontProgram).UIDBase = new int?(this.uidBase);
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.uidBase);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.uidBase);
            stream.WriteByte(12);
            stream.WriteByte(0x23);
        }
    }
}

