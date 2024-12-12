namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DurationHash : IEquatable<DurationHash>
    {
        public static readonly DurationHash Empty;
        public static readonly DurationHash All;
        public static readonly DurationHash NotLoaded;
        private const int SECONDS_MASK = 0x3f;
        private const int MINUTES_MASK = 0xfc0;
        private const int HOURS_MASK = 0x1f000;
        private readonly long value;
        private DurationHash(DurationHash.Kind kind);
        public bool Equals(DurationHash node);
        public override bool Equals(object obj);
        public override int GetHashCode();
        static DurationHash();
        private enum Kind : long
        {
            public const DurationHash.Kind Empty = DurationHash.Kind.Empty;,
            public const DurationHash.Kind All = DurationHash.Kind.All;,
            public const DurationHash.Kind NotLoaded = DurationHash.Kind.NotLoaded;
        }
    }
}

