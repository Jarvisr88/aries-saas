namespace DevExpress.Xpf.Core
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    internal static class WpfNotifyIconNativeMethods
    {
        [SecuritySafeCritical]
        public static IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam) => 
            CallNextHookExImpl(idHook, nCode, wParam, lParam);

        [DllImport("user32", EntryPoint="CallNextHookEx")]
        private static extern IntPtr CallNextHookExImpl(IntPtr idHook, int nCode, int wParam, IntPtr lParam);
        private static BitmapSource CreateBitmapSource(ImageSource imageSource)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int) imageSource.Width, (int) imageSource.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawImage(imageSource, new Rect(0.0, 0.0, imageSource.Width, imageSource.Height));
            }
            bitmap.Render(visual);
            return bitmap;
        }

        private static MemoryStream CreateStreamWithPatchPngHeaderToIcoHeader(BitmapSource bitmapSource)
        {
            MemoryStream output = new MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder {
                Frames = { BitmapFrame.Create(bitmapSource, null, null, null) }
            };
            BinaryWriter writer = new BinaryWriter(output);
            byte[] buffer = new byte[6];
            buffer[2] = 1;
            buffer[4] = 1;
            writer.Write(buffer);
            writer.Write((byte) bitmapSource.PixelWidth);
            writer.Write((byte) bitmapSource.PixelHeight);
            byte[] buffer2 = new byte[] { 0, 0, 1, 0, 0x20, 0, 0, 0, 0, 0, 0x16, 0, 0, 0 };
            writer.Write(buffer2);
            encoder.Save(output);
            output.Position = 14;
            writer.Write((int) (((int) output.Length) - 0x16));
            output.Position = 0L;
            return output;
        }

        [SecuritySafeCritical]
        public static IntPtr GetCurrentThreadId() => 
            GetCurrentThreadIdImpl();

        [DllImport("kernel32.dll", EntryPoint="GetCurrentThreadId")]
        private static extern IntPtr GetCurrentThreadIdImpl();
        [DllImport("user32.dll", EntryPoint="GetCursorPos")]
        public static extern bool GetCursorPosImpl(out POINT lpPoint);
        public static Icon GetIcon(this ImageSource imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }
            BitmapSource bitmapSource = imageSource as BitmapSource;
            bitmapSource ??= CreateBitmapSource(imageSource);
            return new Icon(CreateStreamWithPatchPngHeaderToIcoHeader(bitmapSource));
        }

        [SecuritySafeCritical]
        public static IntPtr GetModuleHandle(string lpModuleName) => 
            GetModuleHandleImpl(lpModuleName);

        [DllImport("kernel32.dll", EntryPoint="GetModuleHandle")]
        private static extern IntPtr GetModuleHandleImpl(string lpModuleName);
        [SecuritySafeCritical]
        public static uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId) => 
            GetWindowThreadProcessIdImpl(hWnd, out lpdwProcessId);

        [DllImport("user32.dll", EntryPoint="GetWindowThreadProcessId")]
        private static extern uint GetWindowThreadProcessIdImpl(IntPtr hWnd, out uint lpdwProcessId);
        [SecuritySafeCritical]
        public static IntPtr GetWindowUnderCursor()
        {
            System.Windows.Point point2 = new System.Windows.Point();
            POINT lpPoint = point2;
            return (GetCursorPosImpl(out lpPoint) ? WindowFromPointImpl(lpPoint) : IntPtr.Zero);
        }

        [SecuritySafeCritical]
        public static IntPtr SetWindowsHookEx(int idHook, Delegate callback, IntPtr hInstance, IntPtr threadId) => 
            SetWindowsHookExImpl(idHook, callback, hInstance, threadId);

        [DllImport("user32", EntryPoint="SetWindowsHookEx", SetLastError=true)]
        private static extern IntPtr SetWindowsHookExImpl(int idHook, Delegate callback, IntPtr hInstance, IntPtr threadId);
        [SecuritySafeCritical]
        public static bool UnhookWindowsHookEx(IntPtr hInstance) => 
            UnhookWindowsHookExImpl(hInstance);

        [DllImport("user32", EntryPoint="UnhookWindowsHookEx", SetLastError=true)]
        private static extern bool UnhookWindowsHookExImpl(IntPtr hInstance);
        [DllImport("user32.dll", EntryPoint="WindowFromPoint")]
        public static extern IntPtr WindowFromPointImpl(POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(System.Windows.Point pt) : this((int) pt.X, (int) pt.Y)
            {
            }

            public static implicit operator System.Windows.Point(WpfNotifyIconNativeMethods.POINT p) => 
                new System.Windows.Point((double) p.X, (double) p.Y);

            public static implicit operator WpfNotifyIconNativeMethods.POINT(System.Windows.Point p) => 
                new WpfNotifyIconNativeMethods.POINT(p);
        }
    }
}

