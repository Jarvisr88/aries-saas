namespace DevExpress.Data.XtraReports.Wizard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.ColumnInfoCache class from the DevExpress.XtraReports assembly instead.")]
    public class ColumnInfoCache : IColumnInfoCache
    {
        public IEnumerable<ColumnInfo> Columns { get; set; }
    }
}

