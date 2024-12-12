namespace DevExpress.Utils.Zip.Internal
{
    using System.IO;

    public interface IDecompressionStrategy
    {
        Stream Decompress(Stream stream);
    }
}

