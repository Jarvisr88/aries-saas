namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Text;

    public class PdfName : IPdfWritableObject
    {
        private readonly string name;

        public PdfName(string name)
        {
            this.name = name;
        }

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            this.Write(stream);
        }

        internal void Write(PdfDocumentStream stream)
        {
            stream.WriteByte(0x2f);
            foreach (char ch in this.name)
            {
                if (((byte) ch) != ch)
                {
                    foreach (byte num3 in Encoding.UTF8.GetBytes(this.name))
                    {
                        WriteChar(stream, (char) num3);
                    }
                    return;
                }
            }
            foreach (char ch2 in this.name)
            {
                WriteChar(stream, ch2);
            }
        }

        private static void WriteChar(PdfDocumentStream stream, char c)
        {
            if (char.IsLetterOrDigit(c) && ((c != '#') && ((c > '!') && (c < '~'))))
            {
                stream.WriteByte((byte) c);
            }
            else
            {
                stream.WriteString($"#{(byte) c:X2}");
            }
        }

        public string Name =>
            this.name;
    }
}

