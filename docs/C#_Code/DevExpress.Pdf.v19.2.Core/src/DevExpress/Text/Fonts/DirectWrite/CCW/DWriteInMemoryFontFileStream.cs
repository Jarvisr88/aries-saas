namespace DevExpress.Text.Fonts.DirectWrite.CCW
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Interop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DWriteInMemoryFontFileStream : IDWriteFontFileStream, IDisposable
    {
        private readonly MemoryHandle memoryHandle;
        private readonly int length;

        public DWriteInMemoryFontFileStream(byte[] fontFileData)
        {
            this.memoryHandle = new MemoryHandle(fontFileData);
            this.length = fontFileData.Length;
        }

        public void Dispose()
        {
            this.memoryHandle.Dispose();
        }

        public int GetFileSize(out long fileSize)
        {
            fileSize = this.length;
            return 0;
        }

        public int GetLastWriteTime(out long lastWriteTime)
        {
            lastWriteTime = 0L;
            return 0;
        }

        [SecuritySafeCritical]
        public int ReadFileFragment(out IntPtr fragmentStart, long fileOffset, long fragmentSize, out IntPtr fragmentContext)
        {
            fragmentContext = IntPtr.Zero;
            fragmentStart = IntPtr.Add(this.memoryHandle.NativeObject, (int) fileOffset);
            return 0;
        }

        [SecuritySafeCritical]
        public int ReleaseFileFragment(IntPtr fragmentContext) => 
            0;

        private class MemoryHandle : ExternalObject
        {
            [SecuritySafeCritical]
            public MemoryHandle(byte[] data) : base(Marshal.AllocHGlobal(data.Length), true)
            {
                Marshal.Copy(data, 0, base.NativeObject, data.Length);
            }

            [SecuritySafeCritical]
            protected override void DisposeCore(bool disposing)
            {
                Marshal.FreeHGlobal(base.NativeObject);
            }
        }
    }
}

