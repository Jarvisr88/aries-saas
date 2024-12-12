namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexFDArrayOperator : PdfCompactFontFormatDictIndexTwoByteOperator, ICompactFontFormatDictIndexOffsetOperator
    {
        public const byte Code = 0x24;
        private PdfType1FontCIDGlyphGroupData[] glyphGroupData;
        private PdfCompactFontFormatStringIndex stringIndex;
        private PdfCompactFontFormatCIDGlyphGroupDataWriter[] writers;
        private int offset;

        public PdfCompactFontFormatDictIndexFDArrayOperator(IList<object> operands)
        {
            this.offset = GetInteger(operands);
        }

        public PdfCompactFontFormatDictIndexFDArrayOperator(PdfType1FontCIDGlyphGroupData[] glyphGroupData, PdfCompactFontFormatStringIndex stringIndex)
        {
            this.glyphGroupData = glyphGroupData;
            this.stringIndex = stringIndex;
        }

        private void CreateWritersIfNeeded()
        {
            if (this.writers == null)
            {
                int length = this.glyphGroupData.Length;
                this.writers = new PdfCompactFontFormatCIDGlyphGroupDataWriter[length];
                for (int i = 0; i < length; i++)
                {
                    this.writers[i] = new PdfCompactFontFormatCIDGlyphGroupDataWriter(this.glyphGroupData[i], this.stringIndex);
                }
            }
        }

        void ICompactFontFormatDictIndexOffsetOperator.WriteData(PdfBinaryStream stream)
        {
            this.CreateWritersIfNeeded();
            int length = this.writers.Length;
            byte[][] data = new byte[length][];
            for (int i = 0; i < length; i++)
            {
                using (PdfBinaryStream stream2 = new PdfBinaryStream())
                {
                    this.writers[i].Write(stream2);
                    data[i] = stream2.Data;
                }
            }
            new PdfCompactFontFormatBinaryIndex(data).Write(stream);
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            this.stringIndex = fontProgram.StringIndex;
            PdfType1FontCompactCIDFontProgram program = ToCIDFontProgram(fontProgram);
            stream.Position = this.offset;
            IList<byte[]> data = new PdfCompactFontFormatBinaryIndex(stream).Data;
            int count = data.Count;
            this.glyphGroupData = new PdfType1FontCIDGlyphGroupData[count];
            for (int i = 0; i < count; i++)
            {
                this.glyphGroupData[i] = PdfCompactFontFormatTopDictIndexParser.Parse(stream, this.stringIndex, data[i]);
            }
            program.GlyphGroupData = this.glyphGroupData;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            base.GetSize(stringIndex) + CalcIntegerSize(this.offset);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, this.offset);
            stream.WriteByte(12);
            stream.WriteByte(0x24);
        }

        public IList<ICompactFontFormatDictIndexOffsetOperator> OffsetOperators
        {
            get
            {
                this.CreateWritersIfNeeded();
                List<ICompactFontFormatDictIndexOffsetOperator> list = new List<ICompactFontFormatDictIndexOffsetOperator>(this.writers.Length);
                foreach (PdfCompactFontFormatCIDGlyphGroupDataWriter writer in this.writers)
                {
                    PdfCompactFontFormatDictIndexPrivateOperator privateOperator = writer.PrivateOperator;
                    if (privateOperator != null)
                    {
                        list.Add(privateOperator);
                    }
                }
                return list;
            }
        }

        int ICompactFontFormatDictIndexOffsetOperator.Offset
        {
            get => 
                this.offset;
            set => 
                this.offset = value;
        }

        int ICompactFontFormatDictIndexOffsetOperator.Length
        {
            get
            {
                this.CreateWritersIfNeeded();
                int num = ((this.writers.Length + 1) * 4) + 3;
                foreach (PdfCompactFontFormatCIDGlyphGroupDataWriter writer in this.writers)
                {
                    num += writer.DataSize;
                }
                return num;
            }
        }
    }
}

