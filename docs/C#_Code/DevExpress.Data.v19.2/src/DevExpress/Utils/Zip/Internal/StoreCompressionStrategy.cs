namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;

    public class StoreCompressionStrategy : ICompressionStrategy
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
            StreamUtils.CopyStream(stream, targetStream, copyDelegate);
            this.crc32 = stream.ReadCheckSum;
        }

        public short GetGeneralPurposeBitFlag() => 
            0;

        public void PrepareExtraFields(IZipExtraFieldCollection extraFields)
        {
        }

        public DevExpress.Utils.Zip.Internal.CompressionMethod CompressionMethod =>
            DevExpress.Utils.Zip.Internal.CompressionMethod.Store;

        public int Crc32 =>
            this.crc32;
    }
}

