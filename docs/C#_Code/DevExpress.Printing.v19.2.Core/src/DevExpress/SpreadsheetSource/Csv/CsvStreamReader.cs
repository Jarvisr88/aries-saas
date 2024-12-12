namespace DevExpress.SpreadsheetSource.Csv
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Text;

    public class CsvStreamReader
    {
        private const int defaultBufferSize = 0x400;
        private const int minBufferSize = 0x100;
        private readonly Stream stream;
        private readonly System.Text.Decoder decoder;
        private byte[] byteBuffer;
        private char[] charBuffer;
        private int byteLen;
        private int charLen;
        private int charPos;

        public CsvStreamReader(Stream stream, Encoding encoding) : this(stream, encoding, 0x400)
        {
        }

        public CsvStreamReader(Stream stream, Encoding encoding, int bufferSize)
        {
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(encoding, "encoding");
            bufferSize = Math.Max(bufferSize, 0x100);
            this.stream = stream;
            this.decoder = encoding.GetDecoder();
            this.byteBuffer = new byte[bufferSize];
            int maxCharCount = encoding.GetMaxCharCount(bufferSize);
            this.charBuffer = new char[maxCharCount];
            this.byteLen = 0;
            this.charLen = 0;
            this.charPos = 0;
        }

        public int Peek() => 
            ((this.charPos != this.charLen) || (this.ReadBuffer() != 0)) ? this.charBuffer[this.charPos] : -1;

        public int Read()
        {
            if ((this.charPos == this.charLen) && (this.ReadBuffer() == 0))
            {
                return -1;
            }
            int charPos = this.charPos;
            this.charPos = charPos + 1;
            return this.charBuffer[charPos];
        }

        private int ReadBuffer()
        {
            this.charLen = 0;
            this.charPos = 0;
            this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
            if (this.byteLen > 0)
            {
                this.charLen = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
            }
            return this.charLen;
        }

        public bool EndOfStream =>
            (this.charPos >= this.charLen) ? (this.ReadBuffer() == 0) : false;
    }
}

