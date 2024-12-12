namespace DevExpress.DirectX.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class InteropHelpers
    {
        public const int E_NOT_SUFFICIENT_BUFFER = -2147024774;

        [SecuritySafeCritical]
        public static void CheckHResult(int hResult)
        {
            if (hResult < 0)
            {
                Marshal.ThrowExceptionForHR(hResult);
            }
        }

        [SecuritySafeCritical]
        public static IntPtr StructArrayToPtr<T>(T[] structArray) where T: struct
        {
            int num = Marshal.SizeOf<T>();
            IntPtr pointer = Marshal.AllocHGlobal((int) (num * structArray.Length));
            for (int i = 0; i < structArray.Length; i++)
            {
                Marshal.StructureToPtr<T>(structArray[i], IntPtr.Add(pointer, num * i), false);
            }
            return pointer;
        }
    }
}

