namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(SvgEnumConverter))]
    public enum SvgFontStyle
    {
        Normal = 1,
        Oblique = 2,
        Italic = 4,
        All = 7
    }
}

