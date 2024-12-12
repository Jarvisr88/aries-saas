namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.DataProviders;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.ISelectColumnsPageView class from the DevExpress.XtraReports assembly instead.")]
    public interface ISelectColumnsPageView
    {
        event EventHandler ActiveColumnsChanged;

        event EventHandler AddAllColumnsClicked;

        event EventHandler AddColumnsClicked;

        event EventHandler RemoveAllColumnsClicked;

        event EventHandler RemoveColumnsClicked;

        void EnableAddAllColumnsButton(bool enable);
        void EnableAddColumnsButton(bool enable);
        void EnableRemoveAllColumnsButton(bool enable);
        void EnableRemoveColumnsButton(bool enable);
        void FillAvailableColumns(ColumnInfo[] columns);
        void FillSelectedColumns(ColumnInfo[] columns);
        ColumnInfo[] GetActiveAvailableColumns();
        ColumnInfo[] GetActiveSelectedColumns();
        void ShowWaitIndicator(bool show);
    }
}

