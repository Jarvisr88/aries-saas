namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum ParameterType
    {
        String,
        DateTime,
        Int32,
        Int64,
        Float,
        Double,
        Decimal,
        Boolean
    }
}

