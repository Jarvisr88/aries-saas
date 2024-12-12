namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexEncodingOperator : PdfCompactFontFormatDictIndexOperator, ICompactFontFormatDictIndexOffsetOperator
    {
        public const byte Code = 0x10;
        private PdfType1FontEncoding encoding;
        private int offset;

        public PdfCompactFontFormatDictIndexEncodingOperator(PdfType1FontEncoding encoding)
        {
            this.encoding = encoding;
            this.offset = encoding.Offset;
        }

        public PdfCompactFontFormatDictIndexEncodingOperator(IList<object> operands)
        {
            this.offset = GetInteger(operands);
        }

        void ICompactFontFormatDictIndexOffsetOperator.WriteData(PdfBinaryStream stream)
        {
            PdfType1FontCustomEncoding encoding = this.encoding as PdfType1FontCustomEncoding;
            if (encoding != null)
            {
                encoding.Write(stream);
            }
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            PdfType1FontEncoding encoding;
            int offset = this.offset;
            if (offset == 0)
            {
                encoding = new PdfType1FontPredefinedEncoding(PdfType1FontPredefinedEncodingID.StandardEncoding);
            }
            else if (offset == 1)
            {
                encoding = new PdfType1FontPredefinedEncoding(PdfType1FontPredefinedEncodingID.ExpertEncoding);
            }
            else
            {
                stream.Position = this.offset;
                encoding = PdfType1FontCustomEncoding.Parse(stream);
            }
            fontProgram.Encoding = encoding;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.offset);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.offset);
            stream.WriteByte(0x10);
        }

        int ICompactFontFormatDictIndexOffsetOperator.Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }

        int ICompactFontFormatDictIndexOffsetOperator.Length =>
            this.encoding.DataLength;
    }
}

