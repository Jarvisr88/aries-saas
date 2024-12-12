namespace DevExpress.Utils
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class MemoryControllerHelper
    {
        private static readonly MEMORYSTATUSEX memoryStatus = new MEMORYSTATUSEX();

        public static long GetAvailableVirtualMemory() => 
            AvailableVirtualMemory;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);
        public static bool IsEnoughMemory(int limitBytes) => 
            AvailableVirtualMemory > limitBytes;

        public static long AvailableVirtualMemory =>
            GlobalMemoryStatusEx(memoryStatus) ? ((long) memoryStatus.ullAvailVirtual) : 0x7fffffffL;

        public static long AvailableVirtualMemory_Mib =>
            GlobalMemoryStatusEx(memoryStatus) ? ((long) (memoryStatus.ullAvailVirtual >> 20)) : 0x7ffL;

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
        private sealed class MEMORYSTATUSEX
        {
            private readonly uint dwLength = ((uint) Marshal.SizeOf(typeof(MemoryControllerHelper.MEMORYSTATUSEX)));
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }
    }
}

