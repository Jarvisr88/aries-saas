namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfIndirectReference
    {
        public const int Generation = 0;
        private int number;
        private long byteOffset = -1L;

        public void CalculateByteOffset(StreamWriter writer)
        {
            writer.Flush();
            this.byteOffset = writer.BaseStream.Position;
        }

        public void WriteToStream(StreamWriter writer)
        {
            writer.Write("{0} {1} R", this.number, 0);
        }

        public int Number
        {
            get => 
                this.number;
            set => 
                this.number = value;
        }

        public long ByteOffset =>
            this.byteOffset;
    }
}

