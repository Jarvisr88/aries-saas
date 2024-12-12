namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class D2D1Properties : ComObject
    {
        public D2D1Properties(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public int GetPropertyCount() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 3);

        public void GetPropertyIndex()
        {
            throw new NotImplementedException();
        }

        public void GetPropertyName()
        {
            throw new NotImplementedException();
        }

        public void GetPropertyNameLength()
        {
            throw new NotImplementedException();
        }

        public D2D1Properties GetSubProperties(int index)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, out ptr, 13));
            return new D2D1Properties(ptr);
        }

        public D2D1_PROPERTY_TYPE GetType(int index) => 
            (D2D1_PROPERTY_TYPE) ComObject.InvokeHelper.CalliInt(base.NativeObject, index, 6);

        public void GetValue(int index, D2D1_PROPERTY_TYPE type, IntPtr data, int dataSize)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, (int) type, data, dataSize, 11));
        }

        public void GetValueByName()
        {
            throw new NotImplementedException();
        }

        public int GetValueSize(int index) => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, index, 12);

        [SecuritySafeCritical]
        public void SetValue(int index, object value)
        {
            GCHandle handle = GCHandle.Alloc(value);
            this.SetValue(index, D2D1_PROPERTY_TYPE.Unknown, GCHandle.ToIntPtr(handle), IntPtr.Size);
            handle.Free();
        }

        public void SetValue(int index, D2D1_PROPERTY_TYPE type, IntPtr data, int dataSize)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, (int) type, data, dataSize, 9));
        }

        public void SetValueByName()
        {
            throw new NotImplementedException();
        }
    }
}

