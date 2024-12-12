namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;

    [SecuritySafeCritical]
    public sealed class NativeRenderer2 : IDisposable
    {
        private const uint FileMapAllAccess = 0xf001f;
        private const uint PageReadwrite = 4;
        private static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
        private InteropBitmap source;
        private int width;
        private int height;
        private const System.Drawing.Imaging.PixelFormat Format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
        private const int Bpp = 0x20;
        private int stride;
        private uint numBytes;
        private IntPtr section;
        private IntPtr hBuffer;
        private bool isDisposed;
        private readonly Locker renderLocker = new Locker();
        private INativeRendererImpl nativeRenderer;
        private BufferedGraphicsContext bgc;
        private bool isZeroSize;
        private Func<System.Drawing.Color> getBackgroundHandler;

        public NativeRenderer2(Func<System.Drawing.Color> getBackground)
        {
            this.getBackgroundHandler = getBackground;
        }

        private void CleanUp()
        {
            if (this.hBuffer != IntPtr.Zero)
            {
                UnmapViewOfFile(this.hBuffer);
                this.hBuffer = IntPtr.Zero;
            }
            if (this.section != IntPtr.Zero)
            {
                CloseHandle(this.section);
                this.section = IntPtr.Zero;
            }
            Action<BufferedGraphicsContext> action = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Action<BufferedGraphicsContext> local1 = <>c.<>9__38_0;
                action = <>c.<>9__38_0 = x => x.Dispose();
            }
            this.bgc.Do<BufferedGraphicsContext>(action);
            this.bgc = null;
            Action<System.Drawing.Bitmap> action2 = <>c.<>9__38_1;
            if (<>c.<>9__38_1 == null)
            {
                Action<System.Drawing.Bitmap> local2 = <>c.<>9__38_1;
                action2 = <>c.<>9__38_1 = x => x.Dispose();
            }
            this.Bitmap.Do<System.Drawing.Bitmap>(action2);
            this.Bitmap = null;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", EntryPoint="RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr destination, IntPtr source, uint length);
        private static void CopyMemoryImpl(IntPtr source, IntPtr target, int stride, int height, Rectangle region, bool whole)
        {
            if (whole)
            {
                CopyMemory(target, source, (uint) (stride * height));
            }
            else
            {
                int num = region.Left * 4;
                int num2 = region.Width * 4;
                IntPtr ptr = source;
                IntPtr destination = (target + (stride * region.Top)) + num;
                for (int i = 0; i < region.Height; i++)
                {
                    destination += stride;
                    ptr += stride;
                    CopyMemory(destination, ptr, (uint) num2);
                }
            }
        }

        [DllImport("kernel32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern IntPtr CreateFileMapping(IntPtr lpBaseAddress, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);
        private INativeRendererImpl CreateNativeRendererImpl() => 
            new DirectNativeRendererImpl();

        public void Dispose()
        {
            this.DisposeInternal();
            GC.SuppressFinalize(this);
        }

        private void DisposeInternal()
        {
            if (!this.isDisposed)
            {
                this.CleanUp();
                this.source = null;
                this.isDisposed = true;
            }
        }

        public void EndRender(Rect region, bool whole)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("NativeRenderer");
            }
            this.renderLocker.Unlock();
            if (!this.isZeroSize)
            {
                Rect rect = new Rect(0.0, 0.0, (double) this.width, (double) this.height);
                rect.Intersect(region);
                if (!this.IsZeroSize(rect.Size))
                {
                    Rectangle rectangle = rect.ToWinFormsRectangle();
                    BitmapData bitmapdata = this.Bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    CopyMemoryImpl(bitmapdata.Scan0, this.hBuffer, this.stride, this.height, rectangle, whole);
                    this.Bitmap.UnlockBits(bitmapdata);
                }
            }
        }

        ~NativeRenderer2()
        {
            this.DisposeInternal();
        }

        public void InvalidateSource()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("NativeRenderer");
            }
            InteropBitmap source = this.Source;
            if (source == null)
            {
                InteropBitmap local1 = source;
            }
            else
            {
                source.Invalidate();
            }
        }

        private bool IsZeroSize(System.Windows.Size size) => 
            (size.Width < 1.0) || (size.Height < 1.0);

        [DllImport("kernel32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, UIntPtr dwNumberOfBytesToMap);
        public bool Render(INativeImageRenderer renderer, Rect region)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("NativeRenderer");
            }
            if (!this.renderLocker.IsLocked)
            {
                throw new ArgumentException("call StartRender");
            }
            return (!this.isZeroSize ? this.RenderToBitmap(renderer, region) : true);
        }

        private bool RenderToBitmap(INativeImageRenderer renderer, Rect region)
        {
            bool flag = false;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                Rectangle targetRectangle = new Rectangle(0, 0, this.width, this.height);
                using (BufferedGraphics graphics2 = this.bgc.Allocate(graphics, targetRectangle))
                {
                    using (Graphics graphics3 = graphics2.Graphics)
                    {
                        graphics3.Clear(this.getBackgroundHandler());
                        flag |= this.nativeRenderer.RenderToGraphics(graphics3, renderer, region, new System.Windows.Size((double) this.width, (double) this.height));
                        using (Graphics graphics4 = Graphics.FromImage(this.Bitmap))
                        {
                            graphics2.Render(graphics4);
                        }
                    }
                }
            }
            return flag;
        }

        public void ResetCaches()
        {
            this.nativeRenderer.Reset();
        }

        [SecuritySafeCritical]
        public void Resize(System.Windows.Size size)
        {
            this.width = (int) size.Width;
            this.height = (int) size.Height;
            this.isZeroSize = this.IsZeroSize(size);
            if (this.isZeroSize || ((this.Bitmap == null) || ((this.width > this.Bitmap.Width) || (this.height > this.Bitmap.Height))))
            {
                this.CleanUp();
                this.UpdateNativeRenderer();
                if (!this.isZeroSize)
                {
                    this.stride = (this.width * 0x20) / 8;
                    this.numBytes = (uint) (this.height * this.stride);
                    this.Bitmap = new System.Drawing.Bitmap(this.width, this.height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    this.bgc = new BufferedGraphicsContext();
                    this.section = CreateFileMapping(InvalidHandleValue, IntPtr.Zero, 4, 0, this.numBytes, null);
                    if (this.section == IntPtr.Zero)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    this.hBuffer = MapViewOfFile(this.section, 0xf001f, 0, 0, (UIntPtr) this.numBytes);
                    this.source = (InteropBitmap) Imaging.CreateBitmapSourceFromMemorySection(this.section, this.width, this.height, PixelFormats.Bgr32, this.stride, 0);
                }
            }
        }

        public void StartRender()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("NativeRenderer");
            }
            if (this.renderLocker.IsLocked)
            {
                throw new ArgumentException("render started");
            }
            this.renderLocker.Lock();
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);
        public void UpdateNativeRenderer()
        {
            Action<INativeRendererImpl> action = <>c.<>9__45_0;
            if (<>c.<>9__45_0 == null)
            {
                Action<INativeRendererImpl> local1 = <>c.<>9__45_0;
                action = <>c.<>9__45_0 = x => x.Dispose();
            }
            this.nativeRenderer.Do<INativeRendererImpl>(action);
            this.nativeRenderer = this.CreateNativeRendererImpl();
        }

        public void UpdateRenderer()
        {
            this.Resize(new System.Windows.Size((double) this.width, (double) this.height));
        }

        public InteropBitmap Source =>
            this.source;

        private System.Drawing.Bitmap Bitmap { get; set; }

        public int MaxCacheSize { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NativeRenderer2.<>c <>9 = new NativeRenderer2.<>c();
            public static Action<BufferedGraphicsContext> <>9__38_0;
            public static Action<Bitmap> <>9__38_1;
            public static Action<INativeRendererImpl> <>9__45_0;

            internal void <CleanUp>b__38_0(BufferedGraphicsContext x)
            {
                x.Dispose();
            }

            internal void <CleanUp>b__38_1(Bitmap x)
            {
                x.Dispose();
            }

            internal void <UpdateNativeRenderer>b__45_0(INativeRendererImpl x)
            {
                x.Dispose();
            }
        }
    }
}

