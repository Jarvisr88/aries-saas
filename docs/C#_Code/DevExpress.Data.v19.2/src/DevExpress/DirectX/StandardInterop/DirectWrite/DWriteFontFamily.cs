namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;

    public class DWriteFontFamily : DWriteFontList
    {
        private readonly IDWriteFontFamily nativeObject;

        internal DWriteFontFamily(IDWriteFontFamily nativeObject) : base((IDWriteFontList) nativeObject)
        {
            this.nativeObject = nativeObject;
        }

        public DWriteLocalizedStrings GetFamilyNames() => 
            new DWriteLocalizedStrings(this.nativeObject.GetFamilyNames());

        public DWriteFont1 GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style) => 
            new DWriteFont1(this.nativeObject.GetFirstMatchingFont(weight, stretch, style));

        public void GetMatchingFonts()
        {
            throw new NotImplementedException();
        }
    }
}

