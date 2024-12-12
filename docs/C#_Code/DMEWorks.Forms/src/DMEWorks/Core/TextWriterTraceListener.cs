namespace DMEWorks.Core
{
    using System;
    using System.IO;
    using System.Text;

    public class TextWriterTraceListener : BaseTraceListener
    {
        private TextWriter writer;

        public TextWriterTraceListener()
        {
        }

        public TextWriterTraceListener(Stream stream) : this(stream, string.Empty)
        {
        }

        public TextWriterTraceListener(TextWriter writer) : this(writer, string.Empty)
        {
        }

        public TextWriterTraceListener(string fileName) : this(fileName, string.Empty)
        {
        }

        public TextWriterTraceListener(Stream stream, string name) : base(name)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this.writer = new StreamWriter(stream);
        }

        public TextWriterTraceListener(TextWriter writer, string name) : base(name)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            this.writer = writer;
        }

        public TextWriterTraceListener(string fileName, string name) : base(name)
        {
            Encoding encodingWithFallback = GetEncodingWithFallback(new UTF8Encoding(false));
            this.writer = new StreamWriter(fileName, true, encodingWithFallback, 0x1000);
        }

        public override void Close()
        {
            if (this.writer != null)
            {
                this.writer.Close();
            }
            this.writer = null;
        }

        public override void Flush()
        {
            if (this.writer != null)
            {
                this.writer.Flush();
            }
        }

        private static Encoding GetEncodingWithFallback(Encoding encoding)
        {
            Encoding encoding1 = (Encoding) encoding.Clone();
            encoding1.EncoderFallback = EncoderFallback.ReplacementFallback;
            encoding1.DecoderFallback = DecoderFallback.ReplacementFallback;
            return encoding1;
        }

        public override void Write(string message)
        {
            if (this.writer != null)
            {
                if (base.NeedIndent)
                {
                    this.WriteIndent();
                }
                this.writer.Write(message);
            }
        }

        public override void WriteLine(string message)
        {
            if (this.writer != null)
            {
                if (base.NeedIndent)
                {
                    this.WriteIndent();
                }
                this.writer.WriteLine(message);
                base.NeedIndent = true;
            }
        }

        public TextWriter Writer =>
            this.writer;
    }
}

