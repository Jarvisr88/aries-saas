namespace DevExpress.Office.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.Text;

    public class PreprocessedStream
    {
        private const int maxSignatureLength = 8;
        private static readonly byte[] zipSignature = new byte[] { 80, 0x4b };
        private byte[] buffer;
        private InternalZipFileCollection zipFiles;
        private readonly System.IO.Stream stream;

        public PreprocessedStream(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        public bool CheckSignature(byte[] signature)
        {
            for (int i = 0; i < signature.Length; i++)
            {
                if (this.buffer[i] != signature[i])
                {
                    return false;
                }
            }
            return true;
        }

        private byte[] GetSignatureBuffer()
        {
            this.Stream.Seek(0L, SeekOrigin.Begin);
            byte[] buffer = new byte[8];
            this.Stream.Read(buffer, 0, buffer.Length);
            this.Stream.Seek(0L, SeekOrigin.Begin);
            return buffer;
        }

        private void PrepareBuffer()
        {
            this.buffer = this.GetSignatureBuffer();
        }

        private void PrepareZipFiles()
        {
            if (this.CheckSignature(zipSignature))
            {
                this.stream.Seek(0L, SeekOrigin.Begin);
                this.zipFiles = InternalZipArchive.Open(this.Stream);
                this.stream.Seek(0L, SeekOrigin.Begin);
            }
        }

        public void Process()
        {
            this.PrepareBuffer();
            this.PrepareZipFiles();
        }

        public string ReadEntryContent(InternalZipFile entry, Encoding encoding)
        {
            using (entry.CreateDecompressionStream())
            {
                byte[] buffer = new byte[Math.Min(0x400L, entry.UncompressedSize)];
                int count = entry.FileDataStream.Read(buffer, 0, buffer.Length);
                return ((count > 0) ? encoding.GetString(buffer, 0, count) : string.Empty);
            }
        }

        public void Reset()
        {
            this.PrepareZipFiles();
            this.Stream.Seek(0L, SeekOrigin.Begin);
        }

        public bool ValidateStream() => 
            (this.stream != null) ? ((this.stream.Length != 0) && this.stream.CanSeek) : false;

        public byte[] Buffer =>
            this.buffer;

        public InternalZipFileCollection ZipFiles =>
            this.zipFiles;

        public System.IO.Stream Stream =>
            this.stream;
    }
}

