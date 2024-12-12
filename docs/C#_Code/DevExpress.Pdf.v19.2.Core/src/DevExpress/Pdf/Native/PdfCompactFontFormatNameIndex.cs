namespace DevExpress.Pdf.Native
{
    using System;
    using System.Text;

    public class PdfCompactFontFormatNameIndex : PdfCompactFontFormatIndex
    {
        private string[] strings;

        public PdfCompactFontFormatNameIndex(params string[] strings)
        {
            this.strings = strings;
        }

        public PdfCompactFontFormatNameIndex(PdfBinaryStream stream) : base(stream)
        {
        }

        protected override int GetObjectLength(int index) => 
            Encoding.UTF8.GetBytes(this.strings[index]).Length;

        protected override void ProcessObject(int index, byte[] objectData)
        {
            this.strings[index] = Encoding.UTF8.GetString(objectData, 0, objectData.Length);
        }

        protected override void ProcessObjectsCount(int objectsCount)
        {
            this.strings = new string[objectsCount];
        }

        protected override void WriteObject(PdfBinaryStream stream, int index)
        {
            stream.WriteArray(Encoding.UTF8.GetBytes(this.strings[index]));
        }

        public string[] Strings =>
            this.strings;

        protected override int ObjectsCount =>
            this.strings.Length;
    }
}

