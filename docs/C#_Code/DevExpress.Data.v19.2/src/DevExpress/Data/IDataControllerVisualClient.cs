namespace DevExpress.Data
{
    using System;

    public interface IDataControllerVisualClient
    {
        void ColumnsRenewed();
        void RequestSynchronization();
        void RequireSynchronization(IDataSync dataSync);
        void UpdateColumns();
        void UpdateLayout();
        void UpdateRow(int controllerRowHandle);
        void UpdateRowIndexes(int newTopRowIndex);
        void UpdateRows(int topRowIndexDelta);
        void UpdateScrollBar();
        void UpdateTotalSummary();

        int VisibleRowCount { get; }

        int TopRowIndex { get; }

        int PageRowCount { get; }

        bool IsInitializing { get; }
    }
}

