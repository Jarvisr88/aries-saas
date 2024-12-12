namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexCharsetOperator : PdfCompactFontFormatDictIndexOperator, ICompactFontFormatDictIndexOffsetOperator
    {
        public const byte Code = 15;
        private PdfType1FontCharset charset;
        private int offset;

        public PdfCompactFontFormatDictIndexCharsetOperator(PdfType1FontCharset charset)
        {
            this.charset = charset;
            this.offset = charset.Offset;
        }

        public PdfCompactFontFormatDictIndexCharsetOperator(IList<object> operands)
        {
            this.offset = GetInteger(operands);
        }

        void ICompactFontFormatDictIndexOffsetOperator.WriteData(PdfBinaryStream stream)
        {
            this.charset.Write(stream);
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            switch (this.offset)
            {
                case 0:
                case 1:
                case 2:
                    this.charset = new PdfType1FontPredefinedCharset((PdfType1FontPredefinedCharsetID) this.offset);
                    break;

                default:
                {
                    IList<byte[]> charStrings = fontProgram.CharStrings;
                    if (charStrings == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    int size = charStrings.Count - 1;
                    if (size < 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    stream.Position = this.offset;
                    this.charset = PdfType1FontCharset.Parse(stream, size);
                    break;
                }
            }
            fontProgram.Charset = this.charset;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.offset);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.offset);
            stream.WriteByte(15);
        }

        int ICompactFontFormatDictIndexOffsetOperator.Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }

        int ICompactFontFormatDictIndexOffsetOperator.Length =>
            this.charset.DataLength;
    }
}

