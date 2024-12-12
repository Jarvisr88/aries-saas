namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    public class DWriteFontCollection : ComObject
    {
        public DWriteFontCollection(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public bool FindFamilyName(string familyName, out int index)
        {
            int num;
            using (StringMarshaler marshaler = new StringMarshaler(familyName))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, out index, out num, 5));
            }
            return (num != 0);
        }

        public DWriteFontFamily GetFontFamily(int index)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, out ptr, 4));
            return new DWriteFontFamily(ptr);
        }

        public int GetFontFamilyCount() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 3);

        public DWriteFont GetFontFromFontFace(DWriteFontFace fontFace)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, fontFace.ToNativeObject(), out ptr, 6));
            return new DWriteFont(ptr);
        }
    }
}

