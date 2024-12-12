namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class PdfType1FontRangeEncoding : PdfType1FontCustomEncoding
    {
        internal const byte Id = 1;
        private readonly Range[] ranges;

        public PdfType1FontRangeEncoding(PdfBinaryStream stream)
        {
            int num = stream.ReadByte();
            this.ranges = new Range[num];
            for (int i = 0; i < num; i++)
            {
                this.ranges[i] = new Range(stream.ReadByte(), stream.ReadByte());
            }
        }

        protected override void WriteEncodingData(PdfBinaryStream stream)
        {
            stream.WriteByte((byte) this.ranges.Length);
            foreach (Range range in this.ranges)
            {
                stream.WriteByte(range.Start);
                stream.WriteByte(range.Remain);
            }
        }

        protected override byte EncodingID =>
            1;

        protected override short[] CodeToGIDMapping
        {
            get
            {
                short[] mapping = new short[0x100];
                short num = 1;
                Range[] ranges = this.ranges;
                int index = 0;
                while (index < ranges.Length)
                {
                    Range range = ranges[index];
                    byte start = range.Start;
                    short remain = range.Remain;
                    while (true)
                    {
                        if (remain < 0)
                        {
                            index++;
                            break;
                        }
                        byte code = start;
                        start = (byte) (code + 1);
                        short gid = num;
                        num = (short) (gid + 1);
                        FillEntry(mapping, code, gid);
                        remain = (short) (remain - 1);
                    }
                }
                return mapping;
            }
        }

        public override int DataLength =>
            ((this.ranges.Length * 2) + 2) + base.SupplementDataLength;

        [StructLayout(LayoutKind.Sequential)]
        private struct Range
        {
            private readonly byte start;
            private readonly byte remain;
            public byte Start =>
                this.start;
            public byte Remain =>
                this.remain;
            public Range(byte start, byte remain)
            {
                this.start = start;
                this.remain = remain;
            }
        }
    }
}

