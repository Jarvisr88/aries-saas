namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfType1FontWordRangeCharset : PdfType1FontCharset
    {
        internal const byte Id = 2;
        private readonly List<Range> ranges = new List<Range>();
        private Dictionary<short, short> cidToGidMapping;

        public PdfType1FontWordRangeCharset(PdfBinaryStream stream, int size)
        {
            while (size > 0)
            {
                short sid = stream.ReadShort();
                short remain = stream.ReadShort();
                if (remain >= size)
                {
                    remain = (short) (size - 1);
                }
                this.ranges.Add(new Range(sid, remain));
                size -= remain + 1;
            }
        }

        public override void Write(PdfBinaryStream stream)
        {
            stream.WriteByte(2);
            foreach (Range range in this.ranges)
            {
                stream.WriteShort(range.Sid);
                stream.WriteShort(range.Remain);
            }
        }

        public override IDictionary<short, short> SidToGidMapping
        {
            get
            {
                if (this.cidToGidMapping == null)
                {
                    this.cidToGidMapping = new Dictionary<short, short>();
                    short num = 1;
                    foreach (Range range in this.ranges)
                    {
                        short sid = range.Sid;
                        short num1 = sid;
                        sid = (short) (num1 + 1);
                        short num4 = num;
                        num = (short) (num4 + 1);
                        this.cidToGidMapping[num1] = num4;
                        for (int i = range.Remain; i > 0; i--)
                        {
                            short num5 = sid;
                            sid = (short) (num5 + 1);
                            short num6 = num;
                            num = (short) (num6 + 1);
                            this.cidToGidMapping[num5] = num6;
                        }
                    }
                }
                return this.cidToGidMapping;
            }
        }

        public override int DataLength =>
            (this.ranges.Count * 4) + 1;

        [StructLayout(LayoutKind.Sequential)]
        private struct Range
        {
            private readonly short sid;
            private readonly short remain;
            public short Sid =>
                this.sid;
            public short Remain =>
                this.remain;
            public Range(short sid, short remain)
            {
                this.sid = sid;
                this.remain = remain;
            }
        }
    }
}

