namespace DevExpress.Utils.Design
{
    using DevExpress.Utils.Svg;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public interface IDXImagesProviderEx : IDXImagesProvider
    {
        SvgImage GetSvgImage(string id);
        Image GetSvgImage(string id, ISvgPaletteProvider paletteProvider, int width = 0x20, int height = 0x20);
        SvgImage GetSvgImageByPath(string path);
        IEnumerable<string> GetSvgImages();
    }
}

