namespace DevExpress.Utils.Zip
{
    using System;
    using System.IO;

    [CLSCompliant(false)]
    public class Adler32Stream : CheckSumStream<uint>
    {
        public Adler32Stream(Stream stream) : base(stream, Adler32CheckSumCalculator.Instance)
        {
        }
    }
}

