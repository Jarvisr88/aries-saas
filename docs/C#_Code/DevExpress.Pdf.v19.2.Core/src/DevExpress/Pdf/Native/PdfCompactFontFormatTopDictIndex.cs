namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfCompactFontFormatTopDictIndex : PdfCompactFontFormatIndex
    {
        private byte[] objectData;

        public PdfCompactFontFormatTopDictIndex(byte[] objectData)
        {
            this.objectData = objectData;
        }

        public PdfCompactFontFormatTopDictIndex(PdfBinaryStream stream) : base(stream)
        {
        }

        protected override int GetObjectLength(int index) => 
            this.objectData.Length;

        protected override void ProcessObject(int index, byte[] objectData)
        {
            this.objectData = objectData;
        }

        protected override void ProcessObjectsCount(int objectsCount)
        {
            if (objectsCount != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected override void WriteObject(PdfBinaryStream stream, int index)
        {
            stream.WriteArray(this.objectData);
        }

        public byte[] ObjectData =>
            this.objectData;

        protected override int ObjectsCount =>
            1;
    }
}

