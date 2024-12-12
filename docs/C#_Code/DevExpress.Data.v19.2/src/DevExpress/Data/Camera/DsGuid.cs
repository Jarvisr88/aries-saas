namespace DevExpress.Data.Camera
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    internal class DsGuid
    {
        [FieldOffset(0)]
        private Guid guid;
        public static readonly DsGuid Empty;

        static DsGuid();
        public DsGuid();
        public DsGuid(Guid g);
        public DsGuid(string g);
        public static DsGuid FromGuid(Guid g);
        public override int GetHashCode();
        public static implicit operator Guid(DsGuid g);
        public static implicit operator DsGuid(Guid g);
        public Guid ToGuid();
        public override string ToString();
        public string ToString(string format);
    }
}

