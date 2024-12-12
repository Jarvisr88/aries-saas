namespace DevExpress.Utils.StructuredStorage.Internal
{
    using System;

    [CLSCompliant(false)]
    public static class SectorType
    {
        public const uint MaxValue = 0xfffffffa;
        public const uint Dif = 0xfffffffc;
        public const uint Fat = 0xfffffffd;
        public const uint EndOfChain = 0xfffffffe;
        public const uint Free = uint.MaxValue;
        public const uint NoStream = uint.MaxValue;
    }
}

