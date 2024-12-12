namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("28211a43-7d89-476f-8181-2d6159b220ad"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Effect
    {
        void GetPropertyCount();
        void GetPropertyName();
        void GetPropertyNameLength();
        void GetType();
        void GetPropertyIndex();
        void SetValueByName();
        void SetValue(int index, D2D1_PROPERTY_TYPE type, IntPtr data, int dataSize);
        void GetValueByName();
        void GetValue();
        void GetValueSize();
        void GetSubProperties();
        [PreserveSig]
        void SetInput(int index, ID2D1Image input, [MarshalAs(UnmanagedType.Bool)] bool invalidate);
        void SetInputCount();
        void GetInput();
        void GetInputCount();
        [PreserveSig]
        void GetOutput(out ID2D1Image image);
    }
}

