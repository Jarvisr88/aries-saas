namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;

    internal static class FNV1a
    {
        private const int Prime = 0x1000193;
        public const int Basis = -2128831035;
        public static readonly int NotLoaded;
        public static readonly int NullObject;

        static FNV1a();
        [DebuggerStepThrough, DebuggerHidden]
        public static int Create(object[] values);
        [DebuggerStepThrough, DebuggerHidden]
        public static int Next(int hashCode, object value);
        private static int NextInt(int hash, int value);
        private static int NextOctet(int hash, int octet);
    }
}

