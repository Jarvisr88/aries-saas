namespace DevExpress.Text.Fonts
{
    using System;

    public abstract class DXFontFamily : IDXFontFamily, IDisposable
    {
        protected DXFontFamily()
        {
        }

        public abstract void Dispose();
        public abstract DXFontFace GetFirstMatchingFontFace(DXFontWeight weight, DXFontStretch fontStretch, DXFontStyle style);

        public abstract string Name { get; }
    }
}

