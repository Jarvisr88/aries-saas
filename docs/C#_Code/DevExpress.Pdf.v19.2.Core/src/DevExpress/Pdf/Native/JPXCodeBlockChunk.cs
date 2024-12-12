namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXCodeBlockChunk
    {
        private readonly byte codingPassCount;
        private readonly byte[] data;

        public JPXCodeBlockChunk(byte codingPassCount, byte[] data)
        {
            this.codingPassCount = codingPassCount;
            this.data = data;
        }

        public byte CodingPassCount =>
            this.codingPassCount;

        public byte[] Data =>
            this.data;
    }
}

