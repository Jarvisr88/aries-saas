namespace DevExpress.Text.Fonts
{
    using System;

    public interface IDXSystemFontCollection : IDXFontCollection
    {
        string DefaultFontFamily { get; set; }
    }
}

