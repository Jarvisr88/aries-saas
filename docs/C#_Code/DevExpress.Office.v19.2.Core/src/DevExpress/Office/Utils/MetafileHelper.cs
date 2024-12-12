namespace DevExpress.Office.Utils
{
    using DevExpress.Office.PInvoke;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;

    public static class MetafileHelper
    {
        public const int MetafileResolution = 0x60;
        private const int CF_ENHMETAFILE = 14;
        private const int CF_METAFILEPICT = 3;

        [DllImport("user32.dll")]
        private static extern int CloseClipboard();
        public static Image CreateMetafile(MemoryStream stream, Win32.MapMode mapMode, int pictureWidth, int pictureHeight)
        {
            if (OSHelper.IsWindows)
            {
                return CreateMetafileWin(stream, mapMode, pictureWidth, pictureHeight);
            }
            if (stream.CanSeek)
            {
                stream.Seek(0L, SeekOrigin.Begin);
            }
            return new Metafile(stream);
        }

        private static Image CreateMetafileWin(MemoryStream stream, Win32.MapMode mapMode, int pictureWidth, int pictureHeight)
        {
            Image image;
            byte[] buffer = stream.GetBuffer();
            Win32.METAFILEPICT mfp = new Win32.METAFILEPICT(mapMode, pictureWidth, pictureHeight);
            IntPtr handle = Win32.SetWinMetaFileBits((long) ((uint) stream.Length), buffer, IntPtr.Zero, ref mfp);
            Metafile metafile = TryCreateMetafile(handle);
            if (metafile != null)
            {
                return metafile;
            }
            DeleteMetafileHandle(handle);
            handle = Win32.SetMetaFileBitsEx(stream.Length, buffer);
            metafile = TryCreateMetafile(handle);
            if (metafile != null)
            {
                return metafile;
            }
            DeleteMetafileHandle(handle);
            handle = Win32.SetEnhMetaFileBits(stream.Length, buffer);
            metafile = TryCreateMetafile(handle);
            if (metafile != null)
            {
                return metafile;
            }
            DeleteMetafileHandle(handle);
            MemoryStream stream2 = new MemoryStream(buffer, 0, (int) stream.Length);
            try
            {
                image = ImageLoaderHelper.ImageFromStream(stream2).Image;
            }
            catch
            {
                throw new ArgumentException("Invalid metafile.");
            }
            return image;
        }

        public static void DeleteMetafileHandle(IntPtr handle)
        {
            if (!Win32.DeleteEnhMetaFile(handle))
            {
                Win32.DeleteMetaFile(handle);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(int wFormat);
        [SecuritySafeCritical]
        public static byte[] GetEmfMetaFileBits(IntPtr hEmf, SizeF s)
        {
            byte[] buffer3;
            byte[] metaFileBits = Win32.GetMetaFileBits(hEmf);
            Win32.METAFILEPICT mfp = new Win32.METAFILEPICT(Win32.MapMode.Anisotropic, (int) s.Width, (int) s.Height);
            IntPtr ptr = PInvokeSafeNativeMethods.SetWinMetaFileBits((uint) metaFileBits.Length, metaFileBits, IntPtr.Zero, ref mfp);
            try
            {
                uint cbBuffer = PInvokeSafeNativeMethods.GetEnhMetaFileBits(ptr, 0, null);
                byte[] buffer = new byte[cbBuffer];
                PInvokeSafeNativeMethods.GetEnhMetaFileBits(ptr, cbBuffer, buffer);
                buffer3 = buffer;
            }
            finally
            {
                DeleteMetafileHandle(ptr);
            }
            return buffer3;
        }

        private static byte[] GetEnhMetafileBits(IntPtr handle) => 
            Win32.GetEnhMetafileBits(handle);

        public static Metafile GetEnhMetafileFromClipboard() => 
            GetMetafileFromClipboardCore(14, new GetMetafileBytesDelegate(MetafileHelper.GetEnhMetafileBits));

        private static byte[] GetMetaFileBits(IntPtr handle) => 
            Win32.GetMetaFileBits(handle);

        public static Metafile GetMetafileFromClipboard() => 
            GetMetafileFromClipboardCore(3, new GetMetafileBytesDelegate(MetafileHelper.GetMetaFileBits));

        [SecuritySafeCritical]
        private static Metafile GetMetafileFromClipboardCore(int uFormat, GetMetafileBytesDelegate getter)
        {
            Metafile metafile;
            if (!OSHelper.IsWindows || !OpenClipboard(IntPtr.Zero))
            {
                return null;
            }
            IntPtr zero = IntPtr.Zero;
            try
            {
                if (IsClipboardFormatAvailable(uFormat) == 0)
                {
                    metafile = null;
                }
                else
                {
                    zero = GetClipboardData(uFormat);
                    using (MemoryStream stream = new MemoryStream(getter(zero)))
                    {
                        metafile = new Metafile(stream);
                    }
                }
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    DeleteMetafileHandle(zero);
                }
                CloseClipboard();
            }
            return metafile;
        }

        [DllImport("user32.dll")]
        private static extern int IsClipboardFormatAvailable(int wFormat);
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);
        private static Metafile TryCreateMetafile(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            try
            {
                return TryCreateMetafileCore(handle);
            }
            catch
            {
                return null;
            }
        }

        [SecuritySafeCritical, SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
        private static Metafile TryCreateMetafileCore(IntPtr handle) => 
            new Metafile(handle, true);

        private delegate byte[] GetMetafileBytesDelegate(IntPtr handle);
    }
}

