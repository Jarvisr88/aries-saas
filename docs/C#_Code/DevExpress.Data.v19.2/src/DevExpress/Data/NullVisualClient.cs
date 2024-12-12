namespace DevExpress.Data
{
    using System;

    public class NullVisualClient : IDataControllerVisualClient
    {
        internal static NullVisualClient Default;

        static NullVisualClient();
        void IDataControllerVisualClient.ColumnsRenewed();
        void IDataControllerVisualClient.RequestSynchronization();
        void IDataControllerVisualClient.RequireSynchronization(IDataSync dataSync);
        void IDataControllerVisualClient.UpdateColumns();
        void IDataControllerVisualClient.UpdateLayout();
        void IDataControllerVisualClient.UpdateRow(int controllerRowHandle);
        void IDataControllerVisualClient.UpdateRowIndexes(int newTopRowIndex);
        void IDataControllerVisualClient.UpdateRows(int topRowIndexDelta);
        void IDataControllerVisualClient.UpdateScrollBar();
        void IDataControllerVisualClient.UpdateTotalSummary();

        int IDataControllerVisualClient.VisibleRowCount { get; }

        int IDataControllerVisualClient.TopRowIndex { get; }

        int IDataControllerVisualClient.PageRowCount { get; }

        bool IDataControllerVisualClient.IsInitializing { get; }
    }
}

