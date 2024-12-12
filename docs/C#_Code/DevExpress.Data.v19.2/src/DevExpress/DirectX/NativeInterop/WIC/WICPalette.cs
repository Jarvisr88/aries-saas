namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class WICPalette : ComObject
    {
        internal WICPalette(IntPtr nativeObject) : base(nativeObject)
        {
        }

        [SecuritySafeCritical]
        public static WICPalette FromNativeObject(IntPtr nativeObject)
        {
            Marshal.AddRef(nativeObject);
            return new WICPalette(nativeObject);
        }

        public void InitializeCustom(int[] color, int count)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(color))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, count, 4));
            }
        }
    }
}

