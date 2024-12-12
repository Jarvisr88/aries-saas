namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IDXFontCollection
    {
        IDXFontFamily FindFamily(string familyName);
        DXFont FindFirstMatchingFont(string familyName, float sizeInPoints, DXFontWeight weight = 400, DXFontStyle style = 0, DXFontStretch stretch = 5);
        DXFontFace FindFirstMatchingFontFace(DXFontDescriptor descriptor);

        IReadOnlyList<IDXFontFamily> Families { get; }
    }
}

