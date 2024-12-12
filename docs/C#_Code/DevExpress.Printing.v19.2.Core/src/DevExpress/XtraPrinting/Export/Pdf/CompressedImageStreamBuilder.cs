namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;

    internal class CompressedImageStreamBuilder : ImageStreamBuilder
    {
        private byte[] prior;
        private byte[] sub;
        private byte[] up;
        private byte[] average;
        private byte[] paeth;
        private HashSet<byte> noneSet;
        private HashSet<byte> subSet;
        private HashSet<byte> upSet;
        private HashSet<byte> averageSet;
        private HashSet<byte> paethSet;

        public CompressedImageStreamBuilder(PixelConverter converter) : base(converter)
        {
            this.noneSet = new HashSet<byte>();
            this.subSet = new HashSet<byte>();
            this.upSet = new HashSet<byte>();
            this.averageSet = new HashSet<byte>();
            this.paethSet = new HashSet<byte>();
        }

        protected override void Initialize(int pdfByteWidth)
        {
            base.Initialize(pdfByteWidth);
            this.prior = new byte[pdfByteWidth];
            this.sub = new byte[pdfByteWidth];
            this.up = new byte[pdfByteWidth];
            this.average = new byte[pdfByteWidth];
            this.paeth = new byte[pdfByteWidth];
        }

        private static int PaethPredictor(int a, int b, int c)
        {
            int num = (a + b) - c;
            int num2 = Math.Abs((int) (num - a));
            int num3 = Math.Abs((int) (num - b));
            int num4 = Math.Abs((int) (num - c));
            return (((num2 > num3) || (num2 > num4)) ? ((num3 > num4) ? c : b) : a);
        }

        protected override void PutLineToStream(PdfStream stream)
        {
            for (int i = 0; i < base.line.Length; i++)
            {
                this.noneSet.Add(base.line[i]);
                byte a = (i < base.pdfBpp) ? ((byte) 0) : base.line[i - base.pdfBpp];
                this.sub[i] = (byte) (base.line[i] - a);
                this.subSet.Add(this.sub[i]);
                this.up[i] = (byte) (base.line[i] - this.prior[i]);
                this.upSet.Add(this.up[i]);
                this.average[i] = (byte) (base.line[i] - ((a + this.prior[i]) / 2));
                this.averageSet.Add(this.average[i]);
                this.paeth[i] = (byte) (base.line[i] - PaethPredictor(a, this.prior[i], (i < base.pdfBpp) ? 0 : this.prior[i - base.pdfBpp]));
                this.paethSet.Add(this.paeth[i]);
            }
            int min = this.noneSet.Count;
            LineTag tag = LineTag.None;
            byte[] result = base.line;
            Action<int, LineTag, byte[]> action = delegate (int value, LineTag tagValue, byte[] lineValue) {
                if (min > value)
                {
                    tag = tagValue;
                    min = value;
                    result = lineValue;
                }
            };
            action(this.subSet.Count, LineTag.Sub, this.sub);
            action(this.upSet.Count, LineTag.Up, this.up);
            action(this.averageSet.Count, LineTag.Average, this.average);
            action(this.paethSet.Count, LineTag.Paeth, this.paeth);
            stream.SetByte((byte) tag);
            stream.SetBytes(result);
            byte[] prior = this.prior;
            this.prior = base.line;
            base.line = prior;
            this.noneSet.Clear();
            this.subSet.Clear();
            this.upSet.Clear();
            this.averageSet.Clear();
            this.paethSet.Clear();
        }

        private enum LineTag : byte
        {
            None = 0,
            Sub = 1,
            Up = 2,
            Average = 3,
            Paeth = 4
        }
    }
}

