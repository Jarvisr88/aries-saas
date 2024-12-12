namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class PdfStreamingDataStream : PdfDataStream
    {
        private const int tailLength = 10;
        private readonly Stream stream;
        private readonly long streamOffset;
        private byte[] tail;
        private int currentTailPosition;
        private long streamPosition;

        public PdfStreamingDataStream(Stream stream, long length) : base(length)
        {
            this.tail = new byte[10];
            this.currentTailPosition = -1;
            this.stream = stream;
            this.streamOffset = stream.Position;
            this.streamPosition = this.streamOffset;
        }

        public override int Read(byte[] buffer, int offset, int length)
        {
            long num = this.streamOffset + this.Position;
            length = Math.Min(length, (int) (this.stream.Length - num));
            this.stream.Position = num;
            this.stream.Read(buffer, offset, length);
            this.streamPosition = num + length;
            return length;
        }

        public override void Synchronize()
        {
            if (this.streamPosition != this.stream.Position)
            {
                this.stream.Position = this.streamPosition;
            }
        }

        public override int CurrentByte
        {
            get
            {
                if (!this.CanRead)
                {
                    return -1;
                }
                long num = this.streamOffset + this.Position;
                int num2 = (int) (this.streamPosition - num);
                if (num2 != 0)
                {
                    if ((num2 >= 0) && (num2 < 10))
                    {
                        return this.tail[(((10 + this.currentTailPosition) - num2) + 1) % 10];
                    }
                    this.streamPosition = num;
                    this.stream.Position = num;
                }
                this.streamPosition += 1L;
                byte num3 = (byte) this.stream.ReadByte();
                int index = this.currentTailPosition + 1;
                this.currentTailPosition = index;
                this.tail[index] = num3;
                if (this.currentTailPosition == 9)
                {
                    this.currentTailPosition = -1;
                }
                return num3;
            }
        }
    }
}

