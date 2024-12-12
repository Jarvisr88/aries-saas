namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;
    using System.Text;

    public class ChunkedStringBuilderWriter : TextWriter
    {
        private static readonly System.Text.Encoding unicodeEncoding = new UnicodeEncoding(false, false);
        private readonly ChunkedStringBuilder stringBuilder;
        private System.Text.Encoding encoding = unicodeEncoding;
        private bool isOpen = true;

        public ChunkedStringBuilderWriter(ChunkedStringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
        }

        public override void Close()
        {
            this.Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            this.isOpen = false;
            base.Dispose(disposing);
        }

        public virtual ChunkedStringBuilder GetStringBuilder() => 
            this.stringBuilder;

        public void SetEncoding(System.Text.Encoding encoding)
        {
            this.encoding = encoding;
        }

        private void ThrowWriterClosedException()
        {
            Exceptions.ThrowInvalidOperationException("writer is closed");
        }

        public override string ToString() => 
            this.stringBuilder.ToString();

        public override void Write(char value)
        {
            if (this.isOpen)
            {
                this.stringBuilder.Append(value);
            }
            else
            {
                this.ThrowWriterClosedException();
            }
        }

        public override void Write(string value)
        {
            if (this.isOpen)
            {
                this.stringBuilder.Append(value);
            }
            else
            {
                this.ThrowWriterClosedException();
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            if (this.isOpen)
            {
                this.stringBuilder.Append(buffer, index, count);
            }
            else
            {
                this.ThrowWriterClosedException();
            }
        }

        public override System.Text.Encoding Encoding =>
            this.encoding;
    }
}

