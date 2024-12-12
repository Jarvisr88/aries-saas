namespace DevExpress.Utils.Design
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public interface ISvgPaletteProvider
    {
        ISvgPaletteProvider Clone();
        bool Equals(ISvgPaletteProvider provider);
        Color GetColor(Color defaultColor);
        Color GetColor(string defaultColor);
        Color GetColorByStyleName(string styleName, string defaultColor, object tag = null);
        int GetHashCode();

        double Opacity { get; set; }
    }
}

