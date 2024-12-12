namespace DevExpress.Utils.Design
{
    using DevExpress.Utils.Svg;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public interface ISvgPaletteProviderExt : ISvgPaletteProvider
    {
        Color GetColorByStyleName(string styleName, string defaultColor, out SvgGradient svgGradient, object tag = null);

        bool Disabled { get; }
    }
}

