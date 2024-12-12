namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_TRIMMING
    {
        private readonly DWRITE_TRIMMING_GRANULARITY granularity;
        private readonly int delimiter;
        private readonly int delimiterCount;
        public DWRITE_TRIMMING_GRANULARITY Granularity =>
            this.granularity;
        public int Delimiter =>
            this.delimiter;
        public int DelimiterCount =>
            this.delimiterCount;
        public DWRITE_TRIMMING(DWRITE_TRIMMING_GRANULARITY granularity, int delimiter, int delimiterCount)
        {
            this.granularity = granularity;
            this.delimiter = delimiter;
            this.delimiterCount = delimiterCount;
        }
    }
}

