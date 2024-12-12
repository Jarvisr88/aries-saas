namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.IO;

    public class NoCompressionDecompressionStrategy : IDecompressionStrategy
    {
        public Stream Decompress(Stream stream) => 
            stream;
    }
}

