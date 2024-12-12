namespace DevExpress.Data.Export
{
    using System;

    public class MsoCrc32Compute
    {
        private static readonly uint[] crcCache;
        private uint crcValue;

        static MsoCrc32Compute();
        public void Add(byte data);
        public void Add(byte[] data);
        public void Add(short data);
        public void Add(int data);
        public void Add(byte[] data, int start, int count);
        private void AddCore(byte[] data);
        private void AddCore(byte[] data, int start, int count);

        public int CrcValue { get; set; }
    }
}

