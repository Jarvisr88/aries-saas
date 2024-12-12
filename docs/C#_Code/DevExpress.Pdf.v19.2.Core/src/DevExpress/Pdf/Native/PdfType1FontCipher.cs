namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfType1FontCipher
    {
        internal const ushort C1 = 0xce6d;
        internal const ushort C2 = 0x58bf;
        private readonly byte[] data;
        private readonly int endPosition;
        private int currentPosition;
        private ushort r;

        protected PdfType1FontCipher(byte[] data)
        {
            this.data = data;
            this.endPosition = data.Length;
            this.currentPosition = 0;
            this.r = (ushort) this.InitialR;
        }

        protected PdfType1FontCipher(byte[] data, int startPosition, int dataLength)
        {
            this.data = data;
            this.endPosition = startPosition + dataLength;
            this.currentPosition = startPosition;
            this.r = (ushort) this.InitialR;
        }

        public byte[] Decrypt()
        {
            List<byte> list = new List<byte>();
            int skipBytesCount = this.SkipBytesCount;
            while (true)
            {
                short num2 = this.DecryptNextChar();
                if (num2 < 0)
                {
                    return list.ToArray();
                }
                if (skipBytesCount > 0)
                {
                    skipBytesCount--;
                    continue;
                }
                list.Add((byte) num2);
            }
        }

        private short DecryptNextChar()
        {
            short num = this.NextChar();
            if (num < 0)
            {
                return num;
            }
            byte num2 = (byte) (num ^ (this.r >> 8));
            this.r = (ushort) (((num + this.r) * 0xce6d) + 0x58bf);
            return num2;
        }

        protected short NextByte()
        {
            if (this.currentPosition >= this.endPosition)
            {
                return -1;
            }
            int currentPosition = this.currentPosition;
            this.currentPosition = currentPosition + 1;
            return this.data[currentPosition];
        }

        protected abstract short NextChar();

        protected int R
        {
            get => 
                this.r;
            set => 
                this.r = (ushort) value;
        }

        protected virtual int SkipBytesCount =>
            0;

        protected abstract int BytesPerResultByte { get; }

        protected abstract int InitialR { get; }
    }
}

