namespace DevExpress.Utils.Zip
{
    using System;
    using System.IO;

    public class ByteCountStream : CheckSumStream<int>
    {
        public ByteCountStream(Stream stream) : base(stream, ByteCountCheckSumCalculator.Instance)
        {
        }
    }
}

