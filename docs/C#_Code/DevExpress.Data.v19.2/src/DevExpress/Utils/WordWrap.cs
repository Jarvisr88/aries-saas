﻿namespace DevExpress.Utils
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum WordWrap
    {
        Default,
        NoWrap,
        Wrap
    }
}

