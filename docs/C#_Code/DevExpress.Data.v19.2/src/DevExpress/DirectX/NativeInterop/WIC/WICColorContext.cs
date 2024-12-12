namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class WICColorContext : ComObject
    {
        internal WICColorContext(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void InitializeFromFilename(string fileName)
        {
            using (StringMarshaler marshaler = new StringMarshaler(fileName))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, 3));
            }
        }

        public void InitializeFromMemory(byte[] memory)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(memory))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, memory.Length, 4));
            }
        }
    }
}

