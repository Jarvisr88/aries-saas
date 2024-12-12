namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using DevExpress.XtraPrinting.Design;
    using System;
    using System.ComponentModel;

    [Serializable, TypeConverter(typeof(BorderDashStyleConverter)), ResourceFinder(typeof(ResFinder))]
    public enum BorderDashStyle : byte
    {
        Solid = 0,
        Dash = 1,
        Dot = 2,
        DashDot = 3,
        DashDotDot = 4,
        Double = 5
    }
}

