namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public interface IDataControllerVisualClient2 : IDataControllerVisualClient
    {
        event EventHandler VisibleRangeChanged;

        void Notify(DataControllerChangedItemCollection items);
        void NotifyDataRefresh();
        void TotalSummaryCalculated();
        void UpdateVisibleRange();
    }
}

