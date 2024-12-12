namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexPrivateOperator : PdfCompactFontFormatDictIndexOperator, ICompactFontFormatDictIndexOffsetOperator
    {
        public const byte Code = 0x12;
        private readonly int length;
        private int offset;
        private int subrsLength;
        private PdfType1FontPrivateData privateData;

        public PdfCompactFontFormatDictIndexPrivateOperator(PdfType1FontPrivateData privateData)
        {
            this.privateData = privateData;
            PdfCompactFontFormatPrivateDictIndexWriter writer = new PdfCompactFontFormatPrivateDictIndexWriter(privateData);
            this.length = writer.DataLength;
            this.subrsLength = writer.SubrsLength;
        }

        public PdfCompactFontFormatDictIndexPrivateOperator(IList<object> operands)
        {
            if (operands.Count != 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.length = PdfDocumentReader.ConvertToInteger(operands[0]);
            this.offset = PdfDocumentReader.ConvertToInteger(operands[1]);
        }

        void ICompactFontFormatDictIndexOffsetOperator.WriteData(PdfBinaryStream stream)
        {
            new PdfCompactFontFormatPrivateDictIndexWriter(this.privateData).Write(stream);
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.Private = this.ReadData(stream);
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.Private = this.ReadData(stream);
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            (base.GetSize(stringIndex) + CalcIntegerSize(this.length)) + CalcIntegerSize(this.offset);

        private PdfType1FontPrivateData ReadData(PdfBinaryStream stream)
        {
            stream.Position = this.offset;
            byte[] data = stream.ReadArray(this.length);
            stream.Position = this.offset;
            return PdfCompactFontFormatPrivateDictIndexParser.Parse(stream, data);
        }

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.length);
            WriteInteger(stream, this.offset);
            stream.WriteByte(0x12);
        }

        int ICompactFontFormatDictIndexOffsetOperator.Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }

        int ICompactFontFormatDictIndexOffsetOperator.Length =>
            this.length + this.subrsLength;
    }
}

