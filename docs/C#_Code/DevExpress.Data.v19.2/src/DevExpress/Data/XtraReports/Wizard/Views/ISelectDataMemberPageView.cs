namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.DataProviders;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public interface ISelectDataMemberPageView
    {
        event EventHandler SelectedDataMemberChanged;

        void FillTables(IEnumerable<TableInfo> tables);
        void FillViews(IEnumerable<TableInfo> views);
        void ShowWaitIndicator(bool show);

        TableInfo SelectedDataMemberName { get; set; }
    }
}

