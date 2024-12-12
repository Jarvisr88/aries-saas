namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class DeflateDecompressionStrategy : IDecompressionStrategy
    {
        public Stream Decompress(Stream rawStream) => 
            new DeflateStream(rawStream, CompressionMode.Decompress, true);
    }
}

