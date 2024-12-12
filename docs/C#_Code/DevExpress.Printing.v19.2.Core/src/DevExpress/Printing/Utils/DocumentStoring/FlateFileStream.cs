namespace DevExpress.Printing.Utils.DocumentStoring
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.InteropServices;

    public class FlateFileStream : MemoryStream
    {
        private readonly string fileName;
        private bool modified;
        private bool disposed;

        public FlateFileStream(string fileName, int capacity = 100) : base(capacity)
        {
            this.fileName = fileName;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.modified && !this.disposed)
            {
                using (Stream stream = File.Create(this.fileName))
                {
                    using (DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Compress))
                    {
                        this.Position = 0L;
                        base.CopyTo(stream2);
                    }
                }
            }
            this.disposed = true;
            base.Dispose(disposing);
        }

        public static FlateFileStream Open(string fileName)
        {
            FlateFileStream stream4;
            using (Stream stream = File.OpenRead(fileName))
            {
                using (DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Decompress))
                {
                    FlateFileStream destination = new FlateFileStream(fileName, 100);
                    stream2.CopyTo(destination);
                    destination.Position = 0L;
                    stream4 = destination;
                }
            }
            return stream4;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
            this.modified = true;
        }

        public override void WriteByte(byte value)
        {
            base.WriteByte(value);
            this.modified = true;
        }
    }
}

