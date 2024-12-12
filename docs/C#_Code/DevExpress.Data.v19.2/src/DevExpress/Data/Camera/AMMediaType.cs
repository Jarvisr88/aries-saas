namespace DevExpress.Data.Camera
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [StructLayout(LayoutKind.Sequential), SecuritySafeCritical, ComVisible(false)]
    internal class AMMediaType : IDisposable
    {
        public Guid MajorType;
        public Guid SubType;
        [MarshalAs(UnmanagedType.Bool)]
        public bool FixedSizeSamples;
        [MarshalAs(UnmanagedType.Bool)]
        public bool TemporalCompression;
        public int SampleSize;
        public Guid FormatType;
        public IntPtr UnkPtr;
        public int FormatSize;
        public IntPtr FormatPtr;
        protected override void Finalize();
        public void Dispose();
        protected virtual void Dispose(bool disposing);
        public AMMediaType();
    }
}

