namespace DevExpress.Data.XtraReports.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.IColumnInfoCache class from the DevExpress.XtraReports assembly instead.")]
    public interface IColumnInfoCache
    {
        IEnumerable<ColumnInfo> Columns { get; set; }
    }
}

