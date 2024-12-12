namespace DevExpress.Data.Camera
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class DsLong
    {
        private long Value;
        public DsLong(long Value);
        public override string ToString();
        public override int GetHashCode();
        public static implicit operator long(DsLong l);
        public static implicit operator DsLong(long l);
        public long ToInt64();
        public static DsLong FromInt64(long l);
    }
}

