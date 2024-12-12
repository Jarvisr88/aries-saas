namespace DevExpress.DXBinding.Native
{
    using System;
    using System.IO;

    internal class Buffer
    {
        public const int EOF = 0x10000;
        private const int MIN_BUFFER_LENGTH = 0x400;
        private const int MAX_BUFFER_LENGTH = 0x10000;
        private byte[] buf;
        private int bufStart;
        private int bufLen;
        private int fileLen;
        private int bufPos;
        private Stream stream;
        private bool isUserStream;

        protected Buffer(DevExpress.DXBinding.Native.Buffer b)
        {
            this.buf = b.buf;
            this.bufStart = b.bufStart;
            this.bufLen = b.bufLen;
            this.fileLen = b.fileLen;
            this.bufPos = b.bufPos;
            this.stream = b.stream;
            b.stream = null;
            this.isUserStream = b.isUserStream;
        }

        public Buffer(Stream s, bool isUserStream)
        {
            this.stream = s;
            this.isUserStream = isUserStream;
            if (this.stream.CanSeek)
            {
                this.fileLen = (int) this.stream.Length;
                this.bufLen = Math.Min(this.fileLen, 0x10000);
                this.bufStart = 0x7fffffff;
            }
            else
            {
                int num1 = this.bufStart = 0;
                this.fileLen = this.bufLen = num1;
            }
            this.buf = new byte[(this.bufLen > 0) ? this.bufLen : 0x400];
            if (this.fileLen > 0)
            {
                this.Pos = 0;
            }
            else
            {
                this.bufPos = 0;
            }
            if ((this.bufLen == this.fileLen) && this.stream.CanSeek)
            {
                this.Close();
            }
        }

        protected void Close()
        {
            if (!this.isUserStream && (this.stream != null))
            {
                this.stream.Close();
                this.stream = null;
            }
        }

        ~Buffer()
        {
            this.Close();
        }

        public string GetString(int beg, int end)
        {
            int length = 0;
            char[] chArray = new char[end - beg];
            int pos = this.Pos;
            this.Pos = beg;
            while (this.Pos < end)
            {
                chArray[length++] = (char) this.Read();
            }
            this.Pos = pos;
            return new string(chArray, 0, length);
        }

        public int Peek()
        {
            int pos = this.Pos;
            int num2 = this.Read();
            this.Pos = pos;
            return num2;
        }

        public virtual int Read()
        {
            int bufPos;
            if (this.bufPos < this.bufLen)
            {
                bufPos = this.bufPos;
                this.bufPos = bufPos + 1;
                return this.buf[bufPos];
            }
            if (this.Pos < this.fileLen)
            {
                this.Pos = this.Pos;
                bufPos = this.bufPos;
                this.bufPos = bufPos + 1;
                return this.buf[bufPos];
            }
            if ((this.stream == null) || (this.stream.CanSeek || (this.ReadNextStreamChunk() <= 0)))
            {
                return 0x10000;
            }
            bufPos = this.bufPos;
            this.bufPos = bufPos + 1;
            return this.buf[bufPos];
        }

        private int ReadNextStreamChunk()
        {
            int count = this.buf.Length - this.bufLen;
            if (count == 0)
            {
                byte[] destinationArray = new byte[this.bufLen * 2];
                Array.Copy(this.buf, destinationArray, this.bufLen);
                this.buf = destinationArray;
                count = this.bufLen;
            }
            int num2 = this.stream.Read(this.buf, this.bufLen, count);
            if (num2 <= 0)
            {
                return 0;
            }
            this.fileLen = this.bufLen += num2;
            return num2;
        }

        public int Pos
        {
            get => 
                this.bufPos + this.bufStart;
            set
            {
                if ((value >= this.fileLen) && ((this.stream != null) && !this.stream.CanSeek))
                {
                    while ((value >= this.fileLen) && (this.ReadNextStreamChunk() > 0))
                    {
                    }
                }
                if ((value < 0) || (value > this.fileLen))
                {
                    throw new FatalError("buffer out of bounds access, position: " + value);
                }
                if ((value >= this.bufStart) && (value < (this.bufStart + this.bufLen)))
                {
                    this.bufPos = value - this.bufStart;
                }
                else if (this.stream == null)
                {
                    this.bufPos = this.fileLen - this.bufStart;
                }
                else
                {
                    this.stream.Seek((long) value, SeekOrigin.Begin);
                    this.bufLen = this.stream.Read(this.buf, 0, this.buf.Length);
                    this.bufStart = value;
                    this.bufPos = 0;
                }
            }
        }
    }
}

