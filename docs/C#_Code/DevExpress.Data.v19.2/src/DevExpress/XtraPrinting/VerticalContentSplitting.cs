namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(VerticalContentSplittingConverter)), ResourceFinder(typeof(ResFinder))]
    public enum VerticalContentSplitting
    {
        Exact,
        Smart
    }
}

