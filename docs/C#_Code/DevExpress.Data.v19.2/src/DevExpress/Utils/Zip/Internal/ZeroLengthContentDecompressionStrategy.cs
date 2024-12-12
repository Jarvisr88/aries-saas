namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.IO;

    public class ZeroLengthContentDecompressionStrategy : IDecompressionStrategy
    {
        public Stream Decompress(Stream stream) => 
            new MemoryStream();
    }
}

