namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;

    internal class UseCompressedStreamCompressionStrategy : ICompressionStrategy
    {
        private CompressedStream stream;

        public UseCompressedStreamCompressionStrategy(CompressedStream stream)
        {
            this.stream = stream;
        }

        public void Compress(Stream sourceStream, Stream targetStream, IZipComplexOperationProgress progress)
        {
            CopyProgressHandler copyDelegate = null;
            if (progress != null)
            {
                ZipCopyStreamOperationProgress progress2 = new ZipCopyStreamOperationProgress(sourceStream.Length - sourceStream.Position);
                progress.AddOperationProgress(progress);
                copyDelegate = new CopyProgressHandler(progress2.CopyHandler);
            }
            StreamUtils.CopyStream(sourceStream, targetStream, copyDelegate);
        }

        public short GetGeneralPurposeBitFlag() => 
            0;

        public void PrepareExtraFields(IZipExtraFieldCollection extraFields)
        {
        }

        public DevExpress.Utils.Zip.Internal.CompressionMethod CompressionMethod =>
            DevExpress.Utils.Zip.Internal.CompressionMethod.Deflate;

        public int Crc32 =>
            this.stream.Crc32;
    }
}

