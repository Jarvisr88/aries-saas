namespace DevExpress.Utils.Zip
{
    using System;
    using System.IO;

    public class CompressedStream
    {
        private int crc32;
        private int uncompressedSize;
        private System.IO.Stream stream;

        public int Crc32
        {
            get => 
                this.crc32;
            set => 
                this.crc32 = value;
        }

        public int UncompressedSize
        {
            get => 
                this.uncompressedSize;
            set => 
                this.uncompressedSize = value;
        }

        public System.IO.Stream Stream
        {
            get => 
                this.stream;
            set => 
                this.stream = value;
        }
    }
}

