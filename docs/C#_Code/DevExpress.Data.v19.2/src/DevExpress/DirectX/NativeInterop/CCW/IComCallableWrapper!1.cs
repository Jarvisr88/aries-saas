namespace DevExpress.DirectX.NativeInterop.CCW
{
    using System;

    public interface IComCallableWrapper<out T>
    {
        IntPtr NativeObject { get; }
    }
}

