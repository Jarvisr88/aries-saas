namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.Data.XtraReports.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public interface ISelectHierarchicalDataSourceColumnsPageView
    {
        event EventHandler ActiveAvailableColumnChanged;

        event EventHandler ActiveSelectedColumnsChanged;

        event EventHandler AddColumnClicked;

        event EventHandler RemoveAllColumnsClicked;

        event EventHandler RemoveColumnsClicked;

        void EnableAddColumnButton(bool enable);
        void EnableRemoveAllColumnsButton(bool enable);
        void EnableRemoveColumnsButton(bool enable);
        void FillSelectedColumns(ColumnInfo[] columns);
        void ShowWaitIndicator(bool show);

        IList RootNodes { get; }

        IPickManagerDataMemberNode ActiveAvailableDataMemberNode { get; }

        ColumnInfo[] ActiveSelectedColumns { get; }
    }
}

