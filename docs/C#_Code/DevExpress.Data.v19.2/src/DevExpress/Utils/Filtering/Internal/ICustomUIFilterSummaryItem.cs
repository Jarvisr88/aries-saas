namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using System;
    using System.Runtime.InteropServices;

    public interface ICustomUIFilterSummaryItem
    {
        bool QueryValue(object controller, out object value);

        string Column { get; }

        SummaryItemTypeEx Type { get; set; }

        decimal Argument { get; set; }
    }
}

