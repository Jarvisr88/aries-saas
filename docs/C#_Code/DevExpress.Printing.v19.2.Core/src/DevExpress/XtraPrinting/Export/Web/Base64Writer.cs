namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.IO;
    using System.Text;

    public class Base64Writer : TextWriter
    {
        private DXHtmlTextWriter writer;
        private char[] buffer;
        private System.Text.Encoding encoding;
        private int size;
        private bool haveWrittenPreamble;

        public Base64Writer(DXHtmlTextWriter writer) : this(writer, true)
        {
        }

        public Base64Writer(DXHtmlTextWriter writer, bool writePreamble)
        {
            this.writer = writer;
            this.buffer = new char[0x2004];
            this.encoding = new UTF8Encoding(writePreamble, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.writer != null))
            {
                this.Flush(true);
                this.writer = null;
            }
            base.Dispose(disposing);
        }

        public override void Flush()
        {
            this.Flush(false);
        }

        private void Flush(bool end)
        {
            this.WritePreamble();
            for (int i = this.size; i != 0; i--)
            {
                byte[] inArray = this.encoding.GetBytes(this.buffer, 0, i);
                if (end || ((inArray.Length % 3) == 0))
                {
                    this.writer.Write(Convert.ToBase64String(inArray));
                    this.writer.Flush();
                    this.size -= i;
                    if (this.size != 0)
                    {
                        Array.Copy(this.buffer, i, this.buffer, 0, this.size);
                    }
                    return;
                }
            }
        }

        public override void Write(char value)
        {
            if (this.size >= this.buffer.Length)
            {
                this.Flush();
            }
            this.buffer[this.size] = value;
            this.size++;
        }

        private void WritePreamble()
        {
            if (!this.haveWrittenPreamble)
            {
                this.writer.Write(Convert.ToBase64String(this.Encoding.GetPreamble()));
                this.haveWrittenPreamble = true;
            }
        }

        public override System.Text.Encoding Encoding =>
            this.encoding;
    }
}

