namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.IO;

    public interface ICompressionStrategy
    {
        void Compress(Stream sourceStream, Stream targetStream, IZipComplexOperationProgress progress);
        short GetGeneralPurposeBitFlag();
        void PrepareExtraFields(IZipExtraFieldCollection extraFields);

        DevExpress.Utils.Zip.Internal.CompressionMethod CompressionMethod { get; }

        int Crc32 { get; }
    }
}

