namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [StructLayout(LayoutKind.Explicit)]
    internal struct Win32Error
    {
        [FieldOffset(0)]
        private readonly int _value;
        public static readonly Win32Error ERROR_SUCCESS;
        public static readonly Win32Error ERROR_INVALID_FUNCTION;
        public static readonly Win32Error ERROR_FILE_NOT_FOUND;
        public static readonly Win32Error ERROR_PATH_NOT_FOUND;
        public static readonly Win32Error ERROR_TOO_MANY_OPEN_FILES;
        public static readonly Win32Error ERROR_ACCESS_DENIED;
        public static readonly Win32Error ERROR_INVALID_HANDLE;
        public static readonly Win32Error ERROR_OUTOFMEMORY;
        public static readonly Win32Error ERROR_NO_MORE_FILES;
        public static readonly Win32Error ERROR_SHARING_VIOLATION;
        public static readonly Win32Error ERROR_INVALID_PARAMETER;
        public static readonly Win32Error ERROR_INSUFFICIENT_BUFFER;
        public static readonly Win32Error ERROR_NESTING_NOT_ALLOWED;
        public static readonly Win32Error ERROR_KEY_DELETED;
        public static readonly Win32Error ERROR_NOT_FOUND;
        public static readonly Win32Error ERROR_NO_MATCH;
        public static readonly Win32Error ERROR_BAD_DEVICE;
        public static readonly Win32Error ERROR_CANCELLED;
        public static readonly Win32Error ERROR_CLASS_ALREADY_EXISTS;
        public static readonly Win32Error ERROR_INVALID_DATATYPE;

        static Win32Error();
        public Win32Error(int i);
        public override bool Equals(object obj);
        public override int GetHashCode();
        [SecurityCritical]
        public static Win32Error GetLastError();
        public static bool operator ==(Win32Error errLeft, Win32Error errRight);
        public static explicit operator HRESULT(Win32Error error);
        public static bool operator !=(Win32Error errLeft, Win32Error errRight);
        public HRESULT ToHRESULT();
    }
}

