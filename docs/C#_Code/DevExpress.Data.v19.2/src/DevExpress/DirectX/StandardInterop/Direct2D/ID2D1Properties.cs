namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("483473d7-cd46-4f9d-9d3a-3112aa80159d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Properties
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
    }
}

