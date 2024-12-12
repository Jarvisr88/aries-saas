namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public interface IAdditionalRowElement
    {
        int RowHandle { get; }

        FrameworkElement AdditionalElement { get; }

        DataViewBase RowCurrentView { get; }

        bool LockDataContext { get; }
    }
}

