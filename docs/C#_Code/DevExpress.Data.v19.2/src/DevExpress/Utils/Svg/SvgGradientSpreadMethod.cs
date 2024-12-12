namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(SvgEnumConverter))]
    public enum SvgGradientSpreadMethod
    {
        Pad,
        Reflect,
        Repeat
    }
}

