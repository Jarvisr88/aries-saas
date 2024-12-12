namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.StandardInterop;
    using System;

    public class DWriteGdiInterop : ComObject<IDWriteGdiInterop>
    {
        internal DWriteGdiInterop(IDWriteGdiInterop comObject) : base(comObject)
        {
        }

        public DWriteFontFace CreateFontFaceFromHdc(IntPtr hdc)
        {
            IDWriteFontFace face;
            base.WrappedObject.CreateFontFaceFromHdc(hdc, out face);
            return new DWriteFontFace(face);
        }
    }
}

