namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexPostScriptOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 0x15;
        private readonly string postScript;

        public PdfCompactFontFormatDictIndexPostScriptOperator(string postScript)
        {
            this.postScript = postScript;
        }

        public PdfCompactFontFormatDictIndexPostScriptOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.PostScript = this.postScript;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.postScript));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.postScript));
            stream.WriteByte(12);
            stream.WriteByte(0x15);
        }
    }
}

