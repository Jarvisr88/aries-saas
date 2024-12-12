namespace DevExpress.XtraPrinting
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(HorizontalContentSplittingConverter)), ResourceFinder(typeof(ResFinder))]
    public enum HorizontalContentSplitting
    {
        Exact,
        Smart
    }
}

