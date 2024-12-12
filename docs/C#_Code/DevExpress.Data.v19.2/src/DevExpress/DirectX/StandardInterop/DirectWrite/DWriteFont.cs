namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop;
    using System;

    public class DWriteFont : ComObject<IDWriteFont>
    {
        internal DWriteFont(IDWriteFont nativeObject) : base(nativeObject)
        {
        }

        public DWriteFontFace CreateFontFace() => 
            new DWriteFontFace(base.WrappedObject.CreateFontFace());

        public void GetFaceNames()
        {
            throw new NotImplementedException();
        }

        public DWriteFontFamily GetFontFamily() => 
            new DWriteFontFamily(base.WrappedObject.GetFontFamily());

        public void GetInformationalStrings()
        {
            throw new NotImplementedException();
        }

        public void GetMetrics()
        {
            throw new NotImplementedException();
        }

        public DWRITE_FONT_SIMULATIONS GetSimulations() => 
            base.WrappedObject.GetSimulations();

        public DWRITE_FONT_STRETCH GetStretch() => 
            base.WrappedObject.GetStretch();

        public DWRITE_FONT_STYLE GetStyle() => 
            base.WrappedObject.GetStyle();

        public DWRITE_FONT_WEIGHT GetWeight() => 
            base.WrappedObject.GetWeight();

        public void HasCharacter()
        {
            throw new NotImplementedException();
        }

        public void IsSymbolFont()
        {
            throw new NotImplementedException();
        }
    }
}

