namespace DevExpress.Text.Fonts
{
    using System;

    public interface IDXFontFamily
    {
        DXFontFace GetFirstMatchingFontFace(DXFontWeight weight, DXFontStretch fontStretch, DXFontStyle style);

        string Name { get; }
    }
}

