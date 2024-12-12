namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class PdfType1FontCIDGlyphGroupRangeSelector : PdfType1FontCIDGlyphGroupSelector
    {
        internal const int Format = 3;
        private readonly Range[] ranges;
        private readonly ushort sentinel;

        internal PdfType1FontCIDGlyphGroupRangeSelector(PdfBinaryStream stream, int cidCount)
        {
            short num = stream.ReadShort();
            if (num == 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num2 = -1;
            this.ranges = new Range[num];
            for (int i = 0; i < num; i++)
            {
                ushort first = (ushort) stream.ReadShort();
                Range range = new Range(first, stream.ReadByte());
                if (first <= num2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                num2 = first;
                this.ranges[i] = range;
            }
            this.sentinel = (ushort) stream.ReadShort();
            if ((this.sentinel != cidCount) || ((this.sentinel <= num2) || (this.ranges[0].First != 0)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        public override void Write(PdfBinaryStream stream)
        {
            stream.WriteByte(3);
            stream.WriteShort((short) this.ranges.Length);
            foreach (Range range in this.ranges)
            {
                stream.WriteShort((short) range.First);
                stream.WriteByte(range.GlyphGroupIndex);
            }
            stream.WriteShort((short) this.sentinel);
        }

        public override byte[] GlyphGroupIndices
        {
            get
            {
                byte[] buffer = new byte[this.sentinel];
                int length = this.ranges.Length;
                ushort index = 0;
                byte glyphGroupIndex = this.ranges[0].GlyphGroupIndex;
                int num4 = 1;
                while (num4 < length)
                {
                    Range range = this.ranges[num4];
                    ushort first = range.First;
                    while (true)
                    {
                        if (index >= first)
                        {
                            glyphGroupIndex = range.GlyphGroupIndex;
                            num4++;
                            break;
                        }
                        buffer[index] = glyphGroupIndex;
                        index = (ushort) (index + 1);
                    }
                }
                while (index < this.sentinel)
                {
                    buffer[index] = glyphGroupIndex;
                    index = (ushort) (index + 1);
                }
                return buffer;
            }
        }

        public override int DataLength =>
            (this.ranges.Length * 3) + 5;

        [StructLayout(LayoutKind.Sequential)]
        private struct Range
        {
            private readonly ushort first;
            private readonly byte glyphGroupIndex;
            public ushort First =>
                this.first;
            public byte GlyphGroupIndex =>
                this.glyphGroupIndex;
            public Range(ushort first, byte glyphGroupIndex)
            {
                this.first = first;
                this.glyphGroupIndex = glyphGroupIndex;
            }
        }
    }
}

