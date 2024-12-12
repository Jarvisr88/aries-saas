namespace DevExpress.Data.Camera
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    [CLSCompliant(false)]
    public static class CameraImport
    {
        internal static readonly Guid SystemDeviceEnum;
        internal static readonly Guid VideoInputDevice;
        internal static readonly Guid FilterGraph;
        internal static readonly Guid SampleGrabber;
        internal static readonly Guid CaptureGraphBuilder2;
        internal static readonly int WM_GRAPHNOTIFY;

        static CameraImport();
        [DllImport("Kernel32.dll", EntryPoint="RtlMoveMemory")]
        public static extern void CopyMemory(IntPtr destination, IntPtr source, int length);
        [DllImport("ole32.dll")]
        public static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern IntPtr CreateFileMapping(IntPtr handlerFile, IntPtr pointerFileMappingAttributes, uint fileProtect, uint maximumSizeHigh, uint maximumSizeLow, string name);
        [DllImport("user32.dll", CharSet=CharSet.Ansi)]
        public static extern int EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern IntPtr MapViewOfFile(IntPtr fileMappingObject, uint desiredAccess, uint fileOffsetHigh, uint fileOffsetLow, uint numberOfBytesToMap);
        [DllImport("ole32.dll", CharSet=CharSet.Unicode)]
        public static extern int MkParseDisplayName(IBindCtx pbc, string szUserName, ref int pchEaten, out IMoniker ppmk);
        [DllImport("kernel32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);
    }
}

