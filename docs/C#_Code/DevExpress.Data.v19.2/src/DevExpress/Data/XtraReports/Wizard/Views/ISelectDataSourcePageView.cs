namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.ISelectDataSourcePageView class from the DevExpress.XtraReports assembly instead.")]
    public interface ISelectDataSourcePageView
    {
        event EventHandler SelectedDataSourceChanged;

        void FillDataSourceList(IEnumerable<DataSourceInfo> dataSources);
        void ShowWaitIndicator(bool show);

        string SelectedDataSourceName { get; set; }
    }
}

