namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum XpsCompressionOption
    {
        NotCompressed = -1,
        Normal = 0,
        Maximum = 1,
        Fast = 2,
        SuperFast = 3
    }
}

