namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class DWriteFontList : ComObject
    {
        public DWriteFontList(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public DWriteFont GetFont(int index)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, out ptr, 5));
            return new DWriteFont(ptr);
        }

        public DWriteFontCollection GetFontCollection()
        {
            throw new NotImplementedException();
        }

        public int GetFontCount() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 4);
    }
}

