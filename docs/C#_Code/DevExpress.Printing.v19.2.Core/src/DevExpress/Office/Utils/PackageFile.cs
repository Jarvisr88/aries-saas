namespace DevExpress.Office.Utils
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class PackageFile
    {
        private string fileName;
        private System.IO.Stream stream;
        private int streamLength;
        private MemoryStream seekableStream;

        public PackageFile(string fileName, System.IO.Stream stream, int streamLength)
        {
            Guard.ArgumentNotNull(fileName, "fileName");
            Guard.ArgumentNotNull(stream, "stream");
            this.fileName = fileName;
            this.stream = stream;
            this.streamLength = streamLength;
        }

        public string FileName =>
            this.fileName;

        public System.IO.Stream Stream =>
            this.stream;

        public int StreamLength
        {
            get => 
                this.streamLength;
            set => 
                this.streamLength = value;
        }

        public MemoryStream SeekableStream
        {
            get
            {
                if (this.seekableStream == null)
                {
                    byte[] buffer = new byte[this.StreamLength];
                    this.Stream.Read(buffer, 0, buffer.Length);
                    this.seekableStream = new MemoryStream(buffer);
                }
                return this.seekableStream;
            }
        }
    }
}

