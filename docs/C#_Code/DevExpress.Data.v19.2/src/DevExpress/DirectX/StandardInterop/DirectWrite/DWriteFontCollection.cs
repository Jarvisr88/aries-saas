namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Runtime.InteropServices;

    public class DWriteFontCollection : ComObject<IDWriteFontCollection>
    {
        protected internal DWriteFontCollection(IDWriteFontCollection nativeObject) : base(nativeObject)
        {
        }

        public bool FindFamilyName(string familyName, out int index) => 
            base.WrappedObject.FindFamilyName(familyName, out index);

        public DWriteFontFamily GetFontFamily(int index) => 
            new DWriteFontFamily(base.WrappedObject.GetFontFamily(index));

        public int GetFontFamilyCount() => 
            base.WrappedObject.GetFontFamilyCount();

        public void GetFontFromFontFace()
        {
            throw new NotImplementedException();
        }
    }
}

