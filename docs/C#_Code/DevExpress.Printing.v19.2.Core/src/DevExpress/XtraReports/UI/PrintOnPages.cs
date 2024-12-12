namespace DevExpress.XtraReports.UI
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [Flags, TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum PrintOnPages
    {
        AllPages = 1,
        NotWithReportHeader = 2,
        NotWithReportFooter = 4,
        NotWithReportHeaderAndReportFooter = 6
    }
}

