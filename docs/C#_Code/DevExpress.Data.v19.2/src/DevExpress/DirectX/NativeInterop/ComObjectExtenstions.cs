namespace DevExpress.DirectX.NativeInterop
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ComObjectExtenstions
    {
        public static IntPtr ToNativeObject(this ComObject comObject) => 
            (comObject != null) ? comObject.NativeObject : IntPtr.Zero;
    }
}

