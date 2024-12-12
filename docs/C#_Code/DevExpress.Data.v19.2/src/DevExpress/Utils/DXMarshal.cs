namespace DevExpress.Utils
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class DXMarshal
    {
        [SecuritySafeCritical]
        public static T PtrToStructure<T>(IntPtr prt) => 
            (T) Marshal.PtrToStructure(prt, typeof(T));

        [SecuritySafeCritical]
        public static int SizeOf<T>() => 
            Marshal.SizeOf(typeof(T));

        [SecuritySafeCritical]
        public static int SizeOf(object structure) => 
            Marshal.SizeOf(structure);

        [SecuritySafeCritical]
        public static int SizeOf(Type type) => 
            Marshal.SizeOf(type);
    }
}

