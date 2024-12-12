namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(SvgEnumConverter))]
    public enum SvgFontVariant
    {
        Normal,
        SmallCaps,
        Inherit
    }
}

