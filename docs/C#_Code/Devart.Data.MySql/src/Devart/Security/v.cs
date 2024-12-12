namespace Devart.Security
{
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading;

    internal class v
    {
        private const CharSet a = CharSet.Auto;
        public const int b = 1;
        public const int c = 2;
        public const int d = 3;
        public const int e = 4;
        public const int f = 5;
        public const int g = 6;
        public const int h = 7;
        public const int i = 8;
        public const int j = 1;
        public const int k = 0x10000;
        public const int l = 1;
        public const int m = 2;
        public const int n = 1;
        public const int o = 0x100;
        public const int p = 0x200;
        public const int q = 0x1000;
        public const uint r = 1;
        public const uint s = 3;
        public const uint t = 0x40;
        public const uint u = 2;
        public const uint v = 1;
        public const string w = "SB_";
        public const uint x = 1;
        public const uint y = 2;
        public const uint z = 7;
        private const uint aa = uint.MaxValue;
        private const uint ab = 0x80;
        private const uint ac = 0;
        private const uint ad = 0x102;
        private const uint ae = uint.MaxValue;
        public const uint af = 0x80000000;
        public const uint ag = 0x40000000;
        public const int ah = -1;
        public const uint ai = 0xe7;
        public const uint aj = 2;
        public const uint ak = 0x40000000;
        public const uint al = 0x20000000;
        public const uint am = 3;

        public static bool a(WaitHandle A_0, int A_1);
        [DllImport("crypt32.dll")]
        public static extern int CertCloseStore(IntPtr A_0, uint A_1);
        [DllImport("crypt32.dll")]
        public static extern IntPtr CertEnumCertificatesInStore(IntPtr A_0, IntPtr A_1);
        [DllImport("crypt32.dll", CharSet=CharSet.Unicode)]
        public static extern int CertGetNameStringW(IntPtr A_0, uint A_1, int A_2, IntPtr A_3, [In, Out] char[] A_4, int A_5);
        [DllImport("crypt32.dll")]
        public static extern int CertGetValidUsages(int A_0, ref IntPtr A_1, ref int A_2, [In, Out] byte[] A_3, ref int A_4);
        [DllImport("crypt32.dll")]
        public static extern IntPtr CertOpenSystemStore(IntPtr A_0, string A_1);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool CloseHandle(IntPtr A_0);
        [DllImport("Kernel32")]
        public static extern int CreateFile(string A_0, uint A_1, uint A_2, Devart.Security.v.i A_3, uint A_4, uint A_5, uint A_6);
        [DllImport("advapi32.dll", CharSet=CharSet.Auto)]
        public static extern bool CryptAcquireContext(ref uint A_0, IntPtr A_1, IntPtr A_2, uint A_3, uint A_4);
        [DllImport("advapi32.dll")]
        public static extern bool CryptExportKey(uint A_0, uint A_1, uint A_2, uint A_3, byte[] A_4, out uint A_5);
        [DllImport("crypt32.dll")]
        public static extern IntPtr CryptFindOIDInfo(uint A_0, string A_1, uint A_2);
        [DllImport("advapi32.dll", CharSet=CharSet.Auto)]
        public static extern bool CryptGetProvParam(uint A_0, uint A_1, IntPtr A_2, out uint A_3, uint A_4);
        [DllImport("advapi32.dll")]
        public static extern bool CryptGetUserKey(uint A_0, uint A_1, out uint A_2);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool FlushFileBuffers(IntPtr A_0);
        [DllImport("kernel32.dll")]
        public static extern uint FormatMessageW(uint A_0, IntPtr A_1, uint A_2, uint A_3, out IntPtr A_4, uint A_5, IntPtr A_6);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool GetCommTimeouts(int A_0, ref Devart.Security.v.d A_1);
        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool PeekNamedPipe(IntPtr A_0, byte[] A_1, uint A_2, ref uint A_3, ref uint A_4, ref uint A_5);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        internal static extern bool ReadFile(IntPtr A_0, [Out] byte[] A_1, uint A_2, out uint A_3, IntPtr A_4);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool SetCommTimeouts(int A_0, ref Devart.Security.v.d A_1);
        [DllImport("kernel32.dll", SetLastError=true)]
        internal static extern uint WaitForSingleObject(SafeWaitHandle A_0, uint A_1);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll")]
        public static extern bool WaitNamedPipe(string A_0, uint A_1);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("Kernel32")]
        internal static extern bool WriteFile(IntPtr A_0, [In] byte[] A_1, uint A_2, out uint A_3, IntPtr A_4);

        [StructLayout(LayoutKind.Sequential)]
        public struct a
        {
            private IntPtr a;
            private v.f b;
            public int a();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct b
        {
            public uint a;
            public IntPtr b;
            public IntPtr c;
            public b(string A_0, string A_1, uint A_2);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct c
        {
            private int a;
            private Devart.Security.v.f b;
            private Devart.Security.v.a c;
            private Devart.Security.v.f d;
            public FILETIME e;
            public FILETIME f;
            private Devart.Security.v.f g;
            private Devart.Security.v.g h;
            private Devart.Security.v.e i;
            private Devart.Security.v.e j;
            private IntPtr k;
            private IntPtr l;
            public int a(int A_0);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct d
        {
            public uint a;
            public uint b;
            public uint c;
            public uint d;
            public uint e;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct e
        {
            private int a;
            private IntPtr b;
            private int c;
            public void a(int A_0);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct f
        {
            private int a;
            private IntPtr b;
            public void a(int A_0);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct g
        {
            private Devart.Security.v.a a;
            private v.e b;
            public int a();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct h
        {
            private int a;
            private IntPtr b;
            public IntPtr c;
            private int d;
            private int e;
            private Devart.Security.v.f f;
            public int a(int A_0);
        }

        [StructLayout(LayoutKind.Sequential)]
        public class i
        {
            public int a;
            public IntPtr b;
            public bool c;
            public i();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct j
        {
            private int a;
            private IntPtr b;
            private int c;
            public IntPtr d;
            private IntPtr e;
            public void a(int A_0);
        }
    }
}

