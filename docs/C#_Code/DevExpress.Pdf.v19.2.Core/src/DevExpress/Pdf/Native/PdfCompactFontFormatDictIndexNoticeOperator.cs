namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexNoticeOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 1;
        private readonly string notice;

        public PdfCompactFontFormatDictIndexNoticeOperator(string notice)
        {
            this.notice = notice;
        }

        public PdfCompactFontFormatDictIndexNoticeOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands) : this(stringIndex.GetString(operands))
        {
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontInfo.Notice = this.notice;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.notice));

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.notice));
            stream.WriteByte(1);
        }
    }
}

