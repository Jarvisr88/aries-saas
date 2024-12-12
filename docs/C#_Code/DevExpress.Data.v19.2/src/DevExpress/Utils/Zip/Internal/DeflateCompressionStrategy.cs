namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;

    public class DeflateCompressionStrategy : ICompressionStrategy
    {
        private int crc32;

        public void Compress(Stream sourceStream, Stream targetStream, IZipComplexOperationProgress progress)
        {
            CopyProgressHandler copyDelegate = null;
            if (progress != null)
            {
                long position = 0L;
                try
                {
                    position = sourceStream.Position;
                }
                catch (NotSupportedException)
                {
                }
                ZipCopyStreamOperationProgress progressItem = new ZipCopyStreamOperationProgress(sourceStream.Length - position);
                progress.AddOperationProgress(progressItem);
                copyDelegate = new CopyProgressHandler(progressItem.CopyHandler);
            }
            Crc32Stream stream = new Crc32Stream(sourceStream);
            using (Stream stream2 = this.CreateDeflateStream(targetStream))
            {
                StreamUtils.CopyStream(stream, stream2, copyDelegate);
            }
            this.crc32 = stream.ReadCheckSum;
        }

        private Stream CreateDeflateStream(Stream stream) => 
            new DeflateStream(stream, CompressionMode.Compress, true);

        public short GetGeneralPurposeBitFlag() => 
            0;

        public void PrepareExtraFields(IZipExtraFieldCollection extraFields)
        {
        }

        public DevExpress.Utils.Zip.Internal.CompressionMethod CompressionMethod =>
            DevExpress.Utils.Zip.Internal.CompressionMethod.Deflate;

        public int Crc32 =>
            this.crc32;
    }
}

