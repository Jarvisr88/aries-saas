namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.StandardInterop;
    using System;

    public class DWriteFontList : ComObject<IDWriteFontList>
    {
        internal DWriteFontList(IDWriteFontList nativeObject) : base(nativeObject)
        {
        }

        public void GetFont()
        {
            throw new NotImplementedException();
        }

        public void GetFontCollection()
        {
            throw new NotImplementedException();
        }

        public void GetFontCount()
        {
            throw new NotImplementedException();
        }
    }
}

