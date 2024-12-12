namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexVersionOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 0;
        private readonly string version;

        public PdfCompactFontFormatDictIndexVersionOperator(string version)
        {
            this.version = version;
        }

        public PdfCompactFontFormatDictIndexVersionOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.Version = this.version;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.version));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.version));
            stream.WriteByte(0);
        }
    }
}

