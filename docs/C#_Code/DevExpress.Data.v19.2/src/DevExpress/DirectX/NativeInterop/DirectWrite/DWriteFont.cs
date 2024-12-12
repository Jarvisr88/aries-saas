namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class DWriteFont : ComObject
    {
        public DWriteFont(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public DWriteFont1 AsDWriteFont1() => 
            new DWriteFont1(base.QueryInterface<DWriteFont1>());

        public DWriteFontFace CreateFontFace()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 13));
            return new DWriteFontFace(ptr);
        }

        public void GetFaceNames()
        {
            throw new NotImplementedException();
        }

        public DWriteFontFamily GetFontFamily()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 3));
            return new DWriteFontFamily(ptr);
        }

        public DWriteLocalizedStrings GetInformationalStrings(DWRITE_INFORMATIONAL_STRING_ID id)
        {
            IntPtr ptr;
            int num;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, (int) id, out ptr, out num, 9));
            return ((ptr == IntPtr.Zero) ? null : new DWriteLocalizedStrings(ptr));
        }

        public void GetMetrics()
        {
            throw new NotImplementedException();
        }

        public DWRITE_FONT_SIMULATIONS GetSimulations() => 
            (DWRITE_FONT_SIMULATIONS) ComObject.InvokeHelper.CalliInt(base.NativeObject, 10);

        public DWRITE_FONT_STRETCH GetStretch() => 
            (DWRITE_FONT_STRETCH) ComObject.InvokeHelper.CalliInt(base.NativeObject, 5);

        public DWRITE_FONT_STYLE GetStyle() => 
            (DWRITE_FONT_STYLE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 6);

        public DWRITE_FONT_WEIGHT GetWeight() => 
            (DWRITE_FONT_WEIGHT) ComObject.InvokeHelper.CalliInt(base.NativeObject, 4);

        public bool HasCharacter(int unicodeValue)
        {
            int num;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, unicodeValue, out num, 12));
            return (num != 0);
        }

        public bool IsSymbolFont() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 7) != 0;
    }
}

