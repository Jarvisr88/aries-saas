﻿namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(SvgEnumConverter))]
    public enum SvgTextAnchor
    {
        Default,
        Start,
        Middle,
        End
    }
}
