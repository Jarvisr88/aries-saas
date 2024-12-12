namespace #aXd
{
    using ActiproSoftware.Win32;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class #Bi
    {
        internal static readonly IntPtr #T4d = ((IntPtr) (-1));
        internal static readonly HandleRef #U4d = new HandleRef(null, IntPtr.Zero);
        internal const int #Qpb = 0x24;
        internal const int #gue = 0x81;
        internal const int #Upb = 0x84;
        internal const int #Vpb = 0x86;
        internal const int #23d = 6;
        internal const int #x4d = 0x317;
        internal const int #Vqk = 0x20000;
        internal const uint #hue = 0xcc0020;
        internal const uint #iue = 0x550009;
        internal const int #jue = 0;
        internal const int #kue = 0;
        internal const int #lue = 1;
        internal const int #mue = 2;
        internal const int #nue = 4;
        internal const int #oue = 8;
        internal const int #pue = 0x10;
        internal const int #que = 0x20;
        internal const int #rue = 0x40;
        internal const int #sue = 0x80;
        internal const int #tue = 0x100;
        internal const int #uue = 0x200;
        internal const int #vue = 0x400;
        internal const int #wue = 0x800;
        internal const int #xue = 0x1000;
        internal const int #yue = 0x2000;
        internal const int #zue = 0x4000;
        internal const int #Aue = 0x8000;
        internal const int #Bue = 0x10000;
        internal const int #Cue = 0x20000;
        internal const int #Due = 0x40000;
        internal const int #Eue = 1;
        internal const int #Fue = 2;
        internal const int #Gue = 7;
        internal const int #Tn = 0;
        internal const int #Un = -1;
        internal const uint #mn = 1;
        internal const uint #nn = 2;
        internal const uint #on = 4;
        internal const uint #pn = 8;
        internal const uint #qn = 0x10;
        internal const uint #rn = 0x20;
        internal const uint #sn = 0x40;
        internal const uint #tn = 0x80;
        internal const uint #un = 0x100;
        internal const uint #vn = 0x200;
        internal const uint #wn = 0x400;
        internal const int #ln = 4;
        internal const int #gje = 0x8000000;

        [DllImport("Gdi32.dll", EntryPoint="DeleteObject", CharSet=CharSet.Auto)]
        internal static extern int #5oe(IntPtr #YZf);
        [DllImport("User32.dll", EntryPoint="GetDC", CharSet=CharSet.Auto)]
        internal static extern IntPtr #7oe(IntPtr #Qqb);
        [DllImport("User32.dll", EntryPoint="ReleaseDC", CharSet=CharSet.Auto)]
        internal static extern int #9oe(IntPtr #Qqb, IntPtr #8qe);
        [DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
        internal static extern IntPtr #9z(HandleRef #Qqb, int #7Ff, IntPtr #8Ff, IntPtr #Uqb);
        [DllImport("user32.dll", EntryPoint="SetWindowPos", CharSet=CharSet.Auto)]
        internal static extern bool #aA(IntPtr #Qqb, IntPtr #4qb, int #Zn, int #0n, int #5qb, int #6qb, uint #pJf);
        [DllImport("Gdi32.dll", EntryPoint="SelectClipRgn", CharSet=CharSet.Auto)]
        internal static extern int #ape(IntPtr #8qe, IntPtr #0Zf);
        [DllImport("Gdi32.dll", EntryPoint="GetClipRgn", CharSet=CharSet.Auto)]
        internal static extern int #Axe(IntPtr #8qe, IntPtr #0Zf);
        [DllImport("user32.dll", EntryPoint="ShowWindow", CharSet=CharSet.Auto)]
        internal static extern int #bA(IntPtr #Qqb, int #9Ff);
        [DllImport("user32.dll", EntryPoint="GetActiveWindow", CharSet=CharSet.Auto)]
        internal static extern IntPtr #Bbe();
        [DllImport("Gdi32.dll", EntryPoint="SelectObject", CharSet=CharSet.Auto)]
        internal static extern IntPtr #bpe(IntPtr #8qe, IntPtr #YZf);
        [DllImport("Gdi32.dll", EntryPoint="SetTextColor", CharSet=CharSet.Auto)]
        public static extern int #Bxe(IntPtr #8qe, int #eUb);
        [DllImport("Gdi32.dll", EntryPoint="SetBkMode", CharSet=CharSet.Auto)]
        public static extern int #Cxe(IntPtr #8qe, int #ib);
        [DllImport("User32.dll", EntryPoint="CallNextHookEx", CharSet=CharSet.Auto)]
        internal static extern IntPtr #Dxe(IntPtr #j0f, int #k0f, IntPtr #8Ff, IntPtr #Uqb);
        [DllImport("user32.dll", EntryPoint="GetForegroundWindow", CharSet=CharSet.Auto)]
        internal static extern IntPtr #Exe();
        [DllImport("user32.dll", EntryPoint="SetParent", CharSet=CharSet.Auto)]
        internal static extern int #Fxe(IntPtr #EYf, IntPtr #o0f);
        [DllImport("User32.dll", EntryPoint="SetWindowsHookEx", CharSet=CharSet.Auto)]
        internal static extern IntPtr #Gxe(int #p0f, HookBase.#Sqe #q0f, IntPtr #r0f, int #s0f);
        [DllImport("user32.dll", EntryPoint="RedrawWindow", CharSet=CharSet.Auto)]
        internal static extern bool #Hbe(HandleRef #Qqb, #Fi #3Yf, HandleRef #4Yf, int #ui);
        [DllImport("User32.dll", EntryPoint="UnhookWindowsHookEx", CharSet=CharSet.Auto)]
        internal static extern bool #Hxe(IntPtr #j0f);
        [DllImport("Kernel32.dll", EntryPoint="GetCurrentThreadId", CharSet=CharSet.Auto)]
        internal static extern int #Ixe();
        [DllImport("Kernel32.dll", EntryPoint="GetLastError", CharSet=CharSet.Auto)]
        internal static extern int #Jxe();
        [DllImport("User32.dll", EntryPoint="WindowFromPoint", CharSet=CharSet.Auto)]
        internal static extern IntPtr #Kbe(#Ei #Ei);
        [DllImport("UxTheme.dll", EntryPoint="GetCurrentThemeName", CharSet=CharSet.Auto)]
        internal static extern void #Lbe(StringBuilder #6Yf, int #7Yf, StringBuilder #8Yf, int #9Yf, StringBuilder #aZf, int #bZf);
        internal static short #rbe(uint #Ld) => 
            (short) ((#Ld >> 0x10) & 0xffff);

        internal static short #sbe(uint #Ld) => 
            (short) (#Ld & 0xffff);

        [DllImport("user32.dll", EntryPoint="GetWindowLong", CharSet=CharSet.Auto)]
        internal static extern IntPtr #vAb(HandleRef #Qqb, int #rJf);
        [DllImport("Gdi32.dll", EntryPoint="BitBlt", CharSet=CharSet.Auto)]
        internal static extern int #xxe(IntPtr #c0f, int #Zn, int #0n, int #d0f, int #e0f, IntPtr #f0f, int #g0f, int #h0f, uint #i0f);
        [DllImport("user32.dll", EntryPoint="SetWindowLong", CharSet=CharSet.Auto)]
        internal static extern IntPtr #yAb(IntPtr #Qqb, int #rJf, IntPtr #uJf);
        [DllImport("user32.dll", EntryPoint="SetWindowLong", CharSet=CharSet.Auto)]
        internal static extern IntPtr #yAb(HandleRef #Qqb, int #rJf, HandleRef #uJf);
        [DllImport("Gdi32.dll", EntryPoint="CreateCompatibleBitmap", CharSet=CharSet.Auto)]
        internal static extern IntPtr #yxe(IntPtr #8qe, int #d0f, int #e0f);
        [DllImport("Gdi32.dll", EntryPoint="CreateCompatibleDC", CharSet=CharSet.Auto)]
        internal static extern IntPtr #zxe(IntPtr #8qe);
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern int DrawText(IntPtr #8qe, string #l0f, int #m0f, #Fi #n0f, uint #ZYf);

        [StructLayout(LayoutKind.Sequential)]
        internal class #Blb
        {
            public #Bi.#Ei #Vqb = new #Bi.#Ei(0, 0);
            public #Bi.#Ei #Wqb = new #Bi.#Ei(0, 0);
            public #Bi.#Ei #Xqb = new #Bi.#Ei(0, 0);
            public #Bi.#Ei #Yqb = new #Bi.#Ei(0, 0);
            public #Bi.#Ei #Zqb = new #Bi.#Ei(0, 0);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct #Ei
        {
            public int #Zn;
            public int #0n;
            public #Ei(Point pt)
            {
                this.#Zn = pt.X;
                this.#0n = pt.Y;
            }

            public #Ei(int x, int y)
            {
                this.#Zn = x;
                this.#0n = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class #Fi
        {
            public int #1n;
            public int #2n;
            public int #3n;
            public int #4n;
            public #Fi(Rectangle rect)
            {
                this.#4n = rect.Bottom;
                this.#1n = rect.Left;
                this.#3n = rect.Right;
                this.#2n = rect.Top;
            }

            public #Fi(RectangleF rect)
            {
                this.#4n = (int) Math.Ceiling((double) rect.Bottom);
                this.#1n = (int) Math.Floor((double) rect.Left);
                this.#3n = (int) Math.Ceiling((double) rect.Right);
                this.#2n = (int) Math.Floor((double) rect.Top);
            }

            public #Fi(int left, int top, int right, int bottom)
            {
                this.#4n = bottom;
                this.#1n = left;
                this.#3n = right;
                this.#2n = top;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class #Tqe
        {
            public int #Hue;
            public int #Iue;
            public int #Jue;
            public int #Kue;
            public int #Lue;
            public byte #Mue;
            public byte #Nue;
            public byte #Oue;
            public byte #Pue;
            public byte #Que;
            public byte #Rue;
            public byte #Sue;
            public byte #Tue;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0x20)]
            public string #Uue = string.Empty;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct #Uqe
        {
            public #Bi.#Ei #Vue;
            public IntPtr #Qqb;
            public uint #Wue;
            public uint #Xue;
            public uint #fue;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct #xe
        {
            public int #5qb;
            public int #6qb;
            public #xe(Size size)
            {
                this.#5qb = size.Width;
                this.#6qb = size.Height;
            }

            public #xe(int width, int height)
            {
                this.#5qb = width;
                this.#6qb = height;
            }
        }
    }
}

