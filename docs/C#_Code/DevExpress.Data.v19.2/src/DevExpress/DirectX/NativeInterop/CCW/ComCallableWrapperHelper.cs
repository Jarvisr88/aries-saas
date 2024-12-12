namespace DevExpress.DirectX.NativeInterop.CCW
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class ComCallableWrapperHelper
    {
        [SecuritySafeCritical]
        public static T CreateWrapperObject<T>(IntPtr iUnknown) where T: ComObject
        {
            IntPtr ptr;
            Type type = typeof(T);
            Guid gUID = type.GUID;
            Marshal.QueryInterface(iUnknown, ref gUID, out ptr);
            Type[] types = new Type[] { typeof(IntPtr) };
            object[] parameters = new object[] { ptr };
            return (T) type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, types, null).Invoke(parameters);
        }

        [SecuritySafeCritical]
        public static T FromIntPtr<T>(IntPtr iUnknown) => 
            (T) GCHandle.FromIntPtr(Marshal.ReadIntPtr(iUnknown, Marshal.SizeOf<IntPtr>())).Target;
    }
}

