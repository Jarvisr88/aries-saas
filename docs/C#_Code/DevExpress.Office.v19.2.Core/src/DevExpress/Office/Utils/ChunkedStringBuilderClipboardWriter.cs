namespace DevExpress.Office.Utils
{
    using System;
    using System.Text;

    public class ChunkedStringBuilderClipboardWriter : ChunkedStringBuilderWriter
    {
        private int byteCount;
        private char[] singleChar;

        public ChunkedStringBuilderClipboardWriter(ChunkedStringBuilder stringBuilder) : base(stringBuilder)
        {
            this.singleChar = new char[1];
        }

        public override void Write(char value)
        {
            base.Write(value);
            this.singleChar[0] = value;
            this.byteCount += Encoding.UTF8.GetByteCount(this.singleChar);
        }

        public override void Write(string value)
        {
            base.Write(value);
            this.byteCount += Encoding.UTF8.GetByteCount(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
            this.byteCount += Encoding.UTF8.GetByteCount(buffer, index, count);
        }

        public int ByteCount =>
            this.byteCount;
    }
}

