namespace DevExpress.Utils.Zip
{
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class Crc32Stream : CheckSumStream<uint>
    {
        public Crc32Stream(Stream stream) : base(stream, Crc32CheckSumCalculator.Instance)
        {
        }
    }
}

