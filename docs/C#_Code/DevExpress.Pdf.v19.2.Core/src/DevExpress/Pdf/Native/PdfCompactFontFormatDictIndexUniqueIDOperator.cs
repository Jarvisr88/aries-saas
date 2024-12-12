namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexUniqueIDOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 13;
        private readonly int uniqueID;

        public PdfCompactFontFormatDictIndexUniqueIDOperator(IList<object> operands) : this(GetInteger(operands))
        {
        }

        public PdfCompactFontFormatDictIndexUniqueIDOperator(int uniqueID)
        {
            this.uniqueID = uniqueID;
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.UniqueID = this.uniqueID;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.UniqueID = this.uniqueID;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.uniqueID);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.uniqueID);
            stream.WriteByte(13);
        }
    }
}

