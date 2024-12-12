namespace DevExpress.Text.Fonts
{
    using System;

    public interface IFontFace
    {
        string Family { get; }

        DXFontStretch Stretch { get; }

        DXFontStyle Style { get; }

        DXFontWeight Weight { get; }
    }
}

