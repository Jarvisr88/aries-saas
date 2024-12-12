namespace DevExpress.DirectX.NativeInterop
{
    using DevExpress.DirectX.Common;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class NativeStream : ComObject
    {
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;

        private NativeStream(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Clone(out NativeStream ppstm)
        {
            throw new NotImplementedException();
        }

        public void Commit(int grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(NativeStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        [SecuritySafeCritical]
        public static NativeStream Create() => 
            new NativeStream(SHCreateMemStream(IntPtr.Zero, 0));

        [SecuritySafeCritical]
        public static NativeStream Create(byte[] initalContents)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(initalContents))
            {
                return new NativeStream(SHCreateMemStream(marshaler.Pointer, initalContents.Length));
            }
        }

        [SecuritySafeCritical]
        public static NativeStream CreateFileStream(string fileName)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(SHCreateStreamOnFileEx(fileName, STGMFlags.CREATE | STGMFlags.SHARE_EXCLUSIVE | STGMFlags.WRITE, 0x80, true, IntPtr.Zero, out ptr));
            return new NativeStream(ptr);
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            throw new NotImplementedException();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, dlibMove, dwOrigin, plibNewPosition, 5));
        }

        public void SetSize(long libNewSize)
        {
            throw new NotImplementedException();
        }

        [DllImport("Shlwapi.dll", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern IntPtr SHCreateMemStream(IntPtr pInit, int cbInit);
        [DllImport("Shlwapi.dll", CharSet=CharSet.Unicode, ExactSpelling=true)]
        private static extern int SHCreateStreamOnFileEx(string pszFile, STGMFlags grfMode, int dwAttributes, [MarshalAs(UnmanagedType.Bool)] bool fCreate, IntPtr pstmTemplate, out IntPtr ppstm);
        public void Stat()
        {
            throw new NotImplementedException();
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        [Flags]
        private enum STGMFlags
        {
            READ = 0,
            WRITE = 1,
            READWRITE = 2,
            SHARE_DENY_NONE = 0x40,
            SHARE_DENY_READ = 0x30,
            SHARE_DENY_WRITE = 0x20,
            SHARE_EXCLUSIVE = 0x10,
            PRIORITY = 0x40000,
            CREATE = 0x1000,
            CONVERT = 0x20000,
            FAILIFTHERE = 0,
            DIRECT = 0,
            TRANSACTED = 0x10000,
            NOSCRATCH = 0x100000,
            NOSNAPSHOT = 0x200000,
            SIMPLE = 0x8000000,
            DIRECT_SWMR = 0x400000,
            DELETEONRELEASE = 0x4000000
        }
    }
}

