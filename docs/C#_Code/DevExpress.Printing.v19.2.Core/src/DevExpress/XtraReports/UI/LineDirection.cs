namespace DevExpress.XtraReports.UI
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [Serializable, TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum LineDirection
    {
        Slant,
        BackSlant,
        Horizontal,
        Vertical
    }
}

