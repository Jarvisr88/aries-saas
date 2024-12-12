namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatBinaryIndex : PdfCompactFontFormatIndex
    {
        private IList<byte[]> data;

        public PdfCompactFontFormatBinaryIndex(PdfBinaryStream stream) : base(stream)
        {
        }

        public PdfCompactFontFormatBinaryIndex(IList<byte[]> data)
        {
            this.data = data;
        }

        protected override int GetObjectLength(int index) => 
            this.data[index].Length;

        protected override void ProcessObject(int index, byte[] objectData)
        {
            this.data[index] = objectData;
        }

        protected override void ProcessObjectsCount(int objectsCount)
        {
            this.data = new byte[objectsCount][];
        }

        protected override void WriteObject(PdfBinaryStream stream, int index)
        {
            stream.WriteArray(this.data[index]);
        }

        public IList<byte[]> Data =>
            this.data;

        protected override int ObjectsCount =>
            this.data.Count;
    }
}

