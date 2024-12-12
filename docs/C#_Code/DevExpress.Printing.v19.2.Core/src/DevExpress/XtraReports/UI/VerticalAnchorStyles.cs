namespace DevExpress.XtraReports.UI
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [Flags, TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum VerticalAnchorStyles
    {
        None,
        Top,
        Bottom,
        Both
    }
}

